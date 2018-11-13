namespace Quattroid.Converters
{
	using System;
	using System.Collections.Generic;
	using System.Globalization;
	using System.Text;
	using Xamarin.Forms;

	/// <summary>
	/// Convierte una fecha pasada en un color, en función de si la fecha apunta a un día laborable (negro),
	/// un sábado (azul) o un festivo (rojo).
	/// </summary>
	public class DayColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is ValueTuple<DateTime, bool> valor)
			{
				if (valor.Item2) return Color.Red;
				if (valor.Item1.DayOfWeek == DayOfWeek.Saturday) return Color.Blue;
				if (valor.Item1.DayOfWeek == DayOfWeek.Sunday) return Color.Red;
			}
			return Color.Black;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
