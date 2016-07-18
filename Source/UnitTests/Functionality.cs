using Microsoft.VisualStudio.TestTools.UnitTesting;
using MoviePotato.Functionality;
using MoviePotato.Models;
using System.Collections.Generic;

namespace UnitTests
{
    [TestClass]
    public class Functionality
    {
        private static readonly string DATABASE_TEST = "dbtest.xml";
        private static readonly string DATABASE_NEW = "dbnew.xml";
        private static readonly string DATABASE_NONE = "dbnone.xml";
        private static readonly string FILENAME = "file.txt";

        public string BaseDirectory => System.AppDomain.CurrentDomain.BaseDirectory;

        public Video Video { get; set; }
        public List<Video> VideosFromNewDatabase { get; set; }
        public List<Video> VideosFromTestDatabase { get; set; }

        public Functionality()
        {
            VideosFromNewDatabase = MovieExplorerReader.GetAllVideos(DATABASE_NEW);
            VideosFromTestDatabase = MovieExplorerReader.GetAllVideos(DATABASE_TEST);
            Video = new Video() { Location = BaseDirectory, FileName = FILENAME, Watched = true };
        }

        [TestMethod]
        public void ShouldHandleNoDatabaseFile()
        {
            Assert.IsFalse(MovieExplorerReader.HasDatabase(DATABASE_NONE));
        }

        [TestMethod]
        public void ShouldHandleNewDatabase()
        {
            Assert.IsTrue(MovieExplorerReader.HasDatabase(DATABASE_NEW));
            Assert.AreEqual(VideosFromNewDatabase.Count, 0);
            Assert.AreEqual(VideosFromNewDatabase.Unwatched().GetCount(), 0);
        }

        [TestMethod]
        public void ShouldHandleDatabaseWithItems()
        {
            Assert.IsTrue(MovieExplorerReader.HasDatabase(DATABASE_TEST));
            Assert.AreEqual(VideosFromTestDatabase.Count, 2);
            Assert.AreEqual(VideosFromTestDatabase.Unwatched().GetCount(), 1);
            Assert.IsNotNull(VideosFromTestDatabase.Unwatched().Random());
        }

        [TestMethod]
        public void ShouldFindFile()
        {
            Assert.IsTrue(Video.HasLocalCopy);
        }
    }
}