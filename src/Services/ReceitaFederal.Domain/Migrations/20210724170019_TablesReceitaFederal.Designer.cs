﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ReceitaFederal.Domain.Models;

namespace ReceitaFederal.Domain.Migrations
{
    [DbContext(typeof(ReceitaFederalDbContext))]
    [Migration("20210724170019_TablesReceitaFederal")]
    partial class TablesReceitaFederal
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.8")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ReceitaFederal.Domain.Models.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Atualizacao")
                        .HasColumnType("datetime2");

                    b.Property<decimal>("CapitalSocial")
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<DateTime?>("InicioAtividade")
                        .HasColumnType("datetime2");

                    b.Property<int?>("MotivoSituacaoCadastralId")
                        .HasColumnType("int");

                    b.Property<string>("NomeFantasia")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("RazaoSocial")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<int>("SituacaoCadastral")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Cnpj");

                    b.HasIndex("MotivoSituacaoCadastralId");

                    b.ToTable("Empresas");
                });

            modelBuilder.Entity("ReceitaFederal.Domain.Models.MotivoSituacaoCadastral", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("DescricaoMotivo")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("MotivosSituacaoCadastral");
                });

            modelBuilder.Entity("ReceitaFederal.Domain.Models.ReceitaFederalArquivo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Atualizacao")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Lido")
                        .HasColumnType("bit");

                    b.Property<string>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.HasKey("Id");

                    b.ToTable("ReceitaFederalArquivos");
                });

            modelBuilder.Entity("ReceitaFederal.Domain.Models.Socio", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("EmpresaId")
                        .HasColumnType("int");

                    b.Property<int>("IdEmpresa")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Numero")
                        .IsRequired()
                        .HasMaxLength(18)
                        .HasColumnType("nvarchar(18)");

                    b.Property<int>("TipoSocio")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasAlternateKey("Numero");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Socios");
                });

            modelBuilder.Entity("ReceitaFederal.Domain.Models.Empresa", b =>
                {
                    b.HasOne("ReceitaFederal.Domain.Models.MotivoSituacaoCadastral", "MotivoSituacaoCadastral")
                        .WithMany()
                        .HasForeignKey("MotivoSituacaoCadastralId");

                    b.Navigation("MotivoSituacaoCadastral");
                });

            modelBuilder.Entity("ReceitaFederal.Domain.Models.Socio", b =>
                {
                    b.HasOne("ReceitaFederal.Domain.Models.Empresa", "Empresa")
                        .WithMany()
                        .HasForeignKey("EmpresaId");

                    b.Navigation("Empresa");
                });
#pragma warning restore 612, 618
        }
    }
}
