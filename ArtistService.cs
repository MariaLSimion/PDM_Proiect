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

           
            using (XmlReader reader = XmlReader.Create("https://raw.githubusercontent.com/sanducristina-alexandra/artists-data/refs/heads/main/artists.xml.bak", settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Artist")
                    {
                        Artist artist = new Artist();

                        reader.ReadToDescendant("id");
                        artist.id = int.Parse(reader.ReadElementContentAsString());
                        Console.WriteLine(artist.id);

                        reader.ReadToNextSibling("name");
                        artist.name = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("urlPhoto");
                        artist.urlPhoto = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("genre");
                        artist.genre = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("urlMerchStore");
                        artist.urlMerchStore = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("biography");
                        artist.biography = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("funfacts");
                        artist.funfacts = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("socials");
                        artist.socials = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("popularity");
                        artist.popularity = reader.ReadElementContentAsString();

                        artists.Add(artist);
                    }
                }
            }
            return artists;
        }

    }
}
