#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using Quattro.Common;
using Quattro.Notify;

namespace Quattro.Models {


	public class Convenio : NotifyBase {


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================


		private void RecalcularServicio2(Servicio servicio, int tipoIncidencia) {

			// Si el servicio no tiene un inicio o un final o un turno válido, salimos.
			if (servicio.Inicio == null || servicio.Final == null || servicio.Turno == 0) return;

			// Definimos los inicios y finales
			List<(int inicio, int final)> horas = new List<(int inicio, int final)>();

			int inicioTemporal = (int)servicio.Inicio.Value.TotalMinutes;
			int finalTemporal = (int)servicio.Final.Value.TotalMinutes;
			if (finalTemporal < inicioTemporal) finalTemporal += 1440;
			horas.Add((inicioTemporal, finalTemporal));

			// Recorremos los servicios auxiliares y añadimos el inicio y final.
			foreach (ServicioBase serv in servicio.ServiciosAuxiliares) {
				if (serv.Inicio != null && serv.Final != null) {
					inicioTemporal = (int)serv.Inicio.Value.TotalMinutes;
					finalTemporal = (int)serv.Final.Value.TotalMinutes;
					if (finalTemporal < inicioTemporal) finalTemporal += 1440;
					horas.Add((inicioTemporal, finalTemporal));
				}
			}

			// Definimos el primer inicio y el último final.
			int primerInicio = horas.Min(h => h.inicio);
			int ultimoFinal = horas.Max(h => h.final);

			// Definimos las variables que vamos a usar.
			int minutosTotales = ultimoFinal - primerInicio;
			int minutoActual;
			int minutosTrabajados = 0;
			int intermedio = 0;
			int intermedioParcial = 0;
			int minutosNocturnos = 0;
			int intermedioNocturno = 0;
			int intermedioNocturnoParcial = 0;
			bool dietaDesayuno = false;
			bool dietaComida = false;
			bool dietaCena = false;
			bool esTrabajado = false;
			bool esNocturno = false;

			// Iniciamos el primer bucle: Paso minuto a minuto por el tiempo total.
			for (int m = 1; m<= minutosTotales; m++) {
				bool salirBucle = false;
				minutoActual = primerInicio + m;
				// Iniciamos el segundo bucle: Recorrido por las horas de los servicios.
				foreach(var hora in horas) {
					// Si el inicio y final son diferentes...
					if (hora.inicio != hora.final) {
						// Si el minuto actual es trabajado...
						if (minutoActual> hora.inicio && minutoActual < hora.final) {
							minutosTrabajados++;
							// Evaluamos si hay alguna dieta...
							switch (servicio.Turno) {
								case 1:
									if (minutoActual < (int)LimiteDesayuno.TotalMinutes) dietaDesayuno = true;
									if (minutoActual > (int)LimiteComidaTurno1.TotalMinutes) dietaComida = true;
									break;
								case 2:
									if (minutoActual <= (int)LimiteComidaTurno2.TotalMinutes) dietaComida = true;
									int cena = (int)LimiteCena.TotalMinutes;
									if (cena < (int)LimiteDesayuno.TotalMinutes) cena += 1440;
									if (minutoActual > cena) dietaCena = true;
									break;
							}
							// Evaluamos si es un minuto nocturno.
							if ((minutoActual -1 > 0 && minutoActual -1 < (int)FinalNocturnas.TotalMinutes ||
								(minutoActual > (int)InicioNocturnas.TotalMinutes && minutoActual < (int)FinalNocturnas.TotalMinutes + 1440))) {
								minutosNocturnos++;
								if (intermedioNocturnoParcial > 0 && intermedioNocturnoParcial < (int)LimiteEntreServicios.TotalMinutes) {
									intermedioNocturno += intermedioNocturnoParcial;
								}
								esNocturno = true;
							} else {
								esNocturno = false;
							}
							// Si el tiempo parcial no supera el límite, se suma al tiempo intermedio.
							if (intermedioParcial > 0 && intermedioParcial < (int)LimiteEntreServicios.TotalMinutes) {
								intermedio += intermedioParcial;
							}
							//Ponemos los parciales a cero.
							intermedioParcial = 0;
							intermedioNocturnoParcial = 0;
							// Marcamos el minuto como trabajado y continuamos con el siguiente minuto.
							esTrabajado = true;
							salirBucle = true;
							break;
						} else {
							esTrabajado = false;
						}
					}
				}
				if (salirBucle) continue;
				// Si el minuto no es trabajado, se añade al intermedio parcial.
				if (!esTrabajado) intermedioParcial++;
				// Si el minuto no es trabajado, pero si nocturno, lo añadimos al intermedio nocturno.
				if (!esTrabajado && esNocturno) intermedioNocturnoParcial++;
			}
			// Sumamos los intermedios a los totales.
			minutosTrabajados += intermedio;
			minutosNocturnos += intermedioNocturno;

			// Rellenamos los datos según el tipo de incidencia.
			switch (tipoIncidencia) {
				case 1: // Trabajo
					servicio.Trabajadas = new TimeSpan(0, minutosTrabajados, 0).ToDecimal(2);
					servicio.Acumuladas = minutosTrabajados < JornadaMinima * 60 ? 0 : (minutosTrabajados / 60) - JornadaMedia;
					servicio.Nocturnas = new TimeSpan(0, minutosNocturnos, 0).ToDecimal(2);
					servicio.Desayuno = dietaDesayuno;
					servicio.Comida = dietaComida;
					servicio.Cena = dietaCena;
					break;
				case 2: // Franqueo A Trabajar
					servicio.Trabajadas = new TimeSpan(0, minutosTrabajados, 0).ToDecimal(2);
					servicio.Acumuladas = new TimeSpan(0, minutosTrabajados, 0).ToDecimal(2);
					servicio.Nocturnas = new TimeSpan(0, minutosNocturnos, 0).ToDecimal(2);
					servicio.Desayuno = dietaDesayuno;
					servicio.Comida = dietaComida;
					servicio.Cena = dietaCena;
					break;
				case 3: // Fiesta Por Otro Día
					servicio.Trabajadas = 0m;
					servicio.Acumuladas = -JornadaMedia;
					servicio.Nocturnas = 0m;
					servicio.Desayuno = false;
					servicio.Comida = false;
					servicio.Cena = false;
					break;
				case 6: // Jornada Media
					servicio.Trabajadas = JornadaMedia;
					servicio.Acumuladas = 0;
					servicio.Nocturnas = 0;
					servicio.Desayuno = false;
					servicio.Comida = false;
					servicio.Cena = false;
					break;
				default: // Resto de incidencias.
					servicio.Trabajadas = 0;
					servicio.Acumuladas = 0;
					servicio.Nocturnas = 0;
					servicio.Desayuno = false;
					servicio.Comida = false;
					servicio.Cena = false;
					break;
			}
		}


