using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DataAccess;
using DataModels;
using Microsoft.Extensions.Options;
using Models;
using Models.Enum;
using Services.Helpers;

namespace Services
{
    public class MovieService : IMovieService
    {
        private readonly IRepository<UserDto> _userRepository;
        private readonly IRepository<MovieDto> _movieRepository;
        private readonly IOptions<AppSettings> _options;

        public MovieService(IRepository<UserDto> userRepository, IRepository<MovieDto> movieRepository, IOptions<AppSettings> options)
        {
            _userRepository = userRepository;
            _movieRepository = movieRepository;
            _options = options;
        }

        public void AddMovie(MovieModel model)
        {
            var user = _userRepository.GetAll().FirstOrDefault(x => x.Id == model.UserId);

            var movieModel = new MovieDto()
            {
                Id = model.Id,
                Title = model.Title,
                Description = model.Description,
                Year = model.Year,
                Genre = (int)model.GenreType,
                UserId = user.Id

            };
            _movieRepository.Add(movieModel);
        }

        public void DeleteMovie(int id, int userId)
        {          
            var movie = _movieRepository.GetAll().FirstOrDefault(x => x.Id == id && x.UserId == userId);
            _movieRepository.Delete(movie);
        }

        public MovieModel GetMovie(int id, int userId)
        {
            var movie = _movieRepository.GetAll().FirstOrDefault(x => x.Id == id && x.UserId == userId);

            MovieModel model = new MovieModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Year = movie.Year,
                GenreType = (Genre)movie.Genre,
                UserId = movie.UserId
            };
            return model;
        }

        public MovieModel GetMovieByGenre(int genre, int userId)
        {
            var movie = _movieRepository.GetAll().FirstOrDefault(x => x.Genre == genre && x.UserId == userId);

            MovieModel model = new MovieModel()
            {
                Id = movie.Id,
                Title = movie.Title,
                Description =movie.Description,
                Year =movie.Year,
                GenreType = (Genre)movie.Genre,
                UserId  = movie.UserId
            };
            return model;
        }
        public IEnumerable<MovieModel> GetUserMovies(int userId)
        {
            return _movieRepository.GetAll()
               .Where(x => x.UserId == userId).Select(x =>
               new MovieModel()
               {
                   Id = x.Id,
                   Title = x.Title,
                   Description = x.Description,
                   Year = x.Year,
                   GenreType = (Genre)x.Genre,
                   UserId = x.UserId
               });
        }
    }
}
