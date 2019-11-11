using System;
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

        #region  Analizer

        private string runAuto(string word) {
            Regex reg = new Regex((@"a-zA-Z+"));
            string finishWord = word, status = "e0", wordNew = "";
            bool sum = false;
            char[] l = word.ToCharArray();

            for (int j = 0; j < word.Length;) {
                switch (status) {
                    case "e0":
                        switch (l[j]) {
                            case '+':
                                status = "e13";
                                sum = true;
                                break;
                            case '-':
                                status = "e16";
                                sum = true;
                                break;
                            case '*':
                                status = "e5";
                                break;
                            case ':':
                                status = "e7";
                                sum = true;
                                break;
                            case ';':
                                status = "e4";
                                break;
                            case '/':
                                status = "e6";
                                break;
                            case '(':
                                status = "e9";
                                break;
                            case ')':
                                status = "e10";
                                break;
                            case '{':
                                status = "e11";
                                break;
                            case '}':
                                status = "e12";
                                break;
                            case '"':
                                status = "e19";
                                break;
                            case ' ':
                                j = word.Length + 2;
                                break;
                            default:
                                status = "error";
                                break;
                        }
                        if (Regex.IsMatch(l[j].ToString(), @"^[1-9_]+$")) {
                            status = "e20";
                            j++;
                        }
                        else if (Regex.IsMatch(l[j].ToString(), @"^[a-zA-Z]+$")) {
                            status = "e1";
                            wordNew = wordNew + l[j];
                            j++;
                        }
                        if (sum) {
                            sum = false;
                            j++;
                        }
                        break;
                    case "e1":
                        if (Regex.IsMatch(l[j].ToString(), @"^[0-9_]+$")) {
                            status = "e3";
                            wordNew = wordNew + l[j];
                            j++;
                        }
                        else if (Regex.IsMatch(l[j].ToString(), @"^[a-zA-Z]+$")) {
                            status = "e1";
                            wordNew = wordNew + l[j];
                            if (j == word.Length - 1) {
                                status = "e2";
                            }
                            else {
                                j++;
                            }
                        }
                        else {
                            status = "e2";
                        }
                        break;
                    case "e2":
                        table(wordNew);
                        status = "e0";
                        break;
                    case "e3":
                        if (Regex.IsMatch(l[j].ToString(), @"^[a-zA-Z]+$")) {
                            status = "e1";
                            wordNew = wordNew + l[j];
                            j++;
                        }
                        else if (Regex.IsMatch(l[j].ToString(), @"^[0-9]+$")) {
                            status = "e3";
                            wordNew = wordNew + l[j];
                            j++;
                        }
                        break;
                    case "e4":
                        Tokens.Add("Cierre");
                        status = "e0";
                        j++;
                        break;
                    case "e5":
                        Tokens.Add("Multi");
                        status = "e0";
                        j++;
                        break;
                    case "e6":
                        Tokens.Add("Div");
                        status = "e0";
                        j++;
                        break;
                    case "e7":
                        if (l[j] == '=') {
                            status = "e8";
                        }
                        break;
                    case "e8":
                        Tokens.Add("asignar");
                        status = "e0";
                        j++;
                        break;
                    case "e9":
                        Tokens.Add("AbrrPar");
                        status = "e0";
                        j++;
                        break;
                    case "e10":
                        Tokens.Add("CerrPar");
                        status = "e0";
                        j++;
                        break;
                    case "e11":
                        Tokens.Add("AbrrLla");
                        status = "e0";
                        j++;
                        break;
                    case "e12":
                        Tokens.Add("CerrLla");
                        status = "e0";
                        j++;
                        break;
                    case "e13":
                        if (l[j] == '+') {
                            status = "e15";
                        }
                        else {
                            status = "e14";
                        }
                        break;
                    case "e14":
                        Tokens.Add("suma");
                        status = "e0";
                        break;
                    case "e15":
                        Tokens.Add("incremento");
                        status = "e0";
                        j++;
                        break;
                    case "e16":
                        if (l[j] == '-') {
                            status = "e18";
                        }
                        else {
                            status = "e17";
                        }
                        break;
                    case "e17":
                        Tokens.Add("resta");
                        status = "e0";
                        break;
                    case "e18":
                        Tokens.Add("decremento");
                        status = "e0";
                        j++;
                        break;
                    case "e19":
                        Tokens.Add("comilla");
                        status = "e0";
                        j++;
                        break;
                    case "e20":
                        if (Regex.IsMatch(l[j].ToString(), @"^[0-9]+$")) {
                            status = "e20";
                            if (j == word.Length - 1) {
                                status = "e21";
                            }
                            else {
                                j++;
                            }
                        }
                        else {
                            status = "e21";
                        }
                        break;
                    case "e21":
                        Tokens.Add("Num");
                        status = "e0";
                        break;
                    case "error":
                        j++;
                        Tokens.Add("Error");
                        status = "e0";
                        break;
                }
            }
            return finishWord;
        }

        private void table(string word) {
            bool isWord = true;
            #region identificadores
            Identificadores.FindAll(j => {

                if (j == word) {
                    switch (j) {
                        case "Num":
                            isWord = false;
                            Tokens.Add("int");
                            break;
                        case "Tex":
                            isWord = false;
                            Tokens.Add("string");
                            break;
                        case "Si":
                            isWord = false;
                            Tokens.Add("if");
                            break;
                        case "Tons":
                            isWord = false;
                            Tokens.Add("else");
                            break;
                        case "SiTons":
                            isWord = false;
                            Tokens.Add("else if");
                            break;
                        case "Lop":
                            isWord = false;
                            Tokens.Add("while");
                            break;
                        case "Sim":
                            isWord = false;
                            Tokens.Add("char");
                            break;
                        case "Dec":
                            isWord = false;
                            Tokens.Add("float");
                            break;
                        case "Imp":
                            isWord = false;
                            Tokens.Add("imprimir");
                            break;
                        default:
                            Tokens.Add("identificador");
                            isWord = false;
                            break;
                    }
                    return true;
                }
                return false;
            });
            if (isWord) {
                Tokens.Add("identificador");
            }
            #endregion
        }

        private void validator() {
            // Variables para declarar.
            string asd = "", asd1 = "", asd2 = "";
            bool valid = false, dec = true, sent = false;
            // Variables para asignar.
            string jkl = "", jkl1 = "", jkl2 = "", jkl3 = "";
            bool fin = false;
            Tokens.FindAll(i => {
                Analizadorxd.AppendText(i + " ");
                if (jkl == "" && sent == true && i == "identificador")
                    dec = false;
                if (sent) {
                    if (jkl2 != "") {
                        fin = sentencia(jkl, jkl1, jkl2) && i == "Cierre" ? true : false;
                        jkl = "";
                        jkl1 = "";
                        jkl2 = "";
                        Analizadorxd.AppendText(fin + "\n\n");
                    }
                    jkl2 = jkl2 == "" && jkl1 != "" ? i : jkl2;
                    jkl1 = jkl1 == "" && jkl != "" ? i : jkl1;
                    if (i == "identificador" && jkl == "")
                        jkl = i;
                }
                if (dec) {
                    asd2 = asd2 == "" && asd1 != "" ? i : asd2;
                    asd1 = asd1 == "" && asd != "" ? i : asd1;
                    if (i == "int" || i == "string") {
                        asd = i;
                        asd1 = "";
                        asd2 = "";
                    }
                    else if (asd == "") {
                        Analizadorxd.AppendText(" Error en la linea" + "\n");
                    }
                    if (asd2 != "") {
                        valid = declaraciones(asd, asd1, asd2);
                        asd = "";
                        asd1 = "";
                        asd2 = "";
                        Analizadorxd.AppendText("\n");
                        if (valid == false)
                            Analizadorxd.AppendText(" Error en la linea" + "\n");
                        else
                            sent = true;
                    }
                }
                return true;
            });
            if (valid == false)
                Analizadorxd.AppendText(" Error en la linea" + "\n");

            bool declaraciones(string tipo, string ident, string cierre) {
                bool _tipo, _ident, _cierre;
                _tipo = tipo == "int" || tipo == "string" ? true : false;
                _ident = ident == "identificador" && _tipo ? true : false;
                _cierre = cierre == "Cierre" && _ident == true ? true : false;
                return _cierre;
            }

            bool sentencia(string ide, string asig, string var) {
                bool _ide, _asig, _var;
                _ide = ide == "identificador" ? true : false;
                _asig = asig == "asignar" && _ide ? true : false;
                _var = ide == "identificador" && _asig ? true : false;
                return _var == true ? true : false;
            }
        }
        private void Sintactico() {
            int turn = 0;
            Tokens.FindAll(token => {
                switch (token) {
                    case "identificador":
                        if (turn == 0 || turn == 2) {
                            Analizadorxd.AppendText(token + " ");
                            turn++;
                        }
                        else {
                            Analizadorxd.AppendText("Error ");
                        }
                        break;
                    case "asignar":
                        if (turn == 1) {
                            Analizadorxd.AppendText(token + " ");
                            turn++;
                        }
                        else {
                            Analizadorxd.AppendText("Error ");
                        }
                        break;
                    case "Num":
                        if (turn == 2) {
                            Analizadorxd.AppendText(token + " ");
                            turn++;
                        }
                        else {
                            Analizadorxd.AppendText("Error ");
                        }
                        break;
                    case "Cierre":
                        Analizadorxd.AppendText(token + "\n");
                        turn = 0;
                        break;
                }
                return true;
            });
        }

        private void save_MouseHover2(object sender, EventArgs e) {
            toolTip1.ToolTipTitle = "Guardar";
            toolTip1.Show("Guardar una palabra en un archivo .TxT", save);
        }

        private void read_MouseHover2(object sender, EventArgs e) {
            toolTip1.ToolTipTitle = "Leer";
            toolTip1.Show("Muestra todas las oraciones de un .TxT", read);
        }

        private void button1_Click2(object sender, EventArgs e) {
            using (var open = new OpenFileDialog()) {
                open.Filter = "Archivos txt(*.txt)|*.txt";
                open.Title = "Archivos txt";
                if (open.ShowDialog() == DialogResult.OK) {
                    ruta = open.FileName;
                    inputMessage.AppendText(ruta);
                }
            }
        }

        #endregion






    }

}

