#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System.Data.Common;
using QuattroNet;

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

		public void FromReader(DbDataReader lector) {
			id = lector.ToInt32("_id");
			matricula = lector.ToInt32("Matricula");
			nombre = lector.ToString("Nombre");
			apellidos = lector.ToString("Apellidos");
			telefono = lector.ToString("Telefono");
			clasificacion = lector.ToInt32("Clasificacion");
			deuda = lector.ToInt32("Deuda");
			notas = lector.ToString("Notas");
		}


		public void ToCommand(ref DbCommand comando) {
			// Matricula
			DbParameter parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Matricula";
			parametro.Value = matricula;
			// Nombre
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Nombre";
			parametro.Value = nombre;
			// Apellidos
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Apellidos";
			parametro.Value = apellidos;
			// Telefono
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Telefono";
			parametro.Value = telefono;
			// Clasificacion
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Clasificacion";
			parametro.Value = clasificacion;
			// Deuda
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Deuda";
			parametro.Value = deuda;
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
		#region MÉTODOS ESTÁTICOS
		// ====================================================================================================

		public static string GetSelectQuery() {
			return "SELECT * " +
				   "FROM Compañeros " +
				   "ORDER BY Matricula;";
		}


		public static string GetInsertQuery() {
			return "INSERT INTO Compañeros " +
				   "   (Matricula, Nombre, Apellidos, Telefono, Clasificacion, Deuda, Notas) " +
				   "VALUES " +
				   "   (@Matricula, @Nombre, @Apellidos, @Telefono, @Clasificacion, @Deuda, @Notas);";
		}


		public static string GetUpdateQuery() {
			return "UPDATE Compañeros " +
				   "SET Matricula=@Matricula, Nombre=@Nombre, Apellidos=@Apellidos, Telefono=@Telefono, " +
				   "Clasificacion=@Clasificacion, Deuda=@Deuda, Notas=@Notas " +
				   "WHERE _id=@Id;";
		}


		public static string GetDeleteQuery() {
			return "DELETE FROM Compañeros " +
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
					PropiedadCambiada();
				}
			}
		}


		private int matricula;
		public int Matricula {
			get { return matricula; }
			set {
				if (matricula != value) {
					matricula = value;
					PropiedadCambiada();
				}
			}
		}


		private string nombre;
		public string Nombre {
			get { return nombre; }
			set {
				if (nombre != value) {
					nombre = value;
					PropiedadCambiada();
				}
			}
		}


		private string apellidos;
		public string Apellidos {
			get { return apellidos; }
			set {
				if (apellidos != value) {
					apellidos = value;
					PropiedadCambiada();
				}
			}
		}


		private string telefono;
		public string Telefono {
			get { return telefono; }
			set {
				if (telefono != value) {
					telefono = value;
					PropiedadCambiada();
				}
			}
		}


		private int clasificacion;
		public int Clasificacion {
			get { return clasificacion; }
			set {
				if (clasificacion != value) {
					clasificacion = value;
					PropiedadCambiada();
				}
			}
		}


		private int deuda;
		public int Deuda {
			get { return deuda; }
			set {
				if (deuda != value) {
					deuda = value;
					PropiedadCambiada();
				}
			}
		}


		private string notas;
		public string Notas {
			get { return notas; }
			set {
				if (notas != value) {
					notas = value;
					PropiedadCambiada();
				}
			}
		}



		#endregion
		// ====================================================================================================



	}
}
