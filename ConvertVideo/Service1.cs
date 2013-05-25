using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Reflection;
using System.Runtime.InteropServices;
using System.ServiceProcess;
using System.Text;
using System.Threading;
using System.Xml;
using System.Xml.Serialization;

namespace ConvertVideo {
    public partial class Service1 : ServiceBase {
        public Service1() {
            InitializeComponent();
        }

        FileSystemWatcher fsw = null;
        Config config = null;
        List<string> files = new List<string>();

        protected override void OnStart(string[] args) {
            config = Config.Load();

            if (Directory.Exists(config.watchfolder))
                Directory.CreateDirectory(config.watchfolder);
            if (Directory.Exists(config.resultfolder))
                Directory.CreateDirectory(config.resultfolder);

            fsw = new FileSystemWatcher(config.watchfolder);
            fsw.Created += fsw_Created;
            fsw.EnableRaisingEvents = true;
            Thread convert = new Thread(() => {
                while (true) {
                    var currentItem = files.LastOrDefault();
                    if (currentItem != null) {
                        Convert2Wmv(currentItem);
                        files.Remove(currentItem);
                    }
                }
            });
            convert.Start();
        }

        void fsw_Created(object sender, FileSystemEventArgs e) {
            FileInfo fi = new FileInfo(e.FullPath);
            if (fi.Extension == ".wmv") {
                return;
            }
            while (IsFileLocked(fi)) {
            }
            files.Add(e.FullPath);
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
                //fsw.Created -= fsw_Created;
                if (Converter.Convert(new FileInfo(fileName), config)) {
                    File.AppendAllText("C:\\info.log", "Konvertierung von " + fileName + " abgeschlossen\r\n");
                    if (!string.IsNullOrWhiteSpace(config.infomail)) {
                        SmtpClient sc = new SmtpClient(config.senderhost, config.senderport);
                        sc.Send(config.sendermailaddress, config.infomail, "Finished conversion", "File " + fileName + " was successfully converted to folder " + config.resultfolder);
                    }
                }
            } catch (SmtpException ex) {
                File.AppendAllText("C:\\error.log", "Fehler beim senden der info mail\r\n" + ex.Message + "\r\n");
            } catch (Exception ex) {
                File.AppendAllText("C:\\error.log", "Fehler beim konvertieren der datei " + fileName + "\r\n" + ex.Message + "\r\n");
            } finally {
                //fsw.Created += fsw_Created;
            }
        }
    }
}
