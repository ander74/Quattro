using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quattro.Common;
using Quattro.Models;
using Quattro.Services;


namespace Prueba {

	class Program {

		private static string ArchivoDB = @"\\Mac\Compartida\QuattroDB.db";


		static void Main(string[] args) {

			var db = QuattroService.GetInstance();

			//using (QuattroContext db2 = new QuattroContext(@"\\Mac\Compartida\QuattroDB.db")) {

			//	//db.Database.Migrate();

			//	//db.Compañeros.Add(new Compañero { Matricula = 5060, Nombre = "Andrés" });

			//	//for (int dia = 1; dia <= 30; dia++) {
			//	//	DiaCalendario d = new DiaCalendario();
			//	//	d.Fecha = new DateTime(2018, 11, dia);
			//	//	d.CodigoIncidencia = 1;
			//	//	db2.Calendario.Add(d);
			//	//}
			//	db2.SaveChanges();
			//}


			//var lista = db.Calendario.Include(c => c.Relevo).Include(c => c.Incidencia).ToList();
			var lista = db.GetCalendariosPorIncidenciaAsync(2, ArchivoDB).Result;
			//var lista = db.GetCalendariosPorServicioAsync("", "", 0, ArchivoDB).Result;


			//var lista = db.Calendario.Where(c => c.Fecha.Day > 15).OrderBy(c => c.Fecha).ToList();

			//lista[0].Relevo.Calificacion= CalificacionCompañero.Malo;

			//db.SaveChanges();

			Console.WriteLine();

			foreach (var l in lista) {
				Console.WriteLine($"{l.Fecha.ToShortDateString()} : " +
					$"{l.Incidencia.TextoIncidencia} => {l.Inicio?.ToString()} - " +
					$"{l.Final?.ToString()}  --  {l.MatriculaRelevo}: {l.Relevo?.Nombre} ({l.Relevo?.Calificacion})");
				//Console.WriteLine($"{l.Fecha}: {l.Servicio} {l.Inicio} - {l.Final}");
			}

			var acum = db.GetAcumuladasHastaMesAsync(2018, 10, false, ArchivoDB).Result;
			var fecha = new DateTime(2019, 2, 1).AddMonths(1);

			Console.WriteLine();
			Console.WriteLine($"{acum}   {fecha}");

			Console.WriteLine();
			Console.WriteLine("Finalizado.");
			Console.ReadKey();
			//}

		}





	}
}
