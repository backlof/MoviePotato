using MoviePotato.Functionality;
using MoviePotato.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MoviePotato.Services
{
    public class MovieExplorerService : IMovieService
    {
        public List<Video> UnwatchedVideos { get; set; }

        public MovieExplorerService()
        {
            if (HasDatabase())
            {
                UnwatchedVideos = MovieExplorerReader.GetAllMovies().Unwatched().ToList();
            }
        }

        public bool HasAtLeastOneUnWatchedVideo()
        {
            return UnwatchedVideos.Any();
        }

        public bool HasDatabase()
        {
            return MovieExplorerReader.HasDatabase();
        }

        public Video GetRandomUnwatchedVideo()
        {
            return UnwatchedVideos.Random();
        }
    }
}