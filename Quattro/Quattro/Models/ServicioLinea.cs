#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models 
{
	using System.Collections.Generic;

	public class ServicioLinea: Servicio {


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override bool Equals(object obj) {
			if (obj is ServicioLinea servicio)
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

		private int servicioLineaId;
		public int ServicioLineaId
		{
			get { return servicioLineaId; }
			set { SetValue(ref servicioLineaId, value); }
		}


		public int LineaId { get; set; }


		public Linea Linea { get; set; }


		private List<ServicioAuxiliar> servicios;
		public virtual List<ServicioAuxiliar> Servicios {
			get { return servicios; }
			set { SetValue(ref servicios, value); }
		}

		#endregion
		// ====================================================================================================



	}
}
