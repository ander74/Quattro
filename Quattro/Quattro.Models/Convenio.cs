#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GNU/GPL 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Quattro.Common;

namespace Quattro.Models {


    public class Convenio : ConvenioBase {



		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================


		private void RecalcularServicio(Servicio servicio, int tipoIncidencia) {

			// Si el servicio no tiene un inicio o un final o un turno válido, salimos.
			if (servicio.Inicio == null || servicio.Final == null || servicio.Turno == 0) return;

			// Definimos los inicios y finales
			List<(int inicio, int final)> horas = new List<(int inicio, int final)>();

			int inicioTemporal = Convert.ToInt32(servicio.Inicio.Value.TotalMinutes);
			int finalTemporal = Convert.ToInt32(servicio.Final.Value.TotalMinutes);
			if (finalTemporal < inicioTemporal) finalTemporal += 1440;
			horas.Add((inicioTemporal, finalTemporal));

			// Recorremos los servicios auxiliares y añadimos el inicio y final.
			foreach (ServicioBase serv in servicio.ServiciosAuxiliares) {
				if (serv.Inicio != null && serv.Final != null) {
					inicioTemporal = Convert.ToInt32(serv.Inicio.Value.TotalMinutes);
					finalTemporal = Convert.ToInt32(serv.Final.Value.TotalMinutes);
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
									if (minutoActual < Convert.ToInt32(LimiteDesayuno.TotalMinutes)) dietaDesayuno = true;
									if (minutoActual > Convert.ToInt32(LimiteComidaTurno1.TotalMinutes)) dietaComida = true;
									break;
								case 2:
									if (minutoActual <= Convert.ToInt32(LimiteComidaTurno2.TotalMinutes)) dietaComida = true;
									int cena = Convert.ToInt32(LimiteCena.TotalMinutes);
									if (cena < Convert.ToInt32(LimiteDesayuno.TotalMinutes)) cena += 1440;
									if (minutoActual > cena) dietaCena = true;
									break;
							}
							// Evaluamos si es un minuto nocturno.
							if ((minutoActual -1 > 0 && minutoActual -1 < Convert.ToInt32(FinalNocturnas.TotalMinutes) ||
								(minutoActual > Convert.ToInt32(InicioNocturnas.TotalMinutes) && minutoActual < Convert.ToInt32(FinalNocturnas.TotalMinutes) + 1440))) {
								minutosNocturnos++;
								if (intermedioNocturnoParcial > 0 && intermedioNocturnoParcial < Convert.ToInt32(LimiteEntreServicios.TotalMinutes)) {
									intermedioNocturno += intermedioNocturnoParcial;
								}
								esNocturno = true;
							} else {
								esNocturno = false;
							}
							// Si el tiempo parcial no supera el límite, se suma al tiempo intermedio.
							if (intermedioParcial > 0 && intermedioParcial < Convert.ToInt32(LimiteEntreServicios.TotalMinutes)) {
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
					servicio.Acumuladas = minutosTrabajados < JornadaMinima * 60 ? 0 : (minutosTrabajados / 60) - JornadaMedia;
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





		#endregion
		// ====================================================================================================



		// ====================================================================================================
		#region MÉTODOS PRIVADOS
		// ====================================================================================================


		/// <summary>
		/// Devuelve una Tupla con el primer inicio y el último final del servicio pasado.
		/// </summary>
		private (int primerInicio, int ultimoFinal) GetLimiteHorasServicio(Servicio servicio) {

			// Si el servicio no tiene un inicio o un final, salimos.
			if (servicio.Inicio == null || servicio.Final == null) return (-1, -1);

			// Definimos el resultado.
			(int primerInicio, int ultimoFinal) resultado;

			// Establecemos el primerInicio y el últimoFinal iniciales.
			int inicioTemporal = Convert.ToInt32(servicio.Inicio.Value.TotalMinutes);
			int finalTemporal = Convert.ToInt32(servicio.Final.Value.TotalMinutes);
			if (finalTemporal < inicioTemporal) finalTemporal += 1440;
			resultado.primerInicio = inicioTemporal;
			resultado.ultimoFinal = finalTemporal;

			// Recorremos los servicios auxiliares y si hay un inicio anterior o un final posterior, se establecen.
			foreach (ServicioBase serv in servicio.ServiciosAuxiliares) {
				if (serv.Inicio != null && serv.Final != null) {
					inicioTemporal = Convert.ToInt32(serv.Inicio.Value.TotalMinutes);
					finalTemporal = Convert.ToInt32(serv.Final.Value.TotalMinutes);
					if (finalTemporal < inicioTemporal) finalTemporal += 1440;
					if (inicioTemporal < resultado.primerInicio) resultado.primerInicio = inicioTemporal;
					if (finalTemporal > resultado.ultimoFinal) resultado.ultimoFinal = finalTemporal;
				}
			}

			// Devolvemos el resultado
			return resultado;
		}

		#endregion
		// ====================================================================================================



	}
}
