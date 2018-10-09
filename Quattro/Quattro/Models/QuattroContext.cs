#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models {

	using System;
	using Microsoft.EntityFrameworkCore;
	using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
	using Quattro.Common;

	public class QuattroContext : DbContext {


		// ====================================================================================================
		#region CONSTRUCTORES
		// ====================================================================================================

		public QuattroContext() {
			ArchivoDb = "Prueba.db";
		}

		public QuattroContext(string archivoDb) {
			ArchivoDb = archivoDb;
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region ON CONFIGURING
		// ====================================================================================================

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) {
			optionsBuilder.UseSqlite($"data source = {ArchivoDb}");
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region ON MODEL CREATING
		// ====================================================================================================

		protected override void OnModelCreating(ModelBuilder modelBuilder) {

			// CONVERTIDOR TIEMPO
			var ConvertidorTiempo = new ValueConverter<Tiempo, int>(
				t => t.TotalMinutos,
				i => Tiempo.FromMinutos(i)
			);


			// CONVERTIDOR FECHA
			var ConvertidorFecha = new ValueConverter<DateTime, string>(
				d => d.ToString("yyyy-MM-dd"),
				s => DateTime.Parse(s)
			);


			// COMPAÑERO
			modelBuilder.Entity<Compañero>()
				.Property(c => c.Matricula)
				.ValueGeneratedNever();
			modelBuilder.Entity<Compañero>()
				.HasKey(c => c.Matricula);
			modelBuilder.Entity<Compañero>()
				.HasData(new Compañero { Matricula = 0, Nombre = "Desconocido" });

			// INCIDENCIA
			modelBuilder.Entity<Incidencia>()
				.Property(c => c.Codigo)
				.ValueGeneratedNever();
			modelBuilder.Entity<Incidencia>()
				.HasKey(c => c.Codigo);
			modelBuilder.Entity<Incidencia>()
				.HasData(
					new Incidencia { Codigo = 0, TextoIncidencia = "Repite día anterior", Tipo = 0, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 1, TextoIncidencia = "Trabajo", Tipo = 1, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 2, TextoIncidencia = "Franqueo", Tipo = 4, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 3, TextoIncidencia = "Vacaciones", Tipo = 4, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 4, TextoIncidencia = "F.O.D.", Tipo = 3, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 5, TextoIncidencia = "Franqueo a trabajar", Tipo = 2, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 6, TextoIncidencia = "Enferma/o", Tipo = 4, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 7, TextoIncidencia = "Accidentada/o", Tipo = 4, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 8, TextoIncidencia = "Permiso", Tipo = 6, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 9, TextoIncidencia = "F.N.R. año actual", Tipo = 4, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 10, TextoIncidencia = "F.N.R. año anterior", Tipo = 4, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 11, TextoIncidencia = "Nos hacen el día", Tipo = 1, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 12, TextoIncidencia = "Hacemos el día", Tipo = 5, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 13, TextoIncidencia = "Sanción", Tipo = 4, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 14, TextoIncidencia = "En otro destino", Tipo = 4, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 15, TextoIncidencia = "Huelga", Tipo = 5, Notas = "Incidencia Protegida." },
					new Incidencia { Codigo = 16, TextoIncidencia = "Día por H. Acumuladas", Tipo = 3, Notas = "Incidencia Protegida." }
				);

			// LÍNEA
			modelBuilder.Entity<Linea>()
				.HasMany(l => l.Servicios)
				.WithOne(s => s.Linea)
				.HasForeignKey(s => s.LineaId)
				.OnDelete(DeleteBehavior.Cascade);

			// HORA AJENA
			modelBuilder.Entity<HoraAjena>()
				.Property(h => h.Fecha)
				.HasConversion(ConvertidorFecha);


			// SERVICIO LÍNEA
			modelBuilder.Entity<ServicioLinea>()
				.HasMany(s => s.Servicios)
				.WithOne(s => s.ServicioLinea)
				.HasForeignKey(s => s.ServicioLineaId)
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<ServicioLinea>()
				.Property(s => s.Inicio)
				.HasConversion(ConvertidorTiempo);
			modelBuilder.Entity<ServicioLinea>()
				.Property(s => s.Final)
				.HasConversion(ConvertidorTiempo);
			modelBuilder.Entity<ServicioLinea>()
				.Property(s => s.TomaDeje)
				.HasConversion(ConvertidorTiempo);

			// DÍA CALENDARIO
			modelBuilder.Entity<DiaCalendario>()
				.HasMany(d => d.Servicios)
				.WithOne(s => s.DiaCalendario)
				.HasForeignKey(d => d.DiaCalendarioId)
				.OnDelete(DeleteBehavior.Cascade);
			modelBuilder.Entity<DiaCalendario>()
				.HasOne(d => d.Relevo)
				.WithMany(r => r.Relevos)
				.HasForeignKey(d => d.MatriculaRelevo)
				.OnDelete(DeleteBehavior.SetNull);
			modelBuilder.Entity<DiaCalendario>()
				.HasOne(d => d.Susti)
				.WithMany(r => r.Sustis)
				.HasForeignKey(d => d.MatriculaSusti)
				.OnDelete(DeleteBehavior.SetNull);
			modelBuilder.Entity<DiaCalendario>()
				.HasOne(d => d.Incidencia)
				.WithMany(i => i.DiasCalendario)
				.HasForeignKey(d => d.CodigoIncidencia)
				.OnDelete(DeleteBehavior.SetNull);
			modelBuilder.Entity<DiaCalendario>()
				.Property(s => s.Inicio)
				.HasConversion(ConvertidorTiempo);
			modelBuilder.Entity<DiaCalendario>()
				.Property(s => s.Final)
				.HasConversion(ConvertidorTiempo);
			modelBuilder.Entity<DiaCalendario>()
				.Property(s => s.TomaDeje)
				.HasConversion(ConvertidorTiempo);
			modelBuilder.Entity<DiaCalendario>()
				.Property(d => d.Fecha)
				.HasConversion(ConvertidorFecha);
			modelBuilder.Entity<DiaCalendario>()
				.Property(d => d.CodigoIncidencia)
				.HasDefaultValue(0);
			modelBuilder.Entity<DiaCalendario>()
				.Property(d => d.MatriculaRelevo)
				.HasDefaultValue(0);
			modelBuilder.Entity<DiaCalendario>()
				.Property(d => d.MatriculaSusti)
				.HasDefaultValue(0);

			// SERVICIO CALENDARIO
			modelBuilder.Entity<ServicioCalendario>()
				.Property(s => s.Inicio)
				.HasConversion(ConvertidorTiempo);
			modelBuilder.Entity<ServicioCalendario>()
				.Property(s => s.Final)
				.HasConversion(ConvertidorTiempo);

			// SERVICIO AUXILIAR
			modelBuilder.Entity<ServicioAuxiliar>()
				.Property(s => s.Inicio)
				.HasConversion(ConvertidorTiempo);
			modelBuilder.Entity<ServicioAuxiliar>()
				.Property(s => s.Final)
				.HasConversion(ConvertidorTiempo);





		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		public string ArchivoDb { get; set; }

		public DbSet<DiaCalendario> Calendario { get; set; }
		public DbSet<ServicioCalendario> ServiciosCalendario { get; set; }
		public DbSet<Compañero> Compañeros { get; set; }
		public DbSet<Incidencia> Incidencias { get; set; }
		public DbSet<HoraAjena> HorasAjenas { get; set; }
		public DbSet<Linea> Lineas { get; set; }
		public DbSet<ServicioLinea> ServiciosLinea { get; set; }
		public DbSet<ServicioAuxiliar> ServiciosAuxiliares { get; set; }

		#endregion
		// ====================================================================================================



	}
}
