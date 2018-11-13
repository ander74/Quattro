namespace Quattroid.Converters
{
	using System;
	using System.Globalization;
	using Xamarin.Forms;

	public class ColorDecimalConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is decimal numero)
			{
				if (numero < 0) return Color.Red;
				if (numero > 0) return Color.Green;
			}
			return Color.Black;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