		private void RecalcularServicio(Servicio servicio, int tipoIncidencia) {

			// Si el servicio no tiene un inicio o un final o un turno válido, salimos.
			if (servicio.Inicio == null || servicio.Final == null || servicio.Turno == 0) return;

			// Definimos los inicios y finales
			List<(Tiempo inicio, Tiempo final)> horas = new List<(Tiempo inicio, Tiempo final)>();

			Tiempo inicioTemporal = (Tiempo)servicio.Inicio;
			Tiempo finalTemporal = (Tiempo)servicio.Final;
			if (finalTemporal < inicioTemporal) finalTemporal.SumaDias(1);
			horas.Add((inicioTemporal, finalTemporal));

			// Recorremos los servicios auxiliares y añadimos el inicio y final.
			foreach (ServicioBase serv in servicio.ServiciosAuxiliares) {
				if (serv.Inicio != null && serv.Final != null) {
					inicioTemporal = (Tiempo)serv.Inicio;
					finalTemporal = (Tiempo)serv.Final;
					if (finalTemporal < inicioTemporal) finalTemporal.SumaDias(1);
					horas.Add((inicioTemporal, finalTemporal));
				}
			}

			// Definimos el primer inicio y el último final.
			Tiempo primerInicio = horas.Min(h => h.inicio);
			Tiempo ultimoFinal = horas.Max(h => h.final);

			// Definimos las variables que vamos a usar.
			Tiempo minutosTotales = ultimoFinal - primerInicio;
			Tiempo minutoActual;
			Tiempo minutosTrabajados = Tiempo.Zero;
			Tiempo intermedio = Tiempo.Zero;
			Tiempo intermedioParcial = Tiempo.Zero;
			Tiempo minutosNocturnos = Tiempo.Zero;
			Tiempo intermedioNocturno = Tiempo.Zero;
			Tiempo intermedioNocturnoParcial = Tiempo.Zero;
			bool dietaDesayuno = false;
			bool dietaComida = false;
			bool dietaCena = false;
			bool esTrabajado = false;
			bool esNocturno = false;

			// Iniciamos el primer bucle: Paso minuto a minuto por el tiempo total.
			for (int m = 1; m <= (int)minutosTotales.TotalMinutos; m++) {
				bool salirBucle = false;
				minutoActual = primerInicio.Add(0, m, 0);
				// Iniciamos el segundo bucle: Recorrido por las horas de los servicios.
				foreach (var hora in horas) {
					// Si el inicio y final son diferentes...
					if (hora.inicio != hora.final) {
						// Si el minuto actual es trabajado...
						if (minutoActual > hora.inicio && minutoActual < hora.final) {
							minutosTrabajados.SumaMinutos(1);
							// Evaluamos si hay alguna dieta...
							switch (servicio.Turno) {
								case 1:
									if (minutoActual < (Tiempo)LimiteDesayuno) dietaDesayuno = true;
									if (minutoActual > (Tiempo)LimiteComidaTurno1) dietaComida = true;
									break;
								case 2:
									if (minutoActual <= (Tiempo)LimiteComidaTurno2) dietaComida = true;
									Tiempo cena = (Tiempo)LimiteCena < (Tiempo)LimiteDesayuno ? ((Tiempo)LimiteCena).Add(1, 0, 0) : (Tiempo)LimiteCena;
									if (minutoActual > cena) dietaCena = true;
									break;
							}
							// Evaluamos si es un minuto nocturno.
							if ((minutoActual.Subtract(0,1,0) > Tiempo.Zero && minutoActual.Subtract(0,1,0) < (Tiempo)FinalNocturnas) ||
								(minutoActual > (Tiempo)InicioNocturnas && minutoActual < ((Tiempo)FinalNocturnas).Add(1,0,0))) {
								minutosNocturnos.SumaMinutos(1);
								if (intermedioNocturnoParcial > Tiempo.Zero && intermedioNocturnoParcial < (Tiempo)LimiteEntreServicios) {
									intermedioNocturno += intermedioNocturnoParcial;
								}
								esNocturno = true;
							} else {
								esNocturno = false;
							}
							// Si el tiempo parcial no supera el límite, se suma al tiempo intermedio.
							if (intermedioParcial > Tiempo.Zero && intermedioParcial < (Tiempo)LimiteEntreServicios) {
								intermedio += intermedioParcial;
							}
							//Ponemos los parciales a cero.
							intermedioParcial = Tiempo.Zero;
							intermedioNocturnoParcial = Tiempo.Zero;
							// Marcamos el minuto como trabajado y continuamos con el siguiente minuto.
							esTrabajado = true;
							salirBucle = true;
							break;
						} else {
							esTrabajado = false;
						}
					}
				}
				if (salirBucle) continue;
				// Si el minuto no es trabajado, se añade al intermedio parcial.
				if (!esTrabajado) intermedioParcial.SumaMinutos(1);
				// Si el minuto no es trabajado, pero si nocturno, lo añadimos al intermedio nocturno.
				if (!esTrabajado && esNocturno) intermedioNocturnoParcial.SumaMinutos(1);
			}
			// Sumamos los intermedios a los totales.
			minutosTrabajados += intermedio;
			minutosNocturnos += intermedioNocturno;

			// Rellenamos los datos según el tipo de incidencia.
			switch (tipoIncidencia) {
				case 1: // Trabajo
					servicio.Trabajadas = (decimal)minutosTrabajados.TotalHoras;
					servicio.Acumuladas = (decimal)minutosTrabajados.TotalHoras < JornadaMinima ? 0 : (decimal)minutosTrabajados.TotalHoras - JornadaMedia;
					servicio.Nocturnas = (decimal)minutosNocturnos.TotalHoras;
					servicio.Desayuno = dietaDesayuno;
					servicio.Comida = dietaComida;
					servicio.Cena = dietaCena;
					break;
				case 2: // Franqueo A Trabajar
					servicio.Trabajadas = (decimal)minutosTrabajados.TotalHoras;
					servicio.Acumuladas = (decimal)minutosTrabajados.TotalHoras;
					servicio.Nocturnas = (decimal)minutosNocturnos.TotalHoras;
					servicio.Desayuno = dietaDesayuno;
					servicio.Comida = dietaComida;
					servicio.Cena = dietaCena;
					break;
				case 3: // Fiesta Por Otro Día
					servicio.Trabajadas = 0m;
					servicio.Acumuladas = -JornadaMedia;
					servicio.Nocturnas = 0m;
					servicio.Desayuno = false;
					servicio.Comida = false;
					servicio.Cena = false;
					break;
				case 6: // Jornada Media
					servicio.Trabajadas = JornadaMedia;
					servicio.Acumuladas = 0;
					servicio.Nocturnas = 0;
					servicio.Desayuno = false;
					servicio.Comida = false;
					servicio.Cena = false;
					break;
				default: // Resto de incidencias.
					servicio.Trabajadas = 0;
					servicio.Acumuladas = 0;
					servicio.Nocturnas = 0;
					servicio.Desayuno = false;
					servicio.Comida = false;
					servicio.Cena = false;
					break;
			}
		}





		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PRIVADOS
		// ====================================================================================================



		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region PROPIEDADES
		// ====================================================================================================

