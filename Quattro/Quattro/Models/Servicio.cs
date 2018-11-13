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
	using Common;

	public class Servicio : ServicioBase, IEquatable<Servicio>
	{

		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override bool Equals(object obj) => (obj is Servicio serviciobase) && Equals(serviciobase);

		public bool Equals(Servicio sb) => (NumeroLinea, Servicio, Turno) == (sb.NumeroLinea, sb.Servicio, sb.Turno);

		public static bool operator ==(Servicio s1, Servicio s2) => Equals(s1, s2);

		public static bool operator !=(Servicio s1, Servicio s2) => !Equals(s1, s2);

		public override int GetHashCode() => (NumeroLinea, Servicio, Turno).GetHashCode();

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================


		private decimal trabajadas;
		public decimal Trabajadas {
			get { return trabajadas; }
			set { SetValue(ref trabajadas, value); }
		}


		private decimal acumuladas;
		public decimal Acumuladas {
			get { return acumuladas; }
			set { SetValue(ref acumuladas, value); }
		}


		private decimal nocturnas;
		public decimal Nocturnas {
			get { return nocturnas; }
			set { SetValue(ref nocturnas, value); }
		}


		private bool desayuno;
		public bool Desayuno {
			get { return desayuno; }
			set { SetValue(ref desayuno, value); }
		}


		private bool comida;
		public bool Comida {
			get { return comida; }
			set { SetValue(ref comida, value); }
		}


		private bool cena;
		public bool Cena {
			get { return cena; }
			set { SetValue(ref cena, value); }
		}


		private Tiempo tomaDeje = new Tiempo(0);
		public Tiempo TomaDeje {
			get { return tomaDeje; }
			set { SetValue(ref tomaDeje, value); }
		}


		private decimal euros;
		public decimal Euros {
			get { return euros; }
			set { SetValue(ref euros, value); }
		}


		private string notas;
		public string Notas {
			get { return notas; }
			set { SetValue(ref notas, value); }
		}


		#endregion
		// ====================================================================================================

	}
}
