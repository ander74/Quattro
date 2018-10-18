#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models
{
	using System.ComponentModel.DataAnnotations.Schema;

	public class ServicioCalendario : ServicioBase {


		// ====================================================================================================
		#region MÉTODOS OVERRIDE
		// ====================================================================================================

		public override bool Equals(object obj) {
			if (obj is ServicioCalendario servicio)
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

		private int servicioCalendarioId;
		public int ServicioCalendarioId
		{
			get { return servicioCalendarioId; }
			set { SetValue(ref servicioCalendarioId, value); }
		}


		public int DiaCalendarioId { get; set; }


		public virtual DiaCalendario DiaCalendario { get; set; }

		#endregion
		// ====================================================================================================


	}
}
