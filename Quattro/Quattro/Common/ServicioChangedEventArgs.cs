#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion

namespace Quattro.Common {

	/// <summary>
	/// Clase que encapsula los datos del evento ServicioChanged, lanzado cuando alguna parte de
	/// Quattro.Models.ServicioBase ha cambiado.
	/// </summary>
    public class ServicioChangedEventArgs {

		/// <summary>
		/// Construye un objeto Quattro.Common.ServicioChangedEventArgs.
		/// </summary>
		/// <param name="propertyName">Nombre de la propiedad de Quattro.Models.ServicioBase que ha cambiado.</param>
		public ServicioChangedEventArgs(string propertyName) {
			PropertyName = propertyName;
		}

		/// <summary>
		/// Nombre de la propiedad de Quattro.Models.ServicioBase que ha cambiado.
		/// </summary>
		public virtual string PropertyName { get; }

	}
}
