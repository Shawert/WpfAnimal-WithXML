using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml;

namespace WpfAnimal
{
    static class DataLayer
    {
        static DataLayer()
        {
            LoadItemsXml();
        }
        public class Animals
        {
            public string Name { get; set; }
            public string Description { get; set; }
            public List<Gallery> Gallery { get; set; }
            public BitmapImage thumb { get; set; }

        }
        public static List<Animals> SampleItems { get; set; }

        public class Gallery
        {
            public BitmapImage BImage { get; set; }
        }
        private static void LoadItemsXml()
        {
            SampleItems = new List<Animals>();
            var commonPath = System.Windows.Forms.Application.StartupPath;
            var xmlPath = commonPath + @"/Data/Data.xml";
            var imgPath = commonPath + @"/Data/Images";

            var reader = new XmlTextReader(new System.IO.FileStream(xmlPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite));
            reader.WhitespaceHandling = WhitespaceHandling.None;
            reader.MoveToContent();
            while (reader.Read())
            {
                if (!reader.EOF)
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Animals")
                    {
                        var doc = new XmlDocument();
                        var nodes = doc.ReadNode(reader).ChildNodes;
                        foreach (XmlNode node in nodes)
                        {
                            var sItem = new Animals();
                            if (node.Name == "Animal")
                            {
                                var nodes2 = node.ChildNodes;
                                foreach (XmlNode node2 in nodes2)
                                {
                                    if (node2.Name == "Name") sItem.Name = node2.InnerText;
                                    else if (node2.Name == "Description") sItem.Description = node2.InnerText;
                                    else if (node2.Name == "Gallery")
                                    {
                                        sItem.Gallery = new List<Gallery>();
                                        var nodes3 = node2.ChildNodes;
                                        foreach (XmlNode node3 in nodes3)
                                        {
                                            if (node3.Name == "Image")
                                            {
                                                var gallery = new Gallery();
                                                gallery.BImage = new BitmapImage(new Uri($"{imgPath}/{node3.InnerText}"));
                                                sItem.Gallery.Add(gallery);
                                            }
                                        }
                                    }
                                    else if (node2.Name == "thumb") sItem.thumb = new BitmapImage(new Uri($"{imgPath}/{node2.InnerText}"));
                                }
                            }
                            SampleItems.Add(sItem);
                        }

                    }
                }
                else break;
            }
        }
    }
}

