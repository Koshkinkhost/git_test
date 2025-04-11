using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace api_for_kursach.Models;

public partial class MusicLabelContext : DbContext
{
    public MusicLabelContext()
    {
    }

    public MusicLabelContext(DbContextOptions<MusicLabelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Album> Albums { get; set; }

    public virtual DbSet<Artist> Artists { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<License> Licenses { get; set; }

    public virtual DbSet<ListeningHistory> ListeningHistories { get; set; }

    public virtual DbSet<News> News { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<RadioStation> RadioStations { get; set; }

    public virtual DbSet<RotationApplication> RotationApplications { get; set; }

    public virtual DbSet<Royalty> Royalties { get; set; }

    public virtual DbSet<Studio> Studios { get; set; }

    public virtual DbSet<Track> Tracks { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-QK0CGSD;Database=music_label;Integrated Security=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Album>(entity =>
        {
            entity.HasKey(e => e.AlbumId).HasName("PK__Albums__97B4BE17C423F503");

            entity.Property(e => e.AlbumId).HasColumnName("AlbumID");
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Artist).WithMany(p => p.Albums)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Albums__ArtistID__4222D4EF");
        });

        modelBuilder.Entity<Artist>(entity =>
        {
            entity.HasKey(e => e.ArtistId).HasName("PK__Artists__25706B7016EC7610");

            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.Bio).HasColumnType("text");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.StudioId).HasColumnName("StudioID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Studio).WithMany(p => p.Artists)
                .HasForeignKey(d => d.StudioId)
                .HasConstraintName("FK_Artists_Studios");

            entity.HasOne(d => d.User).WithMany(p => p.Artists)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("FK__Artists__UserID__3D5E1FD2");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.GenreId).HasName("PK__Genres__0385055EA1BB6626");

            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.GenreName)
                .HasMaxLength(100)
                .IsUnicode(false);
        });

