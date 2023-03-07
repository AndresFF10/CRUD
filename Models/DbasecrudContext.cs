using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CRUD2.Models;

public partial class DbasecrudContext : DbContext
{
    public DbasecrudContext()
    {
    }

    public DbasecrudContext(DbContextOptions<DbasecrudContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
  //      => optionsBuilder.UseMySql("server=localhost;port=3306;database=dbasecrud;uid=root;password=root", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.29-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Address).HasMaxLength(45);
            entity.Property(e => e.Email).HasMaxLength(45);
            entity.Property(e => e.IncreasesDates).HasMaxLength(120);
            entity.Property(e => e.IncreasesSalary).HasMaxLength(120);
            entity.Property(e => e.LastName).HasMaxLength(45);
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Picture).HasColumnType("varbinary(32672)");
            entity.Property(e => e.Rol).HasMaxLength(45);
            entity.Property(e => e.WorkingStartDate).HasColumnName("Working_Start_Date");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
