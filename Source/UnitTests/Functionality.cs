using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviePotato.Functionality;
using MoviePotato.Models;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace UnitTests
{
	[TestClass]
	public class Functionality
	{
		private static readonly string DATABASE_TEST = "dbtest.xml";
		private static readonly string DATABASE_NEW = "dbnew.xml";
		private static readonly string DATABASE_NONE = "dbnone.xml";
		private static readonly string FILENAME = "file.txt";
		public string BaseDirectory { get { return System.AppDomain.CurrentDomain.BaseDirectory; } }

		public List<Video> Movies { get; set; }
		public Video Video { get; set; }

		[TestMethod]
		public void ShouldHandleNoDatabaseFile()
		{
			Assert.IsFalse(MovieExplorerReader.HasDatabase(DATABASE_NONE));
		}

		[TestMethod]
		public void ShouldHandleNewDatabase()
		{
			Movies = MovieExplorerReader.GetAllVideos(DATABASE_NEW);

			Assert.IsTrue(MovieExplorerReader.HasDatabase(DATABASE_NEW));
			Assert.AreEqual(Movies.Count, 0);
			Assert.AreEqual(Movies.Unwatched().GetCount(), 0);
		}

		[TestMethod]
		public void ShouldHandleDatabaseWithItems()
		{
			Movies = MovieExplorerReader.GetAllVideos(DATABASE_TEST);

			Assert.IsTrue(MovieExplorerReader.HasDatabase(DATABASE_TEST));
			Assert.AreEqual(Movies.Count, 2);
			Assert.AreEqual(Movies.Unwatched().GetCount(), 1);
			Assert.IsNotNull(Movies.Unwatched().Random());
		}

		[TestMethod]
		public void ShouldFindFile()
		{
			Video = new Video()
			{
				Location = BaseDirectory,
				FileName = FILENAME,
				Watched = true
			};

			Assert.IsTrue(Video.HasLocalCopy);
		}
	}
}