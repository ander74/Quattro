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

	public enum DataTypeSQL {
		NULL, INTEGER, REAL, TEXT, BLOB
	}


	public class CampoSQL {


		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public CampoSQL() {
			Default = null;
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public string DefinicionCrear() {
			string definicion = $"{Nombre} {Tipo.ToString()}";
			if (PrimaryKey) definicion += " PRIMARY KEY";
			if (Autoincrement) definicion += " AUTOINCREMENT";
			if (Unique) definicion += " UNIQUE";
			if (NotNull) definicion += " NOT NULL";
			switch (Tipo) {
				case DataTypeSQL.INTEGER:
				case DataTypeSQL.REAL:
					if (Default != null) {
						if (!String.IsNullOrEmpty(Default)) definicion += $" DEFAULT ({Default})";
					}
					break;
				case DataTypeSQL.TEXT:
					if (Default != null) definicion += $" DEFAULT ('{Default}')";
					break;
			}
			return definicion;
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		public string Nombre { get; set; }

		public DataTypeSQL Tipo { get; set; }

		public bool PrimaryKey { get; set; }

		public bool Autoincrement { get; set; }

		public bool Unique { get; set; }

		public bool NotNull { get; set; }

		public string Default { get; set; }


		#endregion
		// ====================================================================================================



	}



}
