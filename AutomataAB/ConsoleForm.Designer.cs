namespace AutomataAB
{
    partial class ConsoleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Analizadorxd = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // Analizadorxd
            // 
            this.Analizadorxd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Analizadorxd.Location = new System.Drawing.Point(0, 0);
            this.Analizadorxd.Margin = new System.Windows.Forms.Padding(2);
            this.Analizadorxd.Name = "Analizadorxd";
            this.Analizadorxd.Size = new System.Drawing.Size(461, 450);
            this.Analizadorxd.TabIndex = 13;
            this.Analizadorxd.Text = "";
            // 
            // ConsoleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(461, 450);
            this.Controls.Add(this.Analizadorxd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "ConsoleForm";
            this.Text = "C O N S O L A";
            this.Load += new System.EventHandler(this.ConsoleForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox Analizadorxd;
    }
}