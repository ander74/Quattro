#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models {
	using System.Collections.Generic;
	using Quattro.Notify;

	public class Incidencia : NotifyBase {


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() {
			return $"{codigo:00}: {TextoIncidencia}";
		}


		public override bool Equals(object obj) {
			var incidencia = obj as Incidencia;
			if (incidencia == null) return false;
			return Codigo == incidencia.Codigo;
		}


		public override int GetHashCode() {
			unchecked {
				int hash = 5060;
				hash = hash * codigo.GetHashCode();
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


		private int codigo;
		public int Codigo {
			get { return codigo; }
			set { SetValue(ref codigo, value); }
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


		public List<DiaCalendario> DiasCalendario { get; set; }


		#endregion
		// ====================================================================================================



	}
}
