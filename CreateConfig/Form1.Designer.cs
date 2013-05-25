namespace CreateConfig {
    partial class Form1 {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent() {
            this.lb_properties = new System.Windows.Forms.ListBox();
            this.button1 = new System.Windows.Forms.Button();
            this.tb_property = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.tb_value = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lb_properties
            // 
            this.lb_properties.FormattingEnabled = true;
            this.lb_properties.Location = new System.Drawing.Point(12, 12);
            this.lb_properties.Name = "lb_properties";
            this.lb_properties.Size = new System.Drawing.Size(263, 147);
            this.lb_properties.TabIndex = 0;
            this.lb_properties.SelectedIndexChanged += new System.EventHandler(this.lb_properties_SelectedIndexChanged);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(200, 221);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "Save";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // tb_property
            // 
            this.tb_property.Location = new System.Drawing.Point(61, 169);
            this.tb_property.Name = "tb_property";
            this.tb_property.ReadOnly = true;
            this.tb_property.Size = new System.Drawing.Size(214, 20);
            this.tb_property.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 172);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "Property";
            // 
            // tb_value
            // 
            this.tb_value.Location = new System.Drawing.Point(61, 195);
            this.tb_value.Name = "tb_value";
            this.tb_value.Size = new System.Drawing.Size(214, 20);
            this.tb_value.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 198);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Value";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 255);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.tb_value);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.tb_property);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lb_properties);
            this.Name = "Form1";
            this.Text = "Create Config File";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lb_properties;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox tb_property;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tb_value;
        private System.Windows.Forms.Label label2;
    }
}

