[assembly: Xamarin.Forms.Dependency(typeof(Quattroid.Droid.Implementations.PathService))]
namespace Quattroid.Droid.Implementations
{
	using System;
	using System.IO;
	using Interfaces;

	public class PathService : IPathService
	{
		public string GetFilePath()
		{
			string folder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			return Path.Combine(folder, "BaseDatos.db3");
		}
	}
}