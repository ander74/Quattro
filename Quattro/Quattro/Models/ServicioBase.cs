#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using System.Data.Common;
using QuattroNet;

namespace Quattro.Models {

    class ServicioBase : NotifyBase {

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public ServicioBase() { }


		public ServicioBase(DbDataReader lector) {
			FromReader(lector);
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public void FromReader(DbDataReader lector) {
			id = lector.ToInt32("_id");
			idLinea = lector.ToInt32("IdLinea");
			servicio = lector.ToString("Servicio");
			turno = lector.ToInt32("Turno");
			inicio = lector.ToTimeSpanNulable("Inicio");
			lugarInicio = lector.ToString("LugarInicio");
			final = lector.ToTimeSpanNulable("Final");
			lugarFinal = lector.ToString("LugarFinal");
			notas = lector.ToString("Notas");
		}


		public void ToCommand(ref DbCommand comando) {
			// IdLinea
			DbParameter parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@IdLinea";
			parametro.Value = idLinea;
			// Servicio
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Servicio";
			parametro.Value = servicio;
			// Turno
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Turno";
			parametro.Value = turno;
			// Inicio
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Time;
			parametro.ParameterName = "@Inicio";
			parametro.Value = inicio;
			// Lugar Inicio
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@LugarInicio";
			parametro.Value = lugarInicio;
			// Final
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Time;
			parametro.ParameterName = "@Final";
			parametro.Value = final;
			// Lugar Final
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@LugarFinal";
			parametro.Value = lugarFinal;
			// Notas
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Notas";
			parametro.Value = notas;
			// Id
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Id";
			parametro.Value = id;
		}




		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() {
			return $"{Servicio}/{Turno}: {Inicio} - {Final}";
		}


		public override bool Equals(object obj) {
			var serviciobase = obj as ServicioBase;
			if (serviciobase == null) return false;
			return IdLinea == serviciobase.IdLinea && Servicio == serviciobase.Servicio && Turno == serviciobase.Turno;
		}


		public override int GetHashCode() {
			unchecked {
				int hash = 5060;
				hash = hash * idLinea.GetHashCode();
				hash = hash * servicio?.GetHashCode() ?? 1234;
				hash = hash * turno.GetHashCode();
				return hash;
			}
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS ESTÁTICOS
		// ====================================================================================================

		public static string GetSelectQuery() {
			return "SELECT * " +
				   "FROM Servicios " +
				   "ORDER BY Servicio, Turno;";
		}


		public static string GetInsertQuery() {
			return "INSERT INTO Servicios " +
				   "   (IdLinea, Servicio, Turno, Inicio, LugarInicio, Final, LugarFinal, Notas) " +
				   "VALUES " +
				   "   (@IdLinea, @Servicio, @Turno, @Inicio, @LugarInicio, @Final, @LugarFinal, @Notas);";
		}


		public static string GetUpdateQuery() {
			return "UPDATE Servicios " +
				   "SET IdLinea=@IdLinea, Servicio=@Servicio, Turno=@Turno, Inicio=@Inicio, LugarInicio=@LugarInicio " +
				   "Final=@Final, LugarFinal=@LugarFinal, Notas=@Notas " +
				   "WHERE _id=@Id;";
		}


		public static string GetDeleteQuery() {
			return "DELETE FROM Servicios " +
				   "WHERE _id=@Id;";
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================


		private int id;
		public int Id {
			get { return id; }
			set {
				if (id != value) {
					id = value;
					PropiedadCambiada();
				}
			}
		}


		private int idLinea;
		public int IdLinea {
			get { return idLinea; }
			set {
				if (idLinea != value) {
					idLinea = value;
					PropiedadCambiada();
				}
			}
		}


		private string servicio;
		public string Servicio {
			get { return servicio; }
			set {
				if (servicio != value) {
					servicio = value;
					PropiedadCambiada();
				}
			}
		}


		private int turno;
		public int Turno {
			get { return turno; }
			set {
				if (turno != value) {
					turno = value;
					PropiedadCambiada();
				}
			}
		}


		private TimeSpan? inicio;
		public TimeSpan? Inicio {
			get { return inicio; }
			set {
				if (inicio != value) {
					inicio = value;
					PropiedadCambiada();
				}
			}
		}


		private string lugarInicio;
		public string LugarInicio {
			get { return lugarInicio; }
			set {
				if (lugarInicio != value) {
					lugarInicio = value;
					PropiedadCambiada();
				}
			}
		}


		private TimeSpan? final;
		public TimeSpan? Final {
			get { return final; }
			set {
				if (final != value) {
					final = value;
					PropiedadCambiada();
				}
			}
		}


		private string lugarFinal;
		public string LugarFinal {
			get { return lugarFinal; }
			set {
				if (lugarFinal != value) {
					lugarFinal = value;
					PropiedadCambiada();
				}
			}
		}


		private string notas;
		public string Notas {
			get { return notas; }
			set {
				if (notas != value) {
					notas = value;
					PropiedadCambiada();
				}
			}
		}

		#endregion
		// ====================================================================================================

	}
}
