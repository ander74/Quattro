﻿#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using Quattro.Notify;

namespace Quattro.Models {

	class Compañero : NotifyBase {

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public Compañero() { }

		#endregion
		// ====================================================================================================


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


		private int id;
		public int Id {
			get { return id; }
			set {
				if (id != value) {
					id = value;
					OnPropertyChanged();
				}
			}
		}


		private int matricula;
		public int Matricula {
			get { return matricula; }
			set {
				if (matricula != value) {
					matricula = value;
					OnPropertyChanged();
				}
			}
		}


		private string nombre;
		public string Nombre {
			get { return nombre; }
			set {
				if (nombre != value) {
					nombre = value;
					OnPropertyChanged();
				}
			}
		}


		private string apellidos;
		public string Apellidos {
			get { return apellidos; }
			set {
				if (apellidos != value) {
					apellidos = value;
					OnPropertyChanged();
				}
			}
		}


		private string telefono;
		public string Telefono {
			get { return telefono; }
			set {
				if (telefono != value) {
					telefono = value;
					OnPropertyChanged();
				}
			}
		}


		private int clasificacion;
		public int Clasificacion {
			get { return clasificacion; }
			set {
				if (clasificacion != value) {
					clasificacion = value;
					OnPropertyChanged();
				}
			}
		}


		private int deuda;
		public int Deuda {
			get { return deuda; }
			set {
				if (deuda != value) {
					deuda = value;
					OnPropertyChanged();
				}
			}
		}


		private string notas;
		public string Notas {
			get { return notas; }
			set {
				if (notas != value) {
					notas = value;
					OnPropertyChanged();
				}
			}
		}



		#endregion
		// ====================================================================================================



	}
}
