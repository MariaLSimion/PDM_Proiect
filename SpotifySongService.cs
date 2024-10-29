using System;
using System.Collections.Generic;
using System.Xml;

namespace PDMProiect
{
    public class SpotifySongService
    {
        public static List<SpotifyTopSong> GetTopSongs()
        {
            List<SpotifyTopSong> topSongs = new List<SpotifyTopSong>();

            // Create XmlReaderSettings and enable DTD processing
            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse
            };

            // Pass the settings object to XmlReader.Create
            using (XmlReader reader = XmlReader.Create("https://pastebin.com/raw/BCUeuS4x", settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Song")
                    {
                        SpotifyTopSong song = new SpotifyTopSong();
                        song.id = int.Parse(reader.GetAttribute("rank"));

                        // Read nested elements within <Song>
                        reader.ReadToDescendant("Name");
                        song.title = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("Album");
                        song.albumName = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("Duration");
                        song.duration = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("Artist");
                        song.artistName = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("NoOfListens");
                        song.noOfListens = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("Genre");
                        song.genre = reader.ReadElementContentAsString();

                        topSongs.Add(song);
                    }
                }
            }
            return topSongs;
        }
    }
}

