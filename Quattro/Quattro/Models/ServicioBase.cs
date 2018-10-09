#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models {

	using System.Runtime.CompilerServices;
	using System.Text.RegularExpressions;
	using Common;
	using Notify;

	public class ServicioBase: NotifyBase {


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() {
			return $"{NumeroLinea} - {Servicio}/{Turno}: {Inicio} - {Final}";
		}


		//TODO: Determinar si eliminamos la sobrecarga de estos métodos o no.
		public override bool Equals(object obj) {
			if (obj is ServicioBase serviciobase)
				return NumeroLinea == serviciobase.NumeroLinea && Servicio == serviciobase.Servicio && Turno == serviciobase.Turno;
			return false;
		}


		public override int GetHashCode() {
			unchecked {
				int hash = 5060;
				hash = (hash * 7) + NumeroLinea?.GetHashCode() ?? 1234;
				hash = (hash * 7) + Servicio?.GetHashCode() ?? 1234;
				hash = (hash * 7) + Turno.GetHashCode();
				return hash;
			}
		}


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

		private int id;
		public int Id {
			get { return id; }
			set { SetValue(ref id, value); }
		}


		private string numeroLinea;
		public string NumeroLinea {
			get { return numeroLinea; }
			set { SetValue(ref numeroLinea, value); OnFirmaChanged(); }
		}


		private string servicio;
		public string Servicio {
			get { return servicio; }
			set {
				var temp = value.ToUpper();
				if (!Regex.IsMatch(temp, @"^\d{2,}\D*")) temp = "0" + temp;
				SetValue(ref servicio, temp);
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
