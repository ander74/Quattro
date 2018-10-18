[assembly: Xamarin.Forms.Dependency(typeof(Quattroid.iOS.Implementations.PathService))]
namespace Quattroid.iOS.Implementations
{
	using System;
	using System.IO;
	using Interfaces;

	public class PathService : IPathService
	{
		public string GetFilePath()
		{
			string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
			string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");
			if(!Directory.Exists(libFolder))
			{
				Directory.CreateDirectory(libFolder);
			}
			return Path.Combine(libFolder, "BaseDatos");
		}

	}
}