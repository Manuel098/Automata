using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AutomataAB
{
    public partial class ConsoleForm : Form
    {
        Form1 frm = new Form1();

        public ConsoleForm()
        {
            InitializeComponent();
        }

        private void ConsoleForm_Load(object sender, EventArgs e)
        {
            
        }
        public void PaintText(string txt) 
        {
            Analizadorxd.AppendText(txt +"\n");
        }
    }
}
