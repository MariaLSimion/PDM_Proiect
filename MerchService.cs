using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PDMProiect
{
    internal class MerchService
    {
        public static List<Merch> getMerch()
        {
            List<Merch> merchList = new List<Merch>();

            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse
            };


            using (XmlReader reader = XmlReader.Create("https://raw.githubusercontent.com/sanducristina-alexandra/merch_data/refs/heads/main/merch.txt", settings))
            {
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "merchItem")
                    {
                        Merch merch = new Merch();

                        reader.ReadToDescendant("id");
                        merch.id = int.Parse(reader.ReadElementContentAsString());

                        reader.ReadToNextSibling("artist");
                        merch.artist = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("productName");
                        merch.productName = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("imageLink");
                        merch.imageLink = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("price");
                        merch.price = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("type");
                        merch.type = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("disponibility");
                        merch.disponibility = reader.ReadElementContentAsString();

                        reader.ReadToNextSibling("details");
                        merch.details = reader.ReadElementContentAsString();

                        merchList.Add(merch);
                    }
                }
            }
            return merchList;
        }
    }
}
