namespace Quattroid.ViewModels
{
	using System;
	using System.Collections.ObjectModel;
	using Quattro.Models;
	using Quattro.Notify;
	using Quattro.Services;
	using Interfaces;
	using Xamarin.Forms;
	using System.Linq;
	using System.Windows.Input;
	using Microsoft.EntityFrameworkCore;
	using System.IO;
	using GalaSoft.MvvmLight.Command;
	using System.Collections.Generic;

	public class CalendarioViewModel: EntityNotifyBase
    {

		// ====================================================================================================
		#region CAMPOS PRIVADOS
		// ====================================================================================================

		ObservableCollection<DiaCalendarioItemViewModel> calendario;

		//string archivoDB = @"\\Mac\Compartida\QuattroDB.db"; //Temporal.
		string archivoDB = string.Empty;

		int control = 0;

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region CONSTRUCTOR
		// ====================================================================================================

		public CalendarioViewModel()
		{
			archivoDB = DependencyService.Get<IPathService>().GetFilePath();
			using (QuattroContext context = new QuattroContext(archivoDB))
			{
				context.Database.Migrate();
				var lista = context.Calendario.AsNoTracking().ToList();
				if (!lista.Any())
				{
					var cal = new List<DiaCalendario>();
					for (int mes = 1; mes <= 12; mes++) {
						for (int dia = 1; dia <= DateTime.DaysInMonth(2018, mes); dia++)
							cal.Add(new DiaCalendario { Fecha = new DateTime(2018, mes, dia) });
					}
					context.Calendario.AddRange(cal);
					context.SaveChanges();
				}
			}
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		public ObservableCollection<DiaCalendarioItemViewModel> Calendario
		{
			get { return this.calendario; }
			set { this.SetValue(ref this.calendario, value); }
		}


		public bool IsRefreshing { get; set; }


		//private ObservableCollection<ListaAgrupada<DiaCalendario>> listaCalendarios;
		//public ObservableCollection<ListaAgrupada<DiaCalendario>> ListaCalendarios
		//{
		//	get { return this.listaCalendarios; }
		//	set { this.SetValue(ref this.listaCalendarios, value); }
		//}

		private ObservableCollection<ListaAgrupada> listaCalendarios;
		public ObservableCollection<ListaAgrupada> ListaCalendarios
		{
			get { return this.listaCalendarios; }
			set { this.SetValue(ref this.listaCalendarios, value); }
		}




		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region COMANDOS
		// ====================================================================================================

		public ICommand CargarCommand
		{
			get { return new RelayCommand(Cargar); }
		}


		private void Cargar2()
		{
			using (QuattroContext context = new QuattroContext(archivoDB))
			{
				var lista = from dia in context.Calendario
							orderby dia.Fecha
							group dia by dia.Fecha.Month into grupo
							select new ListaAgrupada(grupo.ToList());

				ListaCalendarios = new ObservableCollection<ListaAgrupada>(lista);
			}
		}


		private void Cargar()
		{
			using (QuattroContext context = new QuattroContext(archivoDB))
			{
				if (control == 0)
				{
					var lista = QuattroService.GetInstance().GetCalendariosPorMesAsync(2018, 10, archivoDB).Result.Select(
						c => new DiaCalendarioItemViewModel { Fecha = c.Fecha, Servicio = c.Servicio }).ToList();
					//var lista = context.Calendario.Where(c => c.Fecha.Year == 2018 && c.Fecha.Month == 9)
					//							  .Select(c => new DiaCalendarioItemViewModel { Fecha = c.Fecha })
					//							  .OrderBy(c => c.Fecha).ToList();
					Calendario = new ObservableCollection<DiaCalendarioItemViewModel>(lista);
					control = 1;
				} else if (control == 1)
				{
					var lista = context.Calendario.Where(c => c.Fecha.Year == 2018 && c.Fecha.Month == 10).OrderBy(c => c.Fecha).ToList();
					Calendario = new ObservableCollection<DiaCalendarioItemViewModel>();
					foreach (var dia in lista)
					{
						var d = new DiaCalendarioItemViewModel();
						d.Fecha = dia.Fecha;
						d.Servicio = dia.Servicio;
						Calendario.Add(d);
					}
					control = 2;
				} else if (control == 2)
				{
					var lista = context.Calendario.Where(c => c.Fecha.Year == 2018 && c.Fecha.Month == 11).OrderBy(c => c.Fecha).ToList();
					Calendario = new ObservableCollection<DiaCalendarioItemViewModel>();
					foreach (var dia in lista)
					{
						var d = new DiaCalendarioItemViewModel();
						d.Fecha = dia.Fecha;
						Calendario.Add(d);
					}
					control = 3;
				} else if (control == 3)
				{
					var lista = context.Calendario.Where(c => c.Fecha.Year == 2018 && c.Fecha.Month == 12).OrderBy(c => c.Fecha).ToList();
					Calendario = new ObservableCollection<DiaCalendarioItemViewModel>();
					foreach (var dia in lista)
					{
						var d = new DiaCalendarioItemViewModel();
						d.Fecha = dia.Fecha;
						Calendario.Add(d);
					}
					control = 0;
				}
			}
		}

		#endregion
		// ====================================================================================================





	}

	public class GrupoCalendarios : List<DiaCalendario>
	{
		public string Key { get; set; }

		public List<DiaCalendario> Calendarios => this;
	}


	public class Grouping<K, T> : ObservableCollection<T>
	{
		public K Key { get; private set; }

		public Grouping(K key, IEnumerable<T> items)
		{
			Key = key;
			foreach (var item in items)
				this.Items.Add(item);
		}
	}

	public class ListaAgrupada : ObservableCollection<DiaCalendarioItemViewModel>
	{
		public string Key { get; private set; }

		public ListaAgrupada(IEnumerable<DiaCalendario> lista)
		{
			if (lista.Count() > 0)
			Key = $"{lista.ElementAt(0).Fecha:MMMM} - {lista.ElementAt(0).Fecha.Year}".ToUpper();
			foreach (var dia in lista)
			{
				DiaCalendarioItemViewModel d = dia as DiaCalendarioItemViewModel;
				this.Items.Add(d);
			}
		}
	}


}
