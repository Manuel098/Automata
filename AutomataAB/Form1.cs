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
    public struct Memory 
    { 
        public string TOKEN { get; set; }
        public string ID { get; set; }
        public dynamic VALUE { get; set; }

        public Memory( string token, string id=null, dynamic value=null) 
        {
            TOKEN = token;
            ID = id;
            VALUE = value;
        }   
    }
    public partial class Form1 : Form {

        public string ruta, result;
        string[] palabras;
        public int iter = 0;
        public delegate void Cons(string str, List<Memory> ls);
        public event Cons OnCons;
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

                    for(iter=0; iter<palabras.Length; iter++) {
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

        public async Task<bool> IsConditonal(string Pattern) 
        {
            string regex = @"(?<declare>\s*(Num|Dec)\s+[a-z]\s*)(:=(?<value>\s*\d+\s*))?;";
            Regex rgx = new Regex(regex);
            MatchCollection matchCollection = Regex.Matches(Pattern, regex);

            foreach (Match match in matchCollection) {
                string group = match.Groups["declare"].Value;
                Console.WriteLine("Full Text: " + match.Value);
                Console.WriteLine("<declare>: " + match.Groups["declare"].Value);
                Console.WriteLine("<value>: " + match.Groups["value"].Value);
                Console.WriteLine("------------------------------");
            }
            return await Task.Run(() => rgx.IsMatch(Pattern)?true:false);
        }
        public async Task<bool> IsVar(string Pattern) {

            string regex = @"\A((?<declare>\s*(Num|Dec))\s+(?<id>[a-z]+[0-9]*)\s*(:=(?<value>\s*\d+\s*))?;)*$\Z";         
            Regex rgx = new Regex(regex);
            MatchCollection matchCollection = Regex.Matches(Pattern, regex);

            foreach (Match match in matchCollection) 
            {
                string token = match.Groups["declare"].Value.ToString().Replace(" ", null);
                string id = match.Groups["id"].Value.ToString().Replace(" ", null);
                dynamic value = match.Groups["value"].Value;
                STACK.Add(new Memory(token, id ,value));
            }                            
            return await Task.Run(()=> rgx.IsMatch(Pattern) ? true : false);
        }


        private async void COMPILAR_Click_1(object sender, EventArgs e)
        {
            var _IsVar= await IsVar(inputMessage.Text) ?"es valido \n":"no es valido \n";
            OnCons?.Invoke(_IsVar,STACK);
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
