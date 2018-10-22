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

	public class ServicioLinea: Servicio, IEquatable<ServicioLinea>
	{

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public ServicioLinea() : base()
		{
			Servicios = new List<ServicioAuxiliar>();
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override bool Equals(object obj) => (obj is ServicioLinea serviciobase) && Equals(serviciobase);

		public bool Equals(ServicioLinea sb) => (NumeroLinea, Servicio, Turno) == (sb.NumeroLinea, sb.Servicio, sb.Turno);

		public static bool operator ==(ServicioLinea s1, ServicioLinea s2) => Equals(s1, s2);

		public static bool operator !=(ServicioLinea s1, ServicioLinea s2) => !Equals(s1, s2);

		public override int GetHashCode() => (NumeroLinea, Servicio, Turno).GetHashCode();

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		private int servicioLineaId;
		public int ServicioLineaId
		{
			get { return servicioLineaId; }
			set { SetValue(ref servicioLineaId, value); }
		}


		public int LineaId { get; set; }


		public Linea Linea { get; set; }


		private List<ServicioAuxiliar> servicios;
		public virtual List<ServicioAuxiliar> Servicios {
			get { return servicios; }
			set { SetValue(ref servicios, value); }
		}

		#endregion
		// ====================================================================================================

	}
}
