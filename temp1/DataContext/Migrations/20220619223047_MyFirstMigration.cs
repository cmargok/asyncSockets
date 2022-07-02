using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataContext.Migrations
{
    public partial class MyFirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CARGOS",
                columns: table => new
                {
                    cargo_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    cargo_nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    cargo_sueldo_minimo = table.Column<decimal>(type: "money", nullable: false),
                    cargo_sueldo_maximo = table.Column<decimal>(type: "money", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CARGOS", x => x.cargo_ID);
                });

            migrationBuilder.CreateTable(
                name: "DEPARTAMENTOS",
                columns: table => new
                {
                    dpto_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    dpto_nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departamentos", x => x.dpto_ID);
                });

            migrationBuilder.CreateTable(
                name: "PAISES",
                columns: table => new
                {
                    pais_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pais_nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pais", x => x.pais_ID);
                });

            migrationBuilder.CreateTable(
                name: "EMPLEADOS",
                columns: table => new
                {
                    empl_ID = table.Column<int>(type: "int", nullable: false),
                    empl_primer_nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    empl_segundo_nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    empl_email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    empl_sueldo = table.Column<decimal>(type: "money", nullable: false),
                    empl_comision = table.Column<double>(type: "float", nullable: true),
                    empl_cargo_ID = table.Column<int>(type: "int", nullable: false),
                    empl_Gerente_ID = table.Column<int>(type: "int", nullable: false),
                    empl_dpto_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Empleado", x => x.empl_ID);
                    table.ForeignKey(
                        name: "FK_Empleado_Cargo",
                        column: x => x.empl_cargo_ID,
                        principalTable: "CARGOS",
                        principalColumn: "cargo_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleado_Departamento",
                        column: x => x.empl_dpto_ID,
                        principalTable: "DEPARTAMENTOS",
                        principalColumn: "dpto_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Empleados_Gerente",
                        column: x => x.empl_Gerente_ID,
                        principalTable: "EMPLEADOS",
                        principalColumn: "empl_ID");
                });

            migrationBuilder.CreateTable(
                name: "CIUDADES",
                columns: table => new
                {
                    ciud_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ciud_nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    pais_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ciudades", x => x.ciud_ID);
                    table.ForeignKey(
                        name: "FK_Ciudades_Pais",
                        column: x => x.pais_ID,
                        principalTable: "PAISES",
                        principalColumn: "pais_ID");
                });

            migrationBuilder.CreateTable(
                name: "HISTORICO",
                columns: table => new
                {
                    emphist_ID = table.Column<int>(type: "int", nullable: false),
                    emphist_fecha_retiro = table.Column<DateTime>(type: "date", nullable: false),
                    emphist_dpto_ID = table.Column<int>(type: "int", nullable: false),
                    emphist_cargo_ID = table.Column<int>(type: "int", nullable: false),
                    emphist_empl_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Historico", x => x.emphist_ID);
                    table.ForeignKey(
                        name: "FK_Historico_Cargo",
                        column: x => x.emphist_cargo_ID,
                        principalTable: "CARGOS",
                        principalColumn: "cargo_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historico_Departamento",
                        column: x => x.emphist_dpto_ID,
                        principalTable: "DEPARTAMENTOS",
                        principalColumn: "dpto_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Historico_Empleado",
                        column: x => x.emphist_empl_ID,
                        principalTable: "EMPLEADOS",
                        principalColumn: "empl_ID");
                });

            migrationBuilder.CreateTable(
                name: "LOCALIZACIONES",
                columns: table => new
                {
                    localiz_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    localiz_nombre = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    ciud_ID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_localizaciones", x => x.localiz_ID);
                    table.ForeignKey(
                        name: "FK_Localizaciones_Ciudad",
                        column: x => x.ciud_ID,
                        principalTable: "CIUDADES",
                        principalColumn: "ciud_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LOZALIZACION_DEPARTAMENTO",
                columns: table => new
                {
                    localiz_ID = table.Column<int>(type: "int", nullable: false),
                    dpto_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "FK_Departamento_Localizacion",
                        column: x => x.dpto_ID,
                        principalTable: "DEPARTAMENTOS",
                        principalColumn: "dpto_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Localizacion_Departamento",
                        column: x => x.localiz_ID,
                        principalTable: "LOCALIZACIONES",
                        principalColumn: "localiz_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CIUDADES_pais_ID",
                table: "CIUDADES",
                column: "pais_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLEADOS_empl_cargo_ID",
                table: "EMPLEADOS",
                column: "empl_cargo_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLEADOS_empl_dpto_ID",
                table: "EMPLEADOS",
                column: "empl_dpto_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLEADOS_empl_Gerente_ID",
                table: "EMPLEADOS",
                column: "empl_Gerente_ID");

            migrationBuilder.CreateIndex(
                name: "IX_HISTORICO_emphist_cargo_ID",
                table: "HISTORICO",
                column: "emphist_cargo_ID");

            migrationBuilder.CreateIndex(
                name: "IX_HISTORICO_emphist_dpto_ID",
                table: "HISTORICO",
                column: "emphist_dpto_ID");

            migrationBuilder.CreateIndex(
                name: "IX_HISTORICO_emphist_empl_ID",
                table: "HISTORICO",
                column: "emphist_empl_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LOCALIZACIONES_ciud_ID",
                table: "LOCALIZACIONES",
                column: "ciud_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LOZALIZACION_DEPARTAMENTO_dpto_ID",
                table: "LOZALIZACION_DEPARTAMENTO",
                column: "dpto_ID");

            migrationBuilder.CreateIndex(
                name: "IX_LOZALIZACION_DEPARTAMENTO_localiz_ID",
                table: "LOZALIZACION_DEPARTAMENTO",
                column: "localiz_ID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HISTORICO");

            migrationBuilder.DropTable(
                name: "LOZALIZACION_DEPARTAMENTO");

            migrationBuilder.DropTable(
                name: "EMPLEADOS");

            migrationBuilder.DropTable(
                name: "LOCALIZACIONES");

            migrationBuilder.DropTable(
                name: "CARGOS");

            migrationBuilder.DropTable(
                name: "DEPARTAMENTOS");

            migrationBuilder.DropTable(
                name: "CIUDADES");

            migrationBuilder.DropTable(
                name: "PAISES");
        }
    }
}
