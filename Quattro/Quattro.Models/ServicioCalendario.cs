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
	public class ServicioCalendario : ServicioBase {


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		public int DiaCalendarioId { get; set; } // Id del DiaCalendario al que pertenece el servicio.

		public DiaCalendario DiaCalendario { get; set; } // DiaCalendario al que pertenece el servicio, según EFCore. Falta Anotación ForeignKey.

		#endregion
		// ====================================================================================================


	}
}
