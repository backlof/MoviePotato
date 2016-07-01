using MoviePotato.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Security.Cryptography;

namespace MoviePotato.Functionality
{
    public static class Extensions
    {
        public static IEnumerable<Video> ExistsLocally(this IEnumerable<Video> ienum)
        {
            return ienum.Where(x => x.HasLocalCopy);
        }

        public static T FirstOrDefault<T>(this T[] array)
        {
            if (array.Length == 0)
            {
                return default(T);
            }
            return array[0];
        }

        public static int GetCount<T>(this IEnumerable<T> ienum)
        {
            return ienum.Count();
        }

        public static T Random<T>(this IEnumerable<T> ienum)
        {
            if (ienum.Count() == 0) { return default(T); }
            return ienum.ElementAt(ienum.RandomIndex());
        }

        public static List<T> Shuffle<T>(this IEnumerable<T> ienum)
        {
            RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            var list = ienum.ToList();
            int n = list.Count;
            while (n > 1)
            {
                byte[] box = new byte[1];
                do provider.GetBytes(box);
                while (!(box[0] < n * (Byte.MaxValue / n)));
                int k = (box[0] % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }

            return list;
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