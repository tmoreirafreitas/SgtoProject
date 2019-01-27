using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Sgot.Infra.Data.Migrations.SgotDb
{
    public partial class InitialSgotDbContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "ApplicationUser",
                schema: "public",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FullName = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApplicationUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Enderecos",
                schema: "public",
                columns: table => new
                {
                    EnderecoId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Logradouro = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    Bairro = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    Cidade = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Estado = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Numero = table.Column<int>(type: "integer", nullable: true),
                    Complemento = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: true),
                    Cep = table.Column<string>(type: "varchar(9)", maxLength: 9, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("EnderecoIdPk", x => x.EnderecoId);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                schema: "public",
                columns: table => new
                {
                    ClienteId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Nome = table.Column<string>(type: "varchar(60)", maxLength: 60, nullable: false),
                    Rg = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true),
                    Cpf = table.Column<string>(type: "varchar(15)", maxLength: 15, nullable: false),
                    Nascimento = table.Column<DateTime>(type: "date", nullable: false),
                    Filiacao = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: true),
                    EnderecoId = table.Column<long>(nullable: false),
                    IsSPC = table.Column<bool>(nullable: false),
                    Telefone = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false),
                    Email = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    Sexo = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ClienteIdPk", x => x.ClienteId);
                    table.ForeignKey(
                        name: "FK_Clientes_Enderecos_EnderecoId",
                        column: x => x.EnderecoId,
                        principalSchema: "public",
                        principalTable: "Enderecos",
                        principalColumn: "EnderecoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pedidos",
                schema: "public",
                columns: table => new
                {
                    PedidoId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    ApplicationUserId = table.Column<string>(nullable: true),
                    ClienteId = table.Column<long>(nullable: false),
                    FaturaId = table.Column<long>(nullable: false),
                    Servico = table.Column<string>(type: "varchar(1000)", nullable: false),
                    Obs = table.Column<string>(type: "varchar(255)", nullable: false),
                    Medico = table.Column<string>(type: "varchar(80)", nullable: false),
                    DataEntrega = table.Column<DateTime>(type: "date", nullable: false),
                    DataSolicitacao = table.Column<DateTime>(type: "date", nullable: false),
                    FormaPagamento = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    Preco = table.Column<decimal>(type: "numeric(10,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PedidoIdPk", x => x.PedidoId);
                    table.ForeignKey(
                        name: "FK_Pedidos_ApplicationUser_ApplicationUserId",
                        column: x => x.ApplicationUserId,
                        principalSchema: "public",
                        principalTable: "ApplicationUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pedidos_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "public",
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Faturas",
                schema: "public",
                columns: table => new
                {
                    FaturaId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Valor = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Total_A_Pagar = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    Sinal = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    IsPaga = table.Column<bool>(nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "date", nullable: false),
                    NumeroParcelas = table.Column<int>(type: "integer", nullable: false),
                    FormaPagamento = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: false),
                    PedidoId = table.Column<long>(nullable: false),
                    ClienteId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("FaturaIdPk", x => x.FaturaId);
                    table.ForeignKey(
                        name: "FK_Faturas_Clientes_ClienteId",
                        column: x => x.ClienteId,
                        principalSchema: "public",
                        principalTable: "Clientes",
                        principalColumn: "ClienteId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Faturas_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalSchema: "public",
                        principalTable: "Pedidos",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Oculos",
                schema: "public",
                columns: table => new
                {
                    OculosId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Cor = table.Column<string>(type: "varchar(30)", nullable: true),
                    DP = table.Column<float>(type: "float", nullable: false),
                    ALT = table.Column<float>(type: "float", nullable: false),
                    PedidoId = table.Column<long>(nullable: false),
                    Adicao = table.Column<float>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("OculosIdPk", x => x.OculosId);
                    table.ForeignKey(
                        name: "FK_Oculos_Pedidos_PedidoId",
                        column: x => x.PedidoId,
                        principalSchema: "public",
                        principalTable: "Pedidos",
                        principalColumn: "PedidoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parcelas",
                schema: "public",
                columns: table => new
                {
                    ParcelaId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Numero = table.Column<int>(type: "integer", nullable: false),
                    Valor = table.Column<decimal>(type: "numeric(10,2)", nullable: false),
                    DataVencimento = table.Column<DateTime>(type: "date", nullable: false),
                    DataPagamento = table.Column<DateTime>(type: "date", nullable: false),
                    Recebido = table.Column<bool>(type: "boolean", nullable: false),
                    FaturaId = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ParcelaIdPk", x => x.ParcelaId);
                    table.ForeignKey(
                        name: "FK_Parcelas_Faturas_FaturaId",
                        column: x => x.FaturaId,
                        principalSchema: "public",
                        principalTable: "Faturas",
                        principalColumn: "FaturaId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Lentes",
                schema: "public",
                columns: table => new
                {
                    LenteId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Grau = table.Column<float>(type: "float", nullable: false),
                    Cyl = table.Column<float>(type: "float", nullable: false),
                    Eixo = table.Column<byte>(type: "smallint", nullable: false),
                    OculosId = table.Column<long>(nullable: false),
                    LenteType = table.Column<string>(type: "varchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("LenteIdPk", x => x.LenteId);
                    table.ForeignKey(
                        name: "FK_Lentes_Oculos_OculosId",
                        column: x => x.OculosId,
                        principalSchema: "public",
                        principalTable: "Oculos",
                        principalColumn: "OculosId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_EnderecoId",
                schema: "public",
                table: "Clientes",
                column: "EnderecoId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturas_ClienteId",
                schema: "public",
                table: "Faturas",
                column: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Faturas_PedidoId",
                schema: "public",
                table: "Faturas",
                column: "PedidoId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lentes_OculosId",
                schema: "public",
                table: "Lentes",
                column: "OculosId");

            migrationBuilder.CreateIndex(
                name: "IX_Oculos_PedidoId",
                schema: "public",
                table: "Oculos",
                column: "PedidoId");

            migrationBuilder.CreateIndex(
                name: "IX_Parcelas_FaturaId",
                schema: "public",
                table: "Parcelas",
                column: "FaturaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ApplicationUserId",
                schema: "public",
                table: "Pedidos",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Pedidos_ClienteId",
                schema: "public",
                table: "Pedidos",
                column: "ClienteId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lentes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Parcelas",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Oculos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Faturas",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Pedidos",
                schema: "public");

            migrationBuilder.DropTable(
                name: "ApplicationUser",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Clientes",
                schema: "public");

            migrationBuilder.DropTable(
                name: "Enderecos",
                schema: "public");
        }
    }
}
