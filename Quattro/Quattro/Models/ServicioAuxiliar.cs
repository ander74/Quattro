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

	public class ServicioAuxiliar : ServicioBase, IEquatable<ServicioAuxiliar>
	{

		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override bool Equals(object obj) => (obj is ServicioAuxiliar serviciobase) && Equals(serviciobase);

		public bool Equals(ServicioAuxiliar sb) => (NumeroLinea, Servicio, Turno) == (sb.NumeroLinea, sb.Servicio, sb.Turno);

		public static bool operator ==(ServicioAuxiliar s1, ServicioAuxiliar s2) => Equals(s1, s2);

		public static bool operator !=(ServicioAuxiliar s1, ServicioAuxiliar s2) => !Equals(s1, s2);

		public override int GetHashCode() => (NumeroLinea, Servicio, Turno).GetHashCode();

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
