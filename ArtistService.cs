using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PDMProiect
{
    public class ArtistService
    {
       public static List<Artist> getArtists()
        {
            List<Artist> artists = new List<Artist>();

            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse
            };

           
            using (XmlReader reader = XmlReader.Create("https://pastebin.com/raw/SK8vGzRS", settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Artist")
                    {
                        Artist artist = new Artist();

                        reader.ReadToDescendant("id");
                        artist.id = int.Parse(reader.ReadElementContentAsString());

                        reader.ReadToNextSibling("name");
                        artist.name = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("urlPhoto");
                        artist.urlPhoto = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("genre");
                        artist.genre = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("urlMerchStore");
                        artist.urlMerchStore = reader.ReadElementContentAsString();

                        artists.Add(artist);
                    }
                }
            }
            return artists;
        }

    }
}
