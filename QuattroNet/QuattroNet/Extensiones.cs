#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;

namespace QuattroNet {


	public static class Extensiones {


		// ====================================================================================================
		#region MÉTODOS DE EXTENSIÓN PARA TIMESPAN y TIMESPAN?
		// ====================================================================================================

		/// <summary>
		/// Devuelve la hora en formato texto (hh:mm). Si es una hora negativa, el formato es (-hh:mm).
		/// Si es más de 24 horas, los días se convierten en horas (28:45).
		/// </summary>
		/// <param name="hora">Hora que se desea devolver.</param>
		/// <returns>Hora en formato hh:mm</returns>
		public static string ToTexto(this TimeSpan hora) {
			int horas = (int)Math.Truncate(hora.TotalHours);
			int minutos = hora.Minutes;
			if (minutos < 0) minutos *= -1;
			return horas.ToString("00") + ":" + minutos.ToString("00");
		}


		/// <summary>
		/// Devuelve la hora en formato texto (hh:mm). Si es una hora negativa, el formato es (-hh:mm).
		/// Si es más de 24 horas, los días se convierten en horas (28:45).
		/// </summary>
		/// <param name="hora">Hora que se desea devolver.</param>
		/// <returns>Hora en formato hh:mm</returns>
		public static string ToTexto(this TimeSpan? hora) {
			if (!hora.HasValue) return "";
			int horas = (int)Math.Truncate(hora.Value.TotalHours);
			int minutos = hora.Value.Minutes;
			if (minutos < 0) minutos *= -1;
			return horas.ToString("00") + ":" + minutos.ToString("00");
		}


		/// <summary>
		/// Devuelve el valor decimal de las horas de un timespan redondeado a cuatro decimales.
		/// </summary>
		/// <param name="hora">Hora a devolver en decimal</param>
		/// <returns>Decimal con las horas que representan el timespan. Se redondea a cuatro decimales.</returns>
		public static decimal ToDecimal(this TimeSpan hora, int decimales = 4) {
			return Decimal.Round((decimal)(hora.TotalHours), decimales);
		}


		/// <summary>
		/// Devuelve el valor decimal de las horas de un timespan redondeado a cuatro decimales.
		/// </summary>
		/// <param name="hora">Hora a devolver en decimal</param>
		/// <returns>Decimal con las horas que representan el timespan. Se redondea a cuatro decimales.</returns>
		public static decimal ToDecimal(this TimeSpan? hora, int decimales = 4) {
			if (!hora.HasValue) return 0m;
			return Decimal.Round((decimal)(hora.Value.TotalHours), decimales);
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS DE EXTENSIÓN PARA ...
		// ====================================================================================================



		#endregion
		// ====================================================================================================



	}
}
