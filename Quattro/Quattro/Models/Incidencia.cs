#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models 
{
	using System.Collections.Generic;
	using Quattro.Notify;

	public class Incidencia : EntityNotifyBase {


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() {
			return $"{codigoIncidencia:00}: {TextoIncidencia}";
		}


		public override bool Equals(object obj) {
			var incidencia = obj as Incidencia;
			if (incidencia == null) return false;
			return CodigoIncidencia == incidencia.CodigoIncidencia;
		}


		public override int GetHashCode() {
			unchecked {
				int hash = 5060;
				hash = hash * codigoIncidencia.GetHashCode();
				return hash;
			}
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================


		//private int id;
		//public int Id {
		//	get { return id; }
		//	set { SetValue(ref id, value); }
		//}


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
