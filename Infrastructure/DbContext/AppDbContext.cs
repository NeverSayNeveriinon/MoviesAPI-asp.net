using System.Text.Json;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Core.Domain.Entities;
using Core.Domain.Entities.JoinEntities;
using Core.Domain.IdentityEntities;
// using Newtonsoft.Json;
// using Newtonsoft.Json.Converters;


namespace Infrastructure.DbContext;

public class AppDbContext : IdentityDbContext<ApplicationUser,ApplicationRole,Guid>
{
    public virtual DbSet<Show> Shows { get; set; } 
    public virtual DbSet<Movie> Movies { get; set; } 
    public virtual DbSet<Serial> Series { get; set; }
    public virtual DbSet<Genre> Genres { get; set; }
    public virtual DbSet<Person> Persons { get; set; }
    
    
     // This Constructor with the 'DbContextOptions' parameter must be defined, if not we will get a compiler error
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        // this.Set<ShowsWritersJoin>();
    }


    //
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);



        #region Seed_Data

        // Seed Data for Movies //

        // 'movies seed data json' path
        string moviesSeed_Path = @"C:\Visual Studio\Personal Projects\Movies\WebAPI\wwwroot\JSONSeed\seed_movies.json";

        // inserting one row in table per 'each MovieDTO object in List'
        modelBuilder.Entity<Movie>().HasData(JsonToListEntity<Movie>(moviesSeed_Path));  
        
        
        // Seed Data for Series //

        // 'series seed data json' path
        string seriesSeed_Path = @"C:\Visual Studio\Personal Projects\Movies\WebAPI\wwwroot\JSONSeed\seed_series.json";

        // inserting one row in table per 'each Serial object in List'
        modelBuilder.Entity<Serial>().HasData(JsonToListEntity<Serial>(seriesSeed_Path));  
        
        
        // Seed Data for Persons //

        // 'persons seed data json' path
        string personsSeed_Path = @"C:\Visual Studio\Personal Projects\Movies\WebAPI\wwwroot\JSONSeed\seed_persons.json";

        // inserting one row in table per 'each Person object in List'
        modelBuilder.Entity<Person>().HasData(JsonToListEntity<Person>(personsSeed_Path));    
        
        
        // Seed Data for Genres //

        // 'genres seed data json' path
        string genresSeed_Path = @"C:\Visual Studio\Personal Projects\Movies\WebAPI\wwwroot\JSONSeed\seed_genres.json";

        // inserting one row in table per 'each Genre object in List'
        modelBuilder.Entity<Genre>().HasData(JsonToListEntity<Genre>(genresSeed_Path));    
        
        
        // Seed Data for shows.artists //

        // 'shows.artists seed data json' path
        string shows_artistsSeed_Path = @"C:\Visual Studio\Personal Projects\Movies\WebAPI\wwwroot\JSONSeed\seed_shows.artists.json";

        // inserting one row in table per 'each ShowsArtistsJoin object in List'
        modelBuilder.Entity<ShowsArtistsJoin>().HasData(JsonToListEntity<ShowsArtistsJoin>(shows_artistsSeed_Path));      
        
        
        // Seed Data for shows.genres //

        // 'shows.genres seed data json' path
        string shows_genresSeed_Path = @"C:\Visual Studio\Personal Projects\Movies\WebAPI\wwwroot\JSONSeed\seed_shows.genres.json";

        // inserting one row in table per 'each ShowsGenresJoin object in List'
        modelBuilder.Entity<ShowsGenresJoin>().HasData(JsonToListEntity<ShowsGenresJoin>(shows_genresSeed_Path));      
        
        
        // Seed Data for shows.writers //

        // 'shows.writers seed data json' path
        string shows_writersSeed_Path = @"C:\Visual Studio\Personal Projects\Movies\WebAPI\wwwroot\JSONSeed\seed_shows.writers.json";

        // inserting one row in table per 'each ShowsWritersJoin object in List'
        modelBuilder.Entity<ShowsWritersJoin>().HasData(JsonToListEntity<ShowsWritersJoin>(shows_writersSeed_Path));      
        
        
        // Seed Data for series.directors //

        // 'series.directors seed data json' path
        string series_directorsSeed_Path = @"C:\Visual Studio\Personal Projects\Movies\WebAPI\wwwroot\JSONSeed\seed_series.directors.json";

        // inserting one row in table per 'each SeriesDirectorsJoin object in List'
        modelBuilder.Entity<SeriesDirectorsJoin>().HasData(JsonToListEntity<SeriesDirectorsJoin>(series_directorsSeed_Path));  
        
        
        
        // Seed Data for Roles (user,admin) //
            
        List<ApplicationRole> rolesList =
        [
            new ApplicationRole() { Id = Guid.NewGuid(),Name = "User", NormalizedName = "USER", 
                ConcurrencyStamp = Guid.NewGuid().ToString()},
            new ApplicationRole() { Id = Guid.NewGuid(),Name = "Admin", NormalizedName = "ADMIN", 
                ConcurrencyStamp = Guid.NewGuid().ToString()}
        ];
        modelBuilder.Entity<ApplicationRole>().HasData(rolesList);

        //
        // List<MovieDTO> moviesList =
        // [
        //     new MovieDTO()
        //     {
        //         ID = Guid.NewGuid(),
        //         Name = "Interstellar",
        //         PublishYear = 2014,
        //         Time = TimeOnly.Parse("02:00:00")
        //     }
        // ];
        // modelBuilder.Entity<MovieDTO>().HasData(moviesList);
        
        #endregion

        
        #region Fluent_API_Configuration
        
        // Fluent API Configuration //
        
        // -
        modelBuilder.HasDefaultSchema("dbo");

        // -
        modelBuilder.Entity<Show>().ToTable("Shows")
                                   .UseTphMappingStrategy();
        modelBuilder.Entity<Genre>().ToTable("Genres");
        modelBuilder.Entity<Person>().ToTable("Persons");

        
        // - Relationships
        
        // MovieDTO 'N'====......----'1' Director(person)
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

    
    
    // User-Defined Functions
    private List<TEntity> JsonToListEntity<TEntity>(string Seed_Path) where TEntity : class
    {
        // Read the json file into a string
        string Seed_Json = File.ReadAllText(Seed_Path);

        // Deserialize the json file to 'a List of TEntity'
        // List<TEntity> Seed_List = JsonConvert.DeserializeObject<List<TEntity>>(Seed_Json)!;
        //
        List<TEntity> Seed_List = JsonSerializer.Deserialize<List<TEntity>>(Seed_Json)!;
        

        return Seed_List;
    }
}
