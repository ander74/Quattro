#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models 
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Text;

	public class ServicioAuxiliar : ServicioBase {


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override bool Equals(object obj) {
			if (obj is ServicioAuxiliar servicio)
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

		private int servicioAuxiliarId;
		public int ServicioAuxiliarId
		{
			get { return servicioAuxiliarId; }
			set { SetValue(ref servicioAuxiliarId, value); }
		}


		public int ServicioLineaId { get; set; }


		public virtual ServicioLinea ServicioLinea { get; set; }


		#endregion
		// ====================================================================================================



	}
}
