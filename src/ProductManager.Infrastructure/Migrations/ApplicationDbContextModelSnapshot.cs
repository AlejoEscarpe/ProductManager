using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Migrations;
using ProductManager.Infrastructure.Data;

#nullable disable

namespace ProductManager.Infrastructure.Data.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    internal partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "10.0.0");

            modelBuilder.Entity("ProductManager.Core.Entities.Product", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                b.Property<string>("Name")
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnType("nvarchar(100)");

                b.Property<decimal>("Price")
                    .HasColumnType("decimal(18,2)");

                b.Property<int>("Stock")
                    .HasColumnType("int");

                b.HasKey("Id");

                b.ToTable("Products");
            });
        }
    }
}
