using MoviePotato.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Security.Cryptography;

namespace MoviePotato.Functionality
{
    public static class Extensions
    {
        public static int GetCount<T>(this IEnumerable<T> ienum)
        {
            return ienum.Count();
        }

        public static T Random<T>(this IEnumerable<T> ienum)
        {
            if (ienum.Count() == 0) { return default(T); }
            return ienum.ElementAt(ienum.RandomIndex());
        }

        public static List<T> ToList<T>(this T[] array)
        {
            return new List<T>(array);
        }

        public static IEnumerable<Video> Unwatched(this IEnumerable<Video> ienum)
        {
            return ienum.Where(x => !x.Watched);
        }

        private static Random Random()
        {
            return new Random();
        }

        private static int RandomIndex<T>(this IEnumerable<T> ienum)
        {
            return Random().Next(0, ienum.Count());
        }
    }
}