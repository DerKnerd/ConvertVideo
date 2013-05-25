using ConvertVideo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace CreateConfig {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
            config = Config.Load();

            foreach (var item in config.GetType().GetProperties()) {
                lb_properties.Items.Add(item.Name);
            }
        }

        Config config = null;

        private void button1_Click(object sender, EventArgs e) {
            config.GetType().GetProperty(lb_properties.SelectedItem.ToString()).SetValue(config, tb_value.Text, null);
            config.Save();
        }

        private void lb_properties_SelectedIndexChanged(object sender, EventArgs e) {
            tb_property.Text = lb_properties.SelectedItem.ToString();
        }
    }
}
