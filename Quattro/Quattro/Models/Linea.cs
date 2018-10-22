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

	public class Linea: EntityNotifyBase, IEquatable<Linea>
	{

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public Linea()
		{
			Servicios = new List<ServicioLinea>();
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() => $"{NumeroLinea}: {TextoLinea}";

		public override bool Equals(object obj) => (obj is Linea linea) && Equals(linea);

		public bool Equals(Linea linea) => NumeroLinea == linea.NumeroLinea;

		public static bool operator ==(Linea linea1, Linea linea2) => Equals(linea1, linea2);

		public static bool operator !=(Linea linea1, Linea linea2) => !Equals(linea1, linea2);

		public override int GetHashCode() => NumeroLinea.GetHashCode();

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================


		private int lineaId;
		public int LineaId {
			get { return lineaId; }
			set { SetValue(ref lineaId, value); }
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
		public virtual List<ServicioLinea> Servicios {
			get { return servicios; }
			set { SetValue(ref servicios, value); }
		}


		#endregion
		// ====================================================================================================

	}
}
