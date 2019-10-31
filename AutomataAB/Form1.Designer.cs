namespace AutomataAB {
    partial class Form1 {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            this.inputData = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.inputMessage = new System.Windows.Forms.RichTextBox();
            this.save = new System.Windows.Forms.Button();
            this.read = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.progressB = new System.Windows.Forms.ProgressBar();
            this.Analizadorxd = new System.Windows.Forms.RichTextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // inputData
            // 
            this.inputData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputData.Location = new System.Drawing.Point(43, 112);
            this.inputData.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inputData.Name = "inputData";
            this.inputData.Size = new System.Drawing.Size(179, 20);
            this.inputData.TabIndex = 2;
            this.inputData.TextChanged += new System.EventHandler(this.inputData_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(41, 97);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(120, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "2.- Ingresa Una Oracion";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(43, 55);
            this.button1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(178, 24);
            this.button1.TabIndex = 4;
            this.button1.Text = "Subir un archivo";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // inputMessage
            // 
            this.inputMessage.Location = new System.Drawing.Point(265, 34);
            this.inputMessage.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.inputMessage.Name = "inputMessage";
            this.inputMessage.Size = new System.Drawing.Size(242, 235);
            this.inputMessage.TabIndex = 5;
            this.inputMessage.Text = "";
            // 
            // save
            // 
            this.save.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.save.Location = new System.Drawing.Point(45, 161);
            this.save.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(83, 22);
            this.save.TabIndex = 6;
            this.save.Text = "Guardar";
            this.save.UseVisualStyleBackColor = false;
            this.save.Click += new System.EventHandler(this.save_Click);
            this.save.MouseHover += new System.EventHandler(this.save_MouseHover);
            // 
            // read
            // 
            this.read.BackColor = System.Drawing.Color.MintCream;
            this.read.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.read.Location = new System.Drawing.Point(133, 161);
            this.read.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.read.Name = "read";
            this.read.Size = new System.Drawing.Size(89, 22);
            this.read.TabIndex = 7;
            this.read.Text = "leer";
            this.read.UseVisualStyleBackColor = false;
            this.read.Click += new System.EventHandler(this.read_Click);
            this.read.MouseHover += new System.EventHandler(this.read_MouseHover);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(44, 40);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(87, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "1.- Sube un .TxT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(46, 146);
            this.label5.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(134, 13);
            this.label5.TabIndex = 11;
            this.label5.Text = "3.- Selecciona una opcion.";
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Guardar";
            // 
            // progressB
            // 
            this.progressB.Location = new System.Drawing.Point(8, 253);
            this.progressB.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.progressB.Name = "progressB";
            this.progressB.Size = new System.Drawing.Size(253, 15);
            this.progressB.TabIndex = 8;
            // 
            // Analizadorxd
            // 
            this.Analizadorxd.Location = new System.Drawing.Point(546, 32);
            this.Analizadorxd.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Analizadorxd.Name = "Analizadorxd";
            this.Analizadorxd.Size = new System.Drawing.Size(245, 235);
            this.Analizadorxd.TabIndex = 12;
            this.Analizadorxd.Text = "";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Arial Rounded MT Bold", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Crimson;
            this.label6.Location = new System.Drawing.Point(511, 138);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(32, 22);
            this.label6.TabIndex = 13;
            this.label6.Text = "=>";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(351, 19);
            this.label7.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(60, 13);
            this.label7.TabIndex = 14;
            this.label7.Text = "MUESTRA";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(645, 18);
            this.label8.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(55, 13);
            this.label8.TabIndex = 15;
            this.label8.Text = "ANALISIS";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.SystemColors.Menu;
            this.ClientSize = new System.Drawing.Size(810, 292);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.Analizadorxd);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.progressB);
            this.Controls.Add(this.read);
            this.Controls.Add(this.save);
            this.Controls.Add(this.inputMessage);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.inputData);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Text = "AUTOMATA CHIDO";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox inputData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox inputMessage;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button read;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ProgressBar progressB;
        private System.Windows.Forms.RichTextBox Analizadorxd;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}