		private decimal jornadaMedia = 7.75m;
		public decimal JornadaMedia {
			get { return jornadaMedia; }
			set {
				if (jornadaMedia != value) {
					jornadaMedia = value;
					OnPropertyChanged();
				}
			}
		}


		private decimal jornadaMinima = 7.00m;
		public decimal JornadaMinima {
			get { return jornadaMinima; }
			set {
				if (jornadaMinima != value) {
					jornadaMinima = value;
					OnPropertyChanged();
				}
			}
		}


		private decimal jornadaAnual = 1592.00m;
		public decimal JornadaAnual {
			get { return jornadaAnual; }
			set {
				if (jornadaAnual != value) {
					jornadaAnual = value;
					OnPropertyChanged();
				}
			}
		}


		private TimeSpan limiteEntreServicios = new TimeSpan(1, 0, 0);
		public TimeSpan LimiteEntreServicios {
			get { return limiteEntreServicios; }
			set {
				if (limiteEntreServicios != value) {
					limiteEntreServicios = value;
					OnPropertyChanged();
				}
			}
		}


		private TimeSpan inicioNocturnas = new TimeSpan(22, 0, 0);
		public TimeSpan InicioNocturnas {
			get { return inicioNocturnas; }
			set {
				if (inicioNocturnas != value) {
					inicioNocturnas = value;
					OnPropertyChanged();
				}
			}
		}


