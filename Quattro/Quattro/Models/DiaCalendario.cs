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
	using System.Collections.Generic;

	public class DiaCalendario : Servicio, IEquatable<DiaCalendario>
	{

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public DiaCalendario() : base()
		{
			Servicios = new List<ServicioCalendario>();
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() => $"{Fecha.ToString("dd-MM-yyyy")}: {NumeroLinea} - {Servicio}/{Turno} ({Inicio} - {Final})";

		public override bool Equals(object obj) => (obj is DiaCalendario diaCalendario) && Equals(diaCalendario);

		public bool Equals(DiaCalendario dc) => (Fecha) == (dc.Fecha);

		public static bool operator ==(DiaCalendario dc1, DiaCalendario dc2) => Equals(dc1, dc2);

		public static bool operator !=(DiaCalendario dc1, DiaCalendario dc2) => !Equals(dc1, dc2);

		public override int GetHashCode() => (Fecha).GetHashCode();

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		private int diaCalendarioId;
		public int DiaCalendarioId
		{
			get { return diaCalendarioId; }
			set { SetValue(ref diaCalendarioId, value); }
		}


		private DateTime fecha;
		public DateTime Fecha {
			get { return fecha; }
			set { SetValue(ref fecha, value); }
		}


		private bool esFranqueo;
		public bool EsFranqueo {
			get { return esFranqueo; }
			set { SetValue(ref esFranqueo, value); }
		}


		private bool esFestivo;
		public bool EsFestivo {
			get { return esFestivo; }
			set { SetValue(ref esFestivo, value); }
		}


		public int CodigoIncidencia { get; set; }
		public virtual Incidencia Incidencia { get; set; }


		private bool huelgaParcial;
		public bool HuelgaParcial {
			get { return huelgaParcial; }
			set { SetValue(ref huelgaParcial, value); }
		}


		private decimal horasHuelga;
		public decimal HorasHuelga {
			get { return horasHuelga; }
			set { SetValue(ref horasHuelga, value); }
		}


		private string textoLinea;
		public string TextoLinea {
			get { return textoLinea; }
			set { SetValue(ref textoLinea, value); }
		}


		public int MatriculaRelevo { get; set; }
		public virtual Compañero Relevo { get; set; }


		public int MatriculaSusti { get; set; }
		public virtual Compañero Susti { get; set; }


		private string bus;
		public string Bus {
			get { return bus; }
			set { SetValue(ref bus, value); }
		}


		public virtual List<ServicioCalendario> Servicios { get; set; }


		#endregion
		// ====================================================================================================

	}
}
