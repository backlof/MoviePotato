using MoviePotato.Functionality;
using MoviePotato.Models;
using System.Collections.Generic;
using System.Linq;
using System;

namespace MoviePotato.Services
{
    public class MovieExplorerService : IMovieService
    {
        public List<Video> ShuffledListOfVideos { get; set; }
        public Video RandomVideo { get; set; }

        public MovieExplorerService()
        {
            if (HasDatabase())
            {
                ShuffledListOfVideos = MovieExplorerReader.GetAllMovies().Unwatched().Shuffle().ToList();
                RandomVideo = ShuffledListOfVideos.FirstOrDefault(x => x.HasLocalCopy);
            }
        }

        public bool HasAtLeastOneUnWatchedMovie()
        {
            return ShuffledListOfVideos.Any();
        }

        public bool HasDatabase()
        {
            return MovieExplorerReader.HasDatabase();
        }

        public void PlayRandomUnwatchedMovie()
        {
            RandomVideo.Play();
        }

        public bool HasAtLeastOneLocalMovie()
        {
            return RandomVideo != null;
        }
    }
}