//using DSOFile;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace ConvertVideo {
    public static class Converter {
        private static bool callffmpeg(FileInfo file, Config config, out string newfile) {
            File.AppendAllText("C:\\info.log", "starte ffmpeg teil\r\n");
            bool res = false;
            newfile = "\"" + Path.Combine(config.resultfolder, Path.GetFileNameWithoutExtension(file.Name) + ".wmv") + "\"";
            string videobitrate = "";
            string audiobitrate = "";
            File.AppendAllText("C:\\info.log", "hole bitraten\r\n");
            GetBitrate(file.FullName, out videobitrate, out audiobitrate);

            //string arguments = "-i \"" + file.FullName + "\" -b:v " + videobitrate + "k -b:a " + audiobitrate + "k -vcodec wmv2 -acodec wmav2 -y " + newfile;
            string arguments = "-i \"" + file.FullName + "\" -b:v " + videobitrate + "k -vcodec wmv2 -acodec wmav2 -y " + newfile;
            File.AppendAllText("C:\\info.log", "start process\r\n");
            Process proc = new Process();
            proc.StartInfo = new ProcessStartInfo(@"C:\Program Files\Converter\libs\ffmpeg.exe", arguments);
            proc.StartInfo.Verb = "runas";

            File.AppendAllText("C:\\info.log", proc.StartInfo.FileName + " macht magie\r\n");
            proc.Start();
            proc.WaitForExit();
            if (proc.ExitCode == 0) {
                res = true;
            }
            return res;
        }

        private static void GetBitrate(string filename, out string videobitrate, out string audiobitrate) {
            Process proc = new Process();
            string arguments = "-v quiet -print_format json -show_streams \"" + filename + "\"";
            proc.StartInfo = new ProcessStartInfo(@"C:\Program Files\Converter\libs\ffprobe.exe", arguments);
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.UseShellExecute = false;
            proc.Start();
            proc.WaitForExit();
            string stdout = "";
            using (var reader = proc.StandardOutput) {
                stdout = reader.ReadToEnd();
            }
            videobitrate = "0";
            audiobitrate = "0";

            JavaScriptSerializer jss = new JavaScriptSerializer();
            var ffproberes = jss.Deserialize<RootObject>(stdout);

            foreach (var stream in ffproberes.streams) {
                if (stream.codec_type == "audio")
                    audiobitrate = (System.Convert.ToInt32(stream.bit_rate) / 100).ToString();
                else if (stream.codec_type == "video")
                    videobitrate = (System.Convert.ToInt32(stream.bit_rate) / 100).ToString();
            }
        }

        //private static bool ChangeTitle(FileInfo file) {
        //    bool res = false;
        //    try {
        //        OleDocumentProperties dso = new OleDocumentProperties();
        //        File.AppendAllText("C:\\info.log", "Open the file for writing if we can. If not we will get an exception.");
        //        dso.Open(file.FullName, false, dsoFileOpenOptions.dsoOptionOpenReadOnlyIfNoWriteAccess);
        //        File.AppendAllText("C:\\info.log", "Set the summary properties that you want.");
        //        dso.SummaryProperties.Title = Path.GetFileNameWithoutExtension(file.Name);
        //        File.AppendAllText("C:\\info.log", "Save the Summary information.");
        //        dso.Save();
        //        File.AppendAllText("C:\\info.log", "Close the file.");
        //        dso.Close(false);
        //        res = true;
        //    } catch (Exception ex) {
        //        File.AppendAllText("C:\\error.log", "Error changing title");
        //    }

        //    return res;
        //}

        public static bool Convert(FileInfo file, Config config) {
            File.AppendAllText("C:\\info.log", "Starte konvertierung " + file.FullName + "\r\n");
            bool res = false;
            string newfile = "";
            File.AppendAllText("C:\\info.log", "rufe ffmpeg auf\r\n");
            res = callffmpeg(file, config, out newfile);
            //ChangeTitle(new FileInfo(newfile.Replace("\"", "")));
            return res;
        }
    }
}
