using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBanK.Migrations
{
    /// <inheritdoc />
    public partial class AddMovimento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ContaCorrente",
                table: "ContaCorrente");

            migrationBuilder.DropColumn(
                name: "Saldo",
                table: "ContaCorrente");

            migrationBuilder.RenameTable(
                name: "ContaCorrente",
                newName: "ContasCorrentes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContasCorrentes",
                table: "ContasCorrentes",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Movimentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descr = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ocorrencia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Valor = table.Column<double>(type: "float", nullable: false),
                    ContaCorrenteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movimentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Movimentos_ContasCorrentes_ContaCorrenteId",
                        column: x => x.ContaCorrenteId,
                        principalTable: "ContasCorrentes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Movimentos_ContaCorrenteId",
                table: "Movimentos",
                column: "ContaCorrenteId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Movimentos");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ContasCorrentes",
                table: "ContasCorrentes");

            migrationBuilder.RenameTable(
                name: "ContasCorrentes",
                newName: "ContaCorrente");

            migrationBuilder.AddColumn<double>(
                name: "Saldo",
                table: "ContaCorrente",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ContaCorrente",
                table: "ContaCorrente",
                column: "Id");
        }
    }
}
