#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System.Data.Common;
using Quattro;

namespace Quattro.Models {


	class Incidencia : NotifyBase {

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public Incidencia() { }


		public Incidencia(DbDataReader lector) {
			FromReader(lector);
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public void FromReader(DbDataReader lector) {
			id = lector.ToInt32("_id");
			codigo = lector.ToInt32("Codigo");
			texto = lector.ToString("Texto");
			tipo = lector.ToInt32("Tipo");
			notas = lector.ToString("Notas");
		}


		public void ToCommand(ref DbCommand comando) {
			// Codigo
			DbParameter parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Codigo";
			parametro.Value = codigo;
			// Texto
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Texto";
			parametro.Value = texto;
			// Tipo
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Tipo";
			parametro.Value = tipo;
			// Notas
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Notas";
			parametro.Value = notas;
			// Id
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Id";
			parametro.Value = id;
		}




		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() {
			return $"{codigo:00}: {Texto}";
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
		#region MÉTODOS ESTÁTICOS
		// ====================================================================================================

		public static string GetSelectQuery() {
			return "SELECT * " +
				   "FROM Incidencias " +
				   "ORDER BY Codigo;";
		}


		public static string GetInsertQuery() {
			return "INSERT INTO Incidencias " +
				   "   (Codigo, Texto, Tipo, Notas) " +
				   "VALUES " +
				   "   (@Codigo, @Texto, @Tipo, @Notas);";
		}


		public static string GetUpdateQuery() {
			return "UPDATE Incidencias " +
				   "SET Codigo=@Codigo, Texto=@Texto, Tipo=@Tipo, Notas=@Notas " +
				   "WHERE _id=@Id;";
		}


		public static string GetDeleteQuery() {
			return "DELETE FROM Incidencias " +
				   "WHERE _id=@Id;";
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


		private string texto;
		public string Texto {
			get { return texto; }
			set {
				if (texto != value) {
					texto = value;
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
