using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Sgot.Infra.Data.Migrations.SgotDb
{
    public partial class RemoveEnderecoMappingSgotDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clientes_Enderecos_EnderecoId",
                schema: "public",
                table: "Clientes");

            migrationBuilder.DropTable(
                name: "Enderecos",
                schema: "public");

            migrationBuilder.DropIndex(
                name: "IX_Clientes_EnderecoId",
                schema: "public",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "EnderecoId",
                schema: "public",
                table: "Clientes");

            migrationBuilder.AddColumn<string>(
                name: "Bairro",
                schema: "public",
                table: "Clientes",
                type: "varchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cep",
                schema: "public",
                table: "Clientes",
                type: "varchar(9)",
                maxLength: 9,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Cidade",
                schema: "public",
                table: "Clientes",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Complemento",
                schema: "public",
                table: "Clientes",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Estado",
                schema: "public",
                table: "Clientes",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Logradouro",
                schema: "public",
                table: "Clientes",
                type: "varchar(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "Numero",
                schema: "public",
                table: "Clientes",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Bairro",
                schema: "public",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Cep",
                schema: "public",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Cidade",
                schema: "public",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Complemento",
                schema: "public",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Estado",
                schema: "public",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Logradouro",
                schema: "public",
                table: "Clientes");

            migrationBuilder.DropColumn(
                name: "Numero",
                schema: "public",
                table: "Clientes");

            migrationBuilder.AddColumn<long>(
                name: "EnderecoId",
                schema: "public",
                table: "Clientes",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Enderecos",
                schema: "public",
                columns: table => new
                {
                    EnderecoId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Bairro = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    Cep = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false),
                    Cidade = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Complemento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Estado = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Logradouro = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EnderecoIdPk", x => x.EnderecoId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EnderecoId",
                schema: "public",
                table: "Clientes",
                column: "EnderecoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Clientes_Enderecos_EnderecoId",
                schema: "public",
                table: "Clientes",
                column: "EnderecoId",
                principalSchema: "public",
                principalTable: "Enderecos",
                principalColumn: "EnderecoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
