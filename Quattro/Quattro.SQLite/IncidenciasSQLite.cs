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
using Quattro.Models;
using Quattro.Notify;

namespace Quattro.SQLite {


	public class IncidenciasSQLite {


		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public IncidenciasSQLite(string cadenaConexion) {
			CadenaConexion = cadenaConexion;
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region COMANDOS SQL
		// ====================================================================================================

		private const string COMANDO_GET_ALL =
			"SELECT * FROM Incidencias ORDER BY Codigo ASC;";


		private const string COMANDO_INSERTAR =
			"INSERT INTO Incidencias " +
				"(Codigo, TextoIncidencia, Tipo, Notas) " +
			"VALUES " +
				"(@Codigo, @Incidencia, @Tipo, @Notas);";


		private const string COMANDO_ACTUALIZAR =
			"UPDATE Incidencias SET " +
				"Codigo = @Codigo, " +
				"TextoIncidencia = @TextoIncidencia, " +
				"Tipo = @Tipo, " +
				"Notas = @Notas " +
			"WHERE Id = @Id;";


		private const string COMANDO_BORRAR =
			"DELETE FROM Incidencias WHERE Id=@Id;";


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
		public NotifyCollection<Incidencia> GetIncidencias() {
			NotifyCollection<Incidencia> lista = new NotifyCollection<Incidencia>();
			using (SQLiteConnection conexion = new SQLiteConnection(CadenaConexion)) {
				conexion.Open();
				using (SQLiteCommand comando = new SQLiteCommand(COMANDO_GET_ALL, conexion)) {
					using (SQLiteDataReader lector = comando.ExecuteReader()) {
						while (lector.Read()) {
							Incidencia incidencia = new Incidencia(lector);
							incidencia.Nuevo = false;
							lista.Add(incidencia);
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
		public void GuardarIncidencias(IList<Incidencia> lista) {
			if (lista == null || lista.Count == 0) return;
			using (SQLiteConnection conexion = new SQLiteConnection(CadenaConexion)) {
				conexion.Open();
				foreach(Incidencia incidencia in lista) {
					if (incidencia.Nuevo) {
						using (SQLiteCommand comando = new SQLiteCommand(COMANDO_INSERTAR, conexion)) {
							incidencia.ToCommand(comando);
							comando.ExecuteNonQuery();
							comando.CommandText = COMANDO_IDENTIDAD;
							int id = Convert.ToInt32(comando.ExecuteScalar());
							incidencia.Id = id;
							incidencia.Nuevo = false;
							incidencia.Modificado = false;
						}
					} else if (incidencia.Modificado) {
						using (SQLiteCommand comando = new SQLiteCommand(COMANDO_ACTUALIZAR, conexion)) {
							incidencia.ToCommand(comando);
							comando.ExecuteNonQuery();
							incidencia.Modificado = false;
						}
					}
				}
			}
		}


		/// <summary>
		/// Elimina de la tabla las incidencias que se encuentren en la lista.
		/// </summary>
		/// <param name="lista">Lista con las incidencias a eliminar.</param>
		public void BorrarIncidencias(IList<Incidencia> lista) {
			if (lista == null || lista.Count == 0) return;
			using (SQLiteConnection conexion = new SQLiteConnection(CadenaConexion)) {
				conexion.Open();
				foreach(Incidencia incidencia in lista) {
					if (!incidencia.Nuevo) {
						using (SQLiteCommand comando = new SQLiteCommand(COMANDO_BORRAR, conexion)) {
							comando.Parameters.AddWithValue("@Id", incidencia.Id);
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
