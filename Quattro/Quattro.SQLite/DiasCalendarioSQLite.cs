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


	public class DiasCalendarioSQLite {


		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public DiasCalendarioSQLite(string cadenaConexion) {
			CadenaConexion = cadenaConexion;
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region COMANDOS SQL
		// ====================================================================================================

		private const string COMANDO_GET_DIAS_POR_MES =
			"SELECT * FROM Calendario WHERE strftime('%Y', Fecha) = @Año AND strftime('%m', Fecha) = @Mes ORDER BY Fecha ASC;";


		private const string COMANDO_GET_SERVICIOS_POR_DIA =
			"SELECT * FROM ServiciosCalendario WHERE Fecha = @Fecha ORDER BY Servicio, Turno ASC;";


		private const string COMANDO_INSERTAR_DIA =
			"INSERT INTO Calendario " +
				"(Fecha, EsFranqueo, EsFestivo, CodigoIncidencia, HuelgaParcial, HorasHuelga, Servicio, " +
				"Turno, NumeroLinea, TextoLinea, Inicio, LugarInicio, Final, LugarFinal, Trabajadas, " +
				"Acumuladas, Nocturnas, Desayuno, Comida, Cena, TomaDeje, Euros, Relevo, Susti, Bus, Notas) " +
			"VALUES " +
				"(@Fecha, @EsFranqueo, @EsFestivo, @CodigoIncidencia, @HuelgaParcial, @HorasHuelga, @Servicio, " +
				"@Turno, @NumeroLinea, @TextoLinea, @Inicio, @LugarInicio, @Final, @LugarFinal, @Trabajadas, " +
				"@Acumuladas, @Nocturnas, @Desayuno, @Comida, @Cena, @TomaDeje, @Euros, @Relevo, @Susti, @Bus, @Notas) ;";


		private const string COMANDO_INSERTAR_SERVICIO_CALENDARIO =
			"INSERT INTO ServiciosCalendario " +
				"(Fecha, Servicio, Turno, NumeroLinea, TextoLinea, Inicio, LugarInicio, Final, LugarFinal) " +
			"VALUES " +
				"(@Fecha, @Servicio, @Turno, @NumeroLinea, @TextoLinea, @Inicio, @LugarInicio, @Final, @LugarFinal) ;";


		private const string COMANDO_ACTUALIZAR_DIA =
			"UPDATE Calendario SET " +
				"Fecha = @Fecha, " +
				"EsFranqueo = @EsFranqueo, " +
				"EsFestivo = @EsFestivo, " +
				"CodigoIncidencia = @CodigoIncidencia, " +
				"HuelgaParcial = @HuelgaParcial, " +
				"HorasHuelga = @HorasHuelga, " +
				"Servicio = @Servicio, " +
				"Turno = @Turno, " +
				"NumeroLinea = @NumeroLinea, " +
				"TextoLinea = @TextoLinea, " +
				"Inicio = @Inicio, " +
				"LugarInicio = @LugarInicio, " +
				"Final = @Final, " +
				"LugarFinal = @LugarFinal, " +
				"Trabajadas = @Trabajadas, " +
				"Acumuladas = @Acumuladas, " +
				"Nocturnas = @Nocturnas, " +
				"Desayuno = @Desayuno, " +
				"Comida = @Comida, " +
				"Cena = @Cena, " +
				"TomaDeje = @TomaDeje, " +
				"Euros = @Euros, " +
				"Relevo = @Relevo, " +
				"Susti = @Susti, " +
				"Bus = @Bus, " +
				"Notas = @Notas " +
			"WHERE Id = @Id;";


		private const string COMANDO_ACTUALIZAR_SERVICIO_CALENDARIO =
			"UPDATE ServiciosCalendario SET " +
				"Fecha = @Fecha, " +
				"Servicio = @Servicio, " +
				"Turno = @Turno, " +
				"NumeroLinea = @NumeroLinea, " +
				"TextoLinea = @TextoLinea, " +
				"Inicio = @Inicio, " +
				"LugarInicio = @LugarInicio, " +
				"Final = @Final, " +
				"LugarFinal = @LugarFinal " +
			"WHERE Id = @Id;";


		private const string COMANDO_BORRAR_DIA =
			"DELETE FROM Lineas WHERE Id=@Id;";


		private const string COMANDO_IDENTIDAD = "SELECT @@IDENTITY;";


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS CRUD
		// ====================================================================================================


		/// <summary>
		/// Devuelve una NotifyCollection con todos los días de la tabla.
		/// Por cada día se extraen los servicios que contiene.
		/// </summary>
		/// <returns>Todos los días de la tabla con sus servicios.</returns>
		public NotifyCollection<DiaCalendario> GetDiasPorMes(int año, int mes) {
			// Lista a devolver.
			NotifyCollection<DiaCalendario> lista = new NotifyCollection<DiaCalendario>();
			// Conexión a la base de datos.
			using (SQLiteConnection conexion = new SQLiteConnection(CadenaConexion)) {
				conexion.Open();
				// Comando para extraer los días
				using (SQLiteCommand comandoDias = new SQLiteCommand(COMANDO_GET_DIAS_POR_MES, conexion)) {
					comandoDias.Parameters.AddWithValue("@Año", año);
					comandoDias.Parameters.AddWithValue("@Mes", mes);
					// Reader para extraer los días
					using (SQLiteDataReader lectorDias = comandoDias.ExecuteReader()) {
						while (lectorDias.Read()) {
							DiaCalendario dia = new DiaCalendario(lectorDias);
							dia.ServiciosAuxiliares = new NotifyCollection<ServicioBase>();
							// Comando para extraer los servicios de cada día.
							using (SQLiteCommand comandoServicios = new SQLiteCommand(COMANDO_GET_SERVICIOS_POR_DIA, conexion)) {
								comandoServicios.Parameters.AddWithValue("@Fecha", dia.Fecha.ToString("yyyy-MM-dd"));
								// Reader para extraer los servicios de cada día.
								using (SQLiteDataReader lectorServicios = comandoServicios.ExecuteReader()) {
									while (lectorServicios.Read()) {
										ServicioBase servicio = new ServicioBase(lectorServicios);
										servicio.Nuevo = false;
										dia.ServiciosAuxiliares.Add(servicio);
									}
								}
							}
							dia.Nuevo = false;
							lista.Add(dia);
						}
					}
				}
			}
			return lista;
		}


		/// <summary>
		/// Inserta o actualiza los días de la lista que sean nuevos o estén modificados.
		/// </summary>
		/// <param name="lista">Lista de días a actualizar.</param>
		public void GuardarDias(IList<DiaCalendario> lista) {
			if (lista == null || lista.Count == 0) return;
			using (SQLiteConnection conexion = new SQLiteConnection(CadenaConexion)) {
				conexion.Open();
				using (SQLiteTransaction transaccion = conexion.BeginTransaction()) {
					foreach (DiaCalendario dia in lista) {
						if (dia.Nuevo) {
							using (SQLiteCommand comando = new SQLiteCommand(COMANDO_INSERTAR_DIA, conexion, transaccion)) {
								dia.ToCommand(comando);
								comando.ExecuteNonQuery();
								comando.CommandText = COMANDO_IDENTIDAD;
								int id = Convert.ToInt32(comando.ExecuteScalar());
								dia.Id = id;
								dia.Nuevo = false;
								dia.Modificado = false;
							}
						} else if (dia.Modificado) {
							using (SQLiteCommand comando = new SQLiteCommand(COMANDO_ACTUALIZAR_DIA, conexion, transaccion)) {
								dia.ToCommand(comando);
								comando.ExecuteNonQuery();
								dia.Modificado = false;
							}
						}
						// Guardamos los servicios auxiliares de cada servicio de cada línea.
						foreach (ServicioBase servicio in dia.ServiciosAuxiliares) {
							if (servicio.Nuevo) {
								using (SQLiteCommand comando = new SQLiteCommand(COMANDO_INSERTAR_SERVICIO_CALENDARIO, conexion, transaccion)) {
									servicio.ToCommand(comando);
									comando.ExecuteNonQuery();
									comando.CommandText = COMANDO_IDENTIDAD;
									int id = Convert.ToInt32(comando.ExecuteScalar());
									servicio.Id = id;
									servicio.Nuevo = false;
									servicio.Modificado = false;
								}
							} else if (servicio.Modificado) {
								using (SQLiteCommand comando = new SQLiteCommand(COMANDO_ACTUALIZAR_SERVICIO_CALENDARIO, conexion, transaccion)) {
									servicio.ToCommand(comando);
									comando.ExecuteNonQuery();
									servicio.Modificado = false;
								}
							}
						}
					}
					transaccion.Commit();
				}
			}
		}


		/// <summary>
		/// Elimina de la tabla los días que se encuentren en la lista.
		/// </summary>
		/// <param name="lista">Lista con los días a eliminar.</param>
		public void BorrarLineas(IList<DiaCalendario> lista) {
			if (lista == null || lista.Count == 0) return;
			using (SQLiteConnection conexion = new SQLiteConnection(CadenaConexion)) {
				conexion.Open();
				using (SQLiteTransaction transaccion = conexion.BeginTransaction()) {
					foreach (DiaCalendario dia in lista) {
						if (!dia.Nuevo) {
							using (SQLiteCommand comando = new SQLiteCommand(COMANDO_BORRAR_DIA, conexion, transaccion)) {
								comando.Parameters.AddWithValue("@Id", dia.Id);
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
