//using Java.Lang;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PDMProiect
{
    public class SpotifyArtistService
    {
        public static List<SpotifyTopArtist> GetTopArtists()
        {
            List<SpotifyTopArtist> topArtists = new List<SpotifyTopArtist>();

            // Create XmlReaderSettings and enable DTD processing
            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse
            };

            // Pass the settings object to XmlReader.Create
            using (XmlReader reader = XmlReader.Create("https://pastebin.com/raw/mpg438UF", settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Artist")
                    {
                        SpotifyTopArtist artist = new SpotifyTopArtist();
                        artist.id = int.Parse(reader.GetAttribute("rank"));

                        // Read nested elements within <Song>
                        reader.ReadToDescendant("Name");
                        artist.name = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("Country");
                        artist.country = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("Listeners");
                        artist.noOfListeners = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("Genre");
                        artist.genre = reader.ReadElementContentAsString();


                        topArtists.Add(artist);
                    }
                }
            }
            return topArtists;
        }
    }
}

