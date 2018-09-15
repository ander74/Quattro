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

namespace Quattro.SQLite {

    public class TableCreation {


		// ====================================================================================================
		#region COMANDOS SQL CREAR TABLAS
		// ====================================================================================================

		private const string COMANDO_CREAR_CALENDARIO = 
			"CREATE TABLE Calendario (Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE, " +
				"Fecha             STRING   DEFAULT ''  UNIQUE, " +
				"EsFranqueo        BOOL     , " +
				"EsFestivo         BOOL     , " +
				"CodigoIncidencia  INTEGER  , " +
				"HuelgaParcial     BOOL     , " +
				"HorasHuelga       REAL     , " +
				"Servicio          TEXT     DEFAULT '', " +
				"Turno             INTEGER  , " +
				"NumeroLinea       TEXT     DEFAULT '', " +
				"TextoLinea        TEXT     DEFAULT '', " +
				"Inicio            INTEGER  , " +
				"LugarInicio       TEXT     DEFAULT '', " +
				"Final             INTEGER  , " +
				"LugarFinal        TEXT     DEFAULT '', " +
				"Trabajadas        REAL     , " +
				"Acumuladas        REAL     , " +
				"Nocturnas         REAL     , " +
				"Desayuno          BOOL     , " +
				"Comida            BOOL     , " +
				"Cena              BOOL     , " +
				"TomaDeje          INTEGER  , " +
				"Euros             REAL     , " +
				"Relevo            INTEGER  , " +
				"Susti             INTEGER  , " +
				"Bus               TEXT     DEFAULT '', " +
				"Notas             TEXT     DEFAULT '' " +
			");";

		private const string COMANDO_CREAR_SERVICIOS_CALENDARIO = 
			"CREATE TABLE ServiciosCalendario (Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE, " +
				"IdCalendario  INTEGER  , " +
				"Servicio      TEXT     DEFAULT '', " +
				"Turno         INTEGER  , " +
				"NumeroLinea   TEXT     DEFAULT '', " +
				"TextoLinea    TEXT     DEFAULT '', " +
				"Inicio        INTEGER  , " +
				"LugarInicio   TEXT     DEFAULT '', " +
				"Final         INTEGER  , " +
				"LugarFinal    TEXT     DEFAULT '' " +
			");";

		private const string COMANDO_CREAR_HORAS_AJENAS = 
			"CREATE TABLE HorasAjenas (Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE, " +
				"Fecha   TEXT     DEFAULT '', " +
				"Horas   REAL     , " +
				"Motivo  TEXT     DEFAULT '', " +
				"Codigo  INTEGER  " +
			");";

		private const string COMANDO_CREAR_INCIDENCIAS = 
			"CREATE TABLE Incidencias (Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE, " +
				"Codigo           INTEGER  , " +
				"TextoIncidencia  TEXT     DEFAULT '', " +
				"Tipo             INTEGER  , " +
				"Notas            TEXT     DEFAULT '' " +
			");";

		private const string COMANDO_CREAR_COMPAÑEROS = 
			"CREATE TABLE Compañeros (Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE, " +
				"Matricula     INTEGER  UNIQUE, " +
				"Nombre        TEXT     DEFAULT '', " +
				"Apellidos     TEXT     DEFAULT '', " +
				"Telefono      TEXT     DEFAULT '', " +
				"Calificacion  INTEGER  , " +
				"Deuda         INTEGER  , " +
				"Notas         TEXT     DEFAULT '' " +
			");";

		private const string COMANDO_CREAR_LINEAS = 
			"CREATE TABLE Lineas (Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE, " +
				"NumeroLinea  TEXT     DEFAULT '', " +
				"TextoLinea   TEXT     DEFAULT '', " +
				"Notas        TEXT     DEFAULT '' " +
			");";
		
		private const string COMANDO_CREAR_SERVICIOS = 
			"CREATE TABLE Servicios (Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE, " +
				"NumeroLinea   TEXT     DEFAULT '', " +
				"Servicio      TEXT     DEFAULT '', " +
				"Turno         INTEGER  , " +
				"Inicio        INTEGER  , " +
				"LugarInicio   TEXT     DEFAULT '', " +
				"Final         INTEGER  , " +
				"LugarFinal    TEXT     DEFAULT '', " +
				"TomaDeje      INTEGER  , " +
				"Euros         REAL     , " +
				"Notas         TEXT     DEFAULT '' " +
			");";

