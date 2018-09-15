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


	class Incidencia : NotifyBase {

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public Incidencia() { }

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public virtual void FromReader(DbDataReader lector) {
			id = lector.ToInt32("Id");
			codigo = lector.ToInt32("Codigo");
			textoIncidencia = lector.ToString("TextoIncidencia");
			tipo = lector.ToInt32("Tipo");
			notas = lector.ToString("Notas");
		}


		public virtual void ToCommand(ref DbCommand comando) {
			// Codigo
			DbParameter parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Codigo";
			parametro.Value = Codigo;
			comando.Parameters.Add(parametro);
			//TextoIncidencia
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@TextoIncidencia";
			parametro.Value = TextoIncidencia;
			comando.Parameters.Add(parametro);
			//Tipo
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Tipo";
			parametro.Value = Tipo;
			comando.Parameters.Add(parametro);
			//Notas
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


		private int codigo;
		public int Codigo {
			get { return codigo; }
			set {
				if (codigo != value) {
					codigo = value;
					OnPropertyChanged();
				}
			}
		}


		private string textoIncidencia;
		public string TextoIncidencia {
			get { return textoIncidencia; }
			set {
				if (textoIncidencia != value) {
					textoIncidencia = value;
					OnPropertyChanged();
				}
			}
		}


		private int tipo;
		public int Tipo {
			get { return tipo; }
			set {
				if (tipo != value) {
					tipo = value;
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
