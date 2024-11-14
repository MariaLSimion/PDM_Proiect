using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml;

namespace PDMProiect
{
    public class AlbumService
    {
        public static List<Album> getAlbums()
        {
            List<Album> albums = new List<Album>();

            // Descarcă XML-ul și înlocuiește caracterele speciale
            string xmlContent = DownloadAndCleanXml("https://pastebin.com/raw/vE9K5YvC");

            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Ignore
            };

            using (XmlReader reader = XmlReader.Create(new StringReader(xmlContent), settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Album")
                    {
                        Album album = new Album();

                        // Citește detaliile albumului
                        reader.ReadToDescendant("id");
                        album.Id = int.Parse(reader.ReadElementContentAsString());

                        reader.ReadToNextSibling("AlbumName");
                        album.AlbumName = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("ArtistName");
                        album.ArtistName = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("CoverImage");
                        album.CoverImage = reader.ReadElementContentAsString();

                        // Citește lista de melodii folosind un subreader pentru a izola citirea la nivelul de album
                        album.Songs = new List<string>();
                        if (reader.ReadToNextSibling("Songs"))
                        {
                            using (XmlReader songReader = reader.ReadSubtree())
                            {
                                while (songReader.Read())
                                {
                                    if (songReader.NodeType == XmlNodeType.Element && songReader.Name == "Song")
                                    {
                                        album.Songs.Add(songReader.ReadElementContentAsString());
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


        // Metodă pentru a descărca și a curăța XML-ul
        private static string DownloadAndCleanXml(string url)
        {
            using (WebClient client = new WebClient())
            {
                string xmlContent = client.DownloadString(url);
                // Înlocuiește caracterele speciale
                xmlContent = xmlContent.Replace("&", "&amp;");
                //                       .Replace("<", "&lt;")
                //                       .Replace(">", "&gt;")
                //                       .Replace("\"", "&quot;")
                //                       .Replace("'", "&apos;");
                //xmlContent = Regex.Replace(xmlContent, @"&(?!amp;|lt;|gt;|quot;|apos;)", "&amp;");

                // Înlocuiește `&` cu `&amp;`, dar ignoră entitățile deja escapate
                xmlContent = Regex.Replace(xmlContent, @"&(?!amp;|lt;|gt;|quot;|apos;)", "&amp;");

                // Înlocuiește ghilimelele duble cu entitatea escapadă `&quot;` dacă nu sunt deja escapate
                xmlContent = xmlContent.Replace("\"", "&quot;");

                // Înlocuiește apostroafele cu entitatea escapadă `&apos;` dacă nu sunt deja escapate
                xmlContent = xmlContent.Replace("'", "&apos;");

                return xmlContent;
            }
        }
    }
}
