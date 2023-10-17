using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LanchoneteSprint2.Migrations
{
    /// <inheritdoc />
    public partial class secundary : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Movimentacao",
                table: "tbCadVendas",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Movimentacao",
                table: "tbCadCompras",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Movimentacao",
                table: "tbCadVendas");

            migrationBuilder.DropColumn(
                name: "Movimentacao",
                table: "tbCadCompras");
        }
    }
}
