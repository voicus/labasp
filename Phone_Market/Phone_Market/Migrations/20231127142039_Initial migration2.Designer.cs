﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Phone_Market.Models;

#nullable disable

namespace Phone_Market.Migrations
{
    [DbContext(typeof(Phone_MarketContext))]
    [Migration("20231127142039_Initial migration2")]
    partial class Initialmigration2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Phone_Market.Models.Brand", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__Brand__3214EC070667706E");

                    b.ToTable("Brand", (string)null);
                });

            modelBuilder.Entity("Phone_Market.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.HasKey("Id")
                        .HasName("PK__Category__3214EC07D341E9FB");

                    b.ToTable("Category", (string)null);
                });

            modelBuilder.Entity("Phone_Market.Models.Color", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("Id")
                        .HasName("PK__Color__3214EC075A1323B3");

                    b.ToTable("Color", (string)null);
                });

            modelBuilder.Entity("Phone_Market.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte[]>("Picture")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("PK__Image__3214EC076F355951");

                    b.HasIndex("ProductId");

                    b.ToTable("Image", (string)null);
                });

            modelBuilder.Entity("Phone_Market.Models.OrderedItem", b =>
                {
                    b.Property<int>("OrderId")
                        .HasColumnType("int");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<double>("Quantity")
                        .HasColumnType("float");

                    b.HasKey("OrderId", "ProductId")
                        .HasName("PK__OrderedI__08D097A34F814A42");

                    b.HasIndex("ProductId");

                    b.ToTable("OrderedItems");
                });

            modelBuilder.Entity("Phone_Market.Models.Product", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("BrandId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.Property<int>("ColorId")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<int>("Discount")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<double>("Price")
                        .HasColumnType("float");

                    b.HasKey("Id")
                        .HasName("PK__Product__3214EC0713AD3DC1");

                    b.HasIndex("BrandId");

                    b.HasIndex("CategoryId");

                    b.HasIndex("ColorId");

                    b.ToTable("Product", (string)null);
                });

            modelBuilder.Entity("Phone_Market.Models.Receipt", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTimeOffset>("Date")
                        .HasColumnType("datetimeoffset");

                    b.Property<double>("TotalPrice")
                        .HasColumnType("float");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id")
                        .HasName("PK__Receipt__3214EC0794859DAB");

                    b.HasIndex("UserId");

                    b.ToTable("Receipt", (string)null);
                });

            modelBuilder.Entity("Phone_Market.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Role1")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)")
                        .HasColumnName("Role");

                    b.HasKey("Id")
                        .HasName("PK__Role__3214EC074D21B865");

                    b.ToTable("Role", (string)null);
                });

            modelBuilder.Entity("Phone_Market.Models.ShoppingCart", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<int>("Quantity")
                        .HasColumnType("int");

                    b.HasKey("UserId", "ProductId")
                        .HasName("PK__Shopping__DCC8002006EC247D");

                    b.HasIndex("ProductId");

                    b.ToTable("ShoppingCart", (string)null);
                });

            modelBuilder.Entity("Phone_Market.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.HasKey("Id")
                        .HasName("PK__User__3214EC070BEE535E");

                    b.HasIndex("RoleId");

                    b.HasIndex(new[] { "Email" }, "Unique_Email")
                        .IsUnique();

                    b.HasIndex(new[] { "UserName" }, "Unique_UserName")
                        .IsUnique();

                    b.ToTable("User", (string)null);
                });

            modelBuilder.Entity("WishList", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("UserId", "ProductId")
                        .HasName("PK__WishList__DCC8002086A9EA2B");

                    b.HasIndex("ProductId");

                    b.ToTable("WishList", (string)null);
                });

            modelBuilder.Entity("Phone_Market.Models.Image", b =>
                {
                    b.HasOne("Phone_Market.Models.Product", "Product")
                        .WithMany("Images")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Image__ProductId__35BCFE0A");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Phone_Market.Models.OrderedItem", b =>
                {
                    b.HasOne("Phone_Market.Models.Receipt", "Order")
                        .WithMany("OrderedItems")
                        .HasForeignKey("OrderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__OrderedIt__Order__440B1D61");

                    b.HasOne("Phone_Market.Models.Product", "Product")
                        .WithMany("OrderedItems")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__OrderedIt__Produ__4316F928");

                    b.Navigation("Order");

                    b.Navigation("Product");
                });

            modelBuilder.Entity("Phone_Market.Models.Product", b =>
                {
                    b.HasOne("Phone_Market.Models.Brand", "Brand")
                        .WithMany("Products")
                        .HasForeignKey("BrandId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Product__BrandId__30F848ED");

                    b.HasOne("Phone_Market.Models.Category", "Category")
                        .WithMany("Products")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Product__Categor__31EC6D26");

                    b.HasOne("Phone_Market.Models.Color", "Color")
                        .WithMany("Products")
                        .HasForeignKey("ColorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Product__ColorId__32E0915F");

                    b.Navigation("Brand");

                    b.Navigation("Category");

                    b.Navigation("Color");
                });

            modelBuilder.Entity("Phone_Market.Models.Receipt", b =>
                {
                    b.HasOne("Phone_Market.Models.User", "User")
                        .WithMany("Receipts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__Receipt__UserId__403A8C7D");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Phone_Market.Models.ShoppingCart", b =>
                {
                    b.HasOne("Phone_Market.Models.Product", "Product")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__ShoppingC__Produ__398D8EEE");

                    b.HasOne("Phone_Market.Models.User", "User")
                        .WithMany("ShoppingCarts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__ShoppingC__UserI__38996AB5");

                    b.Navigation("Product");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Phone_Market.Models.User", b =>
                {
                    b.HasOne("Phone_Market.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__User__RoleId__2E1BDC42");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("WishList", b =>
                {
                    b.HasOne("Phone_Market.Models.Product", null)
                        .WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__WishList__Produc__3D5E1FD2");

                    b.HasOne("Phone_Market.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired()
                        .HasConstraintName("FK__WishList__UserId__3C69FB99");
                });

            modelBuilder.Entity("Phone_Market.Models.Brand", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Phone_Market.Models.Category", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Phone_Market.Models.Color", b =>
                {
                    b.Navigation("Products");
                });

            modelBuilder.Entity("Phone_Market.Models.Product", b =>
                {
                    b.Navigation("Images");

                    b.Navigation("OrderedItems");

                    b.Navigation("ShoppingCarts");
                });

            modelBuilder.Entity("Phone_Market.Models.Receipt", b =>
                {
                    b.Navigation("OrderedItems");
                });

            modelBuilder.Entity("Phone_Market.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Phone_Market.Models.User", b =>
                {
                    b.Navigation("Receipts");

                    b.Navigation("ShoppingCarts");
                });
#pragma warning restore 612, 618
        }
    }
}
