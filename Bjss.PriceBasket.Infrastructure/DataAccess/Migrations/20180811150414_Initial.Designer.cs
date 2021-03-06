﻿// <auto-generated />
using Bjss.PriceBasket.Infrastructure.DataAccess.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace Bjss.PriceBasket.Infrastructure.DataAccess.Migrations
{
    [DbContext(typeof(PriceBasketContext))]
    [Migration("20180811150414_Initial")]
    partial class Initial
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Bjss.PriceBasket.Core.Models.BuyTwoGetOneHalfPriceSpecialOffer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid>("BuyTwoOfProductId");

                    b.Property<decimal>("DiscountPercent");

                    b.Property<Guid>("DiscountedProductId");

                    b.Property<Guid>("SpecialOfferId");

                    b.HasKey("Id");

                    b.HasIndex("BuyTwoOfProductId");

                    b.HasIndex("DiscountedProductId");

                    b.HasIndex("SpecialOfferId");

                    b.ToTable("BuyTwoGetOneHalfPriceSpecialOffers");
                });

            modelBuilder.Entity("Bjss.PriceBasket.Core.Models.DiscountedProductsSpecialOffer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("DiscountPercent");

                    b.Property<Guid>("DiscountedProductId");

                    b.Property<Guid>("SpecialOfferId");

                    b.HasKey("Id");

                    b.HasIndex("DiscountedProductId");

                    b.HasIndex("SpecialOfferId");

                    b.ToTable("DiscountedProductsSpecialOffers");
                });

            modelBuilder.Entity("Bjss.PriceBasket.Core.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<decimal>("Price");

                    b.Property<int>("QuantityAvailable");

                    b.HasKey("Id");

                    b.ToTable("Products");
                });

            modelBuilder.Entity("Bjss.PriceBasket.Core.Models.SpecialOffer", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.Property<string>("DiscountAssemblyFullName");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("SpecialOffers");
                });

            modelBuilder.Entity("Bjss.PriceBasket.Core.Models.BuyTwoGetOneHalfPriceSpecialOffer", b =>
                {
                    b.HasOne("Bjss.PriceBasket.Core.Models.Product", "BuyTwoOfProduct")
                        .WithMany()
                        .HasForeignKey("BuyTwoOfProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Bjss.PriceBasket.Core.Models.Product", "DiscountedProduct")
                        .WithMany()
                        .HasForeignKey("DiscountedProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Bjss.PriceBasket.Core.Models.SpecialOffer", "SpecialOffer")
                        .WithMany()
                        .HasForeignKey("SpecialOfferId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Bjss.PriceBasket.Core.Models.DiscountedProductsSpecialOffer", b =>
                {
                    b.HasOne("Bjss.PriceBasket.Core.Models.Product", "DiscountedProduct")
                        .WithMany()
                        .HasForeignKey("DiscountedProductId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("Bjss.PriceBasket.Core.Models.SpecialOffer", "SpecialOffer")
                        .WithMany()
                        .HasForeignKey("SpecialOfferId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
