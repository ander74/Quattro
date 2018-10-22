#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Common
{
	using System;
	using System.Globalization;
	using Newtonsoft.Json;

	/// <summary>
	/// Representa un intervalo de tiempo compuesto de días, horas y minutos.
	/// </summary>
	public class Tiempo : IComparable, IComparable<Tiempo>, IEquatable<Tiempo>, IFormattable
	{

		// ====================================================================================================
		#region CAMPOS
		// ====================================================================================================

		/// <summary>
		/// Cantidad de minutos en una hora.
		/// </summary>
		public const int MinutosPorHora = 60;

		/// <summary>
		/// Cantidad de minutos en un día.
		/// </summary>
		public const int MinutosPorDia = 1440;

		/// <summary>
		/// Objeto Tiempo con el máximo valor que puede albergar.
		/// </summary>
		public static Tiempo MaxValue = new Tiempo(Int32.MaxValue);

		/// <summary>
		/// Objeto Tiempo con el mínimo valor que puede albergar.
		/// </summary>
		public static Tiempo MinValue = new Tiempo(Int32.MinValue);

		/// <summary>
		/// Objeto Tiempo de valor cero.
		/// </summary>
		public static Tiempo Zero = new Tiempo(0);


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		/// <summary>
		/// Instancia un objeto Tiempo con un número de minutos iniciales.
		/// </summary>
		/// <param name="minutos">Número de minutos iniciales.</param>
		[JsonConstructor]
		public Tiempo(int minutos) => TotalMinutos = minutos;


		/// <summary>
		/// Instancia un objeto Tiempo con unas horas y minutos.
		/// </summary>
		/// <param name="horas">Número de horas iniciales (pueden ser más de 24).</param>
		/// <param name="minutos">Número de minutos iniciales (pueden ser más de 60).</param>
		public Tiempo(int horas, int minutos) => TotalMinutos = minutos + (horas * MinutosPorHora);


		/// <summary>
		/// Instancia un objeto Tiempo con los días, horas y minutos iniciales.
		/// </summary>
		/// <param name="dias">Número de días iniciales.</param>
		/// <param name="horas">Número de horas iniciales (pueden ser más de 24).</param>
		/// <param name="minutos">Número de minutos iniciales (pueden ser más de 60).</param>
		public Tiempo(int dias, int horas, int minutos) => TotalMinutos = minutos + (horas * MinutosPorHora) + (dias * MinutosPorDia);


		/// <summary>
		/// Instancia un objeto Tiempo indicando el intervalo por medio de un objeto TimeSpan.
		/// El intervalo por debajo de un minuto será desechado.
		/// </summary>
		/// <param name="ts">Objeto TimeSpan del que se extraerá el intervalo.</param>
		public Tiempo(TimeSpan ts) => TotalMinutos = (int)ts.TotalMinutes;


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region OPERADORES
		// ====================================================================================================

		/// <summary>
		/// Convierte explicitamente un TimeSpan en un objeto Tiempo.
		/// El intervalo por debajo de un minuto será desechado.
		/// </summary>
		/// <param name="ts">Objeto TimeSpan que será convertido explicitamente.</param>
		public static explicit operator Tiempo(TimeSpan ts) => new Tiempo((int)ts.TotalMinutes);


		/// <summary>
		/// Suma dos objetos Tiempo.
		/// </summary>
		/// <param name="t1">Primer sumando.</param>
		/// <param name="t2">Segundo sumando.</param>
		/// <returns>Un nuevo objeto Tiempo con el resultado de la suma.</returns>
		public static Tiempo operator +(Tiempo t1, Tiempo t2) => new Tiempo((t1?.TotalMinutos ?? 0) + (t2?.TotalMinutos ?? 0));


		/// <summary>
		/// Resta un objeto Tiempo de otro.
		/// </summary>
		/// <param name="t1">Minuendo de la resta.</param>
		/// <param name="t2">Sustraendo de la resta.</param>
		/// <returns>Un nuevo objeto Tiempo con el resultado de la resta.</returns>
		public static Tiempo operator -(Tiempo t1, Tiempo t2) => new Tiempo((t1?.TotalMinutos ?? 0) - (t2?.TotalMinutos ?? 0));


		/// <summary>
		/// Devuelve un Nuevo objeto Tiempo, negando el actual.
		/// </summary>
		/// <param name="t1">Objeto a negar.</param>
		/// <returns>Nuevo objeto Tiempo negado.</returns>
		public static Tiempo operator -(Tiempo t1) => new Tiempo(-t1.TotalMinutos);


		/// <summary>
		/// Determina si dos objetos Tiempo son iguales.
		/// </summary>
		/// <param name="t1">Primer objeto Tiempo.</param>
		/// <param name="t2">Segundo objeto Tiempo.</param>
		/// <returns>True si son iguales. False en caso contrario.</returns>
		public static bool operator ==(Tiempo t1, Tiempo t2) => t1?.TotalMinutos == t2?.TotalMinutos;


		/// <summary>
		/// Determina si dos objetos Tiempo no son iguales.
		/// </summary>
		/// <param name="t1">Primer objeto Tiempo.</param>
		/// <param name="t2">Segundo objeto Tiempo.</param>
		/// <returns>True si no son iguales. False en caso contrario.</returns>
		public static bool operator !=(Tiempo t1, Tiempo t2) => t1?.TotalMinutos != t2?.TotalMinutos;


		/// <summary>
		/// Determina si el primer objeto Tiempo es mayor que el segundo.
		/// </summary>
		/// <param name="t1">Primer objeto Tiempo.</param>
		/// <param name="t2">Segundo objeto Tiempo.</param>
		/// <returns>True si el primero es mayor. False en caso contrario.</returns>
		public static bool operator >(Tiempo t1, Tiempo t2) => t1?.TotalMinutos > t2?.TotalMinutos;


		/// <summary>
		/// Determina si el primer objeto Tiempo es menor que el segundo.
		/// </summary>
		/// <param name="t1">Primer objeto Tiempo.</param>
		/// <param name="t2">Segundo objeto Tiempo.</param>
		/// <returns>True si el primero es menor. False en caso contrario.</returns>
		public static bool operator <(Tiempo t1, Tiempo t2) => t1?.TotalMinutos < t2?.TotalMinutos;


		/// <summary>
		/// Determina si el primer objeto Tiempo es mayor o igual que el segundo.
		/// </summary>
		/// <param name="t1">Primer objeto Tiempo.</param>
		/// <param name="t2">Segundo objeto Tiempo.</param>
		/// <returns>True si el primero es mayor o igual. False en caso contrario.</returns>
		public static bool operator >=(Tiempo t1, Tiempo t2) => t1?.TotalMinutos >= t2?.TotalMinutos;


		/// <summary>
		/// Determina si el primer objeto Tiempo es menor o igual que el segundo.
		/// </summary>
		/// <param name="t1">Primer objeto Tiempo.</param>
		/// <param name="t2">Segundo objeto Tiempo.</param>
		/// <returns>True si el primero es menor igual. False en caso contrario.</returns>
		public static bool operator <=(Tiempo t1, Tiempo t2) => t1?.TotalMinutos <= t2?.TotalMinutos;


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region OPERADORES * y / (Sin efecto temporalmente)
		// ====================================================================================================


		/// <summary>
		/// Multiplica un objeto Tiempo por el valor de un número doble.
		/// </summary>
		/// <param name="t1">Objeto Tiempo a multiplicar.</param>
		/// <param name="d2">Número por el que se va a multiplicar el objeto Tiempo.</param>
		/// <returns>Objeto Tiempo con el resultado de la multiplicación.</returns>
		//public static Tiempo operator *(Tiempo t1, double d2) => new Tiempo((int)(t1?.TotalMinutos * d2));


		/// <summary>
		/// Multiplica un numero doble por un objeto Tiempo.
		/// </summary>
		/// <param name="d1">Número  que se va a multiplicar con el objeto Tiempo.</param>
		/// <param name="t2">Objeto Tiempo con el que se va a multiplicar el número.</param>
		/// <returns>Objeto Tiempo con el resultado de la multiplicación.</returns>
		//public static Tiempo operator *(double d1, Tiempo t2) => new Tiempo((int)(d1 * t2?.TotalMinutos));


		/// <summary>
		/// Divide un objeto Tiempo por otro.
		/// </summary>
		/// <param name="t1">Dividendo.</param>
		/// <param name="t2">Divisor.</param>
		/// <returns>Número que resulta de la división.</returns>
		//public static double operator /(Tiempo t1, Tiempo t2)
		//{
		//	if (t2 == null) return 0;
		//	return t1?.TotalMinutos ?? 0 / (double)t2.TotalMinutos;
		//}


		/// <summary>
		/// DIvide un objeto tiempo entre un número doble.
		/// </summary>
		/// <param name="t1">Dividendo.</param>
		/// <param name="d2">Divisor.</param>
		/// <returns>Un nuevo objeto Tiempo resultante de la división.</returns>
		//public static Tiempo operator /(Tiempo t1, double d2) => new Tiempo((int)(t1.TotalMinutos / d2));


		/// <summary>
		/// Devuelve un NUEVO objeto Tiempo con el actual multiplicado por el número indicado.
		/// </summary>
		/// <param name="factor">Número por el que se va a multiplicar el objeto Tiempo actual.</param>
		/// <returns>NUEVO objeto Tiempo con el resultado de la multiplicación.</returns>
		//public Tiempo Multiply(double factor) => new Tiempo((int)(TotalMinutos * factor));


		/// <summary>
		/// Devuelve un NUEVO objeto Tiempo con el actual dividido por el indicado.
		/// </summary>
		/// <param name="factor">Objeto Tiempo por el que se va a dividir el objeto Tiempo actual.</param>
		/// <returns>NUEVO objeto Tiempo con el resultado de la división.</returns>
		//public double Divide(Tiempo tiempo) => TotalMinutos / (double)(tiempo?.TotalMinutos ?? 0d);


		/// <summary>
		/// Devuelve un NUEVO objeto Tiempo con el actual dividido por el número indicado.
		/// </summary>
		/// <param name="factor">Número por el que se va a dividir el objeto Tiempo actual.</param>
		/// <returns>NUEVO objeto Tiempo con el resultado de la división.</returns>
		//public Tiempo Divide(double divisor) => new Tiempo((int)(TotalMinutos / divisor));


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		/// <summary>
		/// Devuelve un NUEVO objeto tiempo con la suma del actual y el proporcionado.
		/// </summary>
		/// <param name="tiempo">Objeto tiempo que se sumará al actual.</param>
		/// <returns>NUEVO objeto Tiempo con el resultado de la suma.</returns>
		public Tiempo Add(Tiempo tiempo) => new Tiempo(TotalMinutos + (tiempo?.TotalMinutos ?? 0));


		/// <summary>
		/// Devuelve un NUEVO objeto tiempo con la suma del intervalo actual y el proporcionado.
		/// </summary>
		/// <param name="horas">Horas que se añadirán al objeto actual.</param>
		/// <param name="minutos">Minutos que se añadirán al objeto actual.</param>
		/// <returns>NUEVO objeto Tiempo con la suma de los intervalos.</returns>
		public Tiempo Add(int horas, int minutos) => new Tiempo(TotalMinutos + minutos + (horas * MinutosPorHora));


		/// <summary>
		/// Devuelve un NUEVO objeto tiempo con la suma del intervalo actual y el proporcionado.
		/// </summary>
		/// <param name="dias">Días que se añadirán al objeto actual.</param>
		/// <param name="horas">Horas que se añadirán al objeto actual.</param>
		/// <param name="minutos">Minutos que se añadirán al objeto actual.</param>
		/// <returns>NUEVO objeto Tiempo con la suma de los intervalos.</returns>
		public Tiempo Add(int dias, int horas, int minutos) => new Tiempo(TotalMinutos + minutos + (horas * MinutosPorHora) + (dias * MinutosPorDia));


		/// <summary>
		/// Devuelve un NUEVO objeto tiempo con la resta del actual y el proporcionado.
		/// </summary>
		/// <param name="tiempo">Objeto tiempo que se restará al actual.</param>
		/// <returns>NUEVO objeto Tiempo con el resultado de la resta.</returns>
		public Tiempo Subtract(Tiempo tiempo) => new Tiempo(TotalMinutos - (tiempo?.TotalMinutos ?? 0));


		/// <summary>
		/// Devuelve un NUEVO objeto tiempo con la resta del intervalo proporcionado del actual.
		/// </summary>
		/// <param name="horas">Horas que se quitarán al objeto actual.</param>
		/// <param name="minutos">Minutos que se quitarán al objeto actual.</param>
		/// <returns>NUEVO objeto Tiempo con la resta de los intervalos.</returns>
		public Tiempo Subtract(int horas, int minutos) => new Tiempo(TotalMinutos - minutos - (horas * MinutosPorHora));


		/// <summary>
		/// Devuelve un NUEVO objeto tiempo con la resta del intervalo proporcionado del actual.
		/// </summary>
		/// <param name="dias">Días que se quitarán al objeto actual.</param>
		/// <param name="horas">Horas que se quitarán al objeto actual.</param>
		/// <param name="minutos">Minutos que se quitarán al objeto actual.</param>
		/// <returns>NUEVO objeto Tiempo con la resta de los intervalos.</returns>
		public Tiempo Subtract(int dias, int horas, int minutos) => new Tiempo(TotalMinutos - minutos - (horas * MinutosPorHora) - (dias * MinutosPorDia));


		/// <summary>
		/// Duración absoluta del objeto Tiempo. Si es negativo, se pasará a positivo.
		/// </summary>
		/// <returns>NUEVO objeto Tiempo con la duración absoluta del intervalo. </returns>
		public Tiempo Duration() => new Tiempo(TotalMinutos < 0 ? -TotalMinutos : TotalMinutos);


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS ESTÁTICOS
		// ====================================================================================================

		/// <summary>
		/// Devuelve un nuevo objeto Tiempo a partir del total de días pasado.
		/// Puede producirse algún error de redondeo.
		/// </summary>
		/// <param name="dias">Número de días (con decimales) que contendrá el nuevo objeto Tiempo.</param>
		/// <returns>Nuevo objeto Tiempo con el número de días indicado.</returns>
		public static Tiempo FromDias(double dias) => new Tiempo(Convert.ToInt32(dias * MinutosPorDia));


		/// <summary>
		/// Devuelve un nuevo objeto Tiempo a partir del total de horas pasado.
		/// Puede producirse algún error de redondeo.
		/// </summary>
		/// <param name="horas">Número de horas (con decimales) que contendrá el nuevo objeto Tiempo.</param>
		/// <returns>Nuevo objeto Tiempo con el número de horas indicado.</returns>
		public static Tiempo FromHoras(double horas) => new Tiempo(Convert.ToInt32(horas * MinutosPorHora));


		/// <summary>
		/// Devuelve un nuevo objeto Tiempo a partir del total de minutos pasado.
		/// </summary>
		/// <param name="minutos">Número de minutos que contendrá el nuevo objeto Tiempo.</param>
		/// <returns>Nuevo objeto Tiempo con el número de minutos indicado.</returns>
		public static Tiempo FromMinutos(int minutos) => new Tiempo(minutos);


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS SUMA
		// ====================================================================================================


		/// <summary>
		/// Añade un número de minutos al objeto actual.
		/// </summary>
		/// <param name="minutos">Número de minutos que se va a añadir.</param>
		public void SumaMinutos(int minutos) => TotalMinutos += minutos;


		/// <summary>
		/// Añade un número de horas al objeto actual.
		/// </summary>
		/// <param name="horas">Número de horas que se va a añadir.</param>
		public void SumaHoras(int horas) => TotalMinutos += horas * MinutosPorHora;


		/// <summary>
		/// Añade un número de dias al objeto actual.
		/// </summary>
		/// <param name="dias">Número de dias que se va a añadir.</param>
		public void SumaDias(int dias) => TotalMinutos += dias * MinutosPorDia;


		/// <summary>
		/// Añade el valor de un objeto Tiempo al objeto actual.
		/// </summary>
		/// <param name="tiempo">Objeto Tiempo que se va a añadir.</param>
		public void SumaTiempo(Tiempo tiempo) => TotalMinutos += (tiempo?.TotalMinutos ?? 0);


		/// <summary>
		/// Añade un intervalo determinado al objeto actual
		/// </summary>
		/// <param name="horas">Número de horas que se añadirán.</param>
		/// <param name="minutos">Número de minutos que se añadirán.</param>
		public void SumaTiempo(int horas, int minutos) => TotalMinutos += minutos + (horas * MinutosPorHora);


		/// <summary>
		/// Añade un intervalo determinado al objeto actual
		/// </summary>
		/// <param name="dias">Número de días que se añadirán.</param>
		/// <param name="horas">Número de horas que se añadirán.</param>
		/// <param name="minutos">Número de minutos que se añadirán.</param>
		public void SumaTiempo(int dias, int horas, int minutos) => TotalMinutos += minutos + (horas * MinutosPorHora) + (dias * MinutosPorDia);


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS RESTA
		// ====================================================================================================


		/// <summary>
		/// Resta un número de minutos al objeto actual.
		/// </summary>
		/// <param name="minutos">Número de minutos que se va a restar.</param>
		public void RestatMinutos(int minutos) => TotalMinutos -= minutos;


		/// <summary>
		/// Resta un número de horas al objeto actual.
		/// </summary>
		/// <param name="horas">Número de horas que se va a restar.</param>
		public void RestaHoras(int horas) => TotalMinutos -= horas * MinutosPorHora;


		/// <summary>
		/// Resta un número de dias al objeto actual.
		/// </summary>
		/// <param name="dias">Número de dias que se va a restar.</param>
		public void RestaDias(int dias) => TotalMinutos -= dias * MinutosPorDia;


		/// <summary>
		/// Resta un objeto Tiempo al objeto actual.
		/// </summary>
		/// <param name="tiempo">Objeto tiempo que se va a restar.</param>
		public void RestaTiempo(Tiempo tiempo) => TotalMinutos -= (tiempo?.TotalMinutos ?? 0);


		/// <summary>
		/// Resta un intervalo determinado al objeto actual
		/// </summary>
		/// <param name="horas">Número de horas que se restarán.</param>
		/// <param name="minutos">Número de minutos que se restarán.</param>
		public void RestaTiempo(int horas, int minutos) => TotalMinutos -= minutos + (horas * MinutosPorHora);


		/// <summary>
		/// Resta un intervalo determinado al objeto actual
		/// </summary>
		/// <param name="dias">Número de días que se restarán.</param>
		/// <param name="horas">Número de horas que se restarán.</param>
		/// <param name="minutos">Número de minutos que se restarán.</param>
		public void RestaTiempo(int dias, int horas, int minutos) => TotalMinutos -= minutos + (horas * MinutosPorHora) + (dias * MinutosPorDia);


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		/// <summary>
		/// Devuelve el código hash de esta instancia.
		/// </summary>
		/// <returns>Código hash de esta instancia.</returns>
		public override int GetHashCode() => TotalMinutos.GetHashCode();


		/// <summary>
		/// Determina la instancia actual es igual a la del objeto proporcionado.
		/// </summary>
		/// <param name="obj">Objeto que se va a comparar con el actual.</param>
		/// <returns>True si ambas instancias son iguales. False en caso contrario.</returns>
		public override bool Equals(object obj) => (obj is Tiempo tiempo) && Equals(tiempo);


		/// <summary>
		/// Determina la instancia actual es igual a la instancia proporcionada.
		/// </summary>
		/// <param name="tiempo">Objeto Tiempo a comparar con el actual.</param>
		/// <returns>True si ambas instancias son iguales. False en caso contrario.</returns>
		public bool Equals(Tiempo tiempo) => (TotalMinutos == tiempo?.TotalMinutos);


		/// <summary>
		/// Devuelve una cadena de texto con la representación del intervalo actual.
		/// </summary>
		/// <returns>Texto con la representación del intervalo actual.</returns>
		public override string ToString() => this.ToString("hm", CultureInfo.CurrentCulture);


		/// <summary>
		/// Devuelve una cadena de texto con la representación del intervalo actual.
		/// </summary>
		/// <param name="provider">Proovedor de formato que se usará para el objeto actual.</param>
		/// <returns>Texto con la representación del intervalo actual.</returns>
		public string ToString(IFormatProvider provider) => this.ToString("hm", provider);


		/// <summary>
		/// Devuelve una cadena de texto con la representación del intervalo actual.
		/// </summary>
		/// <param name="formato">dhm (1.01:45) -- hm (01:45) -- hhm (25:45).</param>
		/// <returns>Texto con la representación del intervalo actual.</returns>
		public string ToString(string formato) => this.ToString(formato, CultureInfo.CurrentCulture);


		/// <summary>
		/// Devuelve una cadena de texto con la representación del intervalo actual.
		/// </summary>
		/// <param name="formato">dhm (1.01:45) -- hm (01:45) -- hhm (25:45).</param>
		/// <param name="provider">Proovedor de formato que se usará para el objeto actual.</param>
		/// <returns>Texto con la representación del intervalo actual.</returns>
		public string ToString(string formato, IFormatProvider provider)
		{
			if (string.IsNullOrEmpty(formato)) formato = "hm";
			if (provider == null) provider = CultureInfo.CurrentCulture;
			string signo = TotalMinutos <0 ? "-" : string.Empty;
			switch (formato.ToLowerInvariant())
			{
				case "dhm":
					return $"{signo}{Math.Abs(Dias)}.{Math.Abs(Horas):00}:{Math.Abs(Minutos):00}";
				case "hm":
					return $"{signo}{Math.Abs(Horas):00}:{Math.Abs(Minutos):00}";
				case "hhm":
					return $"{signo}{Math.Abs(Horas + (Dias * 24)):00}:{Math.Abs(Minutos):00}";
				default:
					throw new FormatException($"El formato '{formato}' no está soportado.");
			}
		}


		/// <summary>
		/// Compara el objeto Tiempo actual con un objeto dado para ver si es anterior, igual o posterior.
		/// </summary>
		/// <param name="obj">Objeto con el que se comparará el objeto Tiempo actual.</param>
		/// <returns>-1 si el objeto es anterior, 0 si es igual o 1 si es posterior.</returns>
		public int CompareTo(object obj) => (obj is Tiempo tiempoObj) ? TotalMinutos.CompareTo(tiempoObj?.TotalMinutos) : 1;


		/// <summary>
		/// Compara la instancia actual con una instancia dada para ver si es anterior, igual o posterior.
		/// </summary>
		/// <param name="other">Instancia con la que se comparará la actual.</param>
		/// <returns>-1 si la instancia es anterior, 0 si es igual o 1 si es posterior.</returns>
		public int CompareTo(Tiempo other) => TotalMinutos.CompareTo(other?.TotalMinutos);

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		/// <summary>
		/// Devuelve el componente Dias del intervalo.
		/// </summary>
		[JsonIgnore]
		public int Dias
		{
			get => TotalMinutos / MinutosPorDia;
		}


		/// <summary>
		/// Devuelve el componente Horas del intervalo.
		/// </summary>
		[JsonIgnore]
		public int Horas
		{
			get => (TotalMinutos % MinutosPorDia) / MinutosPorHora;
		}


		/// <summary>
		/// Devuelve el componente Minutos del intervalo.
		/// </summary>
		[JsonIgnore]
		public int Minutos {
			get => (TotalMinutos % MinutosPorDia) % MinutosPorHora;
		}


		/// <summary>
		/// Devuelve el total de días del intervalo, expresado con una parte fraccional del mismo redondeada a 6 decimales.
		/// </summary>
		[JsonIgnore]
		public double TotalDias {
			get => Math.Round(TotalMinutos / (double)MinutosPorDia, 2);
		}


		/// <summary>
		/// Devuelve el total de horas del intervalo, expresado con una parte fraccional del mismo redondeada a 6 decimales.
		/// </summary>
		[JsonIgnore]
		public double TotalHoras {
			get => Math.Round(TotalMinutos / (double)MinutosPorHora, 2);
		}


		/// <summary>
		/// Devuelve el total de segundos del intervalo.
		/// </summary>
		[JsonProperty("TotalMinutos")]
		public int TotalMinutos { get; private set; }


		#endregion
		// ====================================================================================================


	}
}
