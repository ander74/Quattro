#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Common
{
	using System;

	public class Response
	{

		public bool IsSuccess { get; set; }

		public string Message { get; set; }

		public object Result { get; set; }

	}
}
