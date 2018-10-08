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

	/// <summary>
	/// 
	/// </summary>
	public class Linea: NotifyBase {


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() {
			return $"{NumeroLinea}: {TextoLinea}";
		}


		public override bool Equals(object obj) {
			if (obj is Linea linea)
				return NumeroLinea == linea.NumeroLinea;
			return false;
		}


		public override int GetHashCode() {
			unchecked {
				int hash = 5060;
				hash = hash * numeroLinea?.GetHashCode() ?? 1234;
				return hash;
			}
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================


		private int id;
		public int Id {
			get { return id; }
			set { SetValue(ref id, value); }
		}


		private string numeroLinea;
		public string NumeroLinea {
			get { return numeroLinea; }
			set { SetValue(ref numeroLinea, value); }
		}


		private string textoLinea;
		public string TextoLinea {
			get { return textoLinea; }
			set { SetValue(ref textoLinea, value); }
		}


		private string notas;
		public string Notas {
			get { return notas; }
			set { SetValue(ref notas, value); }
		}


		private List<ServicioLinea> servicios;
		public List<ServicioLinea> Servicios {
			get { return servicios; }
			set { SetValue(ref servicios, value); }
		}


		#endregion
		// ====================================================================================================

	}
}