		private TimeSpan finalNocturnas = new TimeSpan(6, 30, 0);
		public TimeSpan FinalNocturnas {
			get { return finalNocturnas; }
			set {
				if (finalNocturnas != value) {
					finalNocturnas = value;
					OnPropertyChanged();
				}
			}
		}


		private TimeSpan limiteDesayuno = new TimeSpan(4, 30, 0);
		public TimeSpan LimiteDesayuno {
			get { return limiteDesayuno; }
			set {
				if (limiteDesayuno != value) {
					limiteDesayuno = value;
					OnPropertyChanged();
				}
			}
		}


		private TimeSpan limiteComidaTurno1 = new TimeSpan(15, 30, 0);
		public TimeSpan LimiteComidaTurno1 {
			get { return limiteComidaTurno1; }
			set {
				if (limiteComidaTurno1 != value) {
					limiteComidaTurno1 = value;
					OnPropertyChanged();
				}
			}
		}


		private TimeSpan limiteComidaTurno2 = new TimeSpan(13, 30, 0);
		public TimeSpan LimiteComidaTurno2 {
			get { return limiteComidaTurno2; }
			set {
				if (limiteComidaTurno2 != value) {
					limiteComidaTurno2 = value;
					OnPropertyChanged();
				}
			}
		}


		private TimeSpan limiteCena = new TimeSpan(0, 30, 0);
		public TimeSpan LimiteCena {
			get { return limiteCena; }
			set {
				if (limiteCena != value) {
					limiteCena = value;
					OnPropertyChanged();
				}
			}
		}




		#endregion
		// ====================================================================================================




	}
}
