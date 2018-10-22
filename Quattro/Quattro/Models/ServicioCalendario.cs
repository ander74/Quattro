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
	
	public class ServicioCalendario : ServicioBase, IEquatable<ServicioCalendario>
	{

		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override bool Equals(object obj) => (obj is ServicioCalendario serviciobase) && Equals(serviciobase);

		public bool Equals(ServicioCalendario sb) => (NumeroLinea, Servicio, Turno) == (sb.NumeroLinea, sb.Servicio, sb.Turno);

		public static bool operator ==(ServicioCalendario s1, ServicioCalendario s2) => Equals(s1, s2);

		public static bool operator !=(ServicioCalendario s1, ServicioCalendario s2) => !Equals(s1, s2);

		public override int GetHashCode() => (NumeroLinea, Servicio, Turno).GetHashCode();

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		private int servicioCalendarioId;
		public int ServicioCalendarioId
		{
			get { return servicioCalendarioId; }
			set { SetValue(ref servicioCalendarioId, value); }
		}


		public int DiaCalendarioId { get; set; }


		public virtual DiaCalendario DiaCalendario { get; set; }

		#endregion
		// ====================================================================================================

	}
}
