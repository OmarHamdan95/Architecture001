﻿// <auto-generated />
using System;
using Architecture.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Architecture.Database.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20220918114126_FixDb")]
    partial class FixDb
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Architecture.Domain.Auth", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.Property<string>("Salt")
                        .IsRequired()
                        .HasMaxLength(1000)
                        .HasColumnType("character varying(1000)");

                    b.HasKey("Id");

                    b.HasIndex("Login")
                        .IsUnique();

                    b.ToTable("Auths", "Architecture");
                });

            modelBuilder.Entity("Architecture.Domain.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ValidFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ValidTo")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Roles", "Architecture");
                });

            modelBuilder.Entity("Architecture.Domain.Status", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ValidFrom")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime?>("ValidTo")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.ToTable("Statuses", "Architecture");
                });

            modelBuilder.Entity("Architecture.Domain.Tree", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<string>("Code")
                        .HasColumnType("text");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("ParentId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("ParentId");

                    b.ToTable("Trees", "Architecture");
                });

            modelBuilder.Entity("Architecture.Domain.User", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<long>("Id"));

                    b.Property<long?>("AuthId")
                        .HasColumnType("bigint");

                    b.Property<string>("CreatedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("ModifiedBy")
                        .HasColumnType("text");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("timestamp with time zone");

                    b.Property<long?>("StatusId")
                        .HasColumnType("bigint");

                    b.HasKey("Id");

                    b.HasIndex("AuthId")
                        .IsUnique();

                    b.HasIndex("StatusId");

                    b.ToTable("Users", "Architecture");
                });

            modelBuilder.Entity("Architecture.Domain.Role", b =>
                {
                    b.OwnsOne("Architecture.Domain.Name", "Description", b1 =>
                        {
                            b1.Property<long>("RoleId")
                                .HasColumnType("bigint");

                            b1.Property<string>("NameAr")
                                .HasColumnType("text");

                            b1.Property<string>("NameEn")
                                .HasColumnType("text");

                            b1.HasKey("RoleId");

                            b1.ToTable("Roles", "Architecture");

                            b1.WithOwner()
                                .HasForeignKey("RoleId");
                        });

                    b.Navigation("Description");
                });

            modelBuilder.Entity("Architecture.Domain.Status", b =>
                {
                    b.OwnsOne("Architecture.Domain.Name", "Description", b1 =>
                        {
                            b1.Property<long>("StatusId")
                                .HasColumnType("bigint");

                            b1.Property<string>("NameAr")
                                .HasColumnType("text");

                            b1.Property<string>("NameEn")
                                .HasColumnType("text");

                            b1.HasKey("StatusId");

                            b1.ToTable("Statuses", "Architecture");

                            b1.WithOwner()
                                .HasForeignKey("StatusId");
                        });

                    b.Navigation("Description");
                });

            modelBuilder.Entity("Architecture.Domain.Tree", b =>
                {
                    b.HasOne("Architecture.Domain.Tree", "Parent")
                        .WithMany()
                        .HasForeignKey("ParentId");

                    b.OwnsOne("Architecture.Domain.Name", "Description", b1 =>
                        {
                            b1.Property<long>("TreeId")
                                .HasColumnType("bigint");

                            b1.Property<string>("NameAr")
                                .HasColumnType("text");

                            b1.Property<string>("NameEn")
                                .HasColumnType("text");

                            b1.HasKey("TreeId");

                            b1.ToTable("Trees", "Architecture");

                            b1.WithOwner()
                                .HasForeignKey("TreeId");
                        });

                    b.Navigation("Description");

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("Architecture.Domain.User", b =>
                {
                    b.HasOne("Architecture.Domain.Auth", "Auth")
                        .WithMany()
                        .HasForeignKey("AuthId");

                    b.HasOne("Architecture.Domain.Status", "Status")
                        .WithMany()
                        .HasForeignKey("StatusId");

                    b.OwnsOne("Architecture.Domain.Name", "Name", b1 =>
                        {
                            b1.Property<long>("UserId")
                                .HasColumnType("bigint");

                            b1.Property<string>("NameAr")
                                .HasColumnType("text");

                            b1.Property<string>("NameEn")
                                .HasColumnType("text");

                            b1.HasKey("UserId");

                            b1.ToTable("Users", "Architecture");

                            b1.WithOwner()
                                .HasForeignKey("UserId");
                        });

                    b.Navigation("Auth");

                    b.Navigation("Name");

                    b.Navigation("Status");
                });
#pragma warning restore 612, 618
        }
    }
}
