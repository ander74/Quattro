#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models.Models 
{
	using System.IO;
	using Newtonsoft.Json;
	using Notify;
	using Quattro.Common;

	public class Opciones : NotifyBase {


		// ====================================================================================================
		#region OPCIONES
		// ====================================================================================================


		private int primerMesMostrado = 1;
		public int PrimerMesMostrado {
			get { return primerMesMostrado; }
			set { SetValue(ref primerMesMostrado, value); }
		}


		private int primerAñoMostrado = 2016;
		public int PrimerAñoMostrado {
			get { return primerAñoMostrado; }
			set { SetValue(ref primerAñoMostrado, value); }
		}


		private decimal horasAnteriores;
		public decimal HorasAnteriores {
			get { return horasAnteriores; }
			set { SetValue(ref horasAnteriores, value); }
		}


		private int relevoFino;
		public int RelevoFijo {
			get { return relevoFino; }
			set { SetValue(ref relevoFino, value); }
		}


		private bool autoRellenarSemana;
		public bool AutoRellenarSemana {
			get { return autoRellenarSemana; }
			set { SetValue(ref autoRellenarSemana, value); }
		}


		private bool rellenarJornadaAnual;
		public bool RellenarJornadaAnual {
			get { return rellenarJornadaAnual; }
			set { SetValue(ref rellenarJornadaAnual, value); }
		}


		private bool regularAñosBisiestos;
		public bool RegularAñosBisiestos {
			get { return regularAñosBisiestos; }
			set { SetValue(ref regularAñosBisiestos, value); }
		}


		private bool iniciarMesActual;
		public bool IniciarMesActual {
			get { return iniciarMesActual; }
			set { SetValue(ref iniciarMesActual, value); }
		}


		private bool iniciarEnCalendario;
		public bool IniciarEnCalendario {
			get { return iniciarEnCalendario; }
			set { SetValue(ref iniciarEnCalendario, value); }
		}


		private bool acumularTomaDeje;
		public bool AcumularTomaDeje {
			get { return acumularTomaDeje; }
			set { SetValue(ref acumularTomaDeje, value); }
		}


		private bool activarTecladoNumerico;
		public bool ActivarTecladoNumerico {
			get { return activarTecladoNumerico; }
			set { SetValue(ref activarTecladoNumerico, value); }
		}


		private bool pdfHorizontal;
		public bool PdfHorizontal {
			get { return pdfHorizontal; }
			set { SetValue(ref pdfHorizontal, value); }
		}


		private bool incluirServicios;
		public bool IncluirServicios {
			get { return incluirServicios; }
			set { SetValue(ref incluirServicios, value); }
		}


		private bool incluirNotas;
		public bool IncluirNotas {
			get { return incluirNotas; }
			set { SetValue(ref incluirNotas, value); }
		}


		private bool agruparNotas;
		public bool AgruparNotas {
			get { return agruparNotas; }
			set { SetValue(ref agruparNotas, value); }
		}




		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public void Guardar(string ruta) {

			string datos = JsonConvert.SerializeObject(this, Formatting.Indented);
			File.WriteAllText(ruta, datos);
		}

		public void Cargar(string ruta) {

			if (File.Exists(ruta)) {
				string datos = File.ReadAllText(ruta);
				JsonConvert.PopulateObject(datos, this);
				OnPropertyChanged("");
			}
		}

		#endregion
		// ====================================================================================================


	}
}