		private const string COMANDO_CREAR_SERVICIOS_AUXILIARES = 
			"CREATE TABLE ServiciosAuxiliares (Id INTEGER PRIMARY KEY AUTOINCREMENT UNIQUE, " +
				"IdServicio    INTEGER  , " +
				"NumeroLinea   TEXT     DEFAULT '', " +
				"Servicio      TEXT     DEFAULT '', " +
				"Turno         INTEGER  , " +
				"Inicio        INTEGER  , " +
				"LugarInicio   TEXT     DEFAULT '', " +
				"Final         INTEGER  , " +
				"LugarFinal    TEXT     DEFAULT '' " +
			");";

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region COMANDOS COPIAR TABLAS V4 a V5
		// ====================================================================================================

		string COMANDO_COPIAR_CALENDARIO_V4To5 =
			"INSERT INTO Calendario " +
				"(Fecha, EsFranqueo, EsFestivo, CodigoIncidencia, HuelgaParcial, HorasHuelga, Servicio, " +
				"Turno, NumeroLinea, TextoLinea, Inicio, LugarInicio, Final, LugarFinal, Trabajadas, " +
				"Acumuladas, Nocturnas, Desayuno, Comida, Cena, TomaDeje, Euros, Relevo, Susti, Bus, Notas) " +
			"SELECT " +
				"(Año || '-' || substr('0' || Mes, -2, 2) || '-' || substr('0' ||Dia, -2, 2)) As Fecha, " +
				"EsFranqueo, " +
				"EsFestivo, " +
				"CodigoIncidencia, " +
				"HuelgaParcial, " +
				"round(HorasHuelga, 2), " +
				"Servicio, " +
				"Turno, " +
				"Linea, " +
				"TextoLinea, " +
				"(strftime('%H', Inicio) * 60 + strftime('%M', Inicio)) * 600000000 As Inicio, " +
				"LugarInicio, " +
				"(strftime('%H', Final) * 60 + strftime('%M', Final)) * 600000000 As Final, " +
				"LugarFinal, " +
				"round(Trabajadas, 2),  " +
				"round(Acumuladas, 2),  " +
				"round(Nocturnas, 2),  " +
				"Desayuno,  " +
				"Comida,  " +
				"Cena,  " +
				"(strftime('%H', TomaDeje) * 60 + strftime('%M', TomaDeje)) * 600000000 As TomaDeje, " +
				"round(Euros, 2),  " +
				"Matricula,  " +
				"MatriculaSusti,  " +
				"Bus, " +
				"Notas " +
			"FROM CalendarioOLD; ";


		string COMANDO_COPIAR_SERVICIOS_CALENDARIO_V4To5 =
			"INSERT INTO ServiciosCalendario " +
				"(IdCalendario, Servicio, Turno, NumeroLinea, TextoLinea, Inicio, LugarInicio, Final, LugarFinal) " +
			"SELECT " +
				"(SELECT Id FROM Calendario " +
				"		    WHERE Calendario.Fecha = ((ServiciosCalendarioOLD.Año || '-' || " +
				"									   substr('0' || ServiciosCalendarioOLD.Mes, -2, 2) || '-' || " +
				"									   substr('0' || ServiciosCalendarioOLD.Dia, -2, 2)))) As IdCalendario, " +
				"Servicio, " +
				"Turno, " +
				"Linea, " +
				"Lineas.TextoLinea As TextoLinea, " +
				"(strftime('%H', Inicio) * 60 + strftime('%M', Inicio)) * 600000000 As Inicio, " +
				"LugarInicio, " +
				"(strftime('%H', Final) * 60 + strftime('%M', Final)) * 600000000 As Final, " +
				"LugarFinal " +
			"FROM ServiciosCalendarioOLD LEFT JOIN Lineas ON ServiciosCalendarioOLD.Linea = Lineas.NumeroLinea; ";


		string COMANDO_COPIAR_HORAS_AJENAS_V4To5 =
			"INSERT INTO HorasAjenas " +
				"(Fecha, Horas, Motivo, Codigo) " +
			"SELECT " +
				"(Año || '-' || substr('0' || Mes, -2, 2) || '-' || substr('0' ||Dia, -2, 2)) As Fecha, " +
				"Round(Horas, 2) As Horas, " +
				"Motivo, " +
				"0 As Codigo -- Falta Regular los años bisiestos y final de año, poniendo el código correspondiente. " +
			"FROM HorasAjenasOLD; ";


