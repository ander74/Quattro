#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Quattro.Common {


	/// <summary>
	/// Representa un intervalo de tiempo compuesto de días, horas, minutos y segundos.
	/// </summary>
    public class Tiempo : IComparable, IComparable<Tiempo>, IEquatable<Tiempo>, IFormattable {


		// ====================================================================================================
		#region CAMPOS
		// ====================================================================================================

		public const int SegundosPorMinuto = 60;
		public const int SegundosPorHora = 3600;
		public const int SegundosPorDia = 86400;

		public static Tiempo MaxValue = new Tiempo(Int32.MaxValue);
		public static Tiempo MinValue = new Tiempo(Int32.MinValue);
		public static Tiempo Zero = new Tiempo(0);


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public Tiempo(int segundos) {
			TotalSegundos = segundos;
		}


		public Tiempo(int horas, int minutos, int segundos) {
			TotalSegundos = segundos + (minutos * SegundosPorMinuto) + (horas * SegundosPorHora);
		}


		public Tiempo(int dias, int horas, int minutos, int segundos) {
			TotalSegundos = segundos + (minutos * SegundosPorMinuto) + (horas * SegundosPorHora) + (dias * SegundosPorDia);
		}


		public Tiempo(TimeSpan ts) {
			TotalSegundos = Convert.ToInt32(ts.TotalSeconds);
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region OPERADORES
		// ====================================================================================================

		public static Tiempo operator +(Tiempo t1, Tiempo t2) {
			return new Tiempo(t1.TotalSegundos + t2.TotalSegundos);
		}


		public static Tiempo operator -(Tiempo t1, Tiempo t2) {
			return new Tiempo(t1.TotalSegundos - t2.TotalSegundos);
		}


		public static Tiempo operator -(Tiempo t1) {
			return new Tiempo(-t1.TotalSegundos);
		}


		public static Tiempo operator *(Tiempo t1, double d2) {
			return new Tiempo(Convert.ToInt32(t1.TotalSegundos * d2));
		}


		public static Tiempo operator *(double d1, Tiempo t2) {
			return new Tiempo(Convert.ToInt32(d1 * t2.TotalSegundos));
		}


		public static double operator /(Tiempo t1, Tiempo t2) {
			return t1.TotalSegundos / Convert.ToDouble(t2.TotalSegundos);
		}


		public static Tiempo operator /(Tiempo t1, double d2) {
			return new Tiempo(Convert.ToInt32(t1.TotalSegundos / d2));
		}


		public static bool operator ==(Tiempo t1, Tiempo t2) {
			return t1.TotalSegundos == t2.TotalSegundos;
		}


		public static bool operator !=(Tiempo t1, Tiempo t2) {
			return t1.TotalSegundos != t2.TotalSegundos;
		}


		public static bool operator >(Tiempo t1, Tiempo t2) {
			return t1.TotalSegundos > t2.TotalSegundos;
		}


		public static bool operator <(Tiempo t1, Tiempo t2) {
			return t1.TotalSegundos < t2.TotalSegundos;
		}


		public static bool operator >=(Tiempo t1, Tiempo t2) {
			return t1.TotalSegundos >= t2.TotalSegundos;
		}


		public static bool operator <=(Tiempo t1, Tiempo t2) {
			return t1.TotalSegundos <= t2.TotalSegundos;
		}



		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public Tiempo Add(Tiempo tiempo) {
			return new Tiempo(TotalSegundos + tiempo.TotalSegundos);
		}


		public Tiempo Add(int horas, int minutos, int segundos) {
			return new Tiempo(TotalSegundos + segundos + (minutos * SegundosPorMinuto) + (horas * SegundosPorHora));
		}


		public Tiempo Add(int dias, int horas, int minutos, int segundos) {
			return new Tiempo(TotalSegundos + segundos + (minutos * SegundosPorMinuto) + (horas * SegundosPorHora) + (dias * SegundosPorDia));
		}


		public Tiempo Subtract(Tiempo tiempo) {
			return new Tiempo(TotalSegundos - tiempo.TotalSegundos);
		}


		public Tiempo Subtract(int horas, int minutos, int segundos) {
			return new Tiempo(TotalSegundos - segundos - (minutos * SegundosPorMinuto) - (horas * SegundosPorHora));
		}


		public Tiempo Subtract(int dias, int horas, int minutos, int segundos) {
			return new Tiempo(TotalSegundos - segundos - (minutos * SegundosPorMinuto) - (horas * SegundosPorHora) - (dias * SegundosPorDia));
		}


		public Tiempo Multiply(double factor) {
			return new Tiempo(Convert.ToInt32(TotalSegundos * factor));
		}


		public double Divide(Tiempo tiempo) {
			return TotalSegundos / Convert.ToDouble(tiempo.TotalSegundos);
		}


		public Tiempo Divide(double divisor) {
			return new Tiempo(Convert.ToInt32(TotalSegundos / divisor));
		}


		public Tiempo Duration() {
			return new Tiempo(TotalSegundos < 0 ? -TotalSegundos : TotalSegundos);
		}



		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS ESTÁTICOS
		// ====================================================================================================

		public static Tiempo FromDias(double dias) {
			return new Tiempo(Convert.ToInt32(dias * SegundosPorDia));
		}


		public static Tiempo FromHoras(double horas) {
			return new Tiempo(Convert.ToInt32(horas * SegundosPorHora));
		}


		public static Tiempo FromMinutos(double minutos) {
			return new Tiempo(Convert.ToInt32(minutos * SegundosPorMinuto));
		}


		public static Tiempo FromSegundos(int segundos) {
			return new Tiempo(segundos);
		}



		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS ADD
		// ====================================================================================================


		public void AddSegundos(int segundos) {
			TotalSegundos += segundos;
		}


		public void AddMinutos(int minutos) {
			TotalSegundos += minutos * SegundosPorMinuto;
		}


		public void AddHoras(int horas) {
			TotalSegundos += horas * SegundosPorHora;
		}


		public void AddDias(int dias) {
			TotalSegundos += dias * SegundosPorDia;
		}


		public void AddTiempo(Tiempo tiempo) {
			TotalSegundos += tiempo.TotalSegundos;
		}


		public void AddTiempo(int horas, int minutos, int segundos) {
			TotalSegundos += segundos + (minutos * SegundosPorMinuto) + (horas * SegundosPorHora) + (dias * SegundosPorDia);
		}


		public void AddTiempo(int dias, int horas, int minutos, int segundos) {
			TotalSegundos += segundos + (minutos * SegundosPorMinuto) + (horas * SegundosPorHora) + (dias * SegundosPorDia);
		}




		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS SUBTRACT
		// ====================================================================================================


		public void SubtractSegundos(int segundos) {
			TotalSegundos -= segundos;
		}


		public void SubtractMinutos(int minutos) {
			TotalSegundos -= minutos * SegundosPorMinuto;
		}


		public void SubtractHoras(int horas) {
			TotalSegundos -= horas * SegundosPorHora;
		}


		public void SubtractDias(int dias) {
			TotalSegundos -= dias * SegundosPorDia;
		}


		public void SubtractTiempo(Tiempo tiempo) {
			TotalSegundos -= tiempo.TotalSegundos;
		}


		public void SubtractTiempo(int horas, int minutos, int segundos) {
			TotalSegundos -= segundos + (minutos * SegundosPorMinuto) + (horas * SegundosPorHora);
		}


		public void SubtractTiempo(int dias, int horas, int minutos, int segundos) {
			TotalSegundos -= segundos + (minutos * SegundosPorMinuto) + (horas * SegundosPorHora) + (dias * SegundosPorDia);
		}




		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override int GetHashCode() {
			return TotalSegundos.GetHashCode();
		}


		public bool Equals(Tiempo tiempo) {
			if (tiempo == null) return false;
			if (TotalSegundos == tiempo.TotalSegundos)
				return true;
			else
				return false;
		}

		public override bool Equals(object obj) {
			if (obj == null) return false;
			Tiempo tiempoObj = obj as Tiempo;
			if (tiempoObj == null)
				return false;
			else
				return Equals(tiempoObj);
		}


		public override string ToString() {
			return this.ToString("DHMS", CultureInfo.CurrentCulture);
		}


		public string ToString(string formato) {
			return this.ToString(formato, CultureInfo.CurrentCulture);
		}


		public string ToString(string formato, IFormatProvider provider) {
			if (string.IsNullOrEmpty(formato)) formato = "DHMS";
			if (provider == null) provider = CultureInfo.CurrentCulture;
			switch (formato.ToUpperInvariant()) {
				case "DHMS":
					return $"{Dias}.{Horas:00}:{Minutos:00}:{Segundos:00}";
				case "DHM":
					return $"{Dias}.{Horas:00}:{Minutos:00}";
				case "HMS":
					return $"{Horas:00}:{Minutos:00}:{Segundos:00}";
				case "HM":
					return $"{Horas}:{Minutos:00}";
				default:
					throw new FormatException($"El formato {formato} no está soportado.");
			}
		}


		public int CompareTo(object obj) {
			if (obj == null) return 1;
			Tiempo tiempoObj = obj as Tiempo;
			if (tiempoObj != null)
				return TotalSegundos.CompareTo(tiempoObj.TotalSegundos);
			else
				throw new ArgumentException("No es un objeto Tiempo.");
		}


		public int CompareTo(Tiempo other) {
			if (other == null) return 1;
			return TotalSegundos.CompareTo(other.TotalSegundos);
		}



		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		public int Dias {
			get {
				return TotalSegundos / SegundosPorDia;
			}
		}


		public int Horas {
			get {
				return (TotalSegundos % SegundosPorDia) / SegundosPorHora;
			}
		}


		public int Minutos {
			get {
				return ((TotalSegundos % SegundosPorDia) % SegundosPorHora) / SegundosPorMinuto;
			}
		}


		public int Segundos {
			get {
				return ((TotalSegundos % SegundosPorDia) % SegundosPorHora) % SegundosPorMinuto;
			}
		}


		public double TotalDias {
			get {
				return Math.Round(TotalSegundos / Convert.ToDouble(SegundosPorDia), 6);
			}
		}


		public double TotalHoras {
			get {
				return Math.Round(TotalSegundos / Convert.ToDouble(SegundosPorHora), 6);
			}
		}


		public double TotalMinutos {
			get {
				return Math.Round(TotalSegundos / Convert.ToDouble(SegundosPorMinuto), 6);
			}
		}


		public int TotalSegundos { get; private set; }


		#endregion
		// ====================================================================================================


	}
}
