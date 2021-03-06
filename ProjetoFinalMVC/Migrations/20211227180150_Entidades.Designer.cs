// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ProjetoFinalMVC.Migrations
{
    [DbContext(typeof(Contexto))]
    [Migration("20211227180150_Entidades")]
    partial class Entidades
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.13")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ProjetoFinalMVC.Models.Consulta", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DoutorId")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("DoutorId");

                    b.ToTable("Consulta");
                });

            modelBuilder.Entity("ProjetoFinalMVC.Models.Doutor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("EspecializacaoId")
                        .HasColumnType("int");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefone")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("EspecializacaoId");

                    b.ToTable("Doutor");
                });

            modelBuilder.Entity("ProjetoFinalMVC.Models.Especializacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Especializacao");
                });

            modelBuilder.Entity("ProjetoFinalMVC.Models.Consulta", b =>
                {
                    b.HasOne("ProjetoFinalMVC.Models.Doutor", "Doutor")
                        .WithMany("Consultas")
                        .HasForeignKey("DoutorId");

                    b.Navigation("Doutor");
                });

            modelBuilder.Entity("ProjetoFinalMVC.Models.Doutor", b =>
                {
                    b.HasOne("ProjetoFinalMVC.Models.Especializacao", "Especializacao")
                        .WithMany("Doutores")
                        .HasForeignKey("EspecializacaoId");

                    b.Navigation("Especializacao");
                });

            modelBuilder.Entity("ProjetoFinalMVC.Models.Doutor", b =>
                {
                    b.Navigation("Consultas");
                });

            modelBuilder.Entity("ProjetoFinalMVC.Models.Especializacao", b =>
                {
                    b.Navigation("Doutores");
                });
#pragma warning restore 612, 618
        }
    }
}