		string COMANDO_COPIAR_INCIDENCIAS_V4To5 =
			"INSERT INTO Incidencias " +
				"(Codigo, TextoIncidencia, Tipo, Notas) " +
			"SELECT " +
				"Codigo, Incidencia, Tipo, '' " +
			"FROM IncidenciasOLD; ";


		string COMANDO_COPIAR_COMPAÑEROS_V4To5 =
			"INSERT INTO Compañeros " +
				"(Matricula, Nombre, Apellidos, Telefono, Calificacion, Deuda, Notas) " +
			"SELECT " +
				"Matricula, Nombre, Apellidos, Telefono, Calificacion, Deuda, Notas " +
			"FROM Relevos;";


		string COMANDO_COPIAR_LINEAS_V4To5 =
			"INSERT INTO Lineas " +
				"(NumeroLinea, TextoLinea, Notas) " +
			"SELECT " +
				"NumeroLinea, TextoLinea, Notas " +
			"FROM LineasOLD; ";


		string COMANDO_COPIAR_SERVICIOS_V4To5 =
			"INSERT INTO Servicios " +
				"(NumeroLinea, Servicio, Turno, Inicio, LugarInicio, Final, LugarFinal, TomaDeje, Euros, Notas) " +
			"SELECT " +
				"Linea As NumeroLinea, " +
				"Servicio, " +
				"Turno, " +
				"(strftime('%H', Inicio) * 60 + strftime('%M', Inicio)) * 600000000 As Inicio, " +
				"LugarInicio, " +
				"(strftime('%H', Final) * 60 + strftime('%M', Final)) * 600000000 As Final, " +
				"LugarFinal, " +
				"(strftime('%H', TomaDeje) * 60 + strftime('%M', TomaDeje)) * 600000000 As TomaDeje, " +
				"Euros, " +
				"'' As Notas " +
			"FROM ServiciosOLD; ";


		string COMANDO_COPIAR_SERVICIOS_AUXILIARES_V4To5 =
			"INSERT INTO ServiciosAuxiliares " +
			"(IdServicio, NumeroLinea, Servicio, Turno, Inicio, LugarInicio, Final, LugarFinal) " +
			"SELECT " +
				"(SELECT Id FROM Servicios WHERE Servicios.NumeroLinea = ServiciosAuxiliaresOLD.Linea " +
				"						   AND Servicios.Servicio = ServiciosAuxiliaresOLD.Servicio " +
				"						   AND Servicios.Turno = ServiciosAuxiliaresOLD.Turno) As IdServicio, " +
				"LineaAuxiliar, " +
				"ServicioAuxiliar, " +
				"TurnoAuxiliar, " +
				"(strftime('%H', Inicio) * 60 + strftime('%M', Inicio)) * 600000000 As Inicio, " +
				"LugarInicio, " +
				"(strftime('%H', Final) * 60 + strftime('%M', Final)) * 600000000 As Final, " +
				"LugarFinal " +
			"FROM ServiciosAuxiliaresOLD; ";


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region COMANDOS SQL COPIAR TABLAS V5 a V6
		// ====================================================================================================
		string COMANDO_COPIAR_CALENDARIO_V5To6 =
			"INSERT INTO Calendario (" +
				"Fecha, EsFranqueo, EsFestivo, CodigoIncidencia, HuelgaParcial, HorasHuelga, Servicio, " +
				"Turno, NumeroLinea, TextoLinea, Inicio, LugarInicio, Final, LugarFinal, Trabajadas, " +
				"Acumuladas, Nocturnas, Desayuno, Comida, Cena, TomaDeje, Euros, Relevo, Susti, Bus, Notas) " +
			"SELECT " +
			//TODO: Falta parsear los campos antiguos en los nuevos.
			"FROM CalendarioOLD;";


		string COMANDO_COPIAR_SERVICIOS_CALENDARIO_V5To6 =
			"INSERT INTO ServiciosCalendario (" +
				"IdCalendario, Servicio, Turno, NumeroLinea, TextoLinea, Inicio, LugarInicio, Final, LugarFinal) " +
			"SELECT " +
			//TODO: Falta parsear los campos antiguos en los nuevos.
			"FROM ServiciosCalendarioOLD;";


