using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASPCoreWebAPICRUD.Models;

public partial class StudentdbContext : DbContext
{
    public StudentdbContext()
    {
    }

    public StudentdbContext(DbContextOptions<StudentdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("students_pkey");

            entity.ToTable("students");

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Fathername)
                .HasMaxLength(100)
                .HasColumnName("fathername");
            entity.Property(e => e.Standard).HasColumnName("standard");
            entity.Property(e => e.Studentgender)
                .HasMaxLength(10)
                .HasColumnName("studentgender");
            entity.Property(e => e.Studentname)
                .HasMaxLength(100)
                .HasColumnName("studentname");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
