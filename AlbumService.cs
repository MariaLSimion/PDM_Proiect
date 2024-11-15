using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Xml;

namespace PDMProiect
{
    public class AlbumService
    {
        public static List<Album> getAlbums()
        {
            List<Album> albums = new List<Album>();

            // Descarcă și curăță XML-ul
            string xmlContent = DownloadAndCleanXml("https://pastebin.com/raw/vE9K5YvC");

            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Ignore
            };

            using (XmlReader reader = XmlReader.Create(new StringReader(xmlContent), settings))
            {
                int songId = 0;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Album")
                    {
                        Album album = new Album();

                        reader.ReadToDescendant("id");
                        album.Id = int.Parse(reader.ReadElementContentAsString());

                        reader.ReadToNextSibling("AlbumName");
                        album.AlbumName = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("ArtistName");
                        album.ArtistName = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("CoverImage");
                        album.CoverImage = reader.ReadElementContentAsString();

                        album.Songs = new List<Song>();
                        if (reader.ReadToNextSibling("Songs"))
                        {
                            using (XmlReader songReader = reader.ReadSubtree())
                            {
                                while (songReader.Read())
                                {
                                    if (songReader.NodeType == XmlNodeType.Element && songReader.Name == "Song")
                                    {
                                        string songContent = songReader.ReadElementContentAsString();

                                        // Folosește o expresie regulată pentru a extrage URL-ul
                                        var match = Regex.Match(songContent, @"https?:\/\/\S+");
                                        if (match.Success)
                                        {
                                            string url = match.Value;
                                            string title = songContent.Replace(url, "").Trim(); // Elimină URL-ul pentru a obține doar titlul

                                            album.Songs.Add(new Song
                                            {
                                                Title = title,
                                                Url = url,
                                                AlbumId = album.Id,
                                                Id = songId++
                                            });
                                        }
                                    }
                                }
                            }
                        }

                        albums.Add(album);
                    }
                }
            }
            return albums;
        }

        private static string DownloadAndCleanXml(string url)
        {
            using (WebClient client = new WebClient())
            {
                string xmlContent = client.DownloadString(url);
                xmlContent = Regex.Replace(xmlContent, @"&(?!amp;|lt;|gt;|quot;|apos;)", "&amp;");
                xmlContent = xmlContent.Replace("\"", "&quot;");
                xmlContent = xmlContent.Replace("'", "&apos;");
                return xmlContent;
            }
        }
    }
}
