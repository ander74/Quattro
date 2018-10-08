#region COPYRIGHT
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
		#region PROPIEDADES
		// ====================================================================================================

		public int ServicioId { get; set; } // Id del servicio al que pertenece el servicio auxiliar.

		public ServicioLinea ServicioLinea { get; set; } // ServicioLinea al que pertenece el servicio, según EFCore. Falta Anotación ForeignKey.

		#endregion
		// ====================================================================================================



	}
}
