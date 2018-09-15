#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using System.Data.Common;

namespace Quattro.Common {


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


		/// <summary>
		/// Devuelve los Ticks de la hora de un TimeSpan? o DbNull si no contiene un valor (es nulo).
		/// </summary>
		/// <param name="hora">Hora de la que devolver los ticks</param>
		/// <returns>Número de ticks que representa el TimeSpan? o DbNull, si no contiene un valor.</returns>
		public static object ToTicksOrDbNull(this TimeSpan? hora) {
			if (!hora.HasValue) return DBNull.Value;
			return hora.Value.Ticks;
		}



		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS DE EXTENSION PARA OLEDBDATAREADER
		// ====================================================================================================

		/// <summary>
		/// Extrae un campo de tipo TimeSpan del lector.
		/// </summary>
		/// <param name="lector">Lector del que se extraerá el campo.</param>
		/// <param name="campo">Campo que se va a extraer</param>
		/// <returns>Un TimeSpan con el valor almacenado en el campo.</returns>
		public static TimeSpan ToTimeSpan(this DbDataReader lector, string campo) {
			if (lector == null || lector[campo] is DBNull) return TimeSpan.Zero;
			return TimeSpan.FromTicks(Convert.ToInt64(lector[campo]));
		}


		/// <summary>
		/// Extrae un campo de tipo TimeSpan? del lector.
		/// </summary>
		/// <param name="lector">Lector del que se extraerá el campo.</param>
		/// <param name="campo">Campo que se va a extraer</param>
		/// <returns>Un TimeSpan? con el valor almacenado en el campo.</returns>
		public static TimeSpan? ToTimeSpanNulable(this DbDataReader lector, string campo) {
			if (lector == null || lector[campo] is DBNull) return null;
			return TimeSpan.FromTicks(Convert.ToInt64(lector[campo]));
		}


		/// <summary>
		/// Extrae un campo de tipo String del lector.
		/// </summary>
		/// <param name="lector">Lector del que se extraerá el campo.</param>
		/// <param name="campo">Campo que se va a extraer</param>
		/// <returns>Un String con el valor almacenado en el campo.</returns>
		public static string ToString(this DbDataReader lector, string campo) {
			if (lector == null || lector[campo] is DBNull) return null;
			return Convert.ToString(lector[campo]);
		}


		/// <summary>
		/// Extrae un campo de tipo Bool del lector.
		/// </summary>
		/// <param name="lector">Lector del que se extraerá el campo.</param>
		/// <param name="campo">Campo que se va a extraer</param>
		/// <returns>Un Bool con el valor almacenado en el campo.</returns>
		public static bool ToBool(this DbDataReader lector, string campo) {
			if (lector == null || lector[campo] is DBNull) return false;
			return Convert.ToBoolean(lector[campo]);
		}


		/// <summary>
		/// Extrae un campo de tipo DateTime del lector.
		/// </summary>
		/// <param name="lector">Lector del que se extraerá el campo.</param>
		/// <param name="campo">Campo que se va a extraer</param>
		/// <returns>Un DateTime con el valor almacenado en el campo.</returns>
		public static DateTime ToDateTime(this DbDataReader lector, string campo) {
			if (lector == null || lector[campo] is DBNull) return new DateTime(0);
			return Convert.ToDateTime(lector[campo]);
		}


		/// <summary>
		/// Extrae un campo de tipo DateTime? del lector.
		/// </summary>
		/// <param name="lector">Lector del que se extraerá el campo.</param>
		/// <param name="campo">Campo que se va a extraer</param>
		/// <returns>Un DateTime? con el valor almacenado en el campo.</returns>
		public static DateTime? ToDateTimeNulable(this DbDataReader lector, string campo) {
			if (lector == null || lector[campo] is DBNull) return null;
			return Convert.ToDateTime(lector[campo]);
		}


		/// <summary>
		/// Extrae un campo de tipo Byte del lector.
		/// </summary>
		/// <param name="lector">Lector del que se extraerá el campo.</param>
		/// <param name="campo">Campo que se va a extraer</param>
		/// <returns>Un Byte con el valor almacenado en el campo.</returns>
		public static byte ToByte(this DbDataReader lector, string campo) {
			if (lector == null || lector[campo] is DBNull) return 0;
			return Convert.ToByte(lector[campo]);
		}


		/// <summary>
		/// Extrae un campo de tipo Single del lector.
		/// </summary>
		/// <param name="lector">Lector del que se extraerá el campo.</param>
		/// <param name="campo">Campo que se va a extraer</param>
		/// <returns>Un Single con el valor almacenado en el campo.</returns>
		public static Single ToSingle(this DbDataReader lector, string campo) {
			if (lector == null || lector[campo] is DBNull) return 0;
			return Convert.ToSingle(lector[campo]);
		}


		/// <summary>
		/// Extrae un campo de tipo Int16 del lector.
		/// </summary>
		/// <param name="lector">Lector del que se extraerá el campo.</param>
		/// <param name="campo">Campo que se va a extraer</param>
		/// <returns>Un Int16 con el valor almacenado en el campo.</returns>
		public static Int16 ToInt16(this DbDataReader lector, string campo) {
			if (lector == null || lector[campo] is DBNull) return 0;
			return Convert.ToInt16(lector[campo]);
		}


		/// <summary>
		/// Extrae un campo de tipo Int32(int) del lector.
		/// </summary>
		/// <param name="lector">Lector del que se extraerá el campo.</param>
		/// <param name="campo">Campo que se va a extraer</param>
		/// <returns>Un Int32(int) con el valor almacenado en el campo.</returns>
		public static Int32 ToInt32(this DbDataReader lector, string campo) {
			if (lector == null || lector[campo] is DBNull) return 0;
			return Convert.ToInt32(lector[campo]);
		}


		/// <summary>
		/// Extrae un campo de tipo Int64(long) del lector.
		/// </summary>
		/// <param name="lector">Lector del que se extraerá el campo.</param>
		/// <param name="campo">Campo que se va a extraer</param>
		/// <returns>Un Int64(long) con el valor almacenado en el campo.</returns>
		public static Int64 ToInt64(this DbDataReader lector, string campo) {
			if (lector == null || lector[campo] is DBNull) return 0;
			return Convert.ToInt64(lector[campo]);
		}


		/// <summary>
		/// Extrae un campo de tipo Double del lector.
		/// </summary>
		/// <param name="lector">Lector del que se extraerá el campo.</param>
		/// <param name="campo">Campo que se va a extraer</param>
		/// <returns>Un Double con el valor almacenado en el campo.</returns>
		public static double ToDouble(this DbDataReader lector, string campo) {
			if (lector == null || lector[campo] is DBNull) return 0;
			return Convert.ToDouble(lector[campo]);
		}


		/// <summary>
		/// Extrae un campo de tipo Decimal del lector.
		/// </summary>
		/// <param name="lector">Lector del que se extraerá el campo.</param>
		/// <param name="campo">Campo que se va a extraer</param>
		/// <returns>Un Decimal con el valor almacenado en el campo.</returns>
		public static decimal ToDecimal(this DbDataReader lector, string campo) {
			if (lector == null || lector[campo] is DBNull) return 0;
			return Convert.ToDecimal(lector[campo]);
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
