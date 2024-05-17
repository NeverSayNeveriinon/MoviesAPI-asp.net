using Core.Domain.Entities;
using Core.Domain.RepositoryContracts;
using Core.DTO.MovieDTO;
using Core.Enums;
using Core.ServiceContracts;


namespace Core.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    

    public async Task<MovieResponse> AddMovie(MovieRequest? movieAddRequest)
    {
        // 'movieAddRequest' is Null //
        ArgumentNullException.ThrowIfNull(movieAddRequest,"The 'MovieRequest' object parameter is Null");
        
        // ValidationHelper.ModelValidation(movieAddRequest);

        // // 'movieAddRequest.Name' is Duplicate //
        // // Way 1
        // if ( (await _moviesRepository.GetFilteredMovies(movie => movie.Name == movieAddRequest.Name))?.Count > 0)
        // {
        //     throw new ArgumentException("The 'Movie Name' is already exists");
        // }


        // 'movieAddRequest.Name' is valid and there is no problem //
        Movie movie = movieAddRequest.ToMovie();
        Movie movieReturned = await _movieRepository.AddMovie(movie);

        return movieReturned.ToMovieResponse();
    }   

    public async Task<List<MovieResponse>> GetAllMovies()
    {
        const string includeEntities = "Director,Writers,Artists,Genres"; 
        List<Movie> movies = await _movieRepository.GetAllMovies(includeEntities);
        
        List<MovieResponse> moviesResponses = movies.Select(movieItem => movieItem.ToMovieResponse()).ToList();
        return moviesResponses;
    }

    public async Task<MovieResponse?> GetMovieByID(Guid? ID)
    {
        // if 'id' is null
        ArgumentNullException.ThrowIfNull(ID,"The Movie'ID' parameter is Null");

        const string includeEntities = "Director,Writers,Artists,Genres"; 
        Movie? movie = await _movieRepository.GetMovieByID(ID.Value, includeEntities);

        // if 'id' doesn't exist in 'movies list' 
        if (movie == null)
        {
            return null;
        }

        // if there is no problem
        MovieResponse movieResponse = movie.ToMovieResponse();

        return movieResponse;;
    }

    public async Task<MovieResponse?> UpdateMovie(MovieRequest? movieUpdateRequest, Guid? movieID)
    {
        // if 'movie ID' is null
        ArgumentNullException.ThrowIfNull(movieID,"The Movie'ID' parameter is Null");
        
        // if 'movieUpdateRequest' is null
        ArgumentNullException.ThrowIfNull(movieUpdateRequest,"The 'MovieRequest' object parameter is Null");

        
        // Validation
        // ValidationHelper.ModelValidation(movieUpdateRequest);

        const string includeEntities = "Director,Writers,Artists,Genres"; 
        Movie? movie = await _movieRepository.GetMovieByID(movieID.Value, includeEntities);
        
        // if 'ID' is invalid (doesn't exist)
        if (movie == null)
        {
            return null;
        }
            
        Movie updatedMovie = await _movieRepository.UpdateMovie(movie, movieUpdateRequest.ToMovie());

        return updatedMovie.ToMovieResponse();
    }

    public async Task<bool?> DeleteMovie(Guid? ID)
    {
        // if 'id' is null
        ArgumentNullException.ThrowIfNull(ID,"The Movie'ID' parameter is Null");

        Movie? movie = await _movieRepository.GetMovieByID(ID.Value);
        
        // if 'ID' is invalid (doesn't exist)
        if (movie == null) 
        {
            return null;
        }
    
        bool result = await _movieRepository.DeleteMovie(movie);
            
        return result;
    }
    
    
    public Task<List<MovieResponse>> GetSearchedMovies(string searchBy, string? searchString)
    {
        throw new NotImplementedException();
    }

    public Task<List<MovieResponse>> GetSortedMovies(List<MovieResponse> allMovies, string sortBy, SortOrderOptions sortOrder)
    {
        throw new NotImplementedException();
    }
}
