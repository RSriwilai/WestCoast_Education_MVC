// <auto-generated />
using App.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20210531113628_AddCourseName")]
    partial class AddCourseName
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "5.0.6");

            modelBuilder.Entity("App.Entities.Course", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("CourseComplexity")
                        .HasColumnType("TEXT");

                    b.Property<string>("CourseDescription")
                        .HasColumnType("TEXT");

                    b.Property<int>("CourseLength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CourseNumber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("CourseStatus")
                        .HasColumnType("TEXT");

                    b.Property<string>("CourseTitle")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Courses");
                });

            modelBuilder.Entity("App.Entities.CourseName", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CourseNames");
                });

            modelBuilder.Entity("App.Entities.Participant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .HasColumnType("TEXT");

                    b.Property<string>("EmailAddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("FirstName")
                        .HasColumnType("TEXT");

                    b.Property<string>("LastName")
                        .HasColumnType("TEXT");

                    b.Property<int>("PhoneNumber")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Participants");
                });
#pragma warning restore 612, 618
        }
    }
}
