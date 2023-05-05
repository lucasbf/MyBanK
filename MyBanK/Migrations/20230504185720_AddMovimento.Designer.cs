﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using MyBanK.Data;

#nullable disable

namespace MyBanK.Migrations
{
    [DbContext(typeof(MyBanKContext))]
    [Migration("20230504185720_AddMovimento")]
    partial class AddMovimento
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MyBanK.Models.ContaCorrente", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("DataAbertura")
                        .HasColumnType("datetime2");

                    b.Property<string>("Titular")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("ContasCorrentes");
                });

            modelBuilder.Entity("MyBanK.Models.Movimento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ContaCorrenteId")
                        .HasColumnType("int");

                    b.Property<string>("Descr")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("Ocorrencia")
                        .IsRequired()
                        .HasColumnType("datetime2");

                    b.Property<double>("Valor")
                        .HasColumnType("float");

                    b.HasKey("Id");

                    b.HasIndex("ContaCorrenteId");

                    b.ToTable("Movimentos");
                });

            modelBuilder.Entity("MyBanK.Models.Movimento", b =>
                {
                    b.HasOne("MyBanK.Models.ContaCorrente", null)
                        .WithMany("Movimentos")
                        .HasForeignKey("ContaCorrenteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("MyBanK.Models.ContaCorrente", b =>
                {
                    b.Navigation("Movimentos");
                });
#pragma warning restore 612, 618
        }
    }
}