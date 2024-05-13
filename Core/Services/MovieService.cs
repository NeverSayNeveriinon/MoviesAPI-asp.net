using Core.Domain.Entities;
using Core.Enums;
using Core.RepositoryContracts;
using Core.ServiceContracts;

namespace Core.Services;

public class MovieService : IMovieService
{
    private readonly IMovieRepository _movieRepository;

    public MovieService(IMovieRepository movieRepository)
    {
        _movieRepository = movieRepository;
    }
    

    public async Task<Movie> AddMovie(Movie? movieAddRequest)
    {
        // 'movieAddRequest' is Null //
        ArgumentNullException.ThrowIfNull(movieAddRequest,"'MovieRequest' object is Null");
        
        // ValidationHelper.ModelValidation(movieAddRequest);

        // // 'movieAddRequest.Name' is Duplicate //
        // // Way 1
        // if ( (await _moviesRepository.GetFilteredMovies(movie => movie.Name == movieAddRequest.Name))?.Count > 0)
        // {
        //     throw new ArgumentException("The 'Movie Name' is already exists");
        // }


        // 'movieAddRequest.Name' is valid and there is no problem //

        Movie movie = await _movieRepository.AddMovie(movieAddRequest);

        return movie;
    }   

    public async Task<List<Movie>> GetAllMovies()
    {
        return await _movieRepository.GetAllMovies(includeEntities:"Directors,Writers,Artists,Genres");
    }

    public Task<Movie?> GetMovieByID(Guid? id)
    {
        throw new NotImplementedException();
    }

    public Task<List<Movie>> GetSearchedMovies(string searchBy, string? searchString)
    {
        throw new NotImplementedException();
    }

    public Task<List<Movie>> GetSortedMovies(List<Movie> allMovies, string sortBy, SortOrderOptions sortOrder)
    {
        throw new NotImplementedException();
    }

    public Task<Movie?> UpdateMovie(Movie? movieUpdateRequest)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteMovie(Guid? id)
    {
        throw new NotImplementedException();
    }
}