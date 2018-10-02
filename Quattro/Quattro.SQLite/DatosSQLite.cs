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

namespace Quattro.SQLite {


	public class DatosSQLite {


		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public DatosSQLite(string cadenaConexion) {
			this.cadenaConexion = cadenaConexion;
			DiasCalendario = new DiasCalendarioSQLite(cadenaConexion);
			Lineas = new LineasSQLite(cadenaConexion);
			Incidencias = new IncidenciasSQLite(cadenaConexion);
			HorasAjenas = new HorasAjenasSQLite(cadenaConexion);
			Compañeros = new CompañerosSQLite(cadenaConexion);
		}

		#endregion
		// ====================================================================================================



		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================


		private string cadenaConexion;
		public string CadenaConexion {
			get { return cadenaConexion; }
			set {
				if (cadenaConexion != value) {
					cadenaConexion = value;
					DiasCalendario.CadenaConexion = cadenaConexion;
					Lineas.CadenaConexion = cadenaConexion;
					Incidencias.CadenaConexion = cadenaConexion;
					HorasAjenas.CadenaConexion = cadenaConexion;
					Compañeros.CadenaConexion = cadenaConexion;
				}
			}
		}


		public DiasCalendarioSQLite DiasCalendario { get; set; }

		public LineasSQLite Lineas { get; set; }

		public IncidenciasSQLite Incidencias { get; set; }

		public HorasAjenasSQLite HorasAjenas { get; set; }

		public CompañerosSQLite Compañeros { get; set; }



		#endregion
		// ====================================================================================================


	}
}
