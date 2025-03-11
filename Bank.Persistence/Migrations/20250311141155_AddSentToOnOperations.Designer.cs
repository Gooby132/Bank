﻿// <auto-generated />
using System;
using Bank.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Bank.Persistence.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    [Migration("20250311141155_AddSentToOnOperations")]
    partial class AddSentToOnOperations
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Bank.Domain.Users.User", b =>
                {
                    b.Property<string>("Tz")
                        .HasColumnType("text")
                        .HasColumnName("tz");

                    b.HasKey("Tz");

                    b.ToTable("users", (string)null);
                });

            modelBuilder.Entity("Bank.Domain.Users.User", b =>
                {
                    b.OwnsOne("Bank.Domain.Users.Entities.Account", "Account", b1 =>
                        {
                            b1.Property<string>("UserTz")
                                .HasColumnType("text");

                            b1.Property<int>("Currency")
                                .HasColumnType("integer")
                                .HasColumnName("currency");

                            b1.Property<int>("Id")
                                .HasColumnType("integer")
                                .HasColumnName("id");

                            b1.HasKey("UserTz");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserTz");

                            b1.OwnsMany("Bank.Domain.Users.Entities.ValueObjects.Operation", "Operations", b2 =>
                                {
                                    b2.Property<string>("AccountUserTz")
                                        .HasColumnType("text");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b2.Property<int>("Id"));

                                    b2.Property<int>("SentTo")
                                        .HasColumnType("integer");

                                    b2.Property<int>("Type")
                                        .HasColumnType("integer")
                                        .HasColumnName("type");

                                    b2.Property<int>("Value")
                                        .HasColumnType("integer")
                                        .HasColumnName("value");

                                    b2.HasKey("AccountUserTz", "Id");

                                    b2.ToTable("operations", (string)null);

                                    b2.WithOwner()
                                        .HasForeignKey("AccountUserTz");
                                });

                            b1.Navigation("Operations");
                        });

                    b.OwnsOne("Bank.Domain.Users.ValueObjects.DateOfBirth", "DateOfBirthInUtc", b1 =>
                        {
                            b1.Property<string>("UserTz")
                                .HasColumnType("text");

                            b1.Property<DateTime>("Value")
                                .HasColumnType("timestamp with time zone")
                                .HasColumnName("date_of_birth");

                            b1.HasKey("UserTz");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserTz");
                        });

                    b.OwnsOne("Bank.Domain.Users.ValueObjects.EnglishFullName", "EnglishFullName", b1 =>
                        {
                            b1.Property<string>("UserTz")
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(15)
                                .HasColumnType("character varying(15)")
                                .HasColumnName("english_full_name");

                            b1.HasKey("UserTz");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserTz");
                        });

                    b.OwnsOne("Bank.Domain.Users.ValueObjects.FullName", "FullName", b1 =>
                        {
                            b1.Property<string>("UserTz")
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasMaxLength(20)
                                .HasColumnType("character varying(20)")
                                .HasColumnName("full_name");

                            b1.HasKey("UserTz");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserTz");
                        });

                    b.OwnsOne("Bank.Domain.Users.ValueObjects.Password", "Password", b1 =>
                        {
                            b1.Property<string>("UserTz")
                                .HasColumnType("text");

                            b1.Property<string>("Value")
                                .IsRequired()
                                .HasColumnType("text")
                                .HasColumnName("password");

                            b1.HasKey("UserTz");

                            b1.ToTable("users");

                            b1.WithOwner()
                                .HasForeignKey("UserTz");
                        });

                    b.Navigation("Account")
                        .IsRequired();

                    b.Navigation("DateOfBirthInUtc")
                        .IsRequired();

                    b.Navigation("EnglishFullName")
                        .IsRequired();

                    b.Navigation("FullName")
                        .IsRequired();

                    b.Navigation("Password")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
