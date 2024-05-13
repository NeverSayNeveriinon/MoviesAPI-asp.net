using Core.Domain.Entities;
using Core.Enums;

namespace Core.ServiceContracts;

public interface IMovieService
{
    /// <summary>
    /// Add a movie object to movies list
    /// </summary>
    /// <param name="movieAddRequest">Movie object to be added</param>
    /// <return>Returns the new movie object (with ID and other attributes) after adding it</return>
    public Task<Movie> AddMovie(Movie? movieAddRequest);


    /// <summary>
    /// Retrieve all Movie objects from movies list
    /// </summary>
    /// <returns>Returns all existing Movies</returns>
    public Task<List<Movie>> GetAllMovies();


    /// <summary>
    /// Retrieve a Movie object from movies list based on given id
    /// </summary>
    /// <param name="id">the movie id to be searched for</param>
    /// <returns></returns>
    public Task<Movie?> GetMovieByID(Guid? id);


    /// <summary>
    /// Search for proper movie objects in movies list
    /// </summary>
    /// <param name="searchBy">search field(property) to search</param>
    /// <param name="searchString">search string to search</param>
    /// <returns>Returns matching movies based on 'searchBy' and 'searchString' </returns>
    public Task<List<Movie>> GetSearchedMovies(string searchBy, string? searchString);


    /// <summary>
    /// Sort all movies in order of 'sortBy' field and in sortOrder (ASC or DSC)
    /// </summary>
    /// <param name="allMovies"></param>
    /// <param name="sortBy"></param>
    /// <param name="sortOrder"></param>
    /// <returns>Returns sorted movies in ASC or DSC (sortOrder) based on 'sortBy'</returns>
    public Task<List<Movie>> GetSortedMovies(List<Movie> allMovies, string sortBy, SortOrderOptions sortOrder);

    
    /// <summary>
    /// Find the object in movies list and update it with new details, then returns the 'Movie' object
    /// </summary>
    /// <param name="movieUpdateRequest"></param>
    /// <returns>Returns the movie with updated details</returns>
    public Task<Movie?> UpdateMovie(Movie? movieUpdateRequest);


    /// <summary>
    /// Find and Delete the movie object with given 'id' from the 'movies list'
    /// </summary>
    /// <param name="id"></param>
    /// <returns>Returns 'True' if movie is deleted and if it isn't 'False'</returns>
    public Task<bool> DeleteMovie(Guid? id); 
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    
    // /// <summary>
    // /// Add a movie object to movies list
    // /// </summary>
    // /// <param name="movieAddRequest">Movie object to be added</param>
    // /// <return>Returns the new movie object (with ID and other attributes) after adding it</return>
    // public Task<MovieResponse> AddMovie(MovieRequest? movieAddRequest);
    //
    //
    // /// <summary>
    // /// Retrieve all Movie objects from movies list
    // /// </summary>
    // /// <returns>Returns all existing Movies</returns>
    // public Task<List<MovieResponse>> GetAllMovies();
    //
    //
    // /// <summary>
    // /// Retrieve a Movie object from movies list based on given id
    // /// </summary>
    // /// <param name="id">the movie id to be searched for</param>
    // /// <returns></returns>
    // public Task<MovieResponse?> GetMovieByID(Guid? id);
    //
    //
    // /// <summary>
    // /// Search for proper movie objects in movies list
    // /// </summary>
    // /// <param name="searchBy">search field(property) to search</param>
    // /// <param name="searchString">search string to search</param>
    // /// <returns>Returns matching movies based on 'searchBy' and 'searchString' </returns>
    // public Task<List<MovieResponse>> GetSearchedMovies(string searchBy, string? searchString);
    //
    //
    // /// <summary>
    // /// Sort all movies in order of 'sortBy' field and in sortOrder (ASC or DSC)
    // /// </summary>
    // /// <param name="allMovies"></param>
    // /// <param name="sortBy"></param>
    // /// <param name="sortOrder"></param>
    // /// <returns>Returns sorted movies in ASC or DSC (sortOrder) based on 'sortBy'</returns>
    // public Task<List<MovieResponse>> GetSortedMovies(List<MovieResponse> allMovies, string sortBy, SortOrderOptions sortOrder);
    //
    //
    // /// <summary>
    // /// Find the object in movies list and update it with new details, then returns the 'MovieResponse' object
    // /// </summary>
    // /// <param name="movieUpdateRequest"></param>
    // /// <returns>Returns the movie with updated details</returns>
    // public Task<MovieResponse> UpdateMovie(MovieUpdateRequest? movieUpdateRequest);
    //
    //
    // /// <summary>
    // /// Find and Delete the movie object with given 'id' from the 'movies list'
    // /// </summary>
    // /// <param name="id"></param>
    // /// <returns>Returns 'True' if movie is deleted and if it isn't 'False'</returns>
    // public Task<bool> DeleteMovie(Guid? id); 
}