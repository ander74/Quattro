namespace PruebaCore
{
	using System;
	using System.Collections.Generic;
	using System.Linq;
	using Microsoft.EntityFrameworkCore;
	using Quattro.Common;
	using Quattro.Models;
	using Quattro.Services;

	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine();

			List<DiaCalendario> lista;
			DateTime d1, d2;
			QuattroService servicio = QuattroService.GetInstance();

			d1 = DateTime.Now;

			servicio.MigrateDataBase(@"D:\QuattroDb.db3");

			d2 = DateTime.Now;

			Console.WriteLine($"{(d2 - d1).TotalSeconds} segundos.");
			Console.ReadKey();

			d1 = DateTime.Now;

			lista = servicio.GetCalendariosPorMesAsync(2018, 10, @"D:\QuattroDb.db3").Result;

			d2 = DateTime.Now;

			Console.WriteLine($"{(d2 - d1).TotalSeconds} segundos.");
			Console.ReadKey();

			d1 = DateTime.Now;

			lista = servicio.GetCalendariosPorMesAsync(DateTime.Now.Year, DateTime.Now.Month, @"D:\QuattroDb.db3").Result;

			d2 = DateTime.Now;

			foreach (var c in lista)
			{
				Console.WriteLine($"Fecha: {c.Fecha.ToShortDateString()}  --  {c.Servicio} -- {c.Trabajadas}");
			}

			Console.WriteLine();
			Console.WriteLine($"{(d2-d1).TotalSeconds} segundos.");

			


			Console.ReadKey();
		}
	}


}
