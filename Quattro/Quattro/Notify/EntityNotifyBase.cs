#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Notify
{
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.Runtime.CompilerServices;

	/// <summary>
	/// Clase que encapsula la interfaz INotifyPropertyChanged para ser heredada por los objetos que
	/// vayan a implementarla. Se añade también las propiedades Modificado y Nuevo.
	/// </summary>
	public class EntityNotifyBase : INotifyPropertyChanged
	{

		/// <summary>
		/// Evento que se lanzará para notificar el cambio en una propiedad.
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;


		/// <summary>
		/// Invoca el evento 'PropertyChanged' con la propiedad que ha cambiado.
		/// </summary>
		public void OnPropertyChanged([CallerMemberName] string propertyName = null)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
		}


		/// <summary>
		/// Si el valor de la propiedad es diferente al que se quiere asignar, se cambia y se lanza el
		/// evento PropertyChanged correspondiente a la propiedad. Para ello, hay que pasar el campo
		/// privado por referencia.
		/// </summary>
		protected void SetValue<T>(ref T backingField, T value, [CallerMemberName] string propertyName = null)
		{
			if (EqualityComparer<T>.Default.Equals(backingField, value)) return;
			backingField = value;
			OnPropertyChanged(propertyName);
		}
	}
}
