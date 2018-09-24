#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using System.Data.Common;
using Quattro.Common;
using Quattro.Notify;

namespace Quattro.Models {

    public class Servicio : ServicioBase {

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public Servicio() : base() { }


		public Servicio(DbDataReader lector) {
			FromReader(lector);
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public override void FromReader(DbDataReader lector) {
			base.FromReader(lector);
			trabajadas = lector.ToDecimal("Trabajadas");
			acumuladas = lector.ToDecimal("Acumuladas");
			nocturnas = lector.ToDecimal("Nocturnas");
			desayuno = lector.ToBool("Desayuno");
			comida = lector.ToBool("Comida");
			cena = lector.ToBool("Cena");
			tomaDeje = lector.ToTiempo("TomaDeje");
			euros = lector.ToDecimal("Euros");
			notas = lector.ToString("Notas");
		}


		public override void ToCommand(ref DbCommand comando) {
			base.ToCommand(ref comando);
			//Trabajadas
			DbParameter parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Decimal;
			parametro.ParameterName = "@Trabajadas";
			parametro.Value = Trabajadas;
			comando.Parameters.Add(parametro);
			//Acumuladas
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Decimal;
			parametro.ParameterName = "@Acumuladas";
			parametro.Value = Acumuladas;
			comando.Parameters.Add(parametro);
			//Nocturnas
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Decimal;
			parametro.ParameterName = "@Nocturnas";
			parametro.Value = Nocturnas;
			comando.Parameters.Add(parametro);
			//Desayuno
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Desayuno";
			parametro.Value = Desayuno ? 1 : 0;
			comando.Parameters.Add(parametro);
			//Comida
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Comida";
			parametro.Value = Comida ? 1 : 0;
			comando.Parameters.Add(parametro);
			//Cena
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@Cena";
			parametro.Value = Cena ? 1 : 0;
			comando.Parameters.Add(parametro);
			//TomaDeje
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Int32;
			parametro.ParameterName = "@TomaDeje";
			parametro.Value = TomaDeje.TotalMinutos;
			comando.Parameters.Add(parametro);
			//Euros
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.Decimal;
			parametro.ParameterName = "@Euros";
			parametro.Value = Euros;
			comando.Parameters.Add(parametro);
			//Notas
			parametro = comando.CreateParameter();
			parametro.DbType = System.Data.DbType.String;
			parametro.ParameterName = "@Notas";
			parametro.Value = Notas;
			comando.Parameters.Add(parametro);
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override bool Equals(object obj) {
			if (obj is Servicio servicio)
				return NumeroLinea == servicio.NumeroLinea && Servicio == servicio.Servicio && Turno == servicio.Turno;
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
		#region PROPIEDADES
		// ====================================================================================================


		private decimal trabajadas;
		public decimal Trabajadas {
			get { return trabajadas; }
			set {
				if (trabajadas != value) {
					trabajadas = value;
					OnPropertyChanged();
				}
			}
		}


		private decimal acumuladas;
		public decimal Acumuladas {
			get { return acumuladas; }
			set {
				if (acumuladas != value) {
					acumuladas = value;
					OnPropertyChanged();
				}
			}
		}


		private decimal nocturnas;
		public decimal Nocturnas {
			get { return nocturnas; }
			set {
				if (nocturnas != value) {
					nocturnas = value;
					OnPropertyChanged();
				}
			}
		}


		private bool desayuno;
		public bool Desayuno {
			get { return desayuno; }
			set {
				if (desayuno != value) {
					desayuno = value;
					OnPropertyChanged();
				}
			}
		}


		private bool comida;
		public bool Comida {
			get { return comida; }
			set {
				if (comida != value) {
					comida = value;
					OnPropertyChanged();
				}
			}
		}


		private bool cena;
		public bool Cena {
			get { return cena; }
			set {
				if (cena != value) {
					cena = value;
					OnPropertyChanged();
				}
			}
		}


		private Tiempo tomaDeje;
		public Tiempo TomaDeje {
			get { return tomaDeje; }
			set {
				if (tomaDeje != value) {
					tomaDeje = value;
					OnPropertyChanged();
				}
			}
		}


		private decimal euros;
		public decimal Euros {
			get { return euros; }
			set {
				if (euros != value) {
					euros = value;
					OnPropertyChanged();
				}
			}
		}


		private NotifyCollection<ServicioBase> serviciosAuxiliares;
		public NotifyCollection<ServicioBase> ServiciosAuxiliares {
			get { return serviciosAuxiliares; }
			set {
				if (serviciosAuxiliares != value) {
					serviciosAuxiliares = value;
					OnPropertyChanged();
				}
			}
		}


		private string notas;
		public string Notas {
			get { return notas; }
			set {
				if (notas != value) {
					notas = value;
					OnPropertyChanged();
				}
			}
		}


		#endregion
		// ====================================================================================================



	}
}
