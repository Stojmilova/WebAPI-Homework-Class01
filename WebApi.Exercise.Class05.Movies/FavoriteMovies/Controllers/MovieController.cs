using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Models;
using Services;


namespace FavoriteMovies.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IMovieService _movieService;
        public MovieController(IMovieService movieService)
        {
            _movieService = movieService;
        }

        // 1.Get all movies:
        //localhost:61121/api/movie
        [HttpGet]
        public ActionResult<List<MovieModel>> GetMovies()
        {
            var userId = GetAuthorizeduserId();
            return Ok(_movieService.GetUserMovies(userId));
        }

        // 2.Get movie by id:
        //localhost:61121/api/movie/movieById/id
        [HttpGet("movieById/{id}")]
        public ActionResult<MovieModel> GetMovie(int id)
        {
            var userId = GetAuthorizeduserId();
            return Ok(_movieService.GetMovie(id, userId));
        }

        // 3.Create new movie:
        //localhost:61121/api/movie/addNewMovie
        [HttpPost("addNewMovie")]
        public void AddMovie([FromBody] MovieModel movie)
        {
            movie.UserId = GetAuthorizeduserId();
            _movieService.AddMovie(movie);
        }

        //4.Get movie by genre:
        //localhost:61121/api/movie/movieByGenre/GenreNum
        [HttpGet("movieByGenre/{GenreNum}")]
        public ActionResult<List<MovieModel>>GetByGenre(int GenreNum)
        {
            var userId = GetAuthorizeduserId();
            return Ok(_movieService.GetMovieByGenre(GenreNum, userId));
        }

        private int GetAuthorizeduserId()
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out var userId))
            {
                throw new Exception("name identifier claim does not exist!");
            }
            return userId;
        }

    }
}