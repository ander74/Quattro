using Microsoft.EntityFrameworkCore.Migrations;

namespace Quattro.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Compañeros",
                columns: table => new
                {
                    Matricula = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Apellidos = table.Column<string>(nullable: true),
                    Telefono = table.Column<string>(nullable: true),
                    Calificacion = table.Column<int>(nullable: false),
                    Deuda = table.Column<int>(nullable: false),
                    Notas = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Compañeros", x => x.Matricula);
                });

            migrationBuilder.CreateTable(
                name: "HorasAjenas",
                columns: table => new
                {
                    HoraAjenaId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<string>(nullable: false),
                    Horas = table.Column<decimal>(nullable: false),
                    Motivo = table.Column<string>(nullable: true),
                    Codigo = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HorasAjenas", x => x.HoraAjenaId);
                });

            migrationBuilder.CreateTable(
                name: "Incidencias",
                columns: table => new
                {
                    CodigoIncidencia = table.Column<int>(nullable: false),
                    TextoIncidencia = table.Column<string>(nullable: true),
                    Tipo = table.Column<int>(nullable: false),
                    Notas = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Incidencias", x => x.CodigoIncidencia);
                });

            migrationBuilder.CreateTable(
                name: "Lineas",
                columns: table => new
                {
                    LineaId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    NumeroLinea = table.Column<string>(nullable: true),
                    TextoLinea = table.Column<string>(nullable: true),
                    Notas = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lineas", x => x.LineaId);
                });

            migrationBuilder.CreateTable(
                name: "Calendario",
                columns: table => new
                {
                    NumeroLinea = table.Column<string>(nullable: true),
                    Servicio = table.Column<string>(nullable: true),
                    Turno = table.Column<int>(nullable: false),
                    Inicio = table.Column<int>(nullable: true),
                    LugarInicio = table.Column<string>(nullable: true),
                    Final = table.Column<int>(nullable: true),
                    LugarFinal = table.Column<string>(nullable: true),
                    Trabajadas = table.Column<decimal>(nullable: false),
                    Acumuladas = table.Column<decimal>(nullable: false),
                    Nocturnas = table.Column<decimal>(nullable: false),
                    Desayuno = table.Column<bool>(nullable: false),
                    Comida = table.Column<bool>(nullable: false),
                    Cena = table.Column<bool>(nullable: false),
                    TomaDeje = table.Column<int>(nullable: true),
                    Euros = table.Column<decimal>(nullable: false),
                    Notas = table.Column<string>(nullable: true),
                    DiaCalendarioId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Fecha = table.Column<string>(nullable: false),
                    EsFranqueo = table.Column<bool>(nullable: false),
                    EsFestivo = table.Column<bool>(nullable: false),
                    CodigoIncidencia = table.Column<int>(nullable: false, defaultValue: 0)
                        .Annotation("Sqlite:Autoincrement", true),
                    HuelgaParcial = table.Column<bool>(nullable: false),
                    HorasHuelga = table.Column<decimal>(nullable: false),
                    TextoLinea = table.Column<string>(nullable: true),
                    MatriculaRelevo = table.Column<int>(nullable: false, defaultValue: 0)
                        .Annotation("Sqlite:Autoincrement", true),
                    MatriculaSusti = table.Column<int>(nullable: false, defaultValue: 0)
                        .Annotation("Sqlite:Autoincrement", true),
                    Bus = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calendario", x => x.DiaCalendarioId);
                    table.ForeignKey(
                        name: "FK_Calendario_Incidencias_CodigoIncidencia",
                        column: x => x.CodigoIncidencia,
                        principalTable: "Incidencias",
                        principalColumn: "CodigoIncidencia",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Calendario_Compañeros_MatriculaRelevo",
                        column: x => x.MatriculaRelevo,
                        principalTable: "Compañeros",
                        principalColumn: "Matricula",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_Calendario_Compañeros_MatriculaSusti",
                        column: x => x.MatriculaSusti,
                        principalTable: "Compañeros",
                        principalColumn: "Matricula",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "ServiciosLinea",
                columns: table => new
                {
                    NumeroLinea = table.Column<string>(nullable: true),
                    Servicio = table.Column<string>(nullable: true),
                    Turno = table.Column<int>(nullable: false),
                    Inicio = table.Column<int>(nullable: true),
                    LugarInicio = table.Column<string>(nullable: true),
                    Final = table.Column<int>(nullable: true),
                    LugarFinal = table.Column<string>(nullable: true),
                    Trabajadas = table.Column<decimal>(nullable: false),
                    Acumuladas = table.Column<decimal>(nullable: false),
                    Nocturnas = table.Column<decimal>(nullable: false),
                    Desayuno = table.Column<bool>(nullable: false),
                    Comida = table.Column<bool>(nullable: false),
                    Cena = table.Column<bool>(nullable: false),
                    TomaDeje = table.Column<int>(nullable: true),
                    Euros = table.Column<decimal>(nullable: false),
                    Notas = table.Column<string>(nullable: true),
                    ServicioLineaId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    LineaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiciosLinea", x => x.ServicioLineaId);
                    table.ForeignKey(
                        name: "FK_ServiciosLinea_Lineas_LineaId",
                        column: x => x.LineaId,
                        principalTable: "Lineas",
                        principalColumn: "LineaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiciosCalendario",
                columns: table => new
                {
                    NumeroLinea = table.Column<string>(nullable: true),
                    Servicio = table.Column<string>(nullable: true),
                    Turno = table.Column<int>(nullable: false),
                    Inicio = table.Column<int>(nullable: true),
                    LugarInicio = table.Column<string>(nullable: true),
                    Final = table.Column<int>(nullable: true),
                    LugarFinal = table.Column<string>(nullable: true),
                    ServicioCalendarioId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DiaCalendarioId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiciosCalendario", x => x.ServicioCalendarioId);
                    table.ForeignKey(
                        name: "FK_ServiciosCalendario_Calendario_DiaCalendarioId",
                        column: x => x.DiaCalendarioId,
                        principalTable: "Calendario",
                        principalColumn: "DiaCalendarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiciosAuxiliares",
                columns: table => new
                {
                    NumeroLinea = table.Column<string>(nullable: true),
                    Servicio = table.Column<string>(nullable: true),
                    Turno = table.Column<int>(nullable: false),
                    Inicio = table.Column<int>(nullable: true),
                    LugarInicio = table.Column<string>(nullable: true),
                    Final = table.Column<int>(nullable: true),
                    LugarFinal = table.Column<string>(nullable: true),
                    ServicioAuxiliarId = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServicioLineaId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiciosAuxiliares", x => x.ServicioAuxiliarId);
                    table.ForeignKey(
                        name: "FK_ServiciosAuxiliares_ServiciosLinea_ServicioLineaId",
                        column: x => x.ServicioLineaId,
                        principalTable: "ServiciosLinea",
                        principalColumn: "ServicioLineaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Compañeros",
                columns: new[] { "Matricula", "Apellidos", "Calificacion", "Deuda", "Nombre", "Notas", "Telefono" },
                values: new object[] { 0, null, 0, 0, "Desconocido", null, null });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 14, "Incidencia Protegida.", "En otro destino", 4 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 13, "Incidencia Protegida.", "Sanción", 4 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 12, "Incidencia Protegida.", "Hacemos el día", 5 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 11, "Incidencia Protegida.", "Nos hacen el día", 1 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 10, "Incidencia Protegida.", "F.N.R. año anterior", 4 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 9, "Incidencia Protegida.", "F.N.R. año actual", 4 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 8, "Incidencia Protegida.", "Permiso", 6 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 7, "Incidencia Protegida.", "Accidentada/o", 4 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 6, "Incidencia Protegida.", "Enferma/o", 4 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 5, "Incidencia Protegida.", "Franqueo a trabajar", 2 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 4, "Incidencia Protegida.", "F.O.D.", 3 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 3, "Incidencia Protegida.", "Vacaciones", 4 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 2, "Incidencia Protegida.", "Franqueo", 4 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 1, "Incidencia Protegida.", "Trabajo", 1 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 0, "Incidencia Protegida.", "Repite día anterior", 0 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 15, "Incidencia Protegida.", "Huelga", 5 });

            migrationBuilder.InsertData(
                table: "Incidencias",
                columns: new[] { "CodigoIncidencia", "Notas", "TextoIncidencia", "Tipo" },
                values: new object[] { 16, "Incidencia Protegida.", "Día por H. Acumuladas", 3 });

            migrationBuilder.CreateIndex(
                name: "IX_Calendario_CodigoIncidencia",
                table: "Calendario",
                column: "CodigoIncidencia");

            migrationBuilder.CreateIndex(
                name: "IX_Calendario_MatriculaRelevo",
                table: "Calendario",
                column: "MatriculaRelevo");

            migrationBuilder.CreateIndex(
                name: "IX_Calendario_MatriculaSusti",
                table: "Calendario",
                column: "MatriculaSusti");

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosAuxiliares_ServicioLineaId",
                table: "ServiciosAuxiliares",
                column: "ServicioLineaId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosCalendario_DiaCalendarioId",
                table: "ServiciosCalendario",
                column: "DiaCalendarioId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiciosLinea_LineaId",
                table: "ServiciosLinea",
                column: "LineaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HorasAjenas");

            migrationBuilder.DropTable(
                name: "ServiciosAuxiliares");

            migrationBuilder.DropTable(
                name: "ServiciosCalendario");

            migrationBuilder.DropTable(
                name: "ServiciosLinea");

            migrationBuilder.DropTable(
                name: "Calendario");

            migrationBuilder.DropTable(
                name: "Lineas");

            migrationBuilder.DropTable(
                name: "Incidencias");

            migrationBuilder.DropTable(
                name: "Compañeros");
        }
    }
}
