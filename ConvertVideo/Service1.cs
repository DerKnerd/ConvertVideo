using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace ConvertVideo {
    public partial class Service1 : ServiceBase {
        public Service1() {
            InitializeComponent();
        }

        FileSystemWatcher fsw = null;
        Config config = null;

        protected override void OnStart(string[] args) {
            XmlSerializer xs = new XmlSerializer(typeof(Config));
            config = (Config)xs.Deserialize(new FileStream(Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "config.xml"), FileMode.Open));

            if (Directory.Exists(config.path))
                Directory.CreateDirectory(config.path);
            if (Directory.Exists(config.pathToMoveTo))
                Directory.CreateDirectory(config.pathToMoveTo);

            fsw = new FileSystemWatcher(config.path);
            fsw.Created += fsw_Created;
            fsw.EnableRaisingEvents = true;
        }

        void fsw_Created(object sender, FileSystemEventArgs e) {
            FileInfo fi = new FileInfo(e.FullPath);
            if (fi.Extension == ".wmv") {
                return;
            }
            while (IsFileLocked(fi)) {
            }
            Convert2Wmv(e.FullPath);
        }
        private bool IsFileLocked(FileInfo file) {
            FileStream stream = null;

            try {
                stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            } catch (IOException) {
                //the file is unavailable because it is:
                //still being written to
                //or being processed by another thread
                //or does not exist (has already been processed)
                return true;
            } finally {
                if (stream != null)
                    stream.Close();
            }

            //file is not locked
            return false;
        }
        protected override void OnStop() {
        }

        private void Convert2Wmv(string fileName) {
            try {
                fsw.Created -= fsw_Created;
            } catch (Exception ex) {
                File.AppendAllText("C:\\error.log", "Fehler beim konvertieren der datei " + fileName + "\n" + ex.Message + "\n");
            } finally {
                fsw.Created += fsw_Created;
            }
        }
    }
}
