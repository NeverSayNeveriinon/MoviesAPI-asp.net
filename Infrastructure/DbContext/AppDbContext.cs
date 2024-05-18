using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Core.Domain.Entities;
using Core.Domain.Entities.JoinEntities;
using Core.Domain.IdentityEntities;
using static Infrastructure.Helpers.HelperMethods;


namespace Infrastructure.DbContext;

public class AppDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
{
    public virtual DbSet<Show> Shows { get; set; } 
    public virtual DbSet<Movie> Movies { get; set; } 
    public virtual DbSet<Serial> Series { get; set; }
    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<Person> Persons { get; set; }
    
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    
    //
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);


        #region Seed_Data

        // Seed Data for Movies //
        
        // 'movies seed data json' path
        string moviesSeed_Path = "../WebAPI/wwwroot/JSONSeed/seed_movies.json";

        // inserting one row in table per 'each Movie object in List'
        modelBuilder.Entity<Movie>().HasData(JsonToListEntity<Movie>(moviesSeed_Path));  
        
        
        // Seed Data for Series //

        // 'series seed data json' path
        string seriesSeed_Path = "../WebAPI/wwwroot/JSONSeed/seed_series.json";

        // inserting one row in table per 'each Serial object in List'
        modelBuilder.Entity<Serial>().HasData(JsonToListEntity<Serial>(seriesSeed_Path));  
        
        
        // Seed Data for Persons //

        // 'persons seed data json' path
        string personsSeed_Path = "../WebAPI/wwwroot/JSONSeed/seed_persons.json";

        // inserting one row in table per 'each Person object in List'
        modelBuilder.Entity<Person>().HasData(JsonToListEntity<Person>(personsSeed_Path));    
        
        
        // Seed Data for Genres //

        // 'genres seed data json' path
        string genresSeed_Path = "../WebAPI/wwwroot/JSONSeed/seed_genres.json";

        // inserting one row in table per 'each Genre object in List'
        modelBuilder.Entity<Genre>().HasData(JsonToListEntity<Genre>(genresSeed_Path));    
        
        
        // Seed Data for shows.artists //

        // 'shows.artists seed data json' path
        string shows_artistsSeed_Path = "../WebAPI/wwwroot/JSONSeed/seed_shows.artists.json";

        // inserting one row in table per 'each ShowsArtistsJoin object in List'
        modelBuilder.Entity<ShowsArtistsJoin>().HasData(JsonToListEntity<ShowsArtistsJoin>(shows_artistsSeed_Path));      
        
        
        // Seed Data for shows.genres //

        // 'shows.genres seed data json' path
        string shows_genresSeed_Path = "../WebAPI/wwwroot/JSONSeed/seed_shows.genres.json";

        // inserting one row in table per 'each ShowsGenresJoin object in List'
        modelBuilder.Entity<ShowsGenresJoin>().HasData(JsonToListEntity<ShowsGenresJoin>(shows_genresSeed_Path));      
        
        
        // Seed Data for shows.writers //

        // 'shows.writers seed data json' path
        string shows_writersSeed_Path = "../WebAPI/wwwroot/JSONSeed/seed_shows.writers.json";

        // inserting one row in table per 'each ShowsWritersJoin object in List'
        modelBuilder.Entity<ShowsWritersJoin>().HasData(JsonToListEntity<ShowsWritersJoin>(shows_writersSeed_Path));      
        
        
        // Seed Data for series.directors //

        // 'series.directors seed data json' path
        string series_directorsSeed_Path = "../WebAPI/wwwroot/JSONSeed/seed_series.directors.json";

        // inserting one row in table per 'each SeriesDirectorsJoin object in List'
        modelBuilder.Entity<SeriesDirectorsJoin>().HasData(JsonToListEntity<SeriesDirectorsJoin>(series_directorsSeed_Path));  
        
        
        
        // Seed Data for Roles (user,admin) //
            
        List<ApplicationRole> rolesList =
        [
            new ApplicationRole() { Id = Guid.Parse("211D03AE-07FE-4CB4-813E-163F46568B44"),Name = "User", NormalizedName = "USER", 
                ConcurrencyStamp = "68296470-E10A-45E8-BA3A-382B5AA093A5"},
            new ApplicationRole() { Id = Guid.Parse("A9C2BC35-61FE-4E60-8158-2BFD6E1478EB"),Name = "Admin", NormalizedName = "ADMIN", 
                ConcurrencyStamp = "6B2DFC5D-F09C-413C-99F3-30C42997A274"}
        ];
        modelBuilder.Entity<ApplicationRole>().HasData(rolesList);
        
        
        #endregion

        
        #region Fluent_API_Configuration
        
        // -
        modelBuilder.HasDefaultSchema("dbo");

        // -
        modelBuilder.Entity<Show>().ToTable("Shows")
                                   .UseTphMappingStrategy();
        modelBuilder.Entity<Genre>().ToTable("Genres");
        modelBuilder.Entity<Person>().ToTable("Persons");

        
        // - Relationships
        
        // Movie 'N'====......----'1' Director(person)
        modelBuilder.Entity<Movie>()
                    .HasOne(e => e.Director)
                    .WithMany(e => e.MoviesDirected)
                    .HasForeignKey(e => e.DirectorID).IsRequired(false).OnDelete(DeleteBehavior.Cascade);

        
        // Serial 'N'====......----'N' Director(person)
        modelBuilder.Entity<Serial>()
            .HasMany(e => e.Directors)
            .WithMany(e => e.SeriesDirected)
            .UsingEntity<SeriesDirectorsJoin>(
                l => l.HasOne<Person>(e => e.Director)
                      .WithMany(e => e.SeriesDirectorsJoin)
                      .HasForeignKey(e => e.DirectorID).IsRequired().OnDelete(DeleteBehavior.ClientCascade),
                r => r.HasOne<Serial>(e => e.Serial)
                      .WithMany(e => e.SeriesDirectorsJoin)
                      .HasForeignKey(e => e.SerialID).IsRequired(false).OnDelete(DeleteBehavior.Cascade));

        
        // Show 'N'====......----'N' Writer(person)
        modelBuilder.Entity<Show>()
            .HasMany(e => e.Writers)
            .WithMany(e => e.ShowsWritten)
            .UsingEntity<ShowsWritersJoin>(
                l => l.HasOne<Person>(e => e.Writer)
                      .WithMany(e => e.ShowsWritersJoin)
                      .HasForeignKey(e => e.WriterID).IsRequired().OnDelete(DeleteBehavior.ClientCascade),
                r => r.HasOne<Show>(e => e.Show)
                      .WithMany(e => e.ShowsWritersJoin)
                      .HasForeignKey(e => e.ShowID).IsRequired(false).OnDelete(DeleteBehavior.Cascade));

        
        // Show 'N'----......----'N' Artist(person)
        modelBuilder.Entity<Show>()
            .HasMany(e => e.Artists)
            .WithMany(e => e.ShowsPlayed)
            .UsingEntity<ShowsArtistsJoin>(    
                l => l.HasOne<Person>(e => e.Artist)
                      .WithMany(e => e.ShowsArtistsJoin)
                      .HasForeignKey(e => e.ArtistID).IsRequired(false).OnDelete(DeleteBehavior.ClientSetNull),
                r => r.HasOne<Show>(e => e.Show)
                      .WithMany(e => e.ShowsArtistsJoin)
                      .HasForeignKey(e => e.ShowID).IsRequired(false).OnDelete(DeleteBehavior.Cascade));
        
        
        // Show 'N'====......----'N' Genre
        modelBuilder.Entity<Show>()
            .HasMany(e => e.Genres)
            .WithMany(e => e.Shows)
            .UsingEntity<ShowsGenresJoin>(    
                l => l.HasOne<Genre>(e => e.Genre)
                      .WithMany(e => e.ShowsGenresJoin)
                      .HasForeignKey(e => e.GenreID).IsRequired().OnDelete(DeleteBehavior.ClientSetNull),
                r => r.HasOne<Show>(e => e.Show)
                      .WithMany(e => e.ShowsGenresJoin)
                      .HasForeignKey(e => e.ShowID).IsRequired(false).OnDelete(DeleteBehavior.Cascade));
        
        
        #endregion
    
    }
}
