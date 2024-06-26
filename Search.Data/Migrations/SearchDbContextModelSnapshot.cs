﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Search.Data;

#nullable disable

namespace Search.Data.Migrations
{
    [DbContext(typeof(SearchDbContext))]
    partial class SearchDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Search.Core.Models.Rectangle", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("X1")
                        .HasColumnType("double precision");

                    b.Property<double>("X2")
                        .HasColumnType("double precision");

                    b.Property<double>("Y1")
                        .HasColumnType("double precision");

                    b.Property<double>("Y2")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Rectangles");
                });
#pragma warning restore 612, 618
        }
    }
}
