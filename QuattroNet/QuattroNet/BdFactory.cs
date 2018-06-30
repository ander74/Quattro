#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System.Data.Common;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.SQLite;


namespace QuattroNet {

	public enum SqlProviders {
		SQLite, Access, SqlServer
	}

    public static class BdFactory {

		public static DbConnection GetConnection(SqlProviders provider) {
			switch (provider) {
				case SqlProviders.SQLite:
					return new SQLiteConnection();
				case SqlProviders.Access:
					return new OleDbConnection();
				case SqlProviders.SqlServer:
					return new SqlConnection();
				default:
					return new SQLiteConnection();
			}
		}

    }
}
