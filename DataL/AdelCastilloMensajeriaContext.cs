using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataL;

public partial class AdelCastilloMensajeriaContext : DbContext
{
    public AdelCastilloMensajeriaContext()
    {
    }

    public AdelCastilloMensajeriaContext(DbContextOptions<AdelCastilloMensajeriaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Conversacion> Conversacions { get; set; }

    public virtual DbSet<Mensaje> Mensajes { get; set; }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<UsuarioConversacion> UsuarioConversacions { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.; Database=ADelCastilloMensajeria; TrustServerCertificate=True; User ID=sa; Password=pass@word1;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Conversacion>(entity =>
        {
            entity.HasKey(e => e.IdConversacion).HasName("PK__Conversa__4F4482764E709158");

            entity.ToTable("Conversacion");

            entity.Property(e => e.Fecha).HasColumnType("datetime");
        });

        modelBuilder.Entity<Mensaje>(entity =>
        {
            entity.HasKey(e => e.IdMensaje).HasName("PK__Mensaje__E4D2A47FA4AAC90E");

            entity.ToTable("Mensaje");

            entity.Property(e => e.Texto)
                .HasMaxLength(254)
                .IsUnicode(false);

            entity.HasOne(d => d.IdUsuarioConversacionNavigation).WithMany(p => p.Mensajes)
                .HasForeignKey(d => d.IdUsuarioConversacion)
                .HasConstraintName("FK__Mensaje__IdUsuar__1920BF5C");
        });

        modelBuilder.Entity<Usuario>(entity =>
        {
            entity.HasKey(e => e.IdUsuario).HasName("PK__Usuario__5B65BF97F139AF61");

            entity.ToTable("Usuario");

            entity.Property(e => e.ApellidoMaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ApellidoPaterno)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Contrasenia)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.CorreoElectronico)
                .HasMaxLength(150)
                .IsUnicode(false);
            entity.Property(e => e.Foto).IsUnicode(false);
            entity.Property(e => e.Nombre)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UsuarioConversacion>(entity =>
        {
            entity.HasKey(e => e.IdUsuarioConversacion).HasName("PK__UsuarioC__D866811B17CBFD0C");

            entity.ToTable("UsuarioConversacion");

            entity.HasOne(d => d.ContactoNavigation).WithMany(p => p.UsuarioConversacionContactoNavigations)
                .HasForeignKey(d => d.Contacto)
                .HasConstraintName("FK__UsuarioCo__Conta__22AA2996");

            entity.HasOne(d => d.IdConversacionNavigation).WithMany(p => p.UsuarioConversacions)
                .HasForeignKey(d => d.IdConversacion)
                .HasConstraintName("FK__UsuarioCo__IdCon__15502E78");

            entity.HasOne(d => d.IdUsuarioNavigation).WithMany(p => p.UsuarioConversacionIdUsuarioNavigations)
                .HasForeignKey(d => d.IdUsuario)
                .HasConstraintName("FK__UsuarioCo__IdUsu__145C0A3F");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
