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
	public class ServicioLinea: Servicio {


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		public int LineaId { get; set; } // Id de la línea a la que pertenece el servicio.

		public Linea Linea { get; set; } // Línea a la que pertenece el servicio, según EFCore. Falta Anotación ForeignKey.


		//TODO: Faltan los servicios auxiliares.

		#endregion
		// ====================================================================================================



	}
}
