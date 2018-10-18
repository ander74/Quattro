namespace Quattroid.ViewModels
{
	using System;
	using System.Windows.Input;
	using GalaSoft.MvvmLight.Command;
	using Quattro.Models;

	public class DiaCalendarioItemViewModel : DiaCalendario
    {


		public ICommand GotoDiaCommand
		{
			get { return new RelayCommand(GotoDia); }
		}

		private void GotoDia()
		{
			this.Servicio = $"{this.Fecha.Day} / 1 - 2314: Plaza Circular";
		}
	}
}
