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
	using System.Runtime.CompilerServices;

	public class DiaCalendario : Servicio, IEquatable<DiaCalendario>
	{

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public DiaCalendario() : base()
		{
			//Incidencia = new Incidencia();
			//Relevo = new Compañero { Matricula = 0 };
			//Susti = new Compañero { Matricula = 0};
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


		private int codigoIncidencia;
		public int CodigoIncidencia
		{
			get { return codigoIncidencia; }
			set { SetValue(ref codigoIncidencia, value); }
		}
		public virtual Incidencia Incidencia { get; set; }


		private bool huelgaParcial;
		public bool HuelgaParcial
		{
			get { return huelgaParcial; }
			set { SetValue(ref huelgaParcial, value); }
		}


		private decimal horasHuelga;
		public decimal HorasHuelga {
			get { return horasHuelga; }
			set { SetValue(ref horasHuelga, value); }
		}


		private string textoLinea;
		public string TextoLinea
		{
			get { return textoLinea; }
			set { SetValue(ref textoLinea, value); }
		}


		private int matriculaRelevo;
		public int MatriculaRelevo
		{
			get { return matriculaRelevo; }
			set { SetValue(ref matriculaRelevo, value); }
		}
		public virtual Compañero Relevo { get; set; }


		private int matriculaSusti;
		public int MatriculaSusti
		{
			get { return matriculaSusti; }
			set { SetValue(ref matriculaSusti, value); }
		}
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
