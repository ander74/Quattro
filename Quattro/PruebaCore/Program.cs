using System;
using Quattro.Common;

namespace PruebaCore
{
	class Program
	{
		static void Main(string[] args)
		{

			Tiempo t1 = new Tiempo(2,0,60);
			Tiempo t2 = new Tiempo(15);
			
			Tiempo t3 =  -t2;

			Console.WriteLine();
			Console.WriteLine($"{-t1:hmm} + {t2} = {t3}");

			Console.ReadKey();
		}
	}
}
