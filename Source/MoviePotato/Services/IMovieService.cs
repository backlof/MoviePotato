namespace MoviePotato.Services
{
	public interface IMovieService
	{
		bool HasDatabase();

		bool HasAtLeastOneUnWatchedMovie();

		bool HasAtLeastOneLocalMovie();

		void PlayRandomUnwatchedMovie();
	}
}