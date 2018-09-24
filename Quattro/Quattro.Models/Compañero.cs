#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System.Data.Common;
using Quattro.Common;
using Quattro.Notify;

namespace Quattro.Models {

	class Compañero : NotifyBase {

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public Compañero() { }


		public Compañero(DbDataReader lector) {
			FromReader(lector);
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public virtual void FromReader(DbDataReader lector) {
			id = lector.ToInt32("Id");
			matricula = lector.ToInt32("Matricula");
			nombre = lector.ToString("Nombre");
			apellidos = lector.ToString("Apellidos");
			telefono = lector.ToString("Telefono");
			calificacion = (CalificacionCompañero)lector.ToInt32("Calificacion");
			deuda = lector.ToInt32("Deuda");
			notas = lector.ToString("Notas");
		}


		public virtual void ToCommand(ref DbCommand comando) {
			// Matricula
			DbParameter parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Matricula";
			parametro.Value = Matricula;
			comando.Parameters.Add(parametro);
			//Nombre
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Nombre";
			parametro.Value = Nombre;
			comando.Parameters.Add(parametro);
			//Apellidos
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Apellidos";
			parametro.Value = Apellidos;
			comando.Parameters.Add(parametro);
			//Telefono
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Telefono";
			parametro.Value = Telefono;
			comando.Parameters.Add(parametro);
			//Calificacion
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Calificacion";
			parametro.Value = Calificacion;
			comando.Parameters.Add(parametro);
			//Deuda
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Deuda";
			parametro.Value = Deuda;
			comando.Parameters.Add(parametro);
			// Notas
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Notas";
			parametro.Value = Notas;
			comando.Parameters.Add(parametro);
			// Id
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Id";
			parametro.Value = Id;
			comando.Parameters.Add(parametro);
		}


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


		private CalificacionCompañero calificacion;
		public CalificacionCompañero Calificacion {
			get { return calificacion; }
			set {
				if (calificacion != value) {
					calificacion = value;
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
