#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models 
{
	using System;
	using Quattro.Notify;

	public class HoraAjena : EntityNotifyBase, IEquatable<HoraAjena>
	{

		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() => $"{Fecha:dd:MM:yyyy}: {Horas:0.00} {Motivo}";

		public override bool Equals(object obj) => (obj is HoraAjena horaAjena) && Equals(horaAjena);

		public bool Equals(HoraAjena ha) => (Fecha, Horas, Motivo, Codigo) == (ha.Fecha, ha.Horas, ha.Motivo, ha.Codigo);

		public static bool operator ==(HoraAjena ha1, HoraAjena ha2) => Equals(ha1, ha2);

		public static bool operator !=(HoraAjena ha1, HoraAjena ha2) => !Equals(ha1, ha2);

		public override int GetHashCode() => (Fecha, Horas, Motivo, Codigo).GetHashCode();

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================


		private int horaAjenaId;
		public int HoraAjenaId {
			get { return horaAjenaId; }
			set { SetValue(ref horaAjenaId, value); }
		}


		private DateTime fecha;
		public DateTime Fecha {
			get { return fecha; }
			set { SetValue(ref fecha, value); }
		}


		private decimal horas;
		public decimal Horas {
			get { return horas; }
			set { SetValue(ref horas, value); }
		}


		private string motivo;
		public string Motivo {
			get { return motivo; }
			set { SetValue(ref motivo, value); }
		}


		private int codigo;
		public int Codigo {
			get { return codigo; }
			set { SetValue(ref codigo, value); }
		}


		#endregion
		// ====================================================================================================

	}
}
