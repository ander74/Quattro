#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using System.Collections.Generic;
using System.Data.Common;
using Quattro;

namespace Quattro.Models {

    class Servicio : ServicioBase {

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public Servicio() : base() { }


		public Servicio(DbDataReader lector) : base(lector) {
			FromReader(lector);
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public void FromReader(DbDataReader lector) {
			base.FromReader(lector);
			tomaDeje = lector.ToTimeSpan("TomaDeje");
			euros = lector.ToDecimal("Euros");
		}


		public void ToCommand(ref DbCommand comando) {
			base.ToCommand(ref comando);
			// TomaDeje
			DbParameter parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Time;
			parametro.ParameterName = "@TomaDeje";
			parametro.Value = tomaDeje;
			// Euros
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Decimal;
			parametro.ParameterName = "@Euros";
			parametro.Value = euros;
		}




		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS ESTÁTICOS
		// ====================================================================================================

		public static string GetInsertQuery() {
			return "INSERT INTO Servicios " +
				   "   (IdLinea, Servicio, Turno, Inicio, LugarInicio, Final, LugarFinal, TomaDeje, Euros, Notas) " +
				   "VALUES " +
				   "   (@IdLinea, @Servicio, @Turno, @Inicio, @LugarInicio, @Final, @LugarFinal, @TomaDeje, @Euros, @Notas);";
		}


		public static string GetUpdateQuery() {
			return "UPDATE Servicios " +
				   "SET IdLinea=@IdLinea, Servicio=@Servicio, Turno=@Turno, Inicio=@Inicio, LugarInicio=@LugarInicio " +
				   "Final=@Final, LugarFinal=@LugarFinal, TomaDeje=@TomaDeje, Euros=@Euros, Notas=@Notas " +
				   "WHERE _id=@Id;";
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================


		private TimeSpan tomaDeje;
		public TimeSpan TomaDeje {
			get { return tomaDeje; }
			set {
				if (tomaDeje != value) {
					tomaDeje = value;
					PropiedadCambiada();
				}
			}
		}


		private decimal euros;
		public decimal Euros {
			get { return euros; }
			set {
				if (euros != value) {
					euros = value;
					PropiedadCambiada();
				}
			}
		}


		private List<ServicioBase> serviciosAuxiliares;
		public List<ServicioBase> ServiciosAuxiliares {
			get { return serviciosAuxiliares; }
			set {
				if (serviciosAuxiliares != value) {
					serviciosAuxiliares = value;
					PropiedadCambiada();
				}
			}
		}

		#endregion
		// ====================================================================================================



	}
}
