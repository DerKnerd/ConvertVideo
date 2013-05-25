using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace ConvertVideo {
    [Serializable]
    public class Config {
        /// <summary>
        /// the folder to watch
        /// </summary>
        public string watchfolder { get; set; }
        /// <summary>
        /// the folder to convert in
        /// </summary>
        public string resultfolder { get; set; }
        /// <summary>
        /// the mail address to send an info that conversion finished
        /// </summary>
        public string infomail { get; set; }
        /// <summary>
        /// the host who sends the mail
        /// </summary>
        public string senderhost { get; set; }
        /// <summary>
        /// the port on which the mail sender works
        /// </summary>
        public int senderport { get; set; }
        /// <summary>
        /// the mailaddress of the sender
        /// </summary>
        public string sendermailaddress { get; set; }
        /// <summary>
        /// true to set the filename as the video title
        /// </summary>
        public bool setfilenameastitle { get; set; }

        /// <summary>
        /// saves the current config file
        /// </summary>
        public void Save() {
            XmlSerializer xs = new XmlSerializer(this.GetType());
            Directory.CreateDirectory(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "ConvertVideo"));
            File.Create(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "ConvertVideo", "config.xml")).Close();
            using (var fs = new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "ConvertVideo", "config.xml"), FileMode.OpenOrCreate)) {
                xs.Serialize(fs, this);
            }
        }
        /// <summary>
        /// loads the config file
        /// </summary>
        /// <returns></returns>
        public static Config Load() {
            try {
                XmlSerializer xs = new XmlSerializer(typeof(Config));
                using (var fs = new FileStream(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "ConvertVideo", "config.xml"), FileMode.OpenOrCreate)) {
                    return (Config)xs.Deserialize(fs);
                }
            } catch (Exception ex) {
                return new Config();
            }
        }
    }
}
