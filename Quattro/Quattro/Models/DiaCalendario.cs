#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models {

	using System;

	/// <summary>
	/// 
	/// DÍA CALENDARIO
	/// ==============
	/// 
	///		Hereda de la calse Servicio.
	///		
	///		Se pueden cargar los datos desde un DataReader y se pueden introducir los datos como parámetros de 
	///		un Command que se pase por referencia.
	///		
	/// </summary>
	public class DiaCalendario : Servicio {


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() {
			return $"{Fecha.ToString("dd-MM-yyyy")}: {NumeroLinea} - {Servicio}/{Turno} ({Inicio} - {Final})";
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
			set { SetValue(ref fecha, value); }
		}


		private bool esFranqueo;
		public bool EsFranqueo {
			get { return esFranqueo; }
			set { SetValue(ref esFranqueo, value); }
		}


		private bool esFestivo;
		public bool EsFestivo {
			get { return esFestivo; }
			set { SetValue(ref esFestivo, value); }
		}


		private int codigoIncidencia; //TODO: Cambiar según EFCore.
		public int CodigoIncidencia {
			get { return codigoIncidencia; }
			set { SetValue(ref codigoIncidencia, value); }
		}


		private bool huelgaParcial;
		public bool HuelgaParcial {
			get { return huelgaParcial; }
			set { SetValue(ref huelgaParcial, value); }
		}


		private decimal horasHuelga;
		public decimal HorasHuelga {
			get { return horasHuelga; }
			set { SetValue(ref horasHuelga, value); }
		}


		private string textoLinea;
		public string TextoLinea {
			get { return textoLinea; }
			set { SetValue(ref textoLinea, value); }
		}


		private int relevo; //TODO: Cambiar según EFCore.
		public int Relevo {
			get { return relevo; }
			set { SetValue(ref relevo, value); }
		}


		private int susti; //TODO: Cambiar según EFCore.
		public int Susti {
			get { return susti; }
			set { SetValue(ref susti, value); }
		}


		private string bus;
		public string Bus {
			get { return bus; }
			set { SetValue(ref bus, value); }
		}


		//TODO: Añadir los servicios del calendario.

		#endregion
		// ====================================================================================================




	}
}
