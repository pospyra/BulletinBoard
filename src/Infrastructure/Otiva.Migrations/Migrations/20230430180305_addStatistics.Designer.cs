﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Otiva.Migrations;

#nullable disable

namespace Otiva.Migrations.Migrations
{
    [DbContext(typeof(MigrationsDbContext))]
    [Migration("20230430180305_addStatistics")]
    partial class addStatistics
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("text");

                    b.Property<string>("RoleId")
                        .HasColumnType("text");

                    b.HasKey("UserId", "RoleId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Otiva.Domain.Ads.Ad", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasMaxLength(800)
                        .HasColumnType("character varying(800)");

                    b.Property<Guid?>("DomainUserId")
                        .HasColumnType("uuid");

                    b.Property<bool>("IsActive")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<decimal>("Price")
                        .HasColumnType("numeric");

                    b.Property<string>("Region")
                        .HasColumnType("text");

                    b.Property<Guid?>("StatisticsTableAdsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SubcategoryId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DomainUserId");

                    b.HasIndex("StatisticsTableAdsId");

                    b.HasIndex("SubcategoryId");

                    b.ToTable("Ad");
                });

            modelBuilder.Entity("Otiva.Domain.Ads.StatisticsTableAds", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AdId")
                        .HasColumnType("uuid");

                    b.Property<int>("QuantityAddToFavorites")
                        .HasColumnType("integer");

                    b.Property<int>("QuantityView")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("StatisticsTableAds");
                });

            modelBuilder.Entity("Otiva.Domain.Category", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Category");
                });

            modelBuilder.Entity("Otiva.Domain.ItemSelectedAd", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AdId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateAdded")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DomainUserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("DomainUserId");

                    b.ToTable("ItemSelectedAd");
                });

            modelBuilder.Entity("Otiva.Domain.Message", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasColumnType("text");

                    b.Property<bool>("Read")
                        .HasColumnType("boolean");

                    b.Property<Guid>("ReceiverId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SenderId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("SendingTime")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("ReceiverId");

                    b.HasIndex("SenderId");

                    b.ToTable("Message");
                });

            modelBuilder.Entity("Otiva.Domain.Photos.PhotoAds", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("AdId")
                        .HasColumnType("uuid");

                    b.Property<string>("KodBase64")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AdId");

                    b.ToTable("PhotoAds");
                });

            modelBuilder.Entity("Otiva.Domain.Photos.PhotoUsers", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid?>("DomainUserId")
                        .HasColumnType("uuid");

                    b.Property<string>("KodBase64")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("DomainUserId");

                    b.ToTable("PhotoUsers");
                });

            modelBuilder.Entity("Otiva.Domain.Review", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Content")
                        .HasMaxLength(800)
                        .HasColumnType("character varying(800)");

                    b.Property<DateTime>("CreatedReview")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("CustomerId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("SellerId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("SellerId");

                    b.ToTable("Review");
                });

            modelBuilder.Entity("Otiva.Domain.Subcategory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("CategoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("CategoryId");

                    b.ToTable("Subcategory");
                });

            modelBuilder.Entity("Otiva.Domain.User.DomainUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("DateBirthday")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("DateRegistration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<string>("Region")
                        .HasColumnType("text");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("DomainUser");
                });

            modelBuilder.Entity("Otiva.Domain.User.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("integer");

                    b.Property<string>("ConcurrencyStamp")
                        .HasColumnType("text");

                    b.Property<DateTime>("DateRegistration")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("boolean");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("text");

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("text");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("text");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("text");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("boolean");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("text");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("UserName")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("IdentityUser");
                });

            modelBuilder.Entity("Otiva.Domain.Ads.Ad", b =>
                {
                    b.HasOne("Otiva.Domain.User.DomainUser", "DomainUser")
                        .WithMany("Ads")
                        .HasForeignKey("DomainUserId");

                    b.HasOne("Otiva.Domain.Ads.StatisticsTableAds", "StatisticsAds")
                        .WithMany("Ads")
                        .HasForeignKey("StatisticsTableAdsId");

                    b.HasOne("Otiva.Domain.Subcategory", "Subcategory")
                        .WithMany()
                        .HasForeignKey("SubcategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DomainUser");

                    b.Navigation("StatisticsAds");

                    b.Navigation("Subcategory");
                });

            modelBuilder.Entity("Otiva.Domain.ItemSelectedAd", b =>
                {
                    b.HasOne("Otiva.Domain.User.DomainUser", null)
                        .WithMany("ItemSelectedAds")
                        .HasForeignKey("DomainUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Otiva.Domain.Message", b =>
                {
                    b.HasOne("Otiva.Domain.User.DomainUser", "Receiver")
                        .WithMany("ReceivedMessages")
                        .HasForeignKey("ReceiverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Otiva.Domain.User.DomainUser", "Sender")
                        .WithMany("SentMessages")
                        .HasForeignKey("SenderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Receiver");

                    b.Navigation("Sender");
                });

            modelBuilder.Entity("Otiva.Domain.Photos.PhotoAds", b =>
                {
                    b.HasOne("Otiva.Domain.Ads.Ad", "Ad")
                        .WithMany("Photos")
                        .HasForeignKey("AdId");

                    b.Navigation("Ad");
                });

            modelBuilder.Entity("Otiva.Domain.Photos.PhotoUsers", b =>
                {
                    b.HasOne("Otiva.Domain.User.DomainUser", "DomainUser")
                        .WithMany("Photos")
                        .HasForeignKey("DomainUserId");

                    b.Navigation("DomainUser");
                });

            modelBuilder.Entity("Otiva.Domain.Review", b =>
                {
                    b.HasOne("Otiva.Domain.User.DomainUser", "DomainUser")
                        .WithMany("Reviews")
                        .HasForeignKey("SellerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("DomainUser");
                });

            modelBuilder.Entity("Otiva.Domain.Subcategory", b =>
                {
                    b.HasOne("Otiva.Domain.Category", "Category")
                        .WithMany("Subcategories")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");
                });

            modelBuilder.Entity("Otiva.Domain.Ads.Ad", b =>
                {
                    b.Navigation("Photos");
                });

            modelBuilder.Entity("Otiva.Domain.Ads.StatisticsTableAds", b =>
                {
                    b.Navigation("Ads");
                });

            modelBuilder.Entity("Otiva.Domain.Category", b =>
                {
                    b.Navigation("Subcategories");
                });

            modelBuilder.Entity("Otiva.Domain.User.DomainUser", b =>
                {
                    b.Navigation("Ads");

                    b.Navigation("ItemSelectedAds");

                    b.Navigation("Photos");

                    b.Navigation("ReceivedMessages");

                    b.Navigation("Reviews");

                    b.Navigation("SentMessages");
                });
#pragma warning restore 612, 618
        }
    }
}
