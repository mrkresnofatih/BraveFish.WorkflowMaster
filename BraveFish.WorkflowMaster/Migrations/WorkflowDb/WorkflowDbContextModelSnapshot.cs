﻿// <auto-generated />
using System;
using BraveFish.WorkflowMaster.EntityFramework;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BraveFish.WorkflowMaster.Migrations.WorkflowDb
{
    [DbContext(typeof(WorkflowDbContext))]
    partial class WorkflowDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.6")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("BraveFish.WorkflowMaster.Entities.Pipeline", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("CurrentStatus")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("JsonParams")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PlanId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("PlanJsonDefinition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PlanId");

                    b.ToTable("Pipelines");
                });

            modelBuilder.Entity("BraveFish.WorkflowMaster.Entities.Plan", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)");

                    b.Property<bool>("IsDeprecated")
                        .HasColumnType("boolean");

                    b.Property<string>("JsonDefinition")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.HasKey("Id");

                    b.ToTable("Plans");
                });

            modelBuilder.Entity("BraveFish.WorkflowMaster.Entities.Transition", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("FromStatus")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.Property<string>("JsonParams")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PipelineId")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)");

                    b.Property<string>("ToStatus")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)");

                    b.HasKey("Id");

                    b.HasIndex("PipelineId");

                    b.ToTable("Transitions");
                });

            modelBuilder.Entity("BraveFish.WorkflowMaster.Entities.Pipeline", b =>
                {
                    b.HasOne("BraveFish.WorkflowMaster.Entities.Plan", "Plan")
                        .WithMany("Pipelines")
                        .HasForeignKey("PlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Plan");
                });

            modelBuilder.Entity("BraveFish.WorkflowMaster.Entities.Transition", b =>
                {
                    b.HasOne("BraveFish.WorkflowMaster.Entities.Pipeline", "Pipeline")
                        .WithMany("Transitions")
                        .HasForeignKey("PipelineId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Pipeline");
                });

            modelBuilder.Entity("BraveFish.WorkflowMaster.Entities.Pipeline", b =>
                {
                    b.Navigation("Transitions");
                });

            modelBuilder.Entity("BraveFish.WorkflowMaster.Entities.Plan", b =>
                {
                    b.Navigation("Pipelines");
                });
#pragma warning restore 612, 618
        }
    }
}
