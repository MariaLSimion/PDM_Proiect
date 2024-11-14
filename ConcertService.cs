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
    public class ConcertService
    {
        public static List<Concert> getConcerts()
        {
            List<Concert> concerts = new List<Concert>();

            // Descarcă XML-ul și înlocuiește caracterele speciale
            string xmlContent = DownloadAndCleanXml("https://pastebin.com/raw/hWsnkGMS");

            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Ignore
            };

            using (XmlReader reader = XmlReader.Create(new StringReader(xmlContent), settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Concert")
                    {
                        Concert concert = new Concert();

                        // Citește detaliile albumului
                        reader.ReadToDescendant("id");
                        concert.Id = int.Parse(reader.ReadElementContentAsString());

                        reader.ReadToNextSibling("NumeConcert");
                        concert.NumeConcert = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("DataLocatie");
                        concert.DataLocatie = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("ImageUrl");
                        concert.ImageUrl = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("PretBilet");
                        concert.PretBilet = reader.ReadElementContentAsString();

                        // Citește lista de artisti folosind un subreader pentru a izola citirea la nivelul de concert
                        concert.Artists = new List<string>();
                        if (reader.ReadToNextSibling("Artists"))
                        {
                            using (XmlReader artistReader = reader.ReadSubtree())
                            {
                                while (artistReader.Read())
                                {
                                    if (artistReader.NodeType == XmlNodeType.Element && artistReader.Name == "Artist")
                                    {
                                        concert.Artists.Add(artistReader.ReadElementContentAsString());
                                    }
                                }
                            }
                        }

                        concerts.Add(concert);
                    }
                }
            }
            return concerts;
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
