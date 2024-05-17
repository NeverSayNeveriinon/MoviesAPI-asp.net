using Microsoft.AspNetCore.Mvc;

using Core.DTO.MovieDTO;
using Core.ServiceContracts;


namespace WebAPI.Controllers;

[Route("api/[controller]")] 
[ApiController]
public class MovieController : ControllerBase
{
    private readonly IMovieService _movieService;
    
    public MovieController(IMovieService movieService)
    {
        _movieService = movieService;
    }
    
    [Route("/")]
    [ApiExplorerSettings(IgnoreApi = true)] // For not showing in 'Swagger'
    public IActionResult Index()
    {
        return Content("Here is the \"Movie\" Home Page");
    }
    
    
    
    /// <summary>
    /// Get All Existing Movies
    /// </summary>
    /// <returns>The Movies List</returns>
    /// <remarks>       
    /// Sample request:
    /// 
    ///     Get -> "api/movie"
    /// 
    /// </remarks>
    /// <response code="200">The Movies List is successfully returned</response>
    [HttpGet]
    // GET: api/Movie
    public async Task<ActionResult<IEnumerable<MovieResponse>>> GetMovies()
    {
        List<MovieResponse> moviesList = await _movieService.GetAllMovies();
        return Ok(moviesList);
    }
    
     
    /// <summary>
    /// Add a New Movie to Movies List
    /// </summary>
    /// <returns>Redirect to 'GetMovie' action to return Movie That Has Been Added</returns>
    /// <remarks>       
    /// Sample request:
    /// 
    ///     POST -> "api/movie"
    ///     {
    ///        "ID": "0866469B-A885-41AA-915C-F5697514CC26",
    ///        "Name": "Gone Girl",
    ///        "PublishYear": 2014,
    ///        "CountryName": "USA",
    ///        "Languages": "English",
    ///        "IMDBPage": "page111",
    ///        "IMDBRating": 8.1,
    ///        "ImagePath": "path111",
    ///        "Time": "02:29:00",
    ///        "Summary": "A movie about wired relationship",
    ///        "DirectorID": "B165C22A-CBB8-4FE9-8697-13D5400379B0",
    ///        "WritersID": 
    ///        [
    ///            "35468564-B78C-419E-9442-B2078B9E7AEA"
    ///        ],
    ///        "ArtistsID": [],
    ///        "GenresID": 
    ///        [
    ///            "69F4AC8F-DFC1-4597-811B-7A7853540B91",
    ///            "81E8F391-5290-49DB-A14D-3DDCDF0C0307"
    ///        ]
    ///     }
    /// 
    /// </remarks>
    /// <response code="201">The New Movie is successfully added to Movies List</response>
    /// <response code="400">There is sth wrong in Validation of properties</response>
    [HttpPost]
    // Post: api/Movie
    public async Task<IActionResult> PostMovie(MovieRequest movie)
    {
        // No need to do this, because it is done by 'ApiController' attribute in BTS
        // if (!ModelState.IsValid)
        // {
        //     return ValidationProblem(ModelState);
        // }
        
        await _movieService.AddMovie(movie);
        
        return CreatedAtAction(nameof(GetMovie), new {movieID = movie.ID}, movie);
    }
    
    
    
    /// <summary>
    /// Get an Existing Movie Based On Given ID
    /// </summary>
    /// <returns>The Movie That Has Been Found</returns>
    /// <remarks>       
    /// Sample request:
    /// 
    ///     Get -> "api/movie/0866469B-A885-41AA-915C-F5697514CC26"
    /// 
    /// </remarks>
    /// <response code="200">The Movie is successfully found and returned</response>
    /// <response code="404">A Movie with Given ID has not been found</response>
    [HttpGet("{movieID:guid}")]
    // GET: api/Movie/{movieID}
    public async Task<ActionResult<MovieResponse>> GetMovie(Guid movieID)
    {
        MovieResponse? movieObject = await _movieService.GetMovieByID(movieID);
        if (movieObject is null)
        {
            return NotFound("notfound:");
        }
        
        return Ok(movieObject);
    }
    
    
    /// <summary>
    /// Update an Existing Movie Based on Given ID and New Movie Object
    /// </summary>
    /// <returns>Nothing</returns>
    /// <remarks>       
    /// Sample request:
    /// 
    ///     Put -> "api/movie/0866469B-A885-41AA-915C-F5697514CC26"
    ///     {
    ///        "ID": "0866469B-A885-41AA-915C-F5697514CC26",
    ///        "Name": "Gone Girl Edited",
    ///        "PublishYear": 2014,
    ///        "CountryName": "USA",
    ///        "Languages": "English",
    ///        "IMDBPage": "page111",
    ///        "IMDBRating": 8.1,
    ///        "ImagePath": "path111",
    ///        "Time": "02:29:00",
    ///        "Summary": "A movie about wired relationship and also a wired woman",
    ///        "DirectorID": "B165C22A-CBB8-4FE9-8697-13D5400379B0",
    ///        "WritersID": 
    ///        [
    ///            "35468564-B78C-419E-9442-B2078B9E7AEA"
    ///        ],
    ///        "ArtistsID": [],
    ///        "GenresID": 
    ///        [
    ///            "69F4AC8F-DFC1-4597-811B-7A7853540B91",
    ///            "81E8F391-5290-49DB-A14D-3DDCDF0C0307"
    ///        ]
    ///     }
    /// 
    /// </remarks>
    /// <response code="204">The Movie is successfully found and has been updated with New Movie</response>
    /// <response code="400">The ID in Url doesn't match with the ID in Body</response>
    /// <response code="404">A Movie with Given ID has not been found</response>
    [HttpPut("{movieID:guid}")]
    // Put: api/Movie/{movieID}
    public async Task<IActionResult> PutMovie(MovieRequest movie, Guid movieID)
    {
        if (movieID != movie.ID)
        {
            return Problem(detail:"The ID in Url doesn't match with the ID in Body", statusCode:400, title: "Problem With the ID");
        }

        MovieResponse? existingCity = await _movieService.UpdateMovie(movie, movieID);
        if (existingCity is null)
        {
            return NotFound("notfound:");
        }
        
        return NoContent();
    }
    
    
    /// <summary>
    /// Delete an Existing Movie Based on Given ID
    /// </summary>
    /// <returns>Nothing</returns>
    /// <remarks>       
    /// Sample request:
    /// 
    ///     Delete -> "api/movie/0866469B-A885-41AA-915C-F5697514CC26"
    /// 
    /// </remarks>
    /// <response code="204">The Movie is successfully found and has been deleted from Movies List</response>
    /// <response code="404">A Movie with Given ID has not been found</response>
    [HttpDelete("{movieID:guid}")]
    // Delete: api/Movie/{movieID}
    public async Task<IActionResult> DeleteMovie(Guid movieID)
    {
        bool? movieObject = await _movieService.DeleteMovie(movieID);
        if (movieObject is null)
        {
            return NotFound("notfound:");
        }

        return NoContent();
    }
}