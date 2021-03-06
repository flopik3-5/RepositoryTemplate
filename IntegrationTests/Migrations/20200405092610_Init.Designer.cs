﻿// <auto-generated />
using System;
using IntegrationTests;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IntegrationTests.Migrations
{
    [DbContext(typeof(TestApplicationContext))]
    [Migration("20200405092610_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn)
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            modelBuilder.Entity("IntegrationTests.Entities.TestEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<decimal>("DecimalProperty")
                        .HasColumnType("numeric");

                    b.Property<double>("DoubleProperty")
                        .HasColumnType("double precision");

                    b.Property<float>("FloatProperty")
                        .HasColumnType("real");

                    b.Property<int>("IntegerProperty")
                        .HasColumnType("integer");

                    b.Property<string>("StringProperty")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("TestEntities");
                });
#pragma warning restore 612, 618
        }
    }
}
