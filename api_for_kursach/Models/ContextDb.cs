using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace api_for_kursach.Models;

public partial class ContextDb : DbContext
{
    public ContextDb()
    {
    }

    public ContextDb(DbContextOptions<ContextDb> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<AlbumComposition> AlbumCompositions { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Composition> Compositions { get; set; }

    public virtual DbSet<CompositionAuthor> CompositionAuthors { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<License> Licenses { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<RadioStation> RadioStations { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Rotation> Rotations { get; set; }

    public virtual DbSet<RotationApplication> RotationApplications { get; set; }

    public virtual DbSet<Royalty> Royalties { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:DefaultConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.AlbumId).HasName("PK__Albums__97B4BE173D7D418A");

            entity.Property(e => e.AlbumId).HasColumnName("AlbumID");
            entity.Property(e => e.PublisherId).HasColumnName("PublisherID");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Publisher).WithMany(p => p.Albums)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("FK__Albums__Publishe__4CA06362");
        });

        modelBuilder.Entity<AlbumComposition>(entity =>
        {
            entity.HasKey(e => new { e.AlbumId, e.CompositionId }).HasName("PK__Album_Co__7C3A9D249080C0D0");

            entity.ToTable("Album_Compositions");

            entity.Property(e => e.AlbumId).HasColumnName("AlbumID");
            entity.Property(e => e.CompositionId).HasColumnName("CompositionID");

            entity.HasOne(d => d.Album).WithMany(p => p.AlbumCompositions)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("FK__Album_Com__Album__60A75C0F");

            entity.HasOne(d => d.Composition).WithMany(p => p.AlbumCompositions)
                .HasForeignKey(d => d.CompositionId)
                .HasConstraintName("FK__Album_Com__Compo__619B8048");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK__Artists__25706B70D12EAE96");

            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.ContactInfo).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PK__Authors__70DAFC1499CE7B0C");

            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.BirthDate).HasColumnType("datetime");
            entity.Property(e => e.ContactInfo).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Composition>(entity =>
        {
            entity.HasKey(e => e.CompositionId).HasName("PK__Composit__B8E2333F0F48F5E9");

            entity.Property(e => e.CompositionId).HasColumnName("CompositionID");
            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.ReleaseDate).HasColumnType("datetime");
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Genre).WithMany(p => p.Compositions)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Compositi__Genre__49C3F6B7");
        });

        modelBuilder.Entity<CompositionAuthor>(entity =>
        {
            entity.HasKey(e => new { e.CompositionId, e.AuthorId }).HasName("PK__Composit__EFEF9CFE4E2BDDC3");

            entity.ToTable("Composition_Authors");

            entity.Property(e => e.CompositionId).HasColumnName("CompositionID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.Role).HasMaxLength(50);

            entity.HasOne(d => d.Author).WithMany(p => p.CompositionAuthors)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Compositi__Autho__5DCAEF64");

            entity.HasOne(d => d.Composition).WithMany(p => p.CompositionAuthors)
                .HasForeignKey(d => d.CompositionId)
                .HasConstraintName("FK__Compositi__Compo__5CD6CB2B");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385055E08965801");

            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.GenreName).HasMaxLength(100);
        });

        modelBuilder.Entity<License>(entity =>
        {
            entity.HasKey(e => e.LicenseId).HasName("PK__Licenses__72D600A2A1B66669");

            entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
            entity.Property(e => e.CompositionId).HasColumnName("CompositionID");
            entity.Property(e => e.EndDate).HasColumnType("datetime");
            entity.Property(e => e.PublisherId).HasColumnName("PublisherID");
            entity.Property(e => e.StartDate).HasColumnType("datetime");
            entity.Property(e => e.Territory).HasMaxLength(255);

            entity.HasOne(d => d.Composition).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.CompositionId)
                .HasConstraintName("FK__Licenses__Compos__6477ECF3");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.PublisherId)
                .HasConstraintName("FK__Licenses__Publis__656C112C");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PK__Publishe__4C657E4B34AFBA31");

