using System.Linq.Expressions;

using Core.Domain.Entities;


namespace Core.Domain.RepositoryContracts;

/// <summary>
/// Represents data access logic for managing MovieDTO entity
/// </summary>
public interface IMovieRepository
{

    /// <summary>
    /// Returns all movies in the data store
    /// </summary>
    /// <param name="includeEntities">the name of navigations to </param>
    /// <returns>List of movie objects from table</returns>
    Task<List<Movie>> GetAllMovies(string includeEntities = "");


    /// <summary>
    /// Returns a movie object based on the given movie id
    /// </summary>
    /// <param name="id">MovieID (guid) to search</param>
    /// <param name="includeEntities">the name of navigations to </param>
    /// <returns>A movie object or null</returns>
    Task<Movie?> GetMovieByID(Guid id, string includeEntities = "");


    /// <summary>
    /// Adds a movie object to the data store
    /// </summary>
    /// <param name="movie">MovieDTO object to add</param>
    /// <returns>Returns the movie object after adding it to the table</returns>
    Task<Movie> AddMovie(Movie movie);

    
    /// <summary>
    /// Deletes a movie object based on the given movie object
    /// </summary>
    /// <param name="movie">MovieDTO object to delete</param>
    /// <returns>Returns true, if the deletion is successful; otherwise false</returns>
    Task<bool> DeleteMovie(Movie movie);


    /// <summary>
    /// Updates a movie object (movie name and other details) based on the given movie object (updatedMovie)
    /// </summary>
    /// <param name="movie">MovieDTO object to be updated</param>
    /// <param name="updatedMovie">The updated MovieDTO object to apply to actual movie object</param>
    /// <returns>Returns the updated movie object</returns>
    Task<Movie> UpdateMovie(Movie movie, Movie updatedMovie);
    
    
    /// <summary>
    /// Returns all movie objects based on the given expression
    /// </summary>
    /// <param name="predicate">LINQ expression to check</param>
    /// <param name="includeEntities">the name of navigations to </param>
    /// <returns>All matching movies with given condition</returns>
    Task<List<Movie>> GetFilteredMovies(Expression<Func<Movie, bool>> predicate, string includeEntities = "");
}