﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using SteamMicroservice.Model.Configuration;

#nullable disable

namespace SteamMicroservice.Migrations
{
    [DbContext(typeof(SteamDbContext))]
    [Migration("20240412082943_NewGameProperties")]
    partial class NewGameProperties
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Proxies:ChangeTracking", false)
                .HasAnnotation("Proxies:CheckEquality", false)
                .HasAnnotation("Proxies:LazyLoading", true)
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("GamePlayer", b =>
                {
                    b.Property<Guid>("GamesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PlayersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GamesId", "PlayersId");

                    b.HasIndex("PlayersId");

                    b.ToTable("GamePlayer");
                });

            modelBuilder.Entity("GameSteamCategory", b =>
                {
                    b.Property<Guid>("CategoriesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GamesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("CategoriesId", "GamesId");

                    b.HasIndex("GamesId");

                    b.ToTable("GameSteamCategory");
                });

            modelBuilder.Entity("GameSteamDeveloper", b =>
                {
                    b.Property<Guid>("DevelopersId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GamesId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("DevelopersId", "GamesId");

                    b.HasIndex("GamesId");

                    b.ToTable("GameSteamDeveloper");
                });

            modelBuilder.Entity("GameSteamGenre", b =>
                {
                    b.Property<Guid>("GamesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GenresId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GamesId", "GenresId");

                    b.HasIndex("GenresId");

                    b.ToTable("GameSteamGenre");
                });

            modelBuilder.Entity("GameSteamPublisher", b =>
                {
                    b.Property<Guid>("GamesId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("PublishersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("GamesId", "PublishersId");

                    b.HasIndex("PublishersId");

                    b.ToTable("GameSteamPublisher");
                });

            modelBuilder.Entity("SteamMicroservice.Model.Games.Game", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AboutGame")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CapsuleImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("CapsuleImageV5")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HeaderImage")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("IsFree")
                        .HasColumnType("bit");

                    b.Property<bool>("IsUpdated")
                        .HasColumnType("bit");

                    b.Property<string>("Languages")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("LastUpdateDate")
                        .HasColumnType("datetime2");

                    b.Property<bool?>("Linux")
                        .HasColumnType("bit");

                    b.Property<bool?>("MacOS")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long?>("Recomendations")
                        .HasColumnType("bigint");

                    b.Property<int?>("RequiredAge")
                        .HasColumnType("int");

                    b.Property<string>("ShortDescription")
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SteamId")
                        .HasColumnType("bigint");

                    b.Property<int?>("Type")
                        .HasColumnType("int");

                    b.Property<string>("Website")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool?>("Windows")
                        .HasColumnType("bit");

                    b.HasKey("Id");

                    b.ToTable("Games", (string)null);
                });

            modelBuilder.Entity("SteamMicroservice.Model.Games.SteamCategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SteamId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Categories", (string)null);
                });

            modelBuilder.Entity("SteamMicroservice.Model.Games.SteamDeveloper", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Developers", (string)null);
                });

            modelBuilder.Entity("SteamMicroservice.Model.Games.SteamGenre", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<long>("SteamId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.ToTable("Genres", (string)null);
                });

            modelBuilder.Entity("SteamMicroservice.Model.Games.SteamPublisher", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Publishers", (string)null);
                });

            modelBuilder.Entity("SteamMicroservice.Model.Games.SteamRequirement", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Minimum")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Recomended")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Type")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Requirements", (string)null);
                });

            modelBuilder.Entity("SteamMicroservice.Model.Games.SteamScreenshot", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Full")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<Guid>("GameId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("SteamId")
                        .HasColumnType("bigint");

                    b.Property<string>("Thumbnail")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("GameId");

                    b.ToTable("Screenshots", (string)null);
                });

            modelBuilder.Entity("SteamMicroservice.Model.Users.Player", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("avatar")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("avatarfull")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("avatarhash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("avatarmedium")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("commentpermission")
                        .HasColumnType("int");

                    b.Property<int>("communityvisibilitystate")
                        .HasColumnType("int");

                    b.Property<int>("lastlogoff")
                        .HasColumnType("int");

                    b.Property<int>("loccityid")
                        .HasColumnType("int");

                    b.Property<string>("loccountrycode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("locstatecode")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("personaname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("personastate")
                        .HasColumnType("int");

                    b.Property<int>("personastateflags")
                        .HasColumnType("int");

                    b.Property<string>("primaryclanid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("profilestate")
                        .HasColumnType("int");

                    b.Property<string>("profileurl")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("realname")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("steamid")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("timecreated")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Players", (string)null);
                });

            modelBuilder.Entity("GamePlayer", b =>
                {
                    b.HasOne("SteamMicroservice.Model.Games.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SteamMicroservice.Model.Users.Player", null)
                        .WithMany()
                        .HasForeignKey("PlayersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameSteamCategory", b =>
                {
                    b.HasOne("SteamMicroservice.Model.Games.SteamCategory", null)
                        .WithMany()
                        .HasForeignKey("CategoriesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SteamMicroservice.Model.Games.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameSteamDeveloper", b =>
                {
                    b.HasOne("SteamMicroservice.Model.Games.SteamDeveloper", null)
                        .WithMany()
                        .HasForeignKey("DevelopersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SteamMicroservice.Model.Games.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameSteamGenre", b =>
                {
                    b.HasOne("SteamMicroservice.Model.Games.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SteamMicroservice.Model.Games.SteamGenre", null)
                        .WithMany()
                        .HasForeignKey("GenresId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("GameSteamPublisher", b =>
                {
                    b.HasOne("SteamMicroservice.Model.Games.Game", null)
                        .WithMany()
                        .HasForeignKey("GamesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("SteamMicroservice.Model.Games.SteamPublisher", null)
                        .WithMany()
                        .HasForeignKey("PublishersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("SteamMicroservice.Model.Games.Game", b =>
                {
                    b.OwnsOne("SteamMicroservice.Model.Games.SteamPrice", "Price", b1 =>
                        {
                            b1.Property<Guid>("GameId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<string>("Currency")
                                .HasColumnType("nvarchar(max)");

                            b1.Property<long?>("Discount")
                                .HasColumnType("bigint");

                            b1.Property<long?>("Final")
                                .HasColumnType("bigint");

                            b1.Property<long?>("Initial")
                                .HasColumnType("bigint");

                            b1.HasKey("GameId");

                            b1.ToTable("Games");

                            b1.WithOwner()
                                .HasForeignKey("GameId");
                        });

                    b.OwnsOne("SteamMicroservice.Model.Games.SteamReleaseDate", "ReleaseDate", b1 =>
                        {
                            b1.Property<Guid>("GameId")
                                .HasColumnType("uniqueidentifier");

                            b1.Property<bool>("ComingSoon")
                                .HasColumnType("bit");

                            b1.Property<DateTime>("Date")
                                .HasColumnType("datetime2");

                            b1.HasKey("GameId");

                            b1.ToTable("Games");

                            b1.WithOwner()
                                .HasForeignKey("GameId");
                        });

                    b.Navigation("Price");

                    b.Navigation("ReleaseDate");
                });

            modelBuilder.Entity("SteamMicroservice.Model.Games.SteamRequirement", b =>
                {
                    b.HasOne("SteamMicroservice.Model.Games.Game", "Game")
                        .WithMany("Requirements")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("SteamMicroservice.Model.Games.SteamScreenshot", b =>
                {
                    b.HasOne("SteamMicroservice.Model.Games.Game", "Game")
                        .WithMany("Screenshots")
                        .HasForeignKey("GameId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Game");
                });

            modelBuilder.Entity("SteamMicroservice.Model.Games.Game", b =>
                {
                    b.Navigation("Requirements");

                    b.Navigation("Screenshots");
                });
#pragma warning restore 612, 618
        }
    }
}
