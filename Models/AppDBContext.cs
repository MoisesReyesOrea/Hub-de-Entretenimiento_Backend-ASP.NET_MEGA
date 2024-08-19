using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace HubDeEntretenimientoMegaLiderlyBackend.Models;

public partial class AppDBContext : DbContext
{
    private readonly IConfiguration _configuration;
    public AppDBContext()
    {
    }

    //inyección de IConfiguration, para acceder a la configuración del archivo appsettings.json y obtener cadena de conexión.
    public AppDBContext(DbContextOptions<AppDBContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public virtual DbSet<Actor> Actors { get; set; }

    public virtual DbSet<Director> Directors { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Genre> Genres { get; set; }

    public virtual DbSet<Hidden> Hiddens { get; set; }

    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Movie> Movies { get; set; }

    public virtual DbSet<MultimediaActor> MultimediaActors { get; set; }

    public virtual DbSet<MultimediaDirector> MultimediaDirectors { get; set; }

    public virtual DbSet<MultimediaGenre> MultimediaGenres { get; set; }

    public virtual DbSet<SerieEpisode> SerieEpisodes { get; set; }

    public virtual DbSet<SerieSeason> SerieSeasons { get; set; }

    public virtual DbSet<Series> Series { get; set; }

    public virtual DbSet<Sport> Sports { get; set; }

    public virtual DbSet<User> Users { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            var connectionString = _configuration.GetConnectionString("stringSQL");
            optionsBuilder.UseSqlServer(connectionString);
        }
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Actor>(entity =>
        {
            entity.HasKey(e => e.IdActor).HasName("Pk_actor");

            entity.ToTable("actors");

            entity.HasIndex(e => new { e.Name, e.LastName }, "UQ_actor").IsUnique();

            entity.Property(e => e.IdActor).HasColumnName("id_actor");
            entity.Property(e => e.Born)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("born");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nationality");
            entity.Property(e => e.ProfilePhoto)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("profile_photo");
        });

        modelBuilder.Entity<Director>(entity =>
        {
            entity.HasKey(e => e.IdDirector).HasName("Pk_director");

            entity.ToTable("directors");

            entity.HasIndex(e => new { e.Name, e.LastName }, "UQ_director").IsUnique();

            entity.Property(e => e.IdDirector).HasColumnName("id_director");
            entity.Property(e => e.Born)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("born");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Nationality)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("nationality");
            entity.Property(e => e.ProfilePhoto)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("profile_photo");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.IdFavorite).HasName("PK_favorite");

            entity.ToTable("favorites");

            entity.HasIndex(e => new { e.IdUser, e.IdMovie, e.IdSerie, e.IdSport }, "UQ_favoriteUniq").IsUnique();

            entity.Property(e => e.IdFavorite).HasColumnName("id_favorite");
            entity.Property(e => e.IdMovie).HasColumnName("id_movie");
            entity.Property(e => e.IdSerie).HasColumnName("id_serie");
            entity.Property(e => e.IdSport).HasColumnName("id_sport");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.IdMovie)
                .HasConstraintName("FK_favorite_movie");

            entity.HasOne(d => d.IdSerieNavigation).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.IdSerie)
                .HasConstraintName("FK_favorite_serie");

