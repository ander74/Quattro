#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using System.Data.Common;
using Quattro.Common;
using Quattro.Notify;

namespace Quattro.Models2 {


	/// <summary>
	/// 
	/// HORA AJENA
	/// ==========
	/// 
	///		Se pueden cargar los datos desde un DataReader y se pueden introducir los datos como parámetros de 
	///		un Command que se pase por referencia.
	///		
	/// </summary>
	public class HoraAjena : NotifyBase {

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

		public virtual void FromReader(DbDataReader lector) {
			id = lector.ToInt32("Id");
			fecha = lector.ToDateTime("Fecha");
			horas = lector.ToDecimal("Horas");
			motivo = lector.ToString("Motivo");
			codigo = lector.ToInt32("Codigo");
		}


		public virtual void ToCommand(DbCommand comando) { //TODO: Comprobar que esto se puede (quitar el ref).
			// Id
			DbParameter parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Id";
			parametro.Value = Id;
			comando.Parameters.Add(parametro);
			// Fecha
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.DateTime;
			parametro.ParameterName = "@Fecha";
			parametro.Value = Fecha.ToString("yyyy-MM-dd");
			comando.Parameters.Add(parametro);
			// Horas
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Decimal;
			parametro.ParameterName = "@Horas";
			parametro.Value = Horas;
			comando.Parameters.Add(parametro);
			// Motivo
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Motivo";
			parametro.Value = Motivo;
			comando.Parameters.Add(parametro);
			//Codigo
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Codigo";
			parametro.Value = Codigo;
			comando.Parameters.Add(parametro);
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
		#region PROPIEDADES
		// ====================================================================================================


		private int id;
		public int Id {
			get { return id; }
			set {
				if (id != value) {
					id = value;
					OnPropertyChanged();
				}
			}
		}


		private DateTime fecha;
		public DateTime Fecha {
			get { return fecha; }
			set {
				if (fecha != value) {
					fecha = value;
					OnPropertyChanged();
				}
			}
		}


		private decimal horas;
		public decimal Horas {
			get { return horas; }
			set {
				if (horas != value) {
					horas = value;
					OnPropertyChanged();
				}
			}
		}


		private string motivo;
		public string Motivo {
			get { return motivo; }
			set {
				if (motivo != value) {
					motivo = value;
					OnPropertyChanged();
				}
			}
		}


		private int codigo;
		public int Codigo {
			get { return codigo; }
			set {
				if (codigo != value) {
					codigo = value;
					OnPropertyChanged();
				}
			}
		}


		#endregion
		// ====================================================================================================




	}
}
