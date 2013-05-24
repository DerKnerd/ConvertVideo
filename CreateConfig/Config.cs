using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace CreateConfig {
    [Serializable]
    public class Config {
        public string path { get; set; }
        public string pathToMoveTo { get; set; }
        public bool move { get; set; }
    }
}
