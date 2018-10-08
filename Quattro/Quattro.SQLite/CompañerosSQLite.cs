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
using Quattro.Models2;
using Quattro.Notify;

namespace Quattro.SQLite {


	public class CompañerosSQLite {


		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public CompañerosSQLite(string cadenaConexion) {
			CadenaConexion = cadenaConexion;
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region COMANDOS SQL
		// ====================================================================================================

		private const string COMANDO_GET_ALL =
			"SELECT * FROM Compañeros ORDER BY Matricula ASC;";


		private const string COMANDO_INSERTAR =
			"INSERT INTO Compañeros " +
				"(Matricula, Nombre, Apellidos, Telefono, Calificacion, Deuda, Notas) " +
			"VALUES " +
				"(@Matricula, @Nombre, @Apellidos, @Telefono, @Calificacion, @Deuda, @Notas);";


		private const string COMANDO_ACTUALIZAR =
			"UPDATE Compañeros SET " +
				"Matricula = @Matricula, " +
				"Nombre = @Nombre, " +
				"Apellidos = @Apellidos, " +
				"Telefono = @Telefono, " +
				"Calificacion = @Calificacion, " +
				"Deuda = @Deuda, " +
				"Notas = @Notas " +
			"WHERE Id = @Id;";


		private const string COMANDO_BORRAR =
			"DELETE FROM Compañeros WHERE Id=@Id;";


		private const string COMANDO_IDENTIDAD = "SELECT @@IDENTITY;";


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS CRUD
		// ====================================================================================================


		/// <summary>
		/// Devuelve una NotifyCollection con todos los compañeros de la tabla.
		/// </summary>
		/// <returns>Todos los compañeros de la tabla.</returns>
		public NotifyCollection<Compañero> GetCompañeros() {
			NotifyCollection<Compañero> lista = new NotifyCollection<Compañero>();
			using (SQLiteConnection conexion = new SQLiteConnection(CadenaConexion)) {
				conexion.Open();
				using (SQLiteCommand comando = new SQLiteCommand(COMANDO_GET_ALL, conexion)) {
					using (SQLiteDataReader lector = comando.ExecuteReader()) {
						while (lector.Read()) {
							Compañero compañero = new Compañero(lector);
							compañero.Nuevo = false;
							lista.Add(compañero);
						}
					}
				}
			}
			return lista;
		}


		/// <summary>
		/// Inserta o actualiza los compañeros de la lista que sean nuevos o estén modificados.
		/// </summary>
		/// <param name="lista">Lista de compañeros a actualizar.</param>
		public void GuardarCompañeros(IList<Compañero> lista) {
			if (lista == null || lista.Count == 0) return;
			using (SQLiteConnection conexion = new SQLiteConnection(CadenaConexion)) {
				conexion.Open();
				using (SQLiteTransaction transaccion = conexion.BeginTransaction()) {
					foreach (Compañero compañero in lista) {
						if (compañero.Nuevo) {
							using (SQLiteCommand comando = new SQLiteCommand(COMANDO_INSERTAR, conexion, transaccion)) {
								compañero.ToCommand(comando);
								comando.ExecuteNonQuery();
								comando.CommandText = COMANDO_IDENTIDAD;
								int id = Convert.ToInt32(comando.ExecuteScalar());
								compañero.Id = id;
								compañero.Nuevo = false;
								compañero.Modificado = false;
							}
						} else if (compañero.Modificado) {
							using (SQLiteCommand comando = new SQLiteCommand(COMANDO_ACTUALIZAR, conexion, transaccion)) {
								compañero.ToCommand(comando);
								comando.ExecuteNonQuery();
								compañero.Modificado = false;
							}
						}
					}
					transaccion.Commit();
				}
			}
		}


		/// <summary>
		/// Elimina de la tabla los compañeros que se encuentren en la lista.
		/// </summary>
		/// <param name="lista">Lista con los compañeros a eliminar.</param>
		public void BorrarCompañeros(IList<Compañero> lista) {
			if (lista == null || lista.Count == 0) return;
			using (SQLiteConnection conexion = new SQLiteConnection(CadenaConexion)) {
				conexion.Open();
				using (SQLiteTransaction transaccion = conexion.BeginTransaction()) {
					foreach (Compañero compañero in lista) {
						if (!compañero.Nuevo) {
							using (SQLiteCommand comando = new SQLiteCommand(COMANDO_BORRAR, conexion, transaccion)) {
								comando.Parameters.AddWithValue("@Id", compañero.Id);
								comando.ExecuteNonQuery();
							}
						}
					}
					transaccion.Commit();
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