            entity.Property(e => e.PublisherId).HasColumnName("PublisherID");
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.ContractTerms).HasMaxLength(255);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<RadioStation>(entity =>
        {
            entity.HasKey(e => e.RadioStationId).HasName("PK__RadioSta__6B221770DED44F1A");

            entity.Property(e => e.RadioStationId).HasColumnName("RadioStationID");
            entity.Property(e => e.ContactInfo).HasMaxLength(255);
            entity.Property(e => e.Country).HasMaxLength(100);
            entity.Property(e => e.Frequency).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(255);
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A4F576B51");

            entity.Property(e => e.RoleName).HasMaxLength(100);
        });

        modelBuilder.Entity<Rotation>(entity =>
        {
            entity.HasKey(e => e.RotationId).HasName("PK__Rotation__A9CF939C087ADB0A");

            entity.Property(e => e.RotationId).HasColumnName("RotationID");
            entity.Property(e => e.AirDate).HasColumnType("datetime");
            entity.Property(e => e.CompositionId).HasColumnName("CompositionID");
            entity.Property(e => e.RadioStationId).HasColumnName("RadioStationID");
            entity.Property(e => e.Region).HasMaxLength(100);

            entity.HasOne(d => d.Composition).WithMany(p => p.Rotations)
                .HasForeignKey(d => d.CompositionId)
                .HasConstraintName("FK__Rotations__Compo__778AC167");

            entity.HasOne(d => d.RadioStation).WithMany(p => p.Rotations)
                .HasForeignKey(d => d.RadioStationId)
                .HasConstraintName("FK__Rotations__Radio__787EE5A0");
        });

        modelBuilder.Entity<RotationApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__Rotation__C93A4F799159E522");

            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.CompositionId).HasColumnName("CompositionID");
            entity.Property(e => e.Notes).HasMaxLength(255);
            entity.Property(e => e.ReviewDate).HasColumnType("datetime");
            entity.Property(e => e.Status).HasMaxLength(50);
            entity.Property(e => e.SubmissionDate).HasColumnType("datetime");

            entity.HasOne(d => d.Author).WithMany(p => p.RotationApplications)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__RotationA__Autho__7D439ABD");

            entity.HasOne(d => d.Composition).WithMany(p => p.RotationApplications)
                .HasForeignKey(d => d.CompositionId)
                .HasConstraintName("FK__RotationA__Compo__7C4F7684");
        });

        modelBuilder.Entity<Royalty>(entity =>
        {
            entity.HasKey(e => e.RoyaltyId).HasName("PK__Royaltie__C48847EFED2A9A48");

            entity.Property(e => e.RoyaltyId).HasColumnName("RoyaltyID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.CompositionId).HasColumnName("CompositionID");
            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.RotationId).HasColumnName("RotationID");

            entity.HasOne(d => d.Author).WithMany(p => p.Royalties)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("FK__Royalties__Autho__01142BA1");

            entity.HasOne(d => d.Composition).WithMany(p => p.Royalties)
                .HasForeignKey(d => d.CompositionId)
                .HasConstraintName("FK__Royalties__Compo__00200768");

            entity.HasOne(d => d.Rotation).WithMany(p => p.Royalties)
                .HasForeignKey(d => d.RotationId)
                .HasConstraintName("FK__Royalties__Rotat__02084FDA");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACA7657178");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4839F9A81").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastLogin).HasColumnType("datetime");
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .HasDefaultValue("Artist");
            entity.Property(e => e.Username).HasMaxLength(100);

            entity.HasOne(d => d.Artist).WithMany(p => p.Users)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Users__ArtistID__3B75D760");

            entity.HasOne(d => d.RoleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.RoleId)
                .HasConstraintName("FK_Artist_Role");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
