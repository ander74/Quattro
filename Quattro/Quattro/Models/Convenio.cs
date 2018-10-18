#region COPYRIGHT
// ===============================================
//   Quattro - Licencia GPL/GNU 3.0 - A.Herrero
// -----------------------------------------------
//  Vea el archivo Licencia.txt para más detalles 
// ===============================================
#endregion
namespace Quattro.Models 
{
	using System;
	using System.Collections.Generic;
	using System.IO;
	using System.Linq;
	using Common;
	using Newtonsoft.Json;
	using Notify;

	public class Convenio : NotifyBase {


		// ====================================================================================================
		#region MÉTODOS PÚBLICOS
		// ====================================================================================================

		public void RecalcularDia(DiaCalendario dia) {

			var Horarios = ParseHorarios(dia);
			var Dietas = CalcularHorarios(Horarios, dia.Turno);

			decimal Trabajadas = Math.Round(Convert.ToDecimal(Dietas.Trabajadas / 60d), 2);
			decimal Nocturnas = Math.Round(Convert.ToDecimal(Dietas.Nocturnas / 60d), 2);

			// Rellenamos los datos según el tipo de incidencia.
			switch (dia.Incidencia.Tipo) {
				case 1: // Trabajo
					dia.Trabajadas = Trabajadas;
					dia.Acumuladas = Trabajadas < JornadaMinima ? 0 : Trabajadas - JornadaMedia;
					dia.Nocturnas = Nocturnas;
					dia.Desayuno = Dietas.Desayuno;
					dia.Comida = Dietas.Comida;
					dia.Cena = Dietas.Cena;
					break;
				case 2: // Franqueo A Trabajar
					dia.Trabajadas = Trabajadas;
					dia.Acumuladas = Trabajadas;
					dia.Nocturnas = Nocturnas;
					dia.Desayuno = Dietas.Desayuno;
					dia.Comida = Dietas.Comida;
					dia.Cena = Dietas.Cena;
					break;
				case 3: // Fiesta Por Otro Día
					dia.Trabajadas = 0m;
					dia.Acumuladas = -JornadaMedia;
					dia.Nocturnas = 0m;
					dia.Desayuno = false;
					dia.Comida = false;
					dia.Cena = false;
					break;
				case 6: // Jornada Media
					dia.Trabajadas = JornadaMedia;
					dia.Acumuladas = 0;
					dia.Nocturnas = 0;
					dia.Desayuno = false;
					dia.Comida = false;
					dia.Cena = false;
					break;
				default: // Resto de incidencias.
					dia.Trabajadas = 0;
					dia.Acumuladas = 0;
					dia.Nocturnas = 0;
					dia.Desayuno = false;
					dia.Comida = false;
					dia.Cena = false;
					break;
			}
		}


		public void Guardar(string ruta) {

			string datos = JsonConvert.SerializeObject(this, Formatting.Indented);
			File.WriteAllText(ruta, datos);
		}


		public void Cargar(string ruta) {

			if (File.Exists(ruta)) {
				string datos = File.ReadAllText(ruta);
				JsonConvert.PopulateObject(datos, this);
				OnPropertyChanged("");
			}
		}

		#endregion
		// ====================================================================================================


		// ====================================================================================================
		#region MÉTODOS PRIVADOS
		// ====================================================================================================


		private List<Horario> ParseHorarios(DiaCalendario dia) {

			List<Horario> lista = new List<Horario>();

			int inicio = dia.Inicio.TotalMinutos;
			int final = dia.Final.TotalMinutos;
			if (final < inicio) final += 1440;
			lista.Add(new Horario { Inicio = inicio, Final = final });
			foreach (var servicio in dia.Servicios) {
				inicio = servicio.Inicio.TotalMinutos;
				final = servicio.Final.TotalMinutos;
				if (final < inicio) final += 1440;
				lista.Add(new Horario { Inicio = inicio, Final = final });
			}

			return lista;
		}


		private CalculoDias CalcularHorarios(List<Horario> horarios, int turno) {

			CalculoDias resultado = new CalculoDias();
			// Definimos el primer inicio y el último final.
			int primerInicio = horarios.Min(h => h.Inicio);
			int ultimoFinal = horarios.Max(h => h.Final);
			// Variables que vamos a usar.
			int minutosTotales = ultimoFinal - primerInicio;
			int minutoActual = 0;
			int minutosTrabajados = 0;
			int intermedio = 0;
			int intermedioParcial = 0;
			int minutosNocturnos = 0;
			int intermedioNocturno = 0;
			int intermedioNocturnoParcial = 0;
			bool esTrabajado = false;
			bool esNocturno = false;

			// Iniciamos el primer bucle: Paso minuto a minuto por el tiempo total.
			for (int m = 1; m <= minutosTotales; m++) {
				bool salirBucle = false;
				minutoActual = primerInicio + m;
				// Iniciamos el segundo bucle: Recorrido por las horas de los servicios.
				foreach (var hora in horarios) {
					// Si el inicio y final son diferentes...
					if (hora.Inicio != hora.Final) {
						// Si el minuto actual es trabajado...
						if (minutoActual > hora.Inicio && minutoActual <= hora.Final) {
							minutosTrabajados++;
							// Evaluamos si hay alguna dieta...
							switch (turno) {
								case 1:
									if (minutoActual <= LimiteDesayuno.TotalMinutos) resultado.Desayuno = true;
									if (minutoActual > LimiteComidaTurno1.TotalMinutos) resultado.Comida = true;
									break;
								case 2:
									if (minutoActual <= LimiteComidaTurno2.TotalMinutos) resultado.Comida = true;
									Tiempo cena = LimiteCena < LimiteDesayuno ? (LimiteCena).Add(1, 0, 0) : LimiteCena;
									if (minutoActual > cena.TotalMinutos) resultado.Cena = true;
									break;
							}
							// Evaluamos si es un minuto nocturno.
							if ((minutoActual >= 0 && minutoActual <= FinalNocturnas.TotalMinutos) || 
									(minutoActual > InicioNocturnas.TotalMinutos && minutoActual < (FinalNocturnas.Add(24,0).TotalMinutos))) {
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
				}// Fin segundo bucle.

				if (salirBucle) continue;
				// Si el minuto no es trabajado, se añade al intermedio parcial.
				if (!esTrabajado) intermedioParcial++;
				// Si el minuto no es trabajado, pero si nocturno, lo añadimos al intermedio nocturno.
				if (!esTrabajado && esNocturno) intermedioNocturnoParcial++;

			}// Fin primer bucle.

			// Sumamos los intermedios a los totales.
			minutosTrabajados += intermedio;
			minutosNocturnos += intermedioNocturno;

			resultado.Trabajadas = minutosTrabajados;
			resultado.Nocturnas = minutosNocturnos;

			return resultado;
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


		// ====================================================================================================
		#region CLASES PRIVADAS
		// ====================================================================================================

		private class Horario {

			public int Inicio { get; set; }
			public int Final { get; set; }

		}


		private class CalculoDias {

			public int Trabajadas { get; set; }
			public int Nocturnas { get; set; }
			public bool Desayuno { get; set; }
			public bool Comida { get; set; }
			public bool Cena { get; set; }

		}

		#endregion
		// ====================================================================================================


	}

}
