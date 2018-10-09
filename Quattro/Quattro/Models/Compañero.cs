#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models {

	using System.Collections.Generic;
	using Common;
	using Notify;

	public class Compañero: NotifyBase {


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() {
			return $"{Matricula:00}: {Nombre} {Apellidos}";
		}


		public override bool Equals(object obj) {
			var compañero = obj as Compañero;
			if (compañero == null) return false;
			return Matricula == compañero.Matricula;
		}


		public override int GetHashCode() {
			unchecked {
				int hash = 5060;
				hash = hash * matricula.GetHashCode();
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


		private int matricula;
		public int Matricula {
			get { return matricula; }
			set { SetValue(ref matricula, value); }
		}


		private string nombre;
		public string Nombre {
			get { return nombre; }
			set { SetValue(ref nombre, value); }
		}


		private string apellidos;
		public string Apellidos {
			get { return apellidos; }
			set { SetValue(ref apellidos, value); }
		}


		private string telefono;
		public string Telefono {
			get { return telefono; }
			set { SetValue(ref telefono, value); }
		}


		private CalificacionCompañero calificacion;
		public CalificacionCompañero Calificacion {
			get { return calificacion; }
			set { SetValue(ref calificacion, value); }
		}


		private int deuda;
		public int Deuda {
			get { return deuda; }
			set { SetValue(ref deuda, value); }
		}


		private string notas;
		public string Notas {
			get { return notas; }
			set { SetValue(ref notas, value); }
		}


		public List<DiaCalendario> Relevos { get; set; }

		public List<DiaCalendario> Sustis { get; set; }

		#endregion
		// ====================================================================================================



	}
}
