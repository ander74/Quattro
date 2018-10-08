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

namespace Quattro.Models2 {


	/// <summary>
	/// 
	/// LÍNEA
	/// =====
	/// 
	///		Se pueden cargar los datos desde un DataReader y se pueden introducir los datos como parámetros de 
	///		un Command que se pase por referencia.
	///		
	///		
	/// </summary>
	public class Linea : NotifyBase {

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public Linea () { }


		public Linea(DbDataReader lector) {
			FromReader(lector);
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public virtual void FromReader(DbDataReader lector) {
			id = lector.ToInt32("Id");
			numeroLinea = lector.ToString("NumeroLinea");
			textoLinea = lector.ToString("TextoLinea");
			notas = lector.ToString("Notas");
		}


		public virtual void ToCommand(DbCommand comando) { //TODO: Comprobar que esto se puede (quitar el ref).
			// Id
			DbParameter parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Id";
			parametro.Value = Id;
			comando.Parameters.Add(parametro);
			// NumeroLinea
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@NumeroLinea";
			parametro.Value = NumeroLinea;
			comando.Parameters.Add(parametro);
			// TextoLinea
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@TextoLinea";
			parametro.Value = TextoLinea;
			comando.Parameters.Add(parametro);
			// Notas
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Notas";
			parametro.Value = Notas;
			comando.Parameters.Add(parametro);
		}


		#endregion
		// ====================================================================================================


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
			set {
				if (id != value) {
					id = value;
					OnPropertyChanged();
				}
			}
		}


		private string numeroLinea;
		public string NumeroLinea {
			get { return numeroLinea; }
			set {
				if (numeroLinea != value) {
					numeroLinea = value;
					OnPropertyChanged();
				}
			}
		}


		private string textoLinea;
		public string TextoLinea {
			get { return textoLinea; }
			set {
				if (textoLinea != value) {
					textoLinea = value;
					OnPropertyChanged();
				}
			}
		}


		private NotifyCollection<Servicio> servicios;
		public NotifyCollection<Servicio> Servicios {
			get { return servicios; }
			set {
				if (servicios != value) {
					servicios = value;
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