        modelBuilder.Entity<License>(entity =>
        {
            entity.HasKey(e => e.LicenseId).HasName("PK__Licenses__72D600A252C7C1BC");

            entity.Property(e => e.LicenseId).HasColumnName("LicenseID");
            entity.Property(e => e.PublisherId).HasColumnName("PublisherID");
            entity.Property(e => e.Territory)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.TrackId).HasColumnName("TrackID");

            entity.HasOne(d => d.Publisher).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.PublisherId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Licenses__Publis__4F7CD00D");

            entity.HasOne(d => d.Track).WithMany(p => p.Licenses)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Licenses__TrackI__4E88ABD4");
        });

        modelBuilder.Entity<ListeningHistory>(entity =>
        {
            entity.HasKey(e => e.ListeningId).HasName("PK__Listenin__EA5EAC1E23199C55");

            entity.ToTable("ListeningHistory");

            entity.Property(e => e.ListeningId).HasColumnName("ListeningID");
            entity.Property(e => e.PlayDate).HasColumnType("datetime");
            entity.Property(e => e.TrackId).HasColumnName("TrackID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Track).WithMany(p => p.ListeningHistories)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Listening__Track__4BAC3F29");

            entity.HasOne(d => d.User).WithMany(p => p.ListeningHistories)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Listening__UserI__4AB81AF0");
        });

        modelBuilder.Entity<News>(entity =>
        {
            entity.HasKey(e => e.NewsId).HasName("PK__News__954EBDD35A42F3FB");

            entity.Property(e => e.NewsId).HasColumnName("NewsID");
            entity.Property(e => e.AdminId).HasColumnName("AdminID");
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.Content).HasColumnType("text");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.ImagePath).HasMaxLength(500);
            entity.Property(e => e.Title).HasMaxLength(255);

            entity.HasOne(d => d.Admin).WithMany(p => p.News)
                .HasForeignKey(d => d.AdminId)
                .HasConstraintName("FK__News__AdminID__71D1E811");

            entity.HasOne(d => d.Artist).WithMany(p => p.News)
                .HasForeignKey(d => d.ArtistId)
                .HasConstraintName("FK__News__ArtistID__70DDC3D8");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.HasKey(e => e.PublisherId).HasName("PK__Publishe__4C657E4B81B19EB6");

            entity.Property(e => e.PublisherId).HasColumnName("PublisherID");
            entity.Property(e => e.Address)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContractTerms)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RadioStation>(entity =>
        {
            entity.HasKey(e => e.RadioStationId).HasName("PK__RadioSta__6B221770F9CF3D7E");

            entity.Property(e => e.RadioStationId).HasColumnName("RadioStationID");
            entity.Property(e => e.ContactInfo).HasColumnType("text");
            entity.Property(e => e.Country)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Frequency)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        modelBuilder.Entity<RotationApplication>(entity =>
        {
            entity.HasKey(e => e.ApplicationId).HasName("PK__Rotation__C93A4F796AC6D53A");

            entity.Property(e => e.ApplicationId).HasColumnName("ApplicationID");
            entity.Property(e => e.ApplicationDate).HasColumnType("datetime");
            entity.Property(e => e.Notes).HasColumnType("text");
            entity.Property(e => e.RadioStationId).HasColumnName("RadioStationID");
            entity.Property(e => e.ReviewDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.TrackId).HasColumnName("TrackID");

            entity.HasOne(d => d.RadioStation).WithMany(p => p.RotationApplications)
                .HasForeignKey(d => d.RadioStationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RotationA__Radio__59FA5E80");

            entity.HasOne(d => d.Track).WithMany(p => p.RotationApplications)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__RotationA__Track__59063A47");
        });

        modelBuilder.Entity<Royalty>(entity =>
        {
            entity.HasKey(e => e.RoyaltyId).HasName("PK__Royaltie__C48847EF08613365");

            entity.Property(e => e.RoyaltyId).HasColumnName("RoyaltyID");
            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.AuthorId).HasColumnName("AuthorID");
            entity.Property(e => e.TrackId).HasColumnName("TrackID");

            entity.HasOne(d => d.Author).WithMany(p => p.Royalties)
                .HasForeignKey(d => d.AuthorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Royalties__Autho__534D60F1");

            entity.HasOne(d => d.Track).WithMany(p => p.Royalties)
                .HasForeignKey(d => d.TrackId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Royalties__Track__52593CB8");
        });

        modelBuilder.Entity<Studio>(entity =>
        {
            entity.HasKey(e => e.StudioId).HasName("PK__Studios__4ACC3B507D530DD3");

            entity.Property(e => e.StudioId).HasColumnName("StudioID");
            entity.Property(e => e.Building).HasMaxLength(50);
            entity.Property(e => e.City).HasMaxLength(100);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.Latitude).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.Location).HasMaxLength(255);
            entity.Property(e => e.Longitude).HasColumnType("decimal(10, 7)");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Phone).HasMaxLength(20);
            entity.Property(e => e.Street).HasMaxLength(100);
        });

        modelBuilder.Entity<Track>(entity =>
        {
            entity.HasKey(e => e.TrackId).HasName("PK__Tracks__7A74F8C0D03036D5");

            entity.Property(e => e.TrackId).HasColumnName("TrackID");
            entity.Property(e => e.AlbumId).HasColumnName("AlbumID");
            entity.Property(e => e.ArtistId).HasColumnName("ArtistID");
            entity.Property(e => e.GenreId).HasColumnName("GenreID");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.Album).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.AlbumId)
                .HasConstraintName("FK__Tracks__AlbumID__46E78A0C");

            entity.HasOne(d => d.Artist).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.ArtistId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Tracks__ArtistID__45F365D3");

            entity.HasOne(d => d.Genre).WithMany(p => p.Tracks)
                .HasForeignKey(d => d.GenreId)
                .HasConstraintName("FK__Tracks__GenreID__47DBAE45");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CCACFE4BC87D");

            entity.HasIndex(e => e.Username, "UQ__Users__536C85E4661EB418").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("UserID");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.Role)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Username)
                .HasMaxLength(255)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
