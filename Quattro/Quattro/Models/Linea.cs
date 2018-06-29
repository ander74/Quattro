#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;
using QuattroNet;

namespace Quattro.Models {

    public class Linea : NotifyBase {


		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public Linea () { }




		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public void FromReader(DbDataReader lector) {
			id = lector.ToInt32("_id");
			numero = lector.ToString("Numero");
			texto = lector.ToString("Texto");
			notas = lector.ToString("Notas");
		}


		public void ToCommand(ref DbCommand comando) {
			// Numero
			DbParameter parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Numero";
			parametro.Value = numero;
			// Texto
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Texto";
			parametro.Value = texto;
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

		public override bool Equals(object obj) {
			var linea = obj as Linea;
			if (linea == null) return false;
			return Numero == linea.Numero;
		}


		public override int GetHashCode() {
			unchecked {
				int hash = 5060;
				hash = hash * numero?.GetHashCode() ?? 1234;
				return hash;
			}
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS ESTÁTICOS
		// ====================================================================================================

		public static string GetInsertQuery() {
			return "INSERT INTO Lineas " +
				   "   (Numero, Texto, Notas) " +
				   "VALUES " +
				   "   (@Numero, @Texto, @Notas);";
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


		private string numero;
		public string Numero {
			get { return numero; }
			set {
				if (numero != value) {
					numero = value;
					PropiedadCambiada();
				}
			}
		}


		private string texto;
		public string Texto {
			get { return texto; }
			set {
				if (texto != value) {
					texto = value;
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
