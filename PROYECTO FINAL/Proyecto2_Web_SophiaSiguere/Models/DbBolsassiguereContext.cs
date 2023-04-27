using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Proyecto2_Web_SophiaSiguere.Models;

public partial class DbBolsassiguereContext : DbContext
{
    public DbBolsassiguereContext()
    {
    }

    public DbBolsassiguereContext(DbContextOptions<DbBolsassiguereContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Cotizacion> Cotizacions { get; set; }

    public virtual DbSet<Pedido> Pedidos { get; set; }

    public virtual DbSet<Rol> Rols { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        if (!optionsBuilder.IsConfigured)

        {

            IConfigurationRoot configuration = new ConfigurationBuilder()

            .SetBasePath(Directory.GetCurrentDirectory())

                        .AddJsonFile("appsettings.json")

                        .Build();

            var connectionString = configuration.GetConnectionString("conexion");

            optionsBuilder.UseMySQL(connectionString);

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Cotizacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("cotizacion");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CorreoCliente)
                .HasMaxLength(50)
                .HasColumnName("correoCliente");
            entity.Property(e => e.FechaCotizacion)
                .HasColumnType("date")
                .HasColumnName("fechaCotizacion");
            entity.Property(e => e.TelefonoCliente)
                .HasMaxLength(50)
                .HasColumnName("telefonoCliente");
        });

        modelBuilder.Entity<Pedido>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("pedido");

            entity.HasIndex(e => e.NoPedido, "NoPedido");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Calibre)
                .HasMaxLength(50)
                .HasColumnName("calibre");
            entity.Property(e => e.ColorPantone)
                .HasMaxLength(50)
                .HasColumnName("colorPantone");
            entity.Property(e => e.Logo)
                .HasMaxLength(50)
                .HasColumnName("logo");
            entity.Property(e => e.Material).HasMaxLength(50);
            entity.Property(e => e.TamañoAltura).HasColumnName("tamañoAltura");
            entity.Property(e => e.TamañoExtension).HasColumnName("tamañoExtension");
            entity.Property(e => e.TamañoHorizonte).HasColumnName("tamañoHorizonte");

            entity.HasOne(d => d.NoPedidoNavigation).WithMany(p => p.Pedidos)
                .HasForeignKey(d => d.NoPedido)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("pedido_ibfk_1");
        });

        modelBuilder.Entity<Rol>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("rol");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("usuarios");

            entity.HasIndex(e => e.IdRol, "id_rol");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contrasena)
                .HasMaxLength(50)
                .HasColumnName("contrasena");
            entity.Property(e => e.Correo)
                .HasMaxLength(50)
                .HasColumnName("correo");
            entity.Property(e => e.IdRol).HasColumnName("id_rol");
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .HasColumnName("nombre");
            entity.Property(e => e.Telefono)
                .HasMaxLength(50)
                .HasColumnName("telefono");
            entity.Property(e => e.Usuario1)
                .HasMaxLength(50)
                .HasColumnName("usuario");

            entity.HasOne(d => d.IdRolNavigation).WithMany(p => p.Usuarios)
                .HasForeignKey(d => d.IdRol)
                .IsRequired(false)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("usuarios_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
