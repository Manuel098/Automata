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
            this.inputMessage = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.label7 = new System.Windows.Forms.Label();
            this.inputData = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.save = new System.Windows.Forms.Button();
            this.read = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.COMPILAR = new System.Windows.Forms.Button();
            this.OpenConsole = new System.Windows.Forms.Button();
            this.Analizadorxd = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // inputMessage
            // 
            this.inputMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.inputMessage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(44)))), ((int)(((byte)(50)))));
            this.inputMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.inputMessage.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.inputMessage.ForeColor = System.Drawing.SystemColors.Window;
            this.inputMessage.Location = new System.Drawing.Point(308, 52);
            this.inputMessage.Name = "inputMessage";
            this.inputMessage.Size = new System.Drawing.Size(891, 538);
            this.inputMessage.TabIndex = 5;
            this.inputMessage.Text = "";
            // 
            // toolTip1
            // 
            this.toolTip1.ToolTipTitle = "Guardar";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label7.Location = new System.Drawing.Point(302, 14);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(157, 29);
            this.label7.TabIndex = 14;
            this.label7.Text = "TU CODIGO";
            // 
            // inputData
            // 
            this.inputData.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputData.Location = new System.Drawing.Point(16, 140);
            this.inputData.Name = "inputData";
            this.inputData.Size = new System.Drawing.Size(268, 26);
            this.inputData.TabIndex = 2;
            this.inputData.TabStop = false;
            this.inputData.TextChanged += new System.EventHandler(this.inputData_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label2.Location = new System.Drawing.Point(8, 114);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(243, 25);
            this.label2.TabIndex = 3;
            this.label2.Text = "2.- Ingresa Una Oracion";
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(16, 52);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(267, 37);
            this.button1.TabIndex = 4;
            this.button1.TabStop = false;
            this.button1.Text = "Subir un archivo";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // save
            // 
            this.save.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.save.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.save.Location = new System.Drawing.Point(20, 215);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(124, 34);
            this.save.TabIndex = 6;
            this.save.TabStop = false;
            this.save.Text = "Guardar";
            this.save.UseVisualStyleBackColor = false;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // read
            // 
            this.read.BackColor = System.Drawing.Color.MintCream;
            this.read.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.read.Location = new System.Drawing.Point(152, 215);
            this.read.Name = "read";
            this.read.Size = new System.Drawing.Size(134, 34);
            this.read.TabIndex = 7;
            this.read.TabStop = false;
            this.read.Text = "leer";
            this.read.UseVisualStyleBackColor = false;
            this.read.Click += new System.EventHandler(this.read_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label4.Location = new System.Drawing.Point(12, 26);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(176, 25);
            this.label4.TabIndex = 10;
            this.label4.Text = "1.- Sube un .TxT";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label5.Location = new System.Drawing.Point(15, 189);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(269, 25);
            this.label5.TabIndex = 11;
            this.label5.Text = "3.- Selecciona una opcion.";
            // 
            // COMPILAR
            // 
            this.COMPILAR.Location = new System.Drawing.Point(20, 282);
            this.COMPILAR.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.COMPILAR.Name = "COMPILAR";
            this.COMPILAR.Size = new System.Drawing.Size(264, 35);
            this.COMPILAR.TabIndex = 16;
            this.COMPILAR.TabStop = false;
            this.COMPILAR.Text = "COMPILAR";
            this.COMPILAR.UseVisualStyleBackColor = true;
            this.COMPILAR.Click += new System.EventHandler(this.COMPILAR_Click_1);
            // 
            // OpenConsole
            // 
            this.OpenConsole.Location = new System.Drawing.Point(22, 354);
            this.OpenConsole.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.OpenConsole.Name = "OpenConsole";
            this.OpenConsole.Size = new System.Drawing.Size(261, 35);
            this.OpenConsole.TabIndex = 17;
            this.OpenConsole.TabStop = false;
            this.OpenConsole.Text = "ABRIR CONSOLA";
            this.OpenConsole.UseVisualStyleBackColor = true;
            this.OpenConsole.Click += new System.EventHandler(this.OpenConsole_Click);
            // 
            // Analizadorxd
            // 
            this.Analizadorxd.Location = new System.Drawing.Point(-13, -45);
            this.Analizadorxd.Name = "Analizadorxd";
            this.Analizadorxd.Size = new System.Drawing.Size(10, 38);
            this.Analizadorxd.TabIndex = 18;
            this.Analizadorxd.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(37)))), ((int)(((byte)(43)))));
            this.ClientSize = new System.Drawing.Size(1215, 608);
            this.Controls.Add(this.Analizadorxd);
            this.Controls.Add(this.OpenConsole);
            this.Controls.Add(this.COMPILAR);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.read);
            this.Controls.Add(this.save);
            this.Controls.Add(this.inputMessage);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.inputData);
            this.Name = "Form1";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RichTextBox inputMessage;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox inputData;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button save;
        private System.Windows.Forms.Button read;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button COMPILAR;
        private System.Windows.Forms.Button OpenConsole;
        private System.Windows.Forms.RichTextBox Analizadorxd;
    }
}

