#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models 
{
	using System;
	using System.Runtime.CompilerServices;
	using System.Text.RegularExpressions;
	using Common;
	using Notify;

	public class ServicioBase: EntityNotifyBase, IEquatable<ServicioBase>
	{

		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() => $"{NumeroLinea} - {Servicio}/{Turno}: {Inicio} - {Final}";
		
		public override bool Equals(object obj) => (obj is ServicioBase serviciobase) && Equals(serviciobase);
		
		public bool Equals(ServicioBase sb) => (NumeroLinea, Servicio, Turno) == (sb.NumeroLinea, sb.Servicio, sb.Turno);

		public static bool operator ==(ServicioBase s1, ServicioBase s2) => Equals(s1, s2);

		public static bool operator !=(ServicioBase s1, ServicioBase s2) => !Equals(s1, s2);

		public override int GetHashCode() => (NumeroLinea, Servicio, Turno).GetHashCode();

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region EVENTOS
		// ====================================================================================================

		/// <summary>
		/// La jornada es el conjunto de la hora de inicio y la de final.
		/// El evento JornadaChanged se dispara cuando uno de los dos valores ha cambiado.
		/// </summary>
		public event ServicioChangedEventHandler JornadaChanged;

		/// <summary>
		/// La firma es el conjunto de la línea, el servicio y el turno.
		/// El evento FirmaChanged se dispara cuando uno de los tres valores ha cambiado.
		/// </summary>
		public event ServicioChangedEventHandler FirmaChanged;

		/// <summary>
		/// Dispara el evento JornadaChanged.
		/// </summary>
		public void OnJornadaChanged([CallerMemberName] string prop = "") {
			var args = new ServicioChangedEventArgs(prop);
			JornadaChanged?.Invoke(this, args);
		}

		/// <summary>
		/// Dispara el evento FirmaChanged.
		/// </summary>
		public void OnFirmaChanged([CallerMemberName] string prop = "") {
			var args = new ServicioChangedEventArgs(prop);
			FirmaChanged?.Invoke(this, args);
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		private string numeroLinea;
		public string NumeroLinea {
			get { return numeroLinea; }
			set { SetValue(ref numeroLinea, value); OnFirmaChanged(); }
		}


		private string servicio;
		public string Servicio {
			get { return servicio; }
			set {
				if (!string.IsNullOrEmpty(value))
				{
					value = value.ToUpper();
					if (!Regex.IsMatch(value, @"^\d{2,}\D*")) value = "0" + value;
				}
				SetValue(ref servicio, value);
				OnFirmaChanged();
			}
		}


		private int turno;
		public int Turno {
			get { return turno; }
			set { SetValue(ref turno, value); OnFirmaChanged(); }
		}


		private Tiempo inicio;
		public Tiempo Inicio {
			get { return inicio; }
			set {
				var temp = value;
				if (temp != null && temp.TotalHoras >= 24) temp.RestaDias(1);
				SetValue(ref inicio, temp); OnJornadaChanged();
			}
		}


		private string lugarInicio;
		public string LugarInicio {
			get { return lugarInicio; }
			set { SetValue(ref lugarInicio, value); }
		}


		private Tiempo final;
		public Tiempo Final {
			get { return final; }
			set {
				var temp = value;
				if (temp != null && temp.TotalHoras >= 24) temp.RestaDias(1);
				SetValue(ref final, temp); OnJornadaChanged();
			}
		}


		private string lugarFinal;
		public string LugarFinal {
			get { return lugarFinal; }
			set { SetValue(ref lugarFinal, value); }
		}



		#endregion
		// ====================================================================================================

	}
}
