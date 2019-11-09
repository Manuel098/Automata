﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace AutomataAB
{ 
    public partial class Form1 : Form {

        public string ruta, result;
        string[] palabras;
        public int iter = 0;
        public event Action<object, List<Memory>> OnCons;
        public List<Memory> STACK = new List<Memory>();


        public static List<string> Tokens = new List<string>();

        public static List<string> Identificadores = new List<string>(){
            "Num","Tex","Si","SiTons","Tons","Lop","Sim","Dec","Imp"
        };

        public Form1() {

            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e) {
            Application.Exit();
        }

        private void save_Click(object sender, EventArgs e) {
            StreamWriter escribir = new StreamWriter(ruta, true);
            try {

                inputMessage.Clear();
                palabras = inputData.Text.Split(' ');
                inputMessage.AppendText(inputData.Text + "\n");
                foreach (var palabra in palabras) {
                    // result = runAuto(palabra);
                }
                escribir.WriteLine(inputData.Text);
                escribir.Close();
                Tokens.Clear();
                inputData.Clear();
            }
            catch {
                MessageBox.Show("error");
            }
        }

        private void label1_Click(object sender, EventArgs e) {
            this.WindowState = FormWindowState.Minimized;
        }

        private void read_Click(object sender, EventArgs e) {
            StreamReader leer = new StreamReader(ruta);
            string linea;
            inputMessage.Clear();
            try
            {

                linea = leer.ReadLine();

                while (linea != null) {

                    palabras = linea.Split(' ');

                    for (iter = 0; iter < palabras.Length; iter++) {
                        // result = runAuto(palabras[iter]);
                        inputMessage.AppendText(result + "\n");
                    }
                    linea = leer.ReadLine();
                }
            }
            catch {
                MessageBox.Show("error");
            }
        }

        private void save_MouseHover(object sender, EventArgs e) {
            toolTip1.ToolTipTitle = "Guardar";
            toolTip1.Show("Guardar una palabra en un archivo .TxT", save);
        }

        private void read_MouseHover(object sender, EventArgs e) {
            toolTip1.ToolTipTitle = "Leer";
            toolTip1.Show("Muestra todas las oraciones de un .TxT", read);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void inputData_TextChanged(object sender, EventArgs e)
        {

        }
        
        
        private async void COMPILAR_Click_1(object sender, EventArgs e)
        {
            var _IsVar= await IsVar(inputMessage.Text) ?"variable valida \n":"variable no valida \n";
            var _IsConditional = await IsConditonal(inputMessage.Text) ? "condicional valido" : "Condicional no valido";
            OnCons?.Invoke(_IsConditional, STACK);           
            STACK.Clear();
        }

        private void OpenConsole_Click(object sender, EventArgs e)
        {
            foreach(Form a in Application.OpenForms)             
                if (a.Name == "ConsoleForm") return;
            
            ConsoleForm csf = new ConsoleForm(this);
            OnCons += csf.PaintText;
            csf.Show();

        }
        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {

            if (keyData == Keys.Tab){
                inputMessage.Text += "mm";
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }
        private void button1_Click(object sender, EventArgs e) {
            using(var open = new OpenFileDialog()) 
            {
                open.Filter = "Archivos txt(*.txt)|*.txt";
                open.Title = "Archivos txt";
                if (open.ShowDialog() == DialogResult.OK) {
                    ruta = open.FileName;
                    inputMessage.AppendText(ruta);
                }
            }
        }
    }
}
