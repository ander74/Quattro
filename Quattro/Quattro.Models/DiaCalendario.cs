#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using Quattro.Common;

namespace Quattro.Models {

    public class DiaCalendario : Servicio {


		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public DiaCalendario() : base() { }


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() {
			return $"{Fecha.ToString("dd-MM-yyyy")}: {NumeroLinea} - {Servicio}/{Turno} ({Inicio.ToTexto()} - {Final.ToTexto()})";
		}


		public override bool Equals(object obj) {
			if (obj is DiaCalendario diaCalendario)
				return NumeroLinea == diaCalendario.NumeroLinea && Servicio == diaCalendario.Servicio && Turno == diaCalendario.Turno;
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


		private bool esFranqueo;
		public bool EsFranqueo {
			get { return esFranqueo; }
			set {
				if (esFranqueo != value) {
					esFranqueo = value;
					OnPropertyChanged();
				}
			}
		}


		private bool esFestivo;
		public bool EsFestivo {
			get { return esFestivo; }
			set {
				if (esFestivo != value) {
					esFestivo = value;
					OnPropertyChanged();
				}
			}
		}


		private int codigoIncidencia;
		public int CodigoIncidencia {
			get { return codigoIncidencia; }
			set {
				if (codigoIncidencia != value) {
					codigoIncidencia = value;
					OnPropertyChanged();
				}
			}
		}


		private bool huelgaParcial;
		public bool HuelgaParcial {
			get { return huelgaParcial; }
			set {
				if (huelgaParcial != value) {
					huelgaParcial = value;
					OnPropertyChanged();
				}
			}
		}


		private decimal horasHuelga;
		public decimal HorasHuelga {
			get { return horasHuelga; }
			set {
				if (horasHuelga != value) {
					horasHuelga = value;
					OnPropertyChanged();
				}
			}
		}


		private int relevo;
		public int Relevo {
			get { return relevo; }
			set {
				if (relevo != value) {
					relevo = value;
					OnPropertyChanged();
				}
			}
		}


		private int susti;
		public int Susti {
			get { return susti; }
			set {
				if (susti != value) {
					susti = value;
					OnPropertyChanged();
				}
			}
		}


		private string bus;
		public string Bus {
			get { return bus; }
			set {
				if (bus != value) {
					bus = value;
					OnPropertyChanged();
				}
			}
		}


		#endregion
		// ====================================================================================================




	}
}
