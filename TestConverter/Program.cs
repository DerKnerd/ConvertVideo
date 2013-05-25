using ConvertVideo;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestConverter {
    class Program {
        static void Main(string[] args) {
            Console.WriteLine("convert");
            Converter.Convert(new FileInfo(@"C:\Users\Kirk\Desktop\Mein Film - Kopie.mp4"), new Config() {
                resultfolder = @"C:\Users\Kirk\Desktop",
                setfilenameastitle = true
            });
            Console.WriteLine("finished");
            Console.ReadLine();
            new Config().Save();
        }
    }
}
