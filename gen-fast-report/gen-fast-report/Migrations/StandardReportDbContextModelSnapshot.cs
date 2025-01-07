﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using gen_fast_report.Data;

#nullable disable

namespace gen_fast_report.Migrations
{
    [DbContext(typeof(StandardReportDbContext))]
    partial class StandardReportDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("gen_fast_report.Models.StandardReport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Area")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StandardReports");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Area = 2,
                            Name = "Padrao Balistica"
                        },
                        new
                        {
                            Id = 2,
                            Area = 6,
                            Name = "Padrao Trânsito"
                        },
                        new
                        {
                            Id = 3,
                            Area = 5,
                            Name = "Padrao Vida"
                        },
                        new
                        {
                            Id = 4,
                            Area = 3,
                            Name = "Padrao Documentoscopia"
                        },
                        new
                        {
                            Id = 5,
                            Area = 4,
                            Name = "Padrao Meio Ambiente"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