		string COMANDO_COPIAR_HORAS_AJENAS_V5To6 =
			"INSERT INTO HorasAjenas (" +
				"Fecha, Horas, Motivo, Codigo) " +
			"SELECT " +
			//TODO: Falta parsear los campos antiguos en los nuevos.
			"FROM HorasAjenasOLD;";


		string COMANDO_COPIAR_INCIDENCIAS_V5To6 =
			"INSERT INTO Incidencias (" +
				"Codigo, TextoIncidencia, Tipo, Notas) " +
			"SELECT " +
			//TODO: Falta parsear los campos antiguos en los nuevos.
			"FROM IncidenciasOLD;";


		string COMANDO_COPIAR_COMPAÑEROS_V5To6 =
			"INSERT INTO Compañeros (" +
				"Matricula, Nombre, Apellidos, Telefono, Calificacion, Deuda, Notas) " +
			"SELECT " +
			//TODO: Falta parsear los campos antiguos en los nuevos.
			"FROM CompañerosOLD;";


		string COMANDO_COPIAR_LINEAS_V5To6 =
			"INSERT INTO Lineas (" +
				"NumeroLinea, TextoLinea, Notas) " +
			"SELECT " +
			//TODO: Falta parsear los campos antiguos en los nuevos.
			"FROM LineasOLD;";


		string COMANDO_COPIAR_SERVICIOS_V5To6 =
			"INSERT INTO Servicios (" +
				"NumeroLinea, Servicio, Turno, Inicio, LugarInicio, Final, LugarFinal, TomaDeje, Euros, Notas) " +
			"SELECT " +
			//TODO: Falta parsear los campos antiguos en los nuevos.
			"FROM ServiciosOLD;";


		string COMANDO_COPIAR_SERVICIOS_AUXILIARES_V5To6 =
			"INSERT INTO ServiciosAuxiliares (" +
				"IdServicio, NumeroLinea, Servicio, Turno, Inicio, LugarInicio, Final, LugarFinal) " +
			"SELECT " +
			//TODO: Falta parsear los campos antiguos en los nuevos.
			"FROM ServiciosAuxiliaresOLD;";


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PRIVADOS
		// ====================================================================================================

		// La cadena de conexión standard será esta:
		//
		//      Data Source=filename;Version=5;Pooling=True;Max Pool Size=100;
		//

