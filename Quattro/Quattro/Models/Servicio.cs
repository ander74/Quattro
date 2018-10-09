#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models {

	using Common;

	public class Servicio : ServicioBase {


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override bool Equals(object obj) {
			if (obj is Servicio servicio)
				return NumeroLinea == servicio.NumeroLinea && Servicio == servicio.Servicio && Turno == servicio.Turno;
			return false;
		}


		public override int GetHashCode() {
			unchecked {
				int hash = 5060;
				hash = (hash * 7) + NumeroLinea?.GetHashCode() ?? 1234;
				hash = (hash * 7) + Servicio?.GetHashCode() ?? 1234;
				hash = (hash * 7) + Turno.GetHashCode();
				return hash;
			}
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================


		private decimal trabajadas;
		public decimal Trabajadas {
			get { return trabajadas; }
			set { SetValue(ref trabajadas, value); }
		}


		private decimal acumuladas;
		public decimal Acumuladas {
			get { return acumuladas; }
			set { SetValue(ref acumuladas, value); }
		}


		private decimal nocturnas;
		public decimal Nocturnas {
			get { return nocturnas; }
			set { SetValue(ref nocturnas, value); }
		}


		private bool desayuno;
		public bool Desayuno {
			get { return desayuno; }
			set { SetValue(ref desayuno, value); }
		}


		private bool comida;
		public bool Comida {
			get { return comida; }
			set { SetValue(ref comida, value); }
		}


		private bool cena;
		public bool Cena {
			get { return cena; }
			set { SetValue(ref cena, value); }
		}


		private Tiempo tomaDeje;
		public Tiempo TomaDeje {
			get { return tomaDeje; }
			set { SetValue(ref tomaDeje, value); }
		}


		private decimal euros;
		public decimal Euros {
			get { return euros; }
			set { SetValue(ref euros, value); }
		}


		private string notas;
		public string Notas {
			get { return notas; }
			set { SetValue(ref notas, value); }
		}


		#endregion
		// ====================================================================================================



	}
}
