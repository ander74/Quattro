using System.ComponentModel;

namespace Quattro.Data
{

	public interface IDataModel
	{
		ModelState State { get; set; }
	}
}
