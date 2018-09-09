#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using Quattro.Notify;

namespace Quattro.Models {

    public class Servicio : ServicioBase {

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public Servicio() : base() { }


		#endregion
		// ====================================================================================================


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
			set {
				if (trabajadas != value) {
					trabajadas = value;
					OnPropertyChanged();
				}
			}
		}


		private decimal acumuladas;
		public decimal Acumuladas {
			get { return acumuladas; }
			set {
				if (acumuladas != value) {
					acumuladas = value;
					OnPropertyChanged();
				}
			}
		}


		private decimal nocturnas;
		public decimal Nocturnas {
			get { return nocturnas; }
			set {
				if (nocturnas != value) {
					nocturnas = value;
					OnPropertyChanged();
				}
			}
		}


		private bool desayuno;
		public bool Desayuno {
			get { return desayuno; }
			set {
				if (desayuno != value) {
					desayuno = value;
					OnPropertyChanged();
				}
			}
		}


		private bool comida;
		public bool Comida {
			get { return comida; }
			set {
				if (comida != value) {
					comida = value;
					OnPropertyChanged();
				}
			}
		}


		private bool cena;
		public bool Cena {
			get { return cena; }
			set {
				if (cena != value) {
					cena = value;
					OnPropertyChanged();
				}
			}
		}


		private TimeSpan tomaDeje;
		public TimeSpan TomaDeje {
			get { return tomaDeje; }
			set {
				if (tomaDeje != value) {
					tomaDeje = value;
					OnPropertyChanged();
				}
			}
		}


		private decimal euros;
		public decimal Euros {
			get { return euros; }
			set {
				if (euros != value) {
					euros = value;
					OnPropertyChanged();
				}
			}
		}


		private NotifyCollection<ServicioBase> serviciosAuxiliares;
		public NotifyCollection<ServicioBase> ServiciosAuxiliares {
			get { return serviciosAuxiliares; }
			set {
				if (serviciosAuxiliares != value) {
					serviciosAuxiliares = value;
					OnPropertyChanged();
				}
			}
		}


		private string notas;
		public string Notas {
			get { return notas; }
			set {
				if (notas != value) {
					notas = value;
					OnPropertyChanged();
				}
			}
		}


		#endregion
		// ====================================================================================================



	}
}
