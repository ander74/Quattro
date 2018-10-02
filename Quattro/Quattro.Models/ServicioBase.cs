#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using System.Data.Common;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Quattro.Common;
using Quattro.Notify;

namespace Quattro.Models {

	/// <summary>
	/// 
	/// SERVICIO BASE
	/// =============
	/// 
	///		Contiene los datos básicos de un servicio.
	///		
	///		Se pueden cargar los datos desde un DataReader y se pueden introducir los datos como parámetros de 
	///		un Command que se pase por referencia.
	///		
	///		La propiedad Servicio se formateará de acuerdo a los servicios típicos (mayusculas y números de
	///		dos cifras).
	///		
	///		Tanto la propiedad Inicio, como la propiedad Final aceptarán valores de más de 24h, pero se ajustarán
	///		automáticamente reflejando su valor real (si ponemos 24:00 se convierte en 00:00).
	///		
	///		
	///		Evento FirmaChanged
	///		-------------------
	///			Se dispara al cambiar el número de línea, el servicio o el turno.
	///			
	///		Evento JornadaChanged
	///		---------------------
	///			Se dispara al cambiar el inicio o el final.
	/// 
	/// </summary>
	public class ServicioBase : NotifyBase {

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public ServicioBase() { }


		public ServicioBase(DbDataReader lector) {
			FromReader(lector);
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public virtual void FromReader(DbDataReader lector) {
			id = lector.ToInt32("Id");
			numeroLinea = lector.ToString("NumeroLinea");
			textoLinea = lector.ToString("TextoLinea");
			servicio = lector.ToString("Servicio");
			turno = lector.ToInt32("Turno");
			inicio = lector.ToTiempo("Inicio"); 
			lugarInicio = lector.ToString("LugarInicio");
			final = lector.ToTiempo("Final"); 
			lugarFinal = lector.ToString("LugarFinal");
		}


		public virtual void ToCommand(DbCommand comando) {
			// Id
			DbParameter parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Id";
			parametro.Value = Id;
			comando.Parameters.Add(parametro);
			// NumeroLinea
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@NumeroLinea";
			parametro.Value = NumeroLinea;
			comando.Parameters.Add(parametro);
			// TextoLinea
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@TextoLinea";
			parametro.Value = TextoLinea;
			comando.Parameters.Add(parametro);
			// Servicio
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Servicio";
			parametro.Value = Servicio;
			comando.Parameters.Add(parametro);
			// Turno
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Turno";
			parametro.Value = Turno;
			comando.Parameters.Add(parametro);
			// Inicio
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Inicio";
			parametro.Value = Inicio.ToMinutosOrDbNull(); 
			comando.Parameters.Add(parametro);
			// LugarInicio
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@LugarInicio";
			parametro.Value = LugarInicio;
			comando.Parameters.Add(parametro);
			// Final
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Final";
			parametro.Value = Final.ToMinutosOrDbNull();
			comando.Parameters.Add(parametro);
			// LugarFinal
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@LugarFinal";
			parametro.Value = LugarFinal;
			comando.Parameters.Add(parametro);
		}


		#endregion
		// ====================================================================================================


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
			set {
				if (id != value) {
					id = value;
					OnPropertyChanged();
				}
			}
		}


		private string numeroLinea;
		public string NumeroLinea {
			get { return numeroLinea; }
			set {
				if (numeroLinea != value) {
					numeroLinea = value;
					OnFirmaChanged();
					OnPropertyChanged();
				}
			}
		}


		private string textoLinea;
		public string TextoLinea {
			get { return textoLinea; }
			set {
				if (textoLinea != value) {
					textoLinea = value;
					OnPropertyChanged();
				}
			}
		}


		private string servicio;
		public string Servicio {
			get { return servicio; }
			set {
				if (servicio != value) {
					servicio = value.ToUpper();
					if (Regex.IsMatch(servicio, "^[0 - 9]{ 1}\\D *")) servicio += "0";
					OnFirmaChanged();
					OnPropertyChanged();
				}
			}
		}


		private int turno;
		public int Turno {
			get { return turno; }
			set {
				if (turno != value) {
					turno = value;
					OnFirmaChanged();
					OnPropertyChanged();
				}
			}
		}


		private Tiempo inicio;
		public Tiempo Inicio {
			get { return inicio; }
			set {
				if (inicio != value) {
					inicio = value;
					if (inicio != null && inicio.TotalHoras >= 24) inicio.RestaDias(1);
					OnJornadaChanged();
					OnPropertyChanged();
				}
			}
		}


		private string lugarInicio;
		public string LugarInicio {
			get { return lugarInicio; }
			set {
				if (lugarInicio != value) {
					lugarInicio = value;
					OnPropertyChanged();
				}
			}
		}


		private Tiempo final;
		public Tiempo Final {
			get { return final; }
			set {
				if (final != value) {
					final = value;
					if (final != null && final.TotalHoras >= 24) final.RestaDias(1);
					OnJornadaChanged();
					OnPropertyChanged();
				}
			}
		}


		private string lugarFinal;
		public string LugarFinal {
			get { return lugarFinal; }
			set {
				if (lugarFinal != value) {
					lugarFinal = value;
					OnPropertyChanged();
				}
			}
		}



		#endregion
		// ====================================================================================================

	}

}
