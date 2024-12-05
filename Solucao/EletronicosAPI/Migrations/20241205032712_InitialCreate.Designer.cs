﻿// <auto-generated />
using EletronicosAPI.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EletronicosAPI.Migrations
{
    [DbContext(typeof(AppDataContext))]
    [Migration("20241205032712_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.0");

            modelBuilder.Entity("EletronicosAPI.models.Eletronicos", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Categoria")
                        .HasColumnType("TEXT");

                    b.Property<string>("Nome")
                        .HasColumnType("TEXT");

                    b.Property<double?>("Valor")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Eletronicos");
                });
#pragma warning restore 612, 618
        }
    }
}