            entity.HasOne(d => d.IdSportNavigation).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.IdSport)
                .HasConstraintName("FK_favorite_sport");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Favorites)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_favorite_user");
        });

        modelBuilder.Entity<Genre>(entity =>
        {
            entity.HasKey(e => e.IdGenre).HasName("PK_genre");

            entity.ToTable("genres");

            entity.HasIndex(e => e.GenreName, "UQ_genre").IsUnique();

            entity.Property(e => e.IdGenre).HasColumnName("id_genre");
            entity.Property(e => e.GenreName)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("genre_name");
        });

        modelBuilder.Entity<Hidden>(entity =>
        {
            entity.HasKey(e => e.IdHidden).HasName("PK_hidden");

            entity.ToTable("hiddens");

            entity.HasIndex(e => new { e.IdUser, e.IdMovie, e.IdSerie, e.IdSport }, "UQ_hiddenUniq").IsUnique();

            entity.Property(e => e.IdHidden).HasColumnName("id_hidden");
            entity.Property(e => e.CreatedAt)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("created_at");
            entity.Property(e => e.IdMovie).HasColumnName("id_movie");
            entity.Property(e => e.IdSerie).HasColumnName("id_serie");
            entity.Property(e => e.IdSport).HasColumnName("id_sport");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.Hiddens)
                .HasForeignKey(d => d.IdMovie)
                .HasConstraintName("FK_hidden_movie");

            entity.HasOne(d => d.IdSerieNavigation).WithMany(p => p.Hiddens)
                .HasForeignKey(d => d.IdSerie)
                .HasConstraintName("FK_hidden_serie");

            entity.HasOne(d => d.IdSportNavigation).WithMany(p => p.Hiddens)
                .HasForeignKey(d => d.IdSport)
                .HasConstraintName("FK_hidden_sport");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Hiddens)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_hidden_user");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.IdHistory);

            entity.ToTable("history");

            entity.HasIndex(e => new { e.IdUser, e.IdMovie, e.IdSerie, e.IdSport }, "UQ_historyUniq").IsUnique();

            entity.Property(e => e.IdHistory).HasColumnName("id_history");
            entity.Property(e => e.CreatedAt)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("created_at");
            entity.Property(e => e.IdMovie).HasColumnName("id_movie");
            entity.Property(e => e.IdSerie).HasColumnName("id_serie");
            entity.Property(e => e.IdSport).HasColumnName("id_sport");
            entity.Property(e => e.IdUser).HasColumnName("id_user");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.IdMovie)
                .HasConstraintName("FK_history_movie");

            entity.HasOne(d => d.IdSerieNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.IdSerie)
                .HasConstraintName("FK_history_serie");

            entity.HasOne(d => d.IdSportNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.IdSport)
                .HasConstraintName("FK_history_sport");

            entity.HasOne(d => d.IdUserNavigation).WithMany(p => p.Histories)
                .HasForeignKey(d => d.IdUser)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_history_user");
        });

        modelBuilder.Entity<Movie>(entity =>
        {
            entity.HasKey(e => e.IdMovie).HasName("PK_movie");

            entity.ToTable("movies");

            entity.HasIndex(e => new { e.Title, e.Date }, "UQ_movie").IsUnique();

            entity.Property(e => e.IdMovie).HasColumnName("id_movie");
            entity.Property(e => e.CoverImg)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("cover_img");
            entity.Property(e => e.Date)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Duration)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("duration");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("video_url");
        });

        modelBuilder.Entity<MultimediaActor>(entity =>
        {
            entity.HasKey(e => e.IdMultimediaActor).HasName("PK_multimedia_actor");

            entity.ToTable("multimedia_actors");

            entity.HasIndex(e => new { e.IdActor, e.IdMovie, e.IdSerie, e.IdSport }, "UQ_multimedia_actorUniq").IsUnique();

            entity.Property(e => e.IdMultimediaActor).HasColumnName("id_multimedia_actor");
            entity.Property(e => e.IdActor).HasColumnName("id_actor");
            entity.Property(e => e.IdMovie).HasColumnName("id_movie");
            entity.Property(e => e.IdSerie).HasColumnName("id_serie");
            entity.Property(e => e.IdSport).HasColumnName("id_sport");

            entity.HasOne(d => d.IdActorNavigation).WithMany(p => p.MultimediaActors)
                .HasForeignKey(d => d.IdActor)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_multimedia_actor");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.MultimediaActors)
                .HasForeignKey(d => d.IdMovie)
                .HasConstraintName("FK_multimedia_movie_a");

            entity.HasOne(d => d.IdSerieNavigation).WithMany(p => p.MultimediaActors)
                .HasForeignKey(d => d.IdSerie)
                .HasConstraintName("FK_multimedia_serie_a");

            entity.HasOne(d => d.IdSportNavigation).WithMany(p => p.MultimediaActors)
                .HasForeignKey(d => d.IdSport)
                .HasConstraintName("FK_multimedia_sport_a");
        });

        modelBuilder.Entity<MultimediaDirector>(entity =>
        {
            entity.HasKey(e => e.IdMultimediaDirector).HasName("PK_multimedia_director");

            entity.ToTable("multimedia_directors");

            entity.HasIndex(e => new { e.IdDirector, e.IdMovie, e.IdSerie, e.IdSport }, "UQ_multimedia_directorUniq").IsUnique();

            entity.Property(e => e.IdMultimediaDirector).HasColumnName("id_multimedia_director");
            entity.Property(e => e.IdDirector).HasColumnName("id_director");
            entity.Property(e => e.IdMovie).HasColumnName("id_movie");
            entity.Property(e => e.IdSerie).HasColumnName("id_serie");
            entity.Property(e => e.IdSport).HasColumnName("id_sport");

            entity.HasOne(d => d.IdDirectorNavigation).WithMany(p => p.MultimediaDirectors)
                .HasForeignKey(d => d.IdDirector)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_multimedia_director");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.MultimediaDirectors)
                .HasForeignKey(d => d.IdMovie)
                .HasConstraintName("FK_multimedia_movie_d");

            entity.HasOne(d => d.IdSerieNavigation).WithMany(p => p.MultimediaDirectors)
                .HasForeignKey(d => d.IdSerie)
                .HasConstraintName("FK_multimedia_serie_d");

            entity.HasOne(d => d.IdSportNavigation).WithMany(p => p.MultimediaDirectors)
                .HasForeignKey(d => d.IdSport)
                .HasConstraintName("FK_multimedia_sport_d");
        });

        modelBuilder.Entity<MultimediaGenre>(entity =>
        {
            entity.HasKey(e => e.IdMultimediaGenre).HasName("PK_multimedia_genre");

            entity.ToTable("multimedia_genres");

            entity.HasIndex(e => new { e.IdGenre, e.IdMovie, e.IdSerie, e.IdSport }, "UQ_multimedia_genreUniq").IsUnique();

            entity.Property(e => e.IdMultimediaGenre).HasColumnName("id_multimedia_genre");
            entity.Property(e => e.IdGenre).HasColumnName("id_genre");
            entity.Property(e => e.IdMovie).HasColumnName("id_movie");
            entity.Property(e => e.IdSerie).HasColumnName("id_serie");
            entity.Property(e => e.IdSport).HasColumnName("id_sport");

            entity.HasOne(d => d.IdGenreNavigation).WithMany(p => p.MultimediaGenres)
                .HasForeignKey(d => d.IdGenre)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_multimedia_genre");

            entity.HasOne(d => d.IdMovieNavigation).WithMany(p => p.MultimediaGenres)
                .HasForeignKey(d => d.IdMovie)
                .HasConstraintName("FK_multimedia_movie_g");

            entity.HasOne(d => d.IdSerieNavigation).WithMany(p => p.MultimediaGenres)
                .HasForeignKey(d => d.IdSerie)
                .HasConstraintName("FK_multimedia_serie_g");

            entity.HasOne(d => d.IdSportNavigation).WithMany(p => p.MultimediaGenres)
                .HasForeignKey(d => d.IdSport)
                .HasConstraintName("FK_multimedia_sport_g");
        });

        modelBuilder.Entity<SerieEpisode>(entity =>
        {
            entity.HasKey(e => e.IdEpisode).HasName("PK_episode");

            entity.ToTable("serie_episodes");

            entity.HasIndex(e => new { e.Title, e.Date }, "UQ_episode").IsUnique();

            entity.Property(e => e.IdEpisode).HasColumnName("id_episode");
            entity.Property(e => e.CoverImg)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("cover_img");
            entity.Property(e => e.Date)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Duration)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("duration");
            entity.Property(e => e.EpisodeNumber).HasColumnName("episode_number");
            entity.Property(e => e.IdSeason).HasColumnName("id_season");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("video_url");

            entity.HasOne(d => d.IdSeasonNavigation).WithMany(p => p.SerieEpisodes)
                .HasForeignKey(d => d.IdSeason)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_season");
        });

        modelBuilder.Entity<SerieSeason>(entity =>
        {
            entity.HasKey(e => e.IdSeason).HasName("PK_season");

            entity.ToTable("serie_seasons");

            entity.HasIndex(e => new { e.Title, e.Date }, "UQ_season").IsUnique();

            entity.Property(e => e.IdSeason).HasColumnName("id_season");
            entity.Property(e => e.CoverImg)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("cover_img");
            entity.Property(e => e.Date)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.IdSerie).HasColumnName("id_serie");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("title");

            entity.HasOne(d => d.IdSerieNavigation).WithMany(p => p.SerieSeasons)
                .HasForeignKey(d => d.IdSerie)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_serie");
        });

        modelBuilder.Entity<Series>(entity =>
        {
            entity.HasKey(e => e.IdSerie);

            entity.ToTable("series");

            entity.HasIndex(e => new { e.Title, e.Date }, "UQ_serie").IsUnique();

            entity.Property(e => e.IdSerie).HasColumnName("id_serie");
            entity.Property(e => e.CoverImg)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("cover_img");
            entity.Property(e => e.Date)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("title");
        });

        modelBuilder.Entity<Sport>(entity =>
        {
            entity.HasKey(e => e.IdSport).HasName("PK_sport");

            entity.ToTable("sports");

            entity.HasIndex(e => new { e.Title, e.Date }, "UQ_sport").IsUnique();

            entity.Property(e => e.IdSport).HasColumnName("id_sport");
            entity.Property(e => e.CoverImg)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("cover_img");
            entity.Property(e => e.Date)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("date");
            entity.Property(e => e.Description)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("description");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("title");
            entity.Property(e => e.VideoUrl)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("video_url");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("Pk_user");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "UQ_user").IsUnique();

            entity.Property(e => e.IdUser).HasColumnName("id_user");
            entity.Property(e => e.Age).HasColumnName("age");
            entity.Property(e => e.Email)
                .HasMaxLength(30)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("last_name");
            entity.Property(e => e.Name)
                .HasMaxLength(25)
                .IsUnicode(false)
                .HasColumnName("name");
            entity.Property(e => e.Password)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.ProfilePhoto)
                .HasMaxLength(1000)
                .IsUnicode(false)
                .HasColumnName("profile_photo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
