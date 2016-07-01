using MoviePotato.Models;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace MoviePotato.Functionality
{
	public static class MovieExplorerReader
	{
		private static readonly string DEFAULT_DATABASE = "Database.xml";

		public static List<Models.Video> GetAllMovies()
		{
			return GetAllVideos(DEFAULT_DATABASE);
		}

		public static List<Video> GetAllVideos(string path)
		{
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.Load(path);

			List<Video> videos = new List<Video>();

			XmlNodeList categories = xmlDocument.SelectNodes("DatabaseFile/Category");
			foreach (XmlNode category in categories)
			{
				XmlNodeList directories = category.SelectNodes("Directory");
				foreach (XmlNode directory in directories)
				{
					string directoryPath = directory.Attributes["path"].Value;

					XmlNodeList files = directory.SelectNodes("File");
					foreach (XmlNode file in files)
					{
						videos.Add(new Video()
						{
							Location = directoryPath,
							FileName = file.Attributes["name"].Value,
							Watched = file.Attributes["seen"] == null ? false : bool.Parse(file.Attributes["seen"].Value)
						});
					}
				}
			}

			return videos;
		}

		public static bool HasDatabase()
		{
			return HasDatabase(DEFAULT_DATABASE);
		}

		public static bool HasDatabase(string path)
		{
			return File.Exists(path);
		}
	}
}