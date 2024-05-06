using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using Entities.JoinEntities;
using Entities.IdentityEntities;

namespace Entities;

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
        
        
        // Seed Data for Roles (user,admin) //
            
        List<ApplicationRole> rolesList =
        [
            new ApplicationRole() { Id = Guid.NewGuid(),Name = "User", NormalizedName = "USER", 
                ConcurrencyStamp = Guid.NewGuid().ToString()},
            new ApplicationRole() { Id = Guid.NewGuid(),Name = "Admin", NormalizedName = "ADMIN", 
                ConcurrencyStamp = Guid.NewGuid().ToString()}
        ];
        modelBuilder.Entity<ApplicationRole>().HasData(rolesList);
        
        
        
        
        // Fluent API Configuration //
        
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
                    .HasForeignKey(e => e.DirectorID).IsRequired().OnDelete(DeleteBehavior.Cascade);

        
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
    }
}
