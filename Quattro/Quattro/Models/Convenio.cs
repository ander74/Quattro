#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models {

	using System;
	using System.Collections.Generic;
	using Quattro.Common;
	using Quattro.Notify;

	public class Convenio : NotifyBase {


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================


		private void RecalcularServicio2(Servicio servicio, int tipoIncidencia) {

			// Si el servicio no tiene un inicio o un final o un turno válido, salimos.
			if (servicio.Inicio == null || servicio.Final == null || servicio.Turno == 0) return;

			// Definimos los inicios y finales
			List<(int inicio, int final)> horas = new List<(int inicio, int final)>();

			int inicioTemporal = servicio.Inicio.TotalMinutos;
			int finalTemporal = servicio.Final.TotalMinutos;
			if (finalTemporal < inicioTemporal) finalTemporal += 1440;
			horas.Add((inicioTemporal, finalTemporal));

			// Recorremos los servicios auxiliares y añadimos el inicio y final.
			foreach (ServicioBase serv in servicio.ServiciosAuxiliares) {
				if (serv.Inicio != null && serv.Final != null) {
					inicioTemporal = serv.Inicio.TotalMinutos;
					finalTemporal = serv.Final.TotalMinutos;
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
			for (int m = 1; m <= minutosTotales; m++) {
				bool salirBucle = false;
				minutoActual = primerInicio + m;
				// Iniciamos el segundo bucle: Recorrido por las horas de los servicios.
				foreach (var hora in horas) {
					// Si el inicio y final son diferentes...
					if (hora.inicio != hora.final) {
						// Si el minuto actual es trabajado...
						if (minutoActual > hora.inicio && minutoActual < hora.final) {
							minutosTrabajados++;
							// Evaluamos si hay alguna dieta...
							switch (servicio.Turno) {
								case 1:
									if (minutoActual < LimiteDesayuno.TotalMinutos) dietaDesayuno = true;
									if (minutoActual > LimiteComidaTurno1.TotalMinutos) dietaComida = true;
									break;
								case 2:
									if (minutoActual <= LimiteComidaTurno2.TotalMinutos) dietaComida = true;
									int cena = LimiteCena.TotalMinutos;
									if (cena < LimiteDesayuno.TotalMinutos) cena += 1440;
									if (minutoActual > cena) dietaCena = true;
									break;
							}
							// Evaluamos si es un minuto nocturno.
							if ((minutoActual - 1 > 0 && minutoActual - 1 < FinalNocturnas.TotalMinutos ||
								(minutoActual > InicioNocturnas.TotalMinutos && minutoActual < FinalNocturnas.TotalMinutos + 1440))) {
								minutosNocturnos++;
								if (intermedioNocturnoParcial > 0 && intermedioNocturnoParcial < LimiteEntreServicios.TotalMinutos) {
									intermedioNocturno += intermedioNocturnoParcial;
								}
								esNocturno = true;
							} else {
								esNocturno = false;
							}
							// Si el tiempo parcial no supera el límite, se suma al tiempo intermedio.
							if (intermedioParcial > 0 && intermedioParcial < LimiteEntreServicios.TotalMinutos) {
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

			Tiempo inicioTemporal = servicio.Inicio;
			Tiempo finalTemporal = servicio.Final;
			if (finalTemporal < inicioTemporal) finalTemporal.SumaDias(1);
			horas.Add((inicioTemporal, finalTemporal));

			// Recorremos los servicios auxiliares y añadimos el inicio y final.
			foreach (ServicioBase serv in servicio.ServiciosAuxiliares) {
				if (serv.Inicio != null && serv.Final != null) {
					inicioTemporal = serv.Inicio;
					finalTemporal = serv.Final;
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
			for (int m = 1; m <= minutosTotales.TotalMinutos; m++) {
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
									if (minutoActual < LimiteDesayuno) dietaDesayuno = true;
									if (minutoActual > LimiteComidaTurno1) dietaComida = true;
									break;
								case 2:
									if (minutoActual <= LimiteComidaTurno2) dietaComida = true;
									Tiempo cena = LimiteCena < LimiteDesayuno ? (LimiteCena).Add(1, 0, 0) : LimiteCena;
									if (minutoActual > cena) dietaCena = true;
									break;
							}
							// Evaluamos si es un minuto nocturno.
							if ((minutoActual.Subtract(0, 1, 0) > Tiempo.Zero && minutoActual.Subtract(0, 1, 0) < FinalNocturnas) ||
								(minutoActual > InicioNocturnas && minutoActual < (FinalNocturnas).Add(1, 0, 0))) {
								minutosNocturnos.SumaMinutos(1);
								if (intermedioNocturnoParcial > Tiempo.Zero && intermedioNocturnoParcial < LimiteEntreServicios) {
									intermedioNocturno += intermedioNocturnoParcial;
								}
								esNocturno = true;
							} else {
								esNocturno = false;
							}
							// Si el tiempo parcial no supera el límite, se suma al tiempo intermedio.
							if (intermedioParcial > Tiempo.Zero && intermedioParcial < LimiteEntreServicios) {
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
		#region PROPIEDADES
		// ====================================================================================================

		private decimal jornadaMedia = 7.75m;
		public decimal JornadaMedia {
			get { return jornadaMedia; }
			set { SetValue(ref jornadaMedia, value); }
		}


		private decimal jornadaMinima = 7.00m;
		public decimal JornadaMinima {
			get { return jornadaMinima; }
			set { SetValue(ref jornadaMinima, value); }
		}


		private decimal jornadaAnual = 1592.00m;
		public decimal JornadaAnual {
			get { return jornadaAnual; }
			set { SetValue(ref jornadaAnual, value); }
		}


		private Tiempo limiteEntreServicios = new Tiempo(1, 0);
		public Tiempo LimiteEntreServicios {
			get { return limiteEntreServicios; }
			set { SetValue(ref limiteEntreServicios, value); }
		}


		private Tiempo inicioNocturnas = new Tiempo(22, 0);
		public Tiempo InicioNocturnas {
			get { return inicioNocturnas; }
			set { SetValue(ref inicioNocturnas, value); }
		}


		private Tiempo finalNocturnas = new Tiempo(6, 30);
		public Tiempo FinalNocturnas {
			get { return finalNocturnas; }
			set { SetValue(ref finalNocturnas, value); }
		}


		private Tiempo limiteDesayuno = new Tiempo(4, 30);
		public Tiempo LimiteDesayuno {
			get { return limiteDesayuno; }
			set { SetValue(ref limiteDesayuno, value); }
		}


		private Tiempo limiteComidaTurno1 = new Tiempo(15, 30);
		public Tiempo LimiteComidaTurno1 {
			get { return limiteComidaTurno1; }
			set { SetValue(ref limiteComidaTurno1, value); }
		}


		private Tiempo limiteComidaTurno2 = new Tiempo(13, 30);
		public Tiempo LimiteComidaTurno2 {
			get { return limiteComidaTurno2; }
			set { SetValue(ref limiteComidaTurno2, value); }
		}


		private Tiempo limiteCena = new Tiempo(0, 30);
		public Tiempo LimiteCena {
			get { return limiteCena; }
			set { SetValue(ref limiteCena, value); }
		}




		#endregion
		// ====================================================================================================




	}

}
