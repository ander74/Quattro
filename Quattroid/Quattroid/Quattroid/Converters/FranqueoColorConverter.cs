namespace Quattroid.Converters
{
	using System;
	using System.Globalization;
	using Xamarin.Forms;

	/// <summary>
	/// Devuelve un color basándose en si el día está marcado como franqueo o no.
	/// </summary>
	class FranqueoColorConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is bool esFranqueo && esFranqueo) return Constansts.FRANQUEO_COLOR;
				//if (fechaFranqueo.Item1.Day % 2 == 0) return Constansts.ALTERNATE_COLOR;
			return Color.Transparent;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
