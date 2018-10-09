#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models {

	using System;
	using Quattro.Notify;

	public class HoraAjena : NotifyBase { 


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
			set { SetValue(ref id, value); }
		}


		private DateTime fecha;
		public DateTime Fecha {
			get { return fecha; }
			set { SetValue(ref fecha, value); }
		}


		private decimal horas;
		public decimal Horas {
			get { return horas; }
			set { SetValue(ref horas, value); }
		}


		private string motivo;
		public string Motivo {
			get { return motivo; }
			set { SetValue(ref motivo, value); }
		}


		private int codigo;
		public int Codigo {
			get { return codigo; }
			set { SetValue(ref codigo, value); }
		}


		#endregion
		// ====================================================================================================


	}
}
