using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Education.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new
                {
                    CursoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    FechaPublicacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Precio = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.CursoId);
                });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "CursoId", "Descripcion", "FechaCreacion", "FechaPublicacion", "Precio", "Titulo" },
                values: new object[,]
                {
                    { new Guid("95359d0e-5ca1-455c-bf8d-70bc314c737b"), "Curso de Java", new DateTime(2022, 11, 19, 16, 2, 35, 96, DateTimeKind.Local).AddTicks(8135), new DateTime(2023, 11, 19, 16, 2, 35, 96, DateTimeKind.Local).AddTicks(8135), 25000m, "Master en Java Spring desde las raices" },
                    { new Guid("c835c80e-a8bb-4555-8782-56cbc9bea182"), "Curso de C# basico", new DateTime(2022, 11, 19, 16, 2, 35, 96, DateTimeKind.Local).AddTicks(8102), new DateTime(2023, 11, 19, 16, 2, 35, 96, DateTimeKind.Local).AddTicks(8112), 56000m, "C# Desde Cero" },
                    { new Guid("e2906b7c-170c-49a8-be97-727ff151ec25"), "Curso de Unit Tests para .NET Core", new DateTime(2022, 11, 19, 16, 2, 35, 96, DateTimeKind.Local).AddTicks(8155), new DateTime(2023, 11, 19, 16, 2, 35, 96, DateTimeKind.Local).AddTicks(8156), 75000m, "Master en Unit Tests con CQRS" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cursos");
        }
    }
}
