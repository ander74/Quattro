#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Common
{

	/// <summary>
	/// Representa el método que manejará el evento lanzado cuando cambia una parte de Quattro.Models.ServicioBase.
	/// </summary>
	/// <param name="sender">Objeto ServicioBase que provoca el evento.</param>
	/// <param name="e">Los datos del evento lanzado.</param>
	public delegate void ServicioChangedEventHandler(object sender, ServicioChangedEventArgs e);

}
