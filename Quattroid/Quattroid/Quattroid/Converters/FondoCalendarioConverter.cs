namespace Quattroid.Converters
{
	using System;
	using System.Globalization;
	using Xamarin.Forms;

	/// <summary>
	/// Devuelve un color basándose en si el día está marcado como franqueo o no.
	/// </summary>
	public class FondoCalendarioConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is DateTime fecha)
			{
				if (fecha.Day % 2 == 0) return Constansts.ALTERNATE_COLOR;
			}
			return Color.White;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
