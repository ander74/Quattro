#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using Quattro.Notify;

namespace Quattro.Models {


    public class ConvenioBase : NotifyBase {



		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		private decimal jornadaMedia = 7.75m;
		public decimal JornadaMedia {
			get { return jornadaMedia; }
			set {
				if (jornadaMedia != value) {
					jornadaMedia = value;
					OnPropertyChanged();
				}
			}
		}


		private decimal jornadaMinima = 7.00m;
		public decimal JornadaMinima {
			get { return jornadaMinima; }
			set {
				if (jornadaMinima != value) {
					jornadaMinima = value;
					OnPropertyChanged();
				}
			}
		}


		private decimal jornadaAnual = 1592.00m;
		public decimal JornadaAnual {
			get { return jornadaAnual; }
			set {
				if (jornadaAnual != value) {
					jornadaAnual = value;
					OnPropertyChanged();
				}
			}
		}


		private TimeSpan limiteEntreServicios = new TimeSpan(1, 0, 0);
		public TimeSpan LimiteEntreServicios {
			get { return limiteEntreServicios; }
			set {
				if (limiteEntreServicios != value) {
					limiteEntreServicios = value;
					OnPropertyChanged();
				}
			}
		}


		private TimeSpan inicioNocturnas = new TimeSpan(22, 0, 0);
		public TimeSpan InicioNocturnas {
			get { return inicioNocturnas; }
			set {
				if (inicioNocturnas != value) {
					inicioNocturnas = value;
					OnPropertyChanged();
				}
			}
		}


		private TimeSpan finalNocturnas = new TimeSpan(6, 30, 0);
		public TimeSpan FinalNocturnas {
			get { return finalNocturnas; }
			set {
				if (finalNocturnas != value) {
					finalNocturnas = value;
					OnPropertyChanged();
				}
			}
		}


		private TimeSpan limiteDesayuno = new TimeSpan(4, 30, 0);
		public TimeSpan LimiteDesayuno {
			get { return limiteDesayuno; }
			set {
				if (limiteDesayuno != value) {
					limiteDesayuno = value;
					OnPropertyChanged();
				}
			}
		}


		private TimeSpan limiteComidaTurno1 = new TimeSpan(15, 30, 0);
		public TimeSpan LimiteComidaTurno1 {
			get { return limiteComidaTurno1; }
			set {
				if (limiteComidaTurno1 != value) {
					limiteComidaTurno1 = value;
					OnPropertyChanged();
				}
			}
		}


		private TimeSpan limiteComidaTurno2 = new TimeSpan(13, 30, 0);
		public TimeSpan LimiteComidaTurno2 {
			get { return limiteComidaTurno2; }
			set {
				if (limiteComidaTurno2 != value) {
					limiteComidaTurno2 = value;
					OnPropertyChanged();
				}
			}
		}


		private TimeSpan limiteCena = new TimeSpan(0, 30, 0);
		public TimeSpan LimiteCena {
			get { return limiteCena; }
			set {
				if (limiteCena != value) {
					limiteCena = value;
					OnPropertyChanged();
				}
			}
		}




		#endregion
		// ====================================================================================================



	}
}
