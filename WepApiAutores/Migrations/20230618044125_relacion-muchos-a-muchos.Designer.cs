﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WepApiAutores;

#nullable disable

namespace WepApiAutores.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20230618044125_relacion-muchos-a-muchos")]
    partial class relacionmuchosamuchos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.16")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WepApiAutores.Entidades.Autor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Autors");
                });

            modelBuilder.Entity("WepApiAutores.Entidades.AutoresLibros", b =>
                {
                    b.Property<int>("AutorId")
                        .HasColumnType("int");

                    b.Property<int>("LibrosId")
                        .HasColumnType("int");

                    b.HasKey("AutorId", "LibrosId");

                    b.HasIndex("LibrosId");

                    b.ToTable("AutoresLibros");
                });

            modelBuilder.Entity("WepApiAutores.Entidades.Comentario", b =>
                {
                    b.Property<int>("ID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ID"), 1L, 1);

                    b.Property<string>("Contenido")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("librosId")
                        .HasColumnType("int");

                    b.HasKey("ID");

                    b.HasIndex("librosId");

                    b.ToTable("Comentarios");
                });

            modelBuilder.Entity("WepApiAutores.Entidades.Libros", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Titulo")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Libros");
                });

            modelBuilder.Entity("WepApiAutores.Entidades.AutoresLibros", b =>
                {
                    b.HasOne("WepApiAutores.Entidades.Autor", "Autor")
                        .WithMany("autorLibro")
                        .HasForeignKey("AutorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("WepApiAutores.Entidades.Libros", "Libros")
                        .WithMany("autorLibro")
                        .HasForeignKey("LibrosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Autor");

                    b.Navigation("Libros");
                });

            modelBuilder.Entity("WepApiAutores.Entidades.Comentario", b =>
                {
                    b.HasOne("WepApiAutores.Entidades.Libros", "Libros")
                        .WithMany("Comentarios")
                        .HasForeignKey("librosId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Libros");
                });

            modelBuilder.Entity("WepApiAutores.Entidades.Autor", b =>
                {
                    b.Navigation("autorLibro");
                });

            modelBuilder.Entity("WepApiAutores.Entidades.Libros", b =>
                {
                    b.Navigation("Comentarios");

                    b.Navigation("autorLibro");
                });
#pragma warning restore 612, 618
        }
    }
}