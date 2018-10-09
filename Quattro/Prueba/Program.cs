using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Quattro.Common;
using Quattro.Models;

namespace Prueba {

	class Program {

		static void Main(string[] args) {

			using (QuattroContext db = new QuattroContext(@"Z:\\QuattroDB.db")) {

				db.Database.Migrate();

				//db.Compañeros.Add(new Compañero { Matricula = 5060, Nombre = "Andrés" });

				//for (int dia = 1; dia <= 31; dia++) {
				//	DiaCalendario d = new DiaCalendario();
				//	d.Fecha = new DateTime(2018, 10, dia);
				//	db.Calendario.Add(d);
				//}
				//db.SaveChanges();

				//var lista = db.Calendario.Include(c => c.Relevo).Include(c => c.Incidencia).ToList();

				var lista = db.Calendario.Where(c => c.Fecha.Day > 15).OrderBy(c => c.Fecha).ToList();

				Console.WriteLine();

				foreach (var l in lista) {
					//Console.WriteLine($"{l.Fecha.ToShortDateString()} : {l.Incidencia.TextoIncidencia} => {l.Inicio.ToString()} - {l.Final.ToString()}  --  {l.MatriculaRelevo}: {l.Relevo.Nombre}");
					Console.WriteLine($"{l.Fecha}: {l.Servicio} {l.Inicio} - {l.Final}");
				}
				Console.WriteLine();
				Console.WriteLine($"{db.Calendario.Count()}");

				Console.WriteLine();
				Console.WriteLine("Finalizado.");
				Console.Read();
			}

		}
	}
}
