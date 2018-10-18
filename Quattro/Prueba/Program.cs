using System;
using System.Collections.Generic;
using System.Linq;
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


			//Console.WriteLine();
			//var lista = db.GetCalendariosPorIncidenciaAsync(2, ArchivoDB).Result;
			//foreach (var l in lista) {
			//	Console.WriteLine($"{l.Fecha.ToShortDateString()} : " +
			//		$"{l.Incidencia.TextoIncidencia} => {l.Inicio?.ToString()} - " +
			//		$"{l.Final?.ToString()}  --  {l.MatriculaRelevo}: {l.Relevo?.Nombre} ({l.Relevo?.Calificacion})");
			//}
			//var acum = db.GetAcumuladasHastaMesAsync(2018, 10, false, ArchivoDB).Result;
			//var fecha = new DateTime(2019, 2, 1).AddMonths(1);

			//Console.WriteLine();
			//Console.WriteLine($"{acum}   {fecha}");

			//Opciones op = new Opciones();
			//op.Cargar(@"D:\Opciones Quattro.txt");
			//Console.WriteLine();
			//Console.WriteLine($"Horas anteriores: {op.HorasAnteriores}");

			ProbarEventos();

			Console.WriteLine();
			Console.WriteLine("Finalizado.");
			Console.ReadKey();
			//}

		}


		static void ProbarCalculos() {

			Convenio convenio = new Convenio();
			
			DiaCalendario dia = new DiaCalendario();
			dia.Incidencia = new Incidencia { Tipo = 1 };
			dia.Servicio = "01";
			dia.Turno = 1;
			dia.Inicio = new Tiempo(7, 30);
			dia.Final = new Tiempo(13, 30);
			dia.Servicios = new List<ServicioCalendario>();
			dia.Servicios.Add(new ServicioCalendario { Inicio = new Tiempo(13, 30), Final = new Tiempo(14, 20) });
			dia.Servicios.Add(new ServicioCalendario { Inicio = new Tiempo(14, 20), Final = new Tiempo(15, 31) });

			convenio.RecalcularDia(dia);

			Console.WriteLine();
			Console.WriteLine($"Inicio: {dia.Inicio}");
			Console.WriteLine($"Final : {dia.Final}");
			Console.WriteLine($"Total : {dia.Final - dia.Inicio}");
			Console.WriteLine();
			Console.WriteLine($"Trabajadas: {dia.Trabajadas}");
			Console.WriteLine($"Acumuladas: {dia.Acumuladas}");
			Console.WriteLine($"Nocturnas : {dia.Nocturnas}");
			Console.WriteLine($"Desyuno   : {dia.Desayuno}");
			Console.WriteLine($"Comida    : {dia.Comida}");
			Console.WriteLine($"Cena      : {dia.Cena}");
			Console.WriteLine();

			dia = new DiaCalendario();
			dia.Incidencia = new Incidencia { Tipo = 1 };
			dia.Servicio = "01";
			dia.Turno = 2;
			dia.Inicio = new Tiempo(16, 30);
			dia.Final = new Tiempo(00, 30);
			dia.Servicios = new List<ServicioCalendario>();
			//dia.Servicios.Add(new ServicioCalendario { Inicio = new Tiempo(20, 0), Final = new Tiempo(21, 0) });
			//dia.Servicios.Add(new ServicioCalendario { Inicio = new Tiempo(22, 0), Final = new Tiempo(00, 45) });

			convenio.RecalcularDia(dia);

			Console.WriteLine();
			Console.WriteLine($"Inicio: {dia.Inicio}");
			Console.WriteLine($"Final : {dia.Final}");
			Console.WriteLine($"Total : {dia.Final - dia.Inicio}");
			Console.WriteLine();
			Console.WriteLine($"Trabajadas: {dia.Trabajadas}");
			Console.WriteLine($"Acumuladas: {dia.Acumuladas}");
			Console.WriteLine($"Nocturnas : {dia.Nocturnas}");
			Console.WriteLine($"Desyuno   : {dia.Desayuno}");
			Console.WriteLine($"Comida    : {dia.Comida}");
			Console.WriteLine($"Cena      : {dia.Cena}");
			Console.WriteLine();

		}


		static void EscribirListaDias(List<DiaCalendario> lista) {

			Console.WriteLine();
			Console.WriteLine("  CALENDARIO");
			Console.WriteLine("  ==========");
			Console.WriteLine();
			Console.WriteLine("  Día     Serv.   Turno   Inicio  Final   Trab.   Acum.   Noct.   Desay.  Comida  Cena   ");
			Console.WriteLine("  ------- ------- ------- ------- ------- ------- ------- ------- ------- ------- -------");
			foreach (var dia in lista) {
				Console.WriteLine($"   {dia.Fecha.Day.ToString("00").PadRight(7)}" +
								  $" {dia.Servicio.PadRight(7)}" +
								  $" {dia.Turno.ToString().PadRight(7)}" +
								  $" {dia.Inicio.ToString().PadRight(7)}" +
								  $" {dia.Final.ToString().PadRight(7)}" +
								  $" {dia.Trabajadas.ToString("0.00").PadRight(7)}" +
								  $" {dia.Acumuladas.ToString("0.00").PadRight(7)}" +
								  $" {dia.Nocturnas.ToString("0.00").PadRight(7)}" +
								  $" {(dia.Desayuno ? "Si" : "No").PadRight(7)}" +
								  $" {(dia.Comida ? "Si" : "No").PadRight(7)}" +
								  $" {(dia.Cena ? "Si" : "No").PadRight(7)}");
				foreach(var s in dia.Servicios) {
					Console.WriteLine($"          " +
									  $" {s.Servicio.PadRight(7)}" +
									  $" {s.Turno.ToString().PadRight(7)}" +
									  $" {s.Inicio.ToString().PadRight(7)}" +
									  $" {s.Final.ToString().PadRight(7)}");
				}
			}


		}

		static Convenio convenio = new Convenio();


		static void ProbarEventos() {

			using (QuattroContext db = new QuattroContext(ArchivoDB)) {

				List<DiaCalendario> lista = new List<DiaCalendario>();
				var dia1 = db.Calendario.FirstOrDefault(d => d.Fecha == new DateTime(2018, 10, 1));
				var dia2 = db.Calendario.FirstOrDefault(d => d.Fecha == new DateTime(2018, 10, 2));
				var dia3 = db.Calendario.FirstOrDefault(d => d.Fecha == new DateTime(2018, 10, 3));
				dia1.Servicios = new List<ServicioCalendario>();
				dia2.Servicios = new List<ServicioCalendario>();
				dia3.Servicios = new List<ServicioCalendario>();
				dia1.Incidencia = new Incidencia { Tipo = 1 };
				dia2.Incidencia = new Incidencia { Tipo = 1 };
				dia3.Incidencia = new Incidencia { Tipo = 1 };
				dia1.JornadaChanged += Dia_JornadaChanged;
				dia2.JornadaChanged += Dia_JornadaChanged;
				dia3.JornadaChanged += Dia_JornadaChanged;
				lista.Add(dia1);
				lista.Add(dia2);
				lista.Add(dia3);

				Console.Clear();
				EscribirListaDias(lista);
				Console.ReadKey();

				dia1.Turno = 1;
				dia1.Inicio = new Tiempo(6, 0);
				dia1.Final = new Tiempo(14, 0);
				dia2.Turno = 2;
				dia2.Inicio = new Tiempo(14, 30);
				dia2.Final = new Tiempo(23, 15);
				dia3.Turno = 1;
				dia3.Inicio = new Tiempo(4, 20);
				dia3.Final = new Tiempo(12, 0);

				Console.Clear();
				EscribirListaDias(lista);
				Console.ReadKey();

				dia1.Turno = 1;
				dia1.Inicio = new Tiempo(9, 0);
				dia1.Final = new Tiempo(15, 45);
				dia2.Turno = 2;
				dia2.Servicios.Add(new ServicioCalendario { Servicio = "6", Turno = 2, Inicio = new Tiempo(22, 00), Final = new Tiempo(24, 35) });
				dia2.Inicio = new Tiempo(16, 30);
				dia2.Final = new Tiempo(22, 00);
				dia3.Turno = 2;
				dia3.Inicio = new Tiempo(13, 20);
				dia3.Final = new Tiempo(20, 5);

				Console.Clear();
				EscribirListaDias(lista);
				Console.ReadKey();

			}
		}


		private static void Dia_JornadaChanged(object sender, ServicioChangedEventArgs e) {
			var dia = sender as DiaCalendario;
			if (dia != null) {
				convenio.RecalcularDia(dia);
			}
		}
	}
}
