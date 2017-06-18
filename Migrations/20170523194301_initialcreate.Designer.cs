using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using VSNumberTumbler.Models;

namespace VSNumberTumbler.Migrations
{
    [DbContext(typeof(NumberTumblerContext))]
    [Migration("20170523194301_initialcreate")]
    partial class initialcreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.1.1")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("VSNumberTumbler.Models.NumberSet", b =>
                {
                    b.Property<int>("NumberSetID")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("IsApplicationNumberPool");

                    b.Property<string>("NumberSetDescription");

                    b.Property<int>("NumberSetMax");

                    b.Property<int>("NumberSetMin");

                    b.Property<string>("NumberSetName");

                    b.HasKey("NumberSetID");

                    b.ToTable("NumberSets");
                });

            modelBuilder.Entity("VSNumberTumbler.Models.NumberSetNumber", b =>
                {
                    b.Property<int>("NumberSetNumberID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Number");

                    b.Property<int>("NumberSetID");

                    b.Property<bool>("SelectedNumber");

                    b.HasKey("NumberSetNumberID");

                    b.HasIndex("NumberSetID", "Number")
                        .IsUnique();

                    b.ToTable("NumberSetNumbers");
                });

            modelBuilder.Entity("VSNumberTumbler.Models.Shuffle", b =>
                {
                    b.Property<int>("ShuffleID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("NumberSetID");

                    b.Property<DateTime>("ShuffleDateTime");

                    b.Property<string>("ShuffleDescription");

                    b.HasKey("ShuffleID");

                    b.HasIndex("NumberSetID");

                    b.ToTable("Shuffles");
                });

            modelBuilder.Entity("VSNumberTumbler.Models.ShuffleNumber", b =>
                {
                    b.Property<int>("ShuffleNumberID")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("Number");

                    b.Property<int>("Order");

                    b.Property<bool>("SelectedNumber");

                    b.Property<int>("ShuffleID");

                    b.HasKey("ShuffleNumberID");

                    b.HasIndex("ShuffleID", "Number")
                        .IsUnique();

                    b.ToTable("ShuffleNumbers");
                });

            modelBuilder.Entity("VSNumberTumbler.Models.NumberSetNumber", b =>
                {
                    b.HasOne("VSNumberTumbler.Models.NumberSet", "NumberSet")
                        .WithMany("NumberSetNumbers")
                        .HasForeignKey("NumberSetID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VSNumberTumbler.Models.Shuffle", b =>
                {
                    b.HasOne("VSNumberTumbler.Models.NumberSet", "NumberSet")
                        .WithMany()
                        .HasForeignKey("NumberSetID")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("VSNumberTumbler.Models.ShuffleNumber", b =>
                {
                    b.HasOne("VSNumberTumbler.Models.Shuffle", "Shuffle")
                        .WithMany("ShuffleNumbers")
                        .HasForeignKey("ShuffleID")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
