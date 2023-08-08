using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace WpfAnimal
{
    static class Settings
    {
        static Settings()
        {
            ReadSettings();
        }
        public static double timerCountdown;
        private static void ReadSettings()
        {
            var commonPath = System.Windows.Forms.Application.StartupPath;
            var xmlPath = commonPath + @"/Data/Settings.xml";
            var reader = new XmlTextReader(new System.IO.FileStream(xmlPath, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite));
            reader.WhitespaceHandling = WhitespaceHandling.None;
            reader.MoveToContent();
            while (reader.Read())
            {
                if (!reader.EOF)
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Parameter")
                    {
                        var doc = new XmlDocument();
                        var nodes = doc.ReadNode(reader).ChildNodes;
                        foreach (XmlNode node in nodes)
                        {
                            if (node.Name == "StandbyTime")
                            {
                                timerCountdown = Convert.ToDouble(node.InnerText);
                                StandByTimer.cek(timerCountdown);
                            }
                        }
                    }
                }
            }
        }
    }
}
