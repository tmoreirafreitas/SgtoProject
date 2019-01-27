﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Sgot.Infra.Data.Context;

namespace Sgot.Infra.Data.Migrations.SgotDb
{
    [DbContext(typeof(SgotDbContext))]
    [Migration("20190125155906_RemoveEnderecoMappingSgotDbContext")]
    partial class RemoveEnderecoMappingSgotDbContext
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("public")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn)
                .HasAnnotation("ProductVersion", "2.2.0-rtm-35687")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("Sgot.Domain.Entities.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<DateTime>("CreateDate");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FullName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("Id");

                    b.ToTable("ApplicationUser");
                });

            modelBuilder.Entity("Sgot.Domain.Entities.Cliente", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ClienteId");

                    b.Property<string>("Bairro")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Cep")
                        .IsRequired()
                        .HasColumnType("varchar(9)")
                        .HasMaxLength(9);

                    b.Property<string>("Cidade")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Complemento")
                        .HasColumnType("varchar(100)")
                        .HasMaxLength(100);

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasColumnType("varchar(15)")
                        .HasMaxLength(15);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40);

                    b.Property<string>("Estado")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.Property<string>("Filiacao")
                        .HasColumnType("varchar(150)")
                        .HasMaxLength(150);

                    b.Property<bool>("IsSPC");

                    b.Property<string>("Logradouro")
                        .IsRequired()
                        .HasColumnType("varchar(40)")
                        .HasMaxLength(40);

                    b.Property<DateTime>("Nascimento")
                        .HasColumnType("date");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(60)")
                        .HasMaxLength(60);

                    b.Property<int?>("Numero")
                        .HasColumnType("integer");

                    b.Property<string>("Rg")
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Sexo")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Telefone")
                        .IsRequired()
                        .HasColumnType("varchar(30)")
                        .HasMaxLength(30);

                    b.HasKey("Id")
                        .HasName("ClienteIdPk");

                    b.ToTable("Clientes");
                });

            modelBuilder.Entity("Sgot.Domain.Entities.Fatura", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("FaturaId");

                    b.Property<long>("ClienteId");

                    b.Property<DateTime>("DataPagamento")
                        .HasColumnType("date");

                    b.Property<string>("FormaPagamento")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<bool>("IsPaga");

                    b.Property<int>("NumeroParcelas")
                        .HasColumnType("integer");

                    b.Property<long>("PedidoId");

                    b.Property<decimal>("Sinal")
                        .HasColumnType("numeric(10,2)");

                    b.Property<decimal>("Total")
                        .HasColumnName("Total_A_Pagar")
                        .HasColumnType("numeric(10,2)");

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric(10,2)");

                    b.HasKey("Id")
                        .HasName("FaturaIdPk");

                    b.HasIndex("ClienteId");

                    b.HasIndex("PedidoId")
                        .IsUnique();

                    b.ToTable("Faturas");
                });

            modelBuilder.Entity("Sgot.Domain.Entities.Lente", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("LenteId");

                    b.Property<float>("Cyl")
                        .HasColumnType("float");

                    b.Property<byte>("Eixo")
                        .HasColumnType("smallint");

                    b.Property<float>("Grau")
                        .HasColumnType("float");

                    b.Property<string>("LenteType")
                        .IsRequired()
                        .HasColumnType("varchar(2)")
                        .HasMaxLength(2);

                    b.Property<long>("OculosId");

                    b.HasKey("Id")
                        .HasName("LenteIdPk");

                    b.HasIndex("OculosId");

                    b.ToTable("Lentes");
                });

            modelBuilder.Entity("Sgot.Domain.Entities.Oculos", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("OculosId");

                    b.Property<float>("ALT")
                        .HasColumnType("float");

                    b.Property<float>("Adicao")
                        .HasColumnType("float");

                    b.Property<string>("Cor")
                        .HasColumnType("varchar(30)");

                    b.Property<float>("DP")
                        .HasColumnType("float");

                    b.Property<long>("PedidoId");

                    b.HasKey("Id")
                        .HasName("OculosIdPk");

                    b.HasIndex("PedidoId");

                    b.ToTable("Oculos");
                });

            modelBuilder.Entity("Sgot.Domain.Entities.Parcela", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("ParcelaId");

                    b.Property<DateTime>("DataPagamento")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataVencimento")
                        .HasColumnType("date");

                    b.Property<long>("FaturaId");

                    b.Property<int>("Numero")
                        .HasColumnType("integer");

                    b.Property<bool>("Recebido")
                        .HasColumnType("boolean");

                    b.Property<decimal>("Valor")
                        .HasColumnType("numeric(10,2)");

                    b.HasKey("Id")
                        .HasName("ParcelaIdPk");

                    b.HasIndex("FaturaId");

                    b.ToTable("Parcelas");
                });

            modelBuilder.Entity("Sgot.Domain.Entities.Pedido", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnName("PedidoId");

                    b.Property<string>("ApplicationUserId");

                    b.Property<long>("ClienteId");

                    b.Property<DateTime>("DataEntrega")
                        .HasColumnType("date");

                    b.Property<DateTime>("DataSolicitacao")
                        .HasColumnType("date");

                    b.Property<long>("FaturaId");

                    b.Property<string>("FormaPagamento")
                        .IsRequired()
                        .HasColumnType("varchar(10)")
                        .HasMaxLength(10);

                    b.Property<string>("Medico")
                        .IsRequired()
                        .HasColumnType("varchar(80)");

                    b.Property<string>("Obs")
                        .IsRequired()
                        .HasColumnType("varchar(255)");

                    b.Property<decimal>("Preco")
                        .HasColumnType("numeric(10,2)");

                    b.Property<string>("Servico")
                        .IsRequired()
                        .HasColumnType("varchar(1000)");

                    b.HasKey("Id")
                        .HasName("PedidoIdPk");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ClienteId");

                    b.ToTable("Pedidos");
                });

            modelBuilder.Entity("Sgot.Domain.Entities.Fatura", b =>
                {
                    b.HasOne("Sgot.Domain.Entities.Cliente", "Cliente")
                        .WithMany("Faturas")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Sgot.Domain.Entities.Pedido", "Pedido")
                        .WithOne("Fatura")
                        .HasForeignKey("Sgot.Domain.Entities.Fatura", "PedidoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sgot.Domain.Entities.Lente", b =>
                {
                    b.HasOne("Sgot.Domain.Entities.Oculos", "Oculos")
                        .WithMany("Lentes")
                        .HasForeignKey("OculosId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sgot.Domain.Entities.Oculos", b =>
                {
                    b.HasOne("Sgot.Domain.Entities.Pedido", "Pedido")
                        .WithMany("Oculos")
                        .HasForeignKey("PedidoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sgot.Domain.Entities.Parcela", b =>
                {
                    b.HasOne("Sgot.Domain.Entities.Fatura", "Fatura")
                        .WithMany("Parcelas")
                        .HasForeignKey("FaturaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Sgot.Domain.Entities.Pedido", b =>
                {
                    b.HasOne("Sgot.Domain.Entities.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Sgot.Domain.Entities.Cliente", "Cliente")
                        .WithMany("Pedidos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
