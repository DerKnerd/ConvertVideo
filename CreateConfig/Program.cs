using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CreateConfig {
    class Program {
        static void Main(string[] args) {
            Config c = new Config();
            c.path = "C:\test";
            c.pathToMoveTo = "C:\test.end";
            c.move = true;

            XmlSerializer xs = new XmlSerializer(c.GetType());
            xs.Serialize(new FileStream(@"C:\test\config.xml", FileMode.OpenOrCreate), c);
        }
    }
}
