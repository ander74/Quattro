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
	using System.Collections.Generic;
	using Common;
	using Notify;

	public class Compañero: EntityNotifyBase, IEquatable<Compañero>
	{

		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public Compañero()
		{
			Relevos = new List<DiaCalendario>();
			Sustis = new List<DiaCalendario>();
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override string ToString() => $"{Matricula:00}: {Nombre} {Apellidos}";

		public override bool Equals(object obj) => (obj is Compañero compañero) && Equals(compañero);

		public bool Equals(Compañero c) => (Matricula) == (c.Matricula);

		public static bool operator ==(Compañero c1, Compañero c2) => Equals(c1, c2);

		public static bool operator !=(Compañero c1, Compañero c2) => !Equals(c1, c2);

		public override int GetHashCode() => (Matricula).GetHashCode();

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================


		private int matricula;
		public int Matricula {
			get { return matricula; }
			set { SetValue(ref matricula, value); }
		}


		private string nombre;
		public string Nombre {
			get { return nombre; }
			set { SetValue(ref nombre, value); }
		}


		private string apellidos;
		public string Apellidos {
			get { return apellidos; }
			set { SetValue(ref apellidos, value); }
		}


		private string telefono;
		public string Telefono {
			get { return telefono; }
			set { SetValue(ref telefono, value); }
		}


		private CalificacionCompañero calificacion;
		public CalificacionCompañero Calificacion {
			get { return calificacion; }
			set { SetValue(ref calificacion, value); }
		}


		private int deuda;
		public int Deuda {
			get { return deuda; }
			set { SetValue(ref deuda, value); }
		}


		private string notas;
		public string Notas {
			get { return notas; }
			set { SetValue(ref notas, value); }
		}


		public virtual List<DiaCalendario> Relevos { get; set; }

		public virtual List<DiaCalendario> Sustis { get; set; }

		#endregion
		// ====================================================================================================

	}
}
