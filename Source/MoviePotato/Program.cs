﻿using MoviePotato.Services;
using MoviePotato.Utilities;
using Ninject;
using System;

namespace MoviePotato
{
    public class Program
    {
        private static IKernel Container;
        private static IMovieService MovieService;

        public static void InitializeIoC()
        {
            Container = new StandardKernel();
            Container.Bind<IMovieService>().To<MovieExplorerService>().InSingletonScope();

            MovieService = Container.Get<IMovieService>();
        }

        public static void Main(string[] args)
        {
            InitializeIoC();
            TryPlayUnwatchedMovie();
        }

        public static void ShowWarningThenPromptBeforeExit(string warning)
        {
            Console.WriteLine(warning);
            Console.Write(ApplicationText.EXIT_PROMPT);
            Console.ReadKey();
        }

        public static void TryPlayUnwatchedMovie()
        {
            if (!MovieService.HasDatabase())
            {
                ShowWarningThenPromptBeforeExit(ApplicationText.ERROR_NO_DATABASE);
            }
            else if (!MovieService.HasAtLeastOneUnWatchedVideo())
            {
                ShowWarningThenPromptBeforeExit(ApplicationText.WARNING_NO_UNWATCHED);
            }
            else
            {
                var file = MovieService.GetRandomUnwatchedVideo();
                if (!file.TryPlay())
                {
                    ShowWarningThenPromptBeforeExit(String.Format("Could not find file: {0} in {1}", file.FileName, file.Location));
                }
            }
        }
    }
}
