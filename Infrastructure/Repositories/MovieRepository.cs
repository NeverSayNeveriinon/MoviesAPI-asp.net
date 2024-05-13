using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Logging;

using Core.Domain.Entities;
using Core.RepositoryContracts;
using Infrastructure.DbContext;
using Infrastructure.Helpers.Extensions;



namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _dbContext;

    // private readonly ILogger<MovieRepository>? _logger;

    
    public MovieRepository(AppDbContext dbContext
        // , ILogger<MovieRepository>? logger
        )
    {
        _dbContext = dbContext;
        
        // _logger = logger;
    }
   

    public async Task<List<Movie>> GetAllMovies(string includeEntities = "")
    {
        // _logger?.LogInformation("~~~ Started 'GetAllMovies' method of 'Movies' repository ");

        
        // List<MovieDTO> moviesList = await _dbContext.Movies.Include(movie => movie.Director)
        //                                                    .ToListAsync();
        
        // List<MovieDTO> moviesList = await _dbContext.Movies.IncludeEntities<MovieDTO>(includeEntities)
        //                                                 .ToListAsync();
        
        var movies = _dbContext.Movies.IncludeEntities(includeEntities);
        List<Movie> moviesList = await movies.ToListAsync();
        
        
        
        return moviesList;
    }

    public async Task<Movie?> GetMovieByID(Guid id,string includeEntities = "")
    {
        Movie? movie = await _dbContext.Movies.IncludeEntities(includeEntities)
                                                .FirstOrDefaultAsync(movieItem => movieItem.ID == id);

        return movie;
    }

     
    public async Task<Movie> AddMovie(Movie movie)
    {
        _dbContext.Movies.Add(movie);
        await _dbContext.SaveChangesAsync();

        return movie;
    }
    
    public async Task<Movie> UpdateMovie(Movie movie, Movie updatedMovie)
    {
        movie.ID = updatedMovie.ID;
        movie.Name = updatedMovie.Name;
        movie.PublishYear = updatedMovie.PublishYear;
        movie.CountryName = updatedMovie.CountryName;
        movie.Summary = updatedMovie.Summary;
        movie.Languages = updatedMovie.Languages;
        movie.IMDBPage = updatedMovie.IMDBPage;
        movie.IMDBRating = updatedMovie.IMDBRating;
        movie.ImagePath = updatedMovie.ImagePath;
        movie.Time = updatedMovie.Time;

        await _dbContext.SaveChangesAsync();
        return movie;
    }
    
    public async Task<bool> DeleteMovie(Movie movie)
    {
        _dbContext.Movies.Remove(movie);
        int rowsAffected = await _dbContext.SaveChangesAsync();
        
        bool result = rowsAffected > 0 ? true : false;
        return result;
    }

    public async Task<List<Movie>> GetFilteredMovies(Expression<Func<Movie, bool>> predicate, string includeEntities = "")
    {
        // _logger?.LogInformation("~~~ Started 'GetFilteredMovies' method of 'Movies' repository ");

        List<Movie> moviesList = await _dbContext.Movies.IncludeEntities(includeEntities)
                                                           .Where(predicate)
                                                           .ToListAsync();
        return moviesList;
    }

}