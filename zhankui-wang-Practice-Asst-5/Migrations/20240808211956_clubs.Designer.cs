﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using zhankui_wang_Practice_Asst_5.Models;

#nullable disable

namespace zhankui_wang_Practice_Asst_5.Migrations
{
    [DbContext(typeof(PracticeAsst5DbContext))]
    [Migration("20240808211956_clubs")]
    partial class clubs
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.7")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("BridgeClub", b =>
                {
                    b.Property<int>("ClubID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ClubID"));

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("ClubName")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("ClubID")
                        .HasName("PK__BridgeClubs__3214EC27");

                    b.HasIndex("CityName");

                    b.ToTable("Clubs");
                });

            modelBuilder.Entity("UserClub", b =>
                {
                    b.Property<int>("ClubId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ClubId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserClub");
                });

            modelBuilder.Entity("zhankui_wang_Practice_Asst_5.Models.City", b =>
                {
                    b.Property<string>("Name")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<int>("Id")
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    b.Property<string>("ProvCode")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.HasKey("Name")
                        .HasName("PK__Cities__737584F6A3497E13");

                    b.HasIndex("ProvCode");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("zhankui_wang_Practice_Asst_5.Models.Province", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.HasKey("Id")
                        .HasName("PK__Province__3214EC2736CD3DBF");

                    b.HasIndex("Code")
                        .IsUnique();

                    b.ToTable("Provinces");
                });

            modelBuilder.Entity("zhankui_wang_Practice_Asst_5.Models.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("ID");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("CityName")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PostalCode")
                        .IsRequired()
                        .HasMaxLength(6)
                        .HasColumnType("nvarchar(6)");

                    b.HasKey("Id")
                        .HasName("PK__Users__3214EC275723A073");

                    b.HasIndex("CityName");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("BridgeClub", b =>
                {
                    b.HasOne("zhankui_wang_Practice_Asst_5.Models.City", "City")
                        .WithMany("Clubs")
                        .HasForeignKey("CityName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Clubs_Cities");

                    b.Navigation("City");
                });

            modelBuilder.Entity("UserClub", b =>
                {
                    b.HasOne("BridgeClub", null)
                        .WithMany()
                        .HasForeignKey("ClubId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("zhankui_wang_Practice_Asst_5.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("zhankui_wang_Practice_Asst_5.Models.City", b =>
                {
                    b.HasOne("zhankui_wang_Practice_Asst_5.Models.Province", "ProvCodeNavigation")
                        .WithMany("Cities")
                        .HasForeignKey("ProvCode")
                        .HasPrincipalKey("Code")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Cities_Provinces");

                    b.Navigation("ProvCodeNavigation");
                });

            modelBuilder.Entity("zhankui_wang_Practice_Asst_5.Models.User", b =>
                {
                    b.HasOne("zhankui_wang_Practice_Asst_5.Models.City", "CityNameNavigation")
                        .WithMany("Users")
                        .HasForeignKey("CityName")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK_Users_Cities");

                    b.Navigation("CityNameNavigation");
                });

            modelBuilder.Entity("zhankui_wang_Practice_Asst_5.Models.City", b =>
                {
                    b.Navigation("Clubs");

                    b.Navigation("Users");
                });

            modelBuilder.Entity("zhankui_wang_Practice_Asst_5.Models.Province", b =>
                {
                    b.Navigation("Cities");
                });
#pragma warning restore 612, 618
        }
    }
}