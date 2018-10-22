#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models 
{
	using System;
	using System.Collections.Generic;
	using Quattro.Notify;

	public class Incidencia : EntityNotifyBase, IEquatable<Incidencia>
	{

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public Incidencia()
		{
			DiasCalendario = new List<DiaCalendario>();
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() => $"{CodigoIncidencia}: {TextoIncidencia}";

		public override bool Equals(object obj) => (obj is Incidencia incidencia) && Equals(incidencia);

		public bool Equals(Incidencia incidencia) => CodigoIncidencia == incidencia.CodigoIncidencia;

		public static bool operator ==(Incidencia i1, Incidencia i2) => Equals(i1, i2);

		public static bool operator !=(Incidencia i1, Incidencia i2) => !Equals(i1, i2);

		public override int GetHashCode() => CodigoIncidencia.GetHashCode();

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================


		private int codigoIncidencia;
		public int CodigoIncidencia {
			get { return codigoIncidencia; }
			set { SetValue(ref codigoIncidencia, value); }
		}


		private string textoIncidencia;
		public string TextoIncidencia {
			get { return textoIncidencia; }
			set { SetValue(ref textoIncidencia, value); }
		}


		private int tipo;
		public int Tipo {
			get { return tipo; }
			set { SetValue(ref tipo, value); }
		}


		private string notas;
		public string Notas {
			get { return notas; }
			set { SetValue(ref notas, value); }
		}


		public virtual List<DiaCalendario> DiasCalendario { get; set; }


		#endregion
		// ====================================================================================================

	}
}
