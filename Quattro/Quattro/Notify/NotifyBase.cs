#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Quattro.Notify {

	/// <summary>
	/// Clase que encapsula la interfaz INotifyPropertyChanged para ser heredada por los objetos que
	/// vayan a implementarla. Se añade también las propiedades Modificado y Nuevo.
	/// </summary>
	public class NotifyBase : INotifyPropertyChanged {

		/// <summary>
		/// Evento que se lanzará para notificar el cambio en una propiedad.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;


		/// <summary>
		/// Establece el objeto como modificado e invoca el evento 'PropertyChanged'.
		/// </summary>
		public void OnPropertyChanged([CallerMemberName] string prop = "") {
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}


		/// <summary>
		/// Indica si el objeto ha cambiado.
		/// </summary>
		private bool _modificado;
		public bool Modificado {
			get { return _modificado; }
			set {
				if (_modificado != value) {
					_modificado = value;
					OnPropertyChanged();
				}
			}
		}


		/// <summary>
		/// Indica si el objeto es nuevo.
		/// </summary>
		public bool Nuevo { get; set; } = true;

	}


}
