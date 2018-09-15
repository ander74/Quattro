#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using System.Collections.Generic;
using System.Text;

namespace Quattro.Tools {

	class TablaSQL {

		// ====================================================================================================
		#region CONSTRUCTOR
		// ====================================================================================================

		public TablaSQL(string nombreTabla) {
			Nombre = nombreTabla;
			Campos = new List<CampoSQL>();
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public string ComandoCrearTabla() {

			if (Campos.Count == 0) return "";
	
			string comando = $"CREATE TABLE {Nombre} (";

			foreach (CampoSQL campo in Campos) {
				comando += $"{campo.DefinicionCrear()}, ";
			}

			comando = comando.Substring(0, comando.Length - 2) + ");";

			return comando;

		}


		public string ComandoInsertar(List<object> valores) {

			if (Campos.Count == 0) return "";

			string comando = $"INSERT INTO {Nombre} (";

			foreach (CampoSQL campo in Campos) {
				comando += $"{campo.Nombre}, ";
			}

			comando = comando.Substring(0, comando.Length - 2);
			comando += ") VALUES (";

			foreach (CampoSQL campo in Campos) {
				comando += $"@{campo.Nombre}, ";
			}
			comando = comando.Substring(0, comando.Length - 2) + ");";

			return comando;

		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		public string Nombre { get; set; }

		public List<CampoSQL> Campos { get; set; }


		#endregion
		// ====================================================================================================


	}
}
