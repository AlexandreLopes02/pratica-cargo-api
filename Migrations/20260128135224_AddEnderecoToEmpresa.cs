using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PraticaCargo.Api.Migrations
{
    /// <inheritdoc />
    public partial class AddEnderecoToEmpresa : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Endereco",
                table: "Empresas",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Endereco",
                table: "Empresas");
        }
    }
}
