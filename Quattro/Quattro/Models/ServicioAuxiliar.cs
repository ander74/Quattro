﻿#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models {

	using System;
	using System.Collections.Generic;
	using System.Text;

	/// <summary>
	/// 
	/// </summary>
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

		public int ServicioId { get; set; } // Id del servicio al que pertenece el servicio auxiliar.

		public ServicioLinea ServicioLinea { get; set; } // ServicioLinea al que pertenece el servicio, según EFCore. Falta Anotación ForeignKey.


		#endregion
		// ====================================================================================================



	}
}
