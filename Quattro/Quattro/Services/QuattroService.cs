#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Services {

	using System;
	using System.Collections.Generic;
	using System.Linq;
	using System.Threading.Tasks;
	using Microsoft.EntityFrameworkCore;
	using Models;
	using Quattro.Common;

	public class QuattroService {

		// ====================================================================================================
		#region CAMPOS PRIVADOS
		// ====================================================================================================

		private static QuattroService instance;

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region CONSTRUCTORES Y SINGLETON
		// ====================================================================================================

		private QuattroService() {

		}

		public static QuattroService GetInstance() {
			if (instance == null) instance = new QuattroService();
			return instance;
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public async void MigrateDataBase(string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				await db.Database.MigrateAsync();
			}
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS CALENDARIO
		// ====================================================================================================

		public async Task<List<DiaCalendario>> GetCalendariosPorMesAsync(int año, int mes, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				return await db.Calendario
							   .Include(c => c.Incidencia)
							   .Include(c => c.Relevo)
							   .Include(c => c.Susti)
							   .Include(c => c.Servicios)
							   .Where(c => c.Fecha.Year == año && c.Fecha.Month == mes)
							   .OrderBy(c => c.Fecha)
							   .ToListAsync();
			}
		}


		public async Task<List<DiaCalendario>> GetCalendariosPorServicioAsync(string linea, string servicio, int turno, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				return await db.Calendario
							   .Include(c => c.Incidencia)
							   .Include(c => c.Relevo)
							   .Include(c => c.Susti)
							   .Include(c => c.Servicios)
							   .Where(c => string.IsNullOrEmpty(linea) ? true : c.NumeroLinea == linea &&
										   string.IsNullOrEmpty(servicio) ? true : c.Servicio == servicio &&
										   turno == 0 ? true : c.Turno == turno)
							   .OrderBy(c => c.Fecha)
							   .ToListAsync();
			}
		}


		public async Task<List<DiaCalendario>> GetCalendariosPorMatriculaAsync(int matricula, bool nosHacen, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				return await db.Calendario
							   .Include(c => c.Incidencia)
							   .Include(c => c.Relevo)
							   .Include(c => c.Susti)
							   .Include(c => c.Servicios)
							   .Where(c => nosHacen ? c.MatriculaSusti == matricula : c.MatriculaRelevo == matricula)
							   .OrderBy(c => c.Fecha)
							   .ToListAsync();
			}
		}


		public async Task<List<DiaCalendario>> GetCalendariosPorIncidenciaAsync(int codigoIncidencia, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				return await db.Calendario
							   .Include(c => c.Incidencia)
							   .Include(c => c.Relevo)
							   .Include(c => c.Susti)
							   .Include(c => c.Servicios)
							   .Where(c => c.CodigoIncidencia == codigoIncidencia)
							   .OrderBy(c => c.Fecha)
							   .ToListAsync();
			}
		}


		public async Task<List<DiaCalendario>> GetCalendariosPorBusAsync(string bus, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				return await db.Calendario
							   .Include(c => c.Incidencia)
							   .Include(c => c.Relevo)
							   .Include(c => c.Susti)
							   .Include(c => c.Servicios)
							   .Where(c => c.Bus == bus)
							   .OrderBy(c => c.Fecha)
							   .ToListAsync();
			}
		}


		public async void UpdateCalendariosAsync(IEnumerable<DiaCalendario> lista, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				db.Calendario.UpdateRange(lista);
				await db.SaveChangesAsync();
			}
		}


		public async void DeleteCalendariosAsync(IEnumerable<DiaCalendario> lista, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				db.Calendario.RemoveRange(lista);
				await db.SaveChangesAsync();
			}
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS COMPAÑEROS
		// ====================================================================================================

		public async Task<List<Compañero>> GetCompañerosAsync(string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				return await db.Compañeros
							   .OrderBy(c => c.Matricula)
							   .ToListAsync();
			}
		}


		public async void UpdateCompañerosAsync(IEnumerable<Compañero> lista, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				db.Compañeros.UpdateRange(lista);
				await db.SaveChangesAsync();
			}
		}


		public async void DeleteCompañerosAsync(IEnumerable<Compañero> lista, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				db.Compañeros.RemoveRange(lista);
				await db.SaveChangesAsync();
			}
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS LÍNEAS
		// ====================================================================================================

		public async Task<List<Linea>> GetLineasAsync(string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				return await db.Lineas
							   .Include(l => l.Servicios)
							   .ThenInclude(s => s.Servicios)
							   .OrderBy(l => l.NumeroLinea)
							   .ToListAsync();
			}
		}


		public async void UpdateLineasAsync(IEnumerable<Linea> lista, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				db.Lineas.UpdateRange(lista);
				await db.SaveChangesAsync();
			}
		}


		public async void DeleteLineasAsync(IEnumerable<Linea> lista, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				db.Lineas.RemoveRange(lista);
				await db.SaveChangesAsync();
			}
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS INCIDENCIAS
		// ====================================================================================================

		public async Task<List<Incidencia>> GetIncidenciasAsync(string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				return await db.Incidencias
							   .OrderBy(i => i.CodigoIncidencia)
							   .ToListAsync();
			}
		}


		public async void UpdateIncidenciasAsync(IEnumerable<Incidencia> lista, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				db.Incidencias.UpdateRange(lista);
				await db.SaveChangesAsync();
			}
		}


		public async void DeleteIncidenciasAsync(IEnumerable<Incidencia> lista, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				db.Incidencias.RemoveRange(lista);
				await db.SaveChangesAsync();
			}
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS HORAS AJENAS
		// ====================================================================================================

		public async Task<List<HoraAjena>> GetHorasAjenasAsync(string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				return await db.HorasAjenas
							   .OrderByDescending(h => h.Fecha)
							   .ToListAsync();
			}
		}


		public async void UpdateHorasAjenasAsync(IEnumerable<HoraAjena> lista, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				db.HorasAjenas.UpdateRange(lista);
				await db.SaveChangesAsync();
			}
		}


		public async void DeleteHorasAjenasAsync(IEnumerable<HoraAjena> lista, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				db.HorasAjenas.RemoveRange(lista);
				await db.SaveChangesAsync();
			}
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS ESTADÍSTICAS
		// ====================================================================================================

		public async Task<decimal> GetAcumuladasHastaMesAsync(int año, int mes, bool incluirTomaDeje, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				var fecha = new DateTime(año, mes, 1).AddMonths(1);
				var acumuladas = await db.Calendario.Where(c => c.Fecha < fecha).SumAsync(c => c.Acumuladas);
				if (incluirTomaDeje) {
					acumuladas += await db.Calendario.Where(c => c.Fecha < fecha)
													 .SumAsync(c => Convert.ToDecimal(Math.Round(c.TomaDeje.TotalHoras, 2)));
				}
				acumuladas += await db.HorasAjenas.Where(h => h.Fecha < fecha).SumAsync(h => h.Horas);
				return acumuladas;
			}
		}


		public async Task<decimal> GetNocturnasHastaMesAsync(int año, int mes, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				var fecha = new DateTime(año, mes, 1).AddMonths(1);
				var nocturnas = await db.Calendario.Where(c => c.Fecha < fecha).SumAsync(c => c.Nocturnas);
				return nocturnas;
			}
		}


		public async Task<Tiempo> GetTomaDejeHastaMesAsync(int año, int mes, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				var fecha = new DateTime(año, mes, 1).AddMonths(1);
				var nocturnas = new Tiempo(await db.Calendario.Where(c => c.Fecha < fecha).SumAsync(c => c.TomaDeje.TotalMinutos));
				return nocturnas;
			}
		}


		public async Task<decimal> GetEurosHastaMesAsync(int año, int mes, string archivoDB) {
			using (QuattroContext db = new QuattroContext(archivoDB)) {
				var fecha = new DateTime(año, mes, 1).AddMonths(1);
				var nocturnas = await db.Calendario.Where(c => c.Fecha < fecha).SumAsync(c => c.Euros);
				return nocturnas;
			}
		}


		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================


		#endregion
		// ====================================================================================================


	}
}
