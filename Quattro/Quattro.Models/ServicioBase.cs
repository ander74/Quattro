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
			inicio = lector.ToTimeSpanNulable("Inicio");
			lugarInicio = lector.ToString("LugarInicio");
			final = lector.ToTimeSpanNulable("Final");
			lugarFinal = lector.ToString("LugarFinal");
		}


		public virtual void ToCommand(ref DbCommand comando) {
			// NumeroLinea
			DbParameter parametro = comando.CreateParameter();
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
			parametro.DbType = System.Data.DbType.Int64;
			parametro.ParameterName = "@Inicio";
			parametro.Value = Inicio.ToTicksOrDbNull();
			comando.Parameters.Add(parametro);
			// LugarInicio
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@LugarInicio";
			parametro.Value = LugarInicio;
			comando.Parameters.Add(parametro);
			// Final
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int64;
			parametro.ParameterName = "@Final";
			parametro.Value = Final.ToTicksOrDbNull();
			comando.Parameters.Add(parametro);
			// LugarFinal
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@LugarFinal";
			parametro.Value = LugarFinal;
			comando.Parameters.Add(parametro);
			// Id
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Id";
			parametro.Value = Id;
			comando.Parameters.Add(parametro);
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() {
			return $"{NumeroLinea} - {Servicio}/{Turno}: {Inicio.ToTexto()} - {Final.ToTexto()}";
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


		private TimeSpan? inicio;
		public TimeSpan? Inicio {
			get { return inicio; }
			set {
				if (inicio != value) {
					inicio = value;
					while (inicio.HasValue && inicio.Value.TotalHours >= 24)
						inicio = inicio.Value.Subtract(new TimeSpan(24, 0, 0));
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


		private TimeSpan? final;
		public TimeSpan? Final {
			get { return final; }
			set {
				if (final != value) {
					final = value;
					while (final.HasValue && final.Value.TotalHours >= 24)
						final = final.Value.Subtract(new TimeSpan(24, 0, 0));
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
