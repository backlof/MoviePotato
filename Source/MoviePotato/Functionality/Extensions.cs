using MoviePotato.Models;
using System.Collections.Generic;
using System.Linq;
using System;

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

		public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> ienum)
		{
			return ienum.Shuffle(new Random());
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

		private static IEnumerable<T> Shuffle<T>(this IEnumerable<T> ienum, Random rng)
		{
			var buffer = ienum.ToList();
			for (int i = 0; i < buffer.Count; i++)
			{
				int j = rng.Next(i, buffer.Count);
				yield return buffer[j];

				buffer[j] = buffer[i];
			}
		}
	}
}