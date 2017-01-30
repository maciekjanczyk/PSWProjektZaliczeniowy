using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using PSWProjektZaliczeniowy.DAL;

namespace PSWProjektZaliczeniowy.Migrations
{
    [DbContext(typeof(LeniwiecContext))]
    [Migration("20170130184612_BezUzCostam")]
    partial class BezUzCostam
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.0-rtm-22752")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("PSWProjektZaliczeniowy.Model.Kategoria", b =>
                {
                    b.Property<int>("KategoriaId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Nazwa");

                    b.HasKey("KategoriaId");

                    b.ToTable("Kategoria");
                });

            modelBuilder.Entity("PSWProjektZaliczeniowy.Model.Obserwowane", b =>
                {
                    b.Property<int>("ObserwowaneId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("OgloszenieId");

                    b.Property<int?>("UzytkownikId");

                    b.HasKey("ObserwowaneId");

                    b.HasIndex("OgloszenieId");

                    b.HasIndex("UzytkownikId");

                    b.ToTable("Obserwowane");
                });

            modelBuilder.Entity("PSWProjektZaliczeniowy.Model.Ogloszenie", b =>
                {
                    b.Property<int>("OgloszenieId")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Cena");

                    b.Property<string>("Opis");

                    b.Property<int>("PodkategoriaId");

                    b.Property<string>("Stan");

                    b.Property<string>("Tytul");

                    b.Property<int>("UzytkownikId");

                    b.Property<bool>("Zamiana");

                    b.Property<string>("Zdjecia");

                    b.HasKey("OgloszenieId");

                    b.HasIndex("PodkategoriaId");

                    b.HasIndex("UzytkownikId");

                    b.ToTable("Ogloszenie");
                });

            modelBuilder.Entity("PSWProjektZaliczeniowy.Model.Podkategoria", b =>
                {
                    b.Property<int>("PodkategoriaId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("KategoriaId");

                    b.Property<string>("Nazwa");

                    b.HasKey("PodkategoriaId");

                    b.HasIndex("KategoriaId");

                    b.ToTable("Podkategoria");
                });

            modelBuilder.Entity("PSWProjektZaliczeniowy.Model.Uzytkownik", b =>
                {
                    b.Property<int>("UzytkownikId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Adres");

                    b.Property<string>("Email");

                    b.Property<string>("Haslo")
                        .IsRequired();

                    b.Property<string>("Imie");

                    b.Property<string>("Login")
                        .IsRequired();

                    b.Property<string>("Nazwisko");

                    b.Property<string>("Nrtel");

                    b.HasKey("UzytkownikId");

                    b.ToTable("Uzytkownik");
                });

            modelBuilder.Entity("PSWProjektZaliczeniowy.Model.Wiadomosc", b =>
                {
                    b.Property<int>("WiadomoscId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("Data");

                    b.Property<bool>("Odczytana");

                    b.Property<int>("ReceiverId");

                    b.Property<int>("SenderId");

                    b.Property<string>("Tekst");

                    b.Property<string>("Tytul");

                    b.HasKey("WiadomoscId");

                    b.ToTable("Wiadomosc");
                });

            modelBuilder.Entity("PSWProjektZaliczeniowy.Model.Obserwowane", b =>
                {
                    b.HasOne("PSWProjektZaliczeniowy.Model.Ogloszenie", "Ogloszenie")
                        .WithMany("Obserwowane")
                        .HasForeignKey("OgloszenieId");

                    b.HasOne("PSWProjektZaliczeniowy.Model.Uzytkownik", "Uzytkownik")
                        .WithMany("Obserwowane")
                        .HasForeignKey("UzytkownikId");
                });

            modelBuilder.Entity("PSWProjektZaliczeniowy.Model.Ogloszenie", b =>
                {
                    b.HasOne("PSWProjektZaliczeniowy.Model.Podkategoria", "Podkategoria")
                        .WithMany("Ogloszenie")
                        .HasForeignKey("PodkategoriaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("PSWProjektZaliczeniowy.Model.Uzytkownik", "Uzytkownik")
                        .WithMany("Ogloszenie")
                        .HasForeignKey("UzytkownikId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("PSWProjektZaliczeniowy.Model.Podkategoria", b =>
                {
                    b.HasOne("PSWProjektZaliczeniowy.Model.Kategoria", "Kategoria")
                        .WithMany("Podkategoria")
                        .HasForeignKey("KategoriaId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