		private void EjecutarComandoSQL(string comandoSQL, string cadenaConexion) {
			using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion)) {
				conexion.Open();
				using (SQLiteCommand comando = conexion.CreateCommand()) {
					comando.CommandText = comandoSQL;
					comando.ExecuteNonQuery();
				}
			}
		}


		private void ActualizarTabla(string nombreTabla, string sqlCrear, string sqlCopiar, string cadenaConexion) {
			string sqlRenombrar = $"ALTER TABLE {nombreTabla} RENAME TO {nombreTabla}OLD";
			string sqlBorrar = $"DROP TABLE IF EXISTS {nombreTabla}OLD";
			using (SQLiteConnection conexion = new SQLiteConnection(cadenaConexion)) {
				conexion.Open();
				using (SQLiteTransaction transaccion = conexion.BeginTransaction()) {
					using (SQLiteCommand comando = conexion.CreateCommand()) {
						comando.Transaction = transaccion;
						comando.CommandText = sqlRenombrar;
						comando.ExecuteNonQuery();
						comando.CommandText = sqlCrear;
						comando.ExecuteNonQuery();
						comando.CommandText = sqlCopiar;
						comando.ExecuteNonQuery();
						comando.CommandText = sqlBorrar;
						comando.ExecuteNonQuery();
					}
					transaccion.Commit();
				}
			}
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region CREAR TABLAS
		// ====================================================================================================

		public void CrearTablaCalendario(string cadenaConexion) {
			EjecutarComandoSQL(COMANDO_CREAR_CALENDARIO, cadenaConexion);
		}


		public void CrearTablaServiciosCalendario(string cadenaConexion) {
			EjecutarComandoSQL(COMANDO_CREAR_SERVICIOS_CALENDARIO, cadenaConexion);
		}


		public void CrearTablaHorasAjenas(string cadenaConexion) {
			EjecutarComandoSQL(COMANDO_CREAR_HORAS_AJENAS, cadenaConexion);
		}


		public void CrearTablaIncidencias(string cadenaConexion) {
			EjecutarComandoSQL(COMANDO_CREAR_INCIDENCIAS, cadenaConexion);
		}


		public void CrearTablaCompañeros(string cadenaConexion) {
			EjecutarComandoSQL(COMANDO_CREAR_COMPAÑEROS, cadenaConexion);
		}


		public void CrearTablaLineas(string cadenaConexion) {
			EjecutarComandoSQL(COMANDO_CREAR_LINEAS, cadenaConexion);
		}


		public void CrearTablaServicios(string cadenaConexion) {
			EjecutarComandoSQL(COMANDO_CREAR_SERVICIOS, cadenaConexion);
		}


		public void CrearTablaServiciosAuxiliares(string cadenaConexion) {
			EjecutarComandoSQL(COMANDO_CREAR_SERVICIOS_AUXILIARES, cadenaConexion);
		}


		public void CrearTablasTodas(string cadenaCOnexion) {
			CrearTablaCalendario(cadenaCOnexion);
			CrearTablaServiciosCalendario(cadenaCOnexion);
			CrearTablaHorasAjenas(cadenaCOnexion);
			CrearTablaIncidencias(cadenaCOnexion);
			CrearTablaCompañeros(cadenaCOnexion);
			CrearTablaLineas(cadenaCOnexion);
			CrearTablaServicios(cadenaCOnexion);
			CrearTablaServiciosAuxiliares(cadenaCOnexion);
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region ACTUALIZAR TABLAS
		// ====================================================================================================

		// EL proceso de actualización de una base de datos es el sigueinte:
		//
		//    - Renombrar tabla antigua por antiguaOLD.
		//    - Crear tabla nueva.
		//    - Copiar de la tabla antigua a la nueva.
		//    - Borrar la tabla antigua.
		//
		// No se permite la actualización de tablas por debajo de la versión 5, ya que la estructura de la
		// versión 4 y anteriores no contiene enlaces a las IDs de las tablas vinculadas.
		//
		// La actualización de las tablas de la versión 4 a la versión 5, se realiza por medio de un algoritmo
		// diferente, para enlazar los datos de las tablas por medio de Ids.

		public void ActualizarTablas(int oldVersion, string cadenaConexion) {
			switch (oldVersion) {
				case 4:
					ActualizarTabla("Calendario", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_CALENDARIO_V4To5, cadenaConexion);
					ActualizarTabla("ServiciosCalendario", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_SERVICIOS_CALENDARIO_V4To5, cadenaConexion);
					ActualizarTabla("HorasAjenas", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_HORAS_AJENAS_V4To5, cadenaConexion);
					ActualizarTabla("Incidencias", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_INCIDENCIAS_V4To5, cadenaConexion);
					ActualizarTabla("Compañeros", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_COMPAÑEROS_V4To5, cadenaConexion);
					ActualizarTabla("Lineas", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_LINEAS_V4To5, cadenaConexion);
					ActualizarTabla("Servicios", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_SERVICIOS_V4To5, cadenaConexion);
					ActualizarTabla("ServiciosAuxiliares", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_SERVICIOS_AUXILIARES_V4To5, cadenaConexion);
					break;
				case 5:
					ActualizarTabla("Calendario", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_CALENDARIO_V5To6, cadenaConexion);
					ActualizarTabla("ServiciosCalendario", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_SERVICIOS_CALENDARIO_V5To6, cadenaConexion);
					ActualizarTabla("HorasAjenas", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_HORAS_AJENAS_V5To6, cadenaConexion);
					ActualizarTabla("Incidencias", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_INCIDENCIAS_V5To6, cadenaConexion);
					ActualizarTabla("Compañeros", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_COMPAÑEROS_V5To6, cadenaConexion);
					ActualizarTabla("Lineas", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_LINEAS_V5To6, cadenaConexion);
					ActualizarTabla("Servicios", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_SERVICIOS_V5To6, cadenaConexion);
					ActualizarTabla("ServiciosAuxiliares", COMANDO_CREAR_CALENDARIO, COMANDO_COPIAR_SERVICIOS_AUXILIARES_V5To6, cadenaConexion);
					break;
			}
		}



		#endregion
		// ====================================================================================================




	}
}
