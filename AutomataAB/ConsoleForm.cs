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
        public static Form1 frm;
        
        public ConsoleForm(Form1 f)
        {
            frm = f;
            InitializeComponent();
        }

        private void ConsoleForm_Load(object sender, EventArgs e)
        {
            
        }
        public void PaintText(List<string> err ,List<Memory> mem) 
        {
            compilderdat.Rows.Clear();
            foreach (var dat in mem)             
                compilderdat.Rows.Add(dat.TOKEN,dat.ID,dat.VALUE);  
           
            foreach(var e in err)
            Analizadorxd.AppendText(e +"\n");
        }

        private void ConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm.OnCons -= PaintText;
        }
    }
}
