using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

using Core.Domain.Entities;
using Core.Domain.RepositoryContracts;
using Infrastructure.DbContext;
using Infrastructure.Helpers;


namespace Infrastructure.Repositories;

public class MovieRepository : IMovieRepository
{
    private readonly AppDbContext _dbContext;

    
    public MovieRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
   

    public async Task<List<Movie>> GetAllMovies(string includeEntities = "")
    {
        var movies = _dbContext.Movies.IncludeEntities(includeEntities)
                                      .AsNoTracking();
        
        List<Movie> moviesList = await movies.ToListAsync();
        
        return moviesList;
    }

    public async Task<Movie?> GetMovieByID(Guid id,string includeEntities = "")
    {
        Movie? movie = await _dbContext.Movies.IncludeEntities(includeEntities)
                                              .AsNoTracking()
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
        _dbContext.Attach(movie);
        _dbContext.Entry(movie).State = EntityState.Modified;
        
        
        // Writers Navigation
        movie.Writers.Clear();
        movie.ShowsWritersJoin.Clear(); // not needed in fact        
        _dbContext.Entry(movie).Collection(s => s.ShowsWritersJoin).CurrentValue = updatedMovie.ShowsWritersJoin;

        // Artists Navigation
        movie.Artists?.Clear();
        movie.ShowsArtistsJoin?.Clear(); // not needed in fact        
        _dbContext.Entry(movie).Collection(s => s.ShowsArtistsJoin!).CurrentValue = updatedMovie.ShowsArtistsJoin;

        // Genres Navigation
        movie.Genres.Clear();
        movie.ShowsGenresJoin.Clear(); // not needed in fact        
        _dbContext.Entry(movie).Collection(s => s.ShowsGenresJoin).CurrentValue = updatedMovie.ShowsGenresJoin;

        _dbContext.Entry(movie).CurrentValues.SetValues(updatedMovie);
        
        
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

        List<Movie> moviesList = await _dbContext.Movies.IncludeEntities(includeEntities)
                                                           .Where(predicate)
                                                           .ToListAsync();
        return moviesList;
    }

}