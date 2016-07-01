using MoviePotato.Models;

namespace MoviePotato.Services
{
    public interface IMovieService
    {
        bool HasDatabase();

        bool HasAtLeastOneUnWatchedVideo();

        Video GetRandomUnwatchedVideo();
    }
}