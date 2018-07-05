#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using System.Data.Common;
using Quattro;

namespace Quattro.Models {

	class HoraAjena : NotifyBase {

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public HoraAjena() { }


		public HoraAjena(DbDataReader lector) {
			FromReader(lector);
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public void FromReader(DbDataReader lector) {
			id = lector.ToInt32("_id");
			fecha = lector.ToDateTime("Fecha");
			horas = lector.ToDecimal("Horas");
			motivo = lector.ToString("Motivo");
			codigo = lector.ToInt32("Codigo");
		}


		public void ToCommand(ref DbCommand comando) {
			// Fecha
			DbParameter parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.DateTime;
			parametro.ParameterName = "@Fecha";
			parametro.Value = fecha;
			// Horas
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Decimal;
			parametro.ParameterName = "@Horas";
			parametro.Value = horas;
			// Motivo
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Motivo";
			parametro.Value = motivo;
			// Codigo
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Codigo";
			parametro.Value = codigo;
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
			return $"{Fecha:dd:MM:yyyy}: {Horas:0.00} {Motivo}";
		}


		public override bool Equals(object obj) {
			var horaajena = obj as HoraAjena;
			if (horaajena == null) return false;
			return Fecha == horaajena.Fecha && Horas == horaajena.Horas && Motivo == horaajena.Motivo && Codigo == horaajena.Codigo;
		}


		public override int GetHashCode() {
			unchecked {
				int hash = 5060;
				hash = hash * fecha.GetHashCode();
				hash = hash * horas.GetHashCode();
				hash = hash * motivo?.GetHashCode() ?? 1234;
				hash = hash * codigo.GetHashCode();
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
				   "FROM HorasAjenas " +
				   "ORDER BY Fecha;";
		}


		public static string GetInsertQuery() {
			return "INSERT INTO HorasAjenas " +
				   "   (Fecha, Horas, Mofivo, Codigo) " +
				   "VALUES " +
				   "   (@Fecha, @Horas, @Motivo, @Codigo);";
		}


		public static string GetUpdateQuery() {
			return "UPDATE HorasAjenas " +
				   "SET Fecha=@Fecha, Horas=@Horas, Motivo=@Motivo, Codigo=@Codigo " +
				   "WHERE _id=@Id;";
		}


		public static string GetDeleteQuery() {
			return "DELETE FROM HorasAjenas " +
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


		private DateTime fecha;
		public DateTime Fecha {
			get { return fecha; }
			set {
				if (fecha != value) {
					fecha = value;
					PropiedadCambiada();
				}
			}
		}


		private decimal horas;
		public decimal Horas {
			get { return horas; }
			set {
				if (horas != value) {
					horas = value;
					PropiedadCambiada();
				}
			}
		}


		private string motivo;
		public string Motivo {
			get { return motivo; }
			set {
				if (motivo != value) {
					motivo = value;
					PropiedadCambiada();
				}
			}
		}


		private int codigo;
		public int Codigo {
			get { return codigo; }
			set {
				if (codigo != value) {
					codigo = value;
					PropiedadCambiada();
				}
			}
		}


		#endregion
		// ====================================================================================================




	}
}
