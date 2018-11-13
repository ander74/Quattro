namespace Quattroid.Converters
{
	using System;
	using System.Globalization;
	using Quattro.Common;
	using Xamarin.Forms;

	public class HorarioConverter : IValueConverter
	{
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
		{
			if (value is ValueTuple<Tiempo, Tiempo> horario && horario.Item1 != null && horario.Item2 != null)
			{
				return $"{horario.Item1.ToString()} - {horario.Item2.ToString()}";
			}
			return "";
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
		{
			throw new NotImplementedException();
		}
	}
}
