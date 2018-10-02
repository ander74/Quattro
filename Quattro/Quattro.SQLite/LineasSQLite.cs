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


	public class LineasSQLite {


		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public LineasSQLite(string cadenaConexion) {
			CadenaConexion = cadenaConexion;
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region COMANDOS SQL
		// ====================================================================================================

		private const string COMANDO_GET_LINEAS =
			"SELECT * FROM Lineas ORDER BY NumeroLinea ASC;";


		private const string COMANDO_GET_SERVICIOS_POR_LINEA =
			"SELECT * FROM Servicios WHERE NumeroLinea = @NumeroLinea ORDER BY Servicio, Turno ASC;";


		private const string COMANDO_GET_SERVICIOS_AUXILIARES_POR_SERVICIO =
			"SELECT * FROM ServiciosAuxiliares WHERE IdServicio = @IdServicio ORDER BY Servicio, Turno ASC;";


		private const string COMANDO_INSERTAR_LINEA =
			"INSERT INTO Lineas " +
				"(NumeroLinea, TextoLinea, Notas) " +
			"VALUES " +
				"(@NumeroLinea, @TextoLinea, @Notas) ;";


		private const string COMANDO_INSERTAR_SERVICIO =
			"INSERT INTO Servicios " +
				"(NumeroLinea, Servicio, Turno, Inicio, LugarInicio, Final, LugarFinal, TomaDeje, Euros, Notas) " +
			"VALUES " +
				"(@NumeroLinea, @Servicio, @Turno, @Inicio, @LugarInicio, @Final, @LugarFinal, @TomaDeje, @Euros, @Notas) ;";


		private const string COMANDO_INSERTAR_SERVICIO_AUXILIAR =
			"INSERT INTO ServiciosAuxiliares " +
				"(IdServicio, NumeroLinea, Servicio, Turno, Inicio, LugarInicio, Final, LugarFinal) " +
			"VALUES " +
				"(@IdServicio, @NumeroLinea, @Servicio, @Turno, @Inicio, @LugarInicio, @Final, @LugarFinal) ;";


		private const string COMANDO_ACTUALIZAR_LINEA =
			"UPDATE Lineas SET " +
				"NumeroLinea = @NumeroLinea, " +
				"TextoLinea = @TextoLinea, " +
				"Notas = @Notas " +
			"WHERE Id = @Id;";


		private const string COMANDO_ACTUALIZAR_SERVICIO =
			"UPDATE Servicios SET " +
				"NumeroLinea = @NumeroLinea, " +
				"Servicio = @Servicio, " +
				"Turno = @Turno, " +
				"Inicio = @Inicio, " +
				"LugarInicio = @LugarInicio, " +
				"Final = @Final, " +
				"LugarFinal = @LugarFinal, " +
				"TomaDeje = @TomaDeje, " +
				"Euros = @Euros, " +
				"Notas = @Notas " +
			"WHERE Id = @Id;";


		private const string COMANDO_ACTUALIZAR_SERVICIO_AUXILIAR =
			"UPDATE ServiciosAuxiliares SET " +
				"IdServicio = @IdServicio, " +
				"NumeroLinea = @NumeroLinea, " +
				"Servicio = @Servicio, " +
				"Turno = @Turno, " +
				"Inicio = @Inicio, " +
				"LugarInicio = @LugarInicio, " +
				"Final = @Final, " +
				"LugarFinal = @LugarFinal " +
			"WHERE Id = @Id;";


		private const string COMANDO_BORRAR_LINEA =
			"DELETE FROM Lineas WHERE Id=@Id;";


		private const string COMANDO_IDENTIDAD = "SELECT @@IDENTITY;";


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS CRUD
		// ====================================================================================================


		/// <summary>
		/// Devuelve una NotifyCollection con todas las líneas de la tabla. Por cada línea se extrae la lista
		/// de servicios que contiene, y por cada uno de estos servicios, se extraen los servicios auxiliares
		/// de los mismos.
		/// </summary>
		/// <returns>Todas las líneas de la tabla con sus servicios y servicios auxiliares.</returns>
		public NotifyCollection<Linea> GetLineas() {
			// Lista a devolver.
			NotifyCollection<Linea> lista = new NotifyCollection<Linea>();
			// Conexión a la base de datos.
			using (SQLiteConnection conexion = new SQLiteConnection(CadenaConexion)) {
				conexion.Open();
				// Transacción.
				using (SQLiteTransaction transaccion = conexion.BeginTransaction()) {
					// Comando para extraer las líneas
					using (SQLiteCommand comandoLineas = new SQLiteCommand(COMANDO_GET_LINEAS, conexion, transaccion)) {
						// Reader para extraer las lineas
						using (SQLiteDataReader lectorLineas = comandoLineas.ExecuteReader()) {
							while (lectorLineas.Read()) {
								Linea linea = new Linea(lectorLineas);
								linea.Servicios = new NotifyCollection<Servicio>();
								// Comando para extraer los servicios de cada línea.
								using (SQLiteCommand comandoServicios = new SQLiteCommand(COMANDO_GET_SERVICIOS_POR_LINEA, conexion, transaccion)) {
									comandoServicios.Parameters.AddWithValue("@NumeroLinea", linea.NumeroLinea);
									// Reader para extraer los servicios de cada línea.
									using (SQLiteDataReader lectorServicios = comandoServicios.ExecuteReader()) {
										while (lectorServicios.Read()) {
											Servicio servicio = new Servicio(lectorServicios);
											servicio.ServiciosAuxiliares = new NotifyCollection<ServicioBase>();
											// Comando para extraer los servicios auxiliares que tienen los servicios de cada línea.
											using (SQLiteCommand comandoAuxiliares = new SQLiteCommand(COMANDO_GET_SERVICIOS_AUXILIARES_POR_SERVICIO, conexion, transaccion)) {
												comandoAuxiliares.Parameters.AddWithValue("@IdServicio", servicio.Id);
												// Reader para extraer los servicios auxiliares que tienen los servicios de cada línea.
												using (SQLiteDataReader lectorAuxiliares = comandoAuxiliares.ExecuteReader()) {
													while (lectorAuxiliares.Read()) {
														ServicioBase servicioBase = new ServicioBase(lectorAuxiliares);
														servicioBase.Nuevo = false;
														servicio.ServiciosAuxiliares.Add(servicioBase);
													}
												}
											}
											servicio.Nuevo = false;
											linea.Servicios.Add(servicio);
										}
									}
								}
								linea.Nuevo = false;
								lista.Add(linea);
							}
						}
					}
				}
			}
			return lista;
		}


		/// <summary>
		/// Inserta o actualiza las líneas de la lista que sean nuevas o estén modificadas.
		/// </summary>
		/// <param name="lista">Lista de líneas a actualizar.</param>
		public void GuardarLineas(IList<Linea> lista) {
			if (lista == null || lista.Count == 0) return;
			using (SQLiteConnection conexion = new SQLiteConnection(CadenaConexion)) {
				conexion.Open();
				foreach (Linea linea in lista) {
					if (linea.Nuevo) {
						using (SQLiteCommand comando = new SQLiteCommand(COMANDO_INSERTAR_LINEA, conexion)) {
							linea.ToCommand(comando);
							comando.ExecuteNonQuery();
							comando.CommandText = COMANDO_IDENTIDAD;
							int id = Convert.ToInt32(comando.ExecuteScalar());
							linea.Id = id;
							linea.Nuevo = false;
							linea.Modificado = false;
						}
					} else if (linea.Modificado) {
						using (SQLiteCommand comando = new SQLiteCommand(COMANDO_ACTUALIZAR_LINEA, conexion)) {
							linea.ToCommand(comando);
							comando.ExecuteNonQuery();
							linea.Modificado = false;
						}
					}
					// Actualizamos los servicios de cada línea.
					foreach (Servicio servicio in linea.Servicios) {
						if (servicio.Nuevo) {
							using (SQLiteCommand comando = new SQLiteCommand(COMANDO_INSERTAR_SERVICIO, conexion)) {
								servicio.ToCommand(comando);
								comando.ExecuteNonQuery();
								comando.CommandText = COMANDO_IDENTIDAD;
								int id = Convert.ToInt32(comando.ExecuteScalar());
								servicio.Id = id;
								servicio.Nuevo = false;
								servicio.Modificado = false;
							}
						} else if (servicio.Modificado) {
							using (SQLiteCommand comando = new SQLiteCommand(COMANDO_ACTUALIZAR_SERVICIO, conexion)) {
								servicio.ToCommand(comando);
								comando.ExecuteNonQuery();
								servicio.Modificado = false;
							}
						}
						// Guardamos los servicios auxiliares de cada servicio de cada línea.
						foreach (ServicioBase servicioAuxiliar in servicio.ServiciosAuxiliares) {
							if (servicioAuxiliar.Nuevo) {
								using (SQLiteCommand comando = new SQLiteCommand(COMANDO_INSERTAR_SERVICIO_AUXILIAR, conexion)) {
									servicioAuxiliar.ToCommand(comando);
									comando.ExecuteNonQuery();
									comando.CommandText = COMANDO_IDENTIDAD;
									int id = Convert.ToInt32(comando.ExecuteScalar());
									servicioAuxiliar.Id = id;
									servicioAuxiliar.Nuevo = false;
									servicioAuxiliar.Modificado = false;
								}
							} else if (servicioAuxiliar.Modificado) {
								using (SQLiteCommand comando = new SQLiteCommand(COMANDO_ACTUALIZAR_SERVICIO_AUXILIAR, conexion)) {
									servicioAuxiliar.ToCommand(comando);
									comando.ExecuteNonQuery();
									servicioAuxiliar.Modificado = false;
								}
							}
						}
					}
				}
			}
		}


		/// <summary>
		/// Elimina de la tabla las líneas que se encuentren en la lista.
		/// </summary>
		/// <param name="lista">Lista con las líneas a eliminar.</param>
		public void BorrarLineas(IList<Linea> lista) {
			if (lista == null || lista.Count == 0) return;
			using (SQLiteConnection conexion = new SQLiteConnection(CadenaConexion)) {
				conexion.Open();
				foreach (Linea linea in lista) {
					if (!linea.Nuevo) {
						using (SQLiteCommand comando = new SQLiteCommand(COMANDO_BORRAR_LINEA, conexion)) {
							comando.Parameters.AddWithValue("@Id", linea.Id);
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
