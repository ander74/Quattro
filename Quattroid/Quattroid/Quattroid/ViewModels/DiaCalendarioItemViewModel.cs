namespace Quattroid.ViewModels
{
	using System;
	using System.Windows.Input;
	using GalaSoft.MvvmLight.Command;
	using Quattro.Common;
	using Quattro.Models;
	using Xamarin.Forms;

	public class DiaCalendarioItemViewModel : DiaCalendario
    {

		// ====================================================================================================
		#region CONSTRUCTOR
		// ====================================================================================================

		public DiaCalendarioItemViewModel() : base()
		{
			PropertyChanged += (sender, e) => {
				switch (e.PropertyName)
				{
					case nameof(MatriculaRelevo):
					case nameof(Relevo):
					case nameof(MatriculaSusti):
					case nameof(Susti):
						OnPropertyChanged("TextoRelevo");
						break;
					case nameof(CodigoIncidencia):
						OnPropertyChanged("TextoServicio");
						OnPropertyChanged("NosHacen");
						OnPropertyChanged("Hacemos");
						OnPropertyChanged("OpacidadHoras");
						break;
					case nameof(Notas):
						OnPropertyChanged("HayNotas");
						break;
				}
			};
			JornadaChanged += (sender, e) => OnPropertyChanged("Horario");
			FirmaChanged += (sender, e) => OnPropertyChanged("TextoServicio");
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		public (DateTime, bool) FechaFestivo
		{
			get => (Fecha, EsFestivo);
		}


		public (Tiempo, Tiempo) Horario
		{
			get => (Inicio, Final);
		}


		public string TextoRelevo
		{
			get
			{
				if (CodigoIncidencia == 7 || CodigoIncidencia == 8)
				{
					if (MatriculaSusti > 0 && Susti != null) return $"{Susti.Matricula}: {Susti?.Apellidos}";
				} 
				else
				{
					if (MatriculaRelevo > 0 && Relevo != null) return $"{Relevo.Matricula}: {Relevo?.Apellidos}";
				}
				return "";
			}
		}


		public string TextoServicio
		{
			get
			{
				//TODO: Extraer de Quattroid 1.6
				if (Incidencia?.Tipo == 1 || Incidencia?.Tipo == 2)
					return $"{Servicio}/{Turno} - {NumeroLinea}: {TextoLinea}";
				else
					return Incidencia?.TextoIncidencia;
			}
		}


		public string DiaSemana
		{
			get
			{
				DiaSemAbrev dia = (DiaSemAbrev)Fecha.DayOfWeek;
				return dia.ToString();
			}
		}


		public bool NosHacen
		{
			get => CodigoIncidencia == 7; //TODO: Cambiar por el codigo correcto.
		}


		public bool Hacemos
		{
			get => CodigoIncidencia == 8; //TODO: Cambiar por el codigo correcto.
		}


		public bool HayNotas
		{
			get => !string.IsNullOrEmpty(Notas?.Trim());
		}


		public int OpacidadHoras
		{
			get
			{
				//TODO: Extraer de Quattroid 1.6
				if (Incidencia?.Tipo == 1 || Incidencia?.Tipo == 2)
					return 1;
				else
					return 0;
			}
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region COMANDOS
		// ====================================================================================================


		#endregion
		// ====================================================================================================



		// ====================================================================================================
		#region COMANDOS TEMPORALES DE CONTROL
		// ====================================================================================================

		public ICommand GotoDiaCommand
		{
			get { return new RelayCommand(GotoDia); }
		}


		private void GotoDia()
		{
			this.Servicio = $"{this.Fecha.Day}";
			Turno = 2;
			NumeroLinea = "3144";
			TextoLinea = "Bilbao - Cruces - Barakaldo";
			CodigoIncidencia = 1;
			Incidencia = new Incidencia { CodigoIncidencia = 1, Tipo = 1 };
			Inicio = new Tiempo(6, 10);
			Final = new Tiempo(13, 45);
			Acumuladas = Convert.ToDecimal((Final - Inicio - new Tiempo(6, 45)).TotalHoras);
			Nocturnas = 0.33m;
			MatriculaRelevo = 4935;
			if (Relevo == null) Relevo = new Compañero { Matricula = MatriculaRelevo,
														 Apellidos = "Moyano Reyero"
													   };
			OnPropertyChanged("Relevo");
			MatriculaSusti = 5060;
			if (Susti == null) Susti = new Compañero { Matricula = MatriculaRelevo };
			Susti.Apellidos = "Herrero Módenes";
			Desayuno = true;
			Comida = true;
			Cena = true;
			Notas = "Hay notas";
		}


		public ICommand FranqueoCommand
		{
			get { return new RelayCommand(Franqueo); }
		}

		private void Franqueo()
		{
			EsFranqueo = !EsFranqueo;
			CodigoIncidencia = 3;
			Incidencia = new Incidencia() { TextoIncidencia = "Franqueo", Tipo = 3 };
		}


		public ICommand FestivoCommand
		{
			get { return new RelayCommand(Festivo); }
		}

		private void Festivo()
		{
			EsFestivo = !EsFestivo;
			OnPropertyChanged("FechaFestivo");
			Acumuladas = -0.30m;
		}

		#endregion
		// ====================================================================================================


	}
}
