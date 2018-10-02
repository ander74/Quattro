#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Text;
using Quattro.Models;
using Quattro.Notify;

namespace Quattro.SQLite {


	public class HorasAjenasSQLite {


		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public HorasAjenasSQLite(string cadenaConexion) {
			CadenaConexion = cadenaConexion;
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region COMANDOS SQL
		// ====================================================================================================

		private const string COMANDO_GET_ALL =
			"SELECT * FROM HorasAjenas ORDER BY Fecha ASC;";


		private const string COMANDO_INSERTAR =
			"INSERT INTO HorasAjenas " +
				"(Fecha, Horas, Motivo, Codigo) " +
			"VALUES " +
				"(@Fecha, @Horas, @Motivo, @Codigo);";


		private const string COMANDO_ACTUALIZAR =
			"UPDATE HorasAjenas SET " +
				"Fecha = @Fecha, " +
				"Horas = @Horas, " +
				"Motivo = @Motivo, " +
				"Codigo = @Codigo " +
			"WHERE Id = @Id;";


		private const string COMANDO_BORRAR =
			"DELETE FROM HorasAjenas WHERE Id=@Id;";


		private const string COMANDO_IDENTIDAD = "SELECT @@IDENTITY;";


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS CRUD
		// ====================================================================================================


		/// <summary>
		/// Devuelve una NotifyCollection con todas las incidencias de la tabla.
		/// </summary>
		/// <returns>Todas las incidencias de la tabla.</returns>
		public NotifyCollection<HoraAjena> GetIncidencias() {
			NotifyCollection<HoraAjena> lista = new NotifyCollection<HoraAjena>();
			using (SQLiteConnection conexion = new SQLiteConnection(CadenaConexion)) {
				conexion.Open();
				using (SQLiteCommand comando = new SQLiteCommand(COMANDO_GET_ALL, conexion)) {
					using (SQLiteDataReader lector = comando.ExecuteReader()) {
						while (lector.Read()) {
							HoraAjena horaAjena = new HoraAjena(lector);
							horaAjena.Nuevo = false;
							lista.Add(horaAjena);
						}
					}
				}
			}
			return lista;
		}


		/// <summary>
		/// Inserta o actualiza las incidencias de la lista que sean nuevas o estén modificadas.
		/// </summary>
		/// <param name="lista">Lista de incidencias a actualizar.</param>
		public void GuardarIncidencias(IList<HoraAjena> lista) {
			if (lista == null || lista.Count == 0) return;
			using (SQLiteConnection conexion = new SQLiteConnection(CadenaConexion)) {
				conexion.Open();
				foreach (HoraAjena horaAjena in lista) {
					if (horaAjena.Nuevo) {
						using (SQLiteCommand comando = new SQLiteCommand(COMANDO_INSERTAR, conexion)) {
							horaAjena.ToCommand(comando);
							comando.ExecuteNonQuery();
							comando.CommandText = COMANDO_IDENTIDAD;
							int id = Convert.ToInt32(comando.ExecuteScalar());
							horaAjena.Id = id;
							horaAjena.Nuevo = false;
							horaAjena.Modificado = false;
						}
					} else if (horaAjena.Modificado) {
						using (SQLiteCommand comando = new SQLiteCommand(COMANDO_ACTUALIZAR, conexion)) {
							horaAjena.ToCommand(comando);
							comando.ExecuteNonQuery();
							horaAjena.Modificado = false;
						}
					}
				}
			}
		}


		/// <summary>
		/// Elimina de la tabla las incidencias que se encuentren en la lista.
		/// </summary>
		/// <param name="lista">Lista con las incidencias a eliminar.</param>
		public void BorrarIncidencias(IList<HoraAjena> lista) {
			if (lista == null || lista.Count == 0) return;
			using (SQLiteConnection conexion = new SQLiteConnection(CadenaConexion)) {
				conexion.Open();
				foreach (HoraAjena horaAjena in lista) {
					if (!horaAjena.Nuevo) {
						using (SQLiteCommand comando = new SQLiteCommand(COMANDO_BORRAR, conexion)) {
							comando.Parameters.AddWithValue("@Id", horaAjena.Id);
							comando.ExecuteNonQuery();
						}
					}
				}
			}
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================


		public string CadenaConexion { get; set; }


		#endregion
		// ====================================================================================================


	}
}
