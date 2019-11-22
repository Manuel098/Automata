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
  /*  public static class extension 
    { 
        public static void LambdaDelete<T>(this List<T> ls, Action<int> p) 
        { 
           
        }
    
    }*/
    public partial class ConsoleForm : Form
    {
        public static Form1 frm;
        public TimeSpan? _Time { get; set; }
        public ConsoleForm(Form1 f)
        {
            frm = f;
            InitializeComponent();
        }
        
        private void ConsoleForm_Load(object sender, EventArgs e)
        {
            
        }
        public void PaintText(List<string> err, List<Memory> mem)
        {
            compilderdat.Rows.Clear();
            Analizadorxd.Text = null;

            mem.FindAll(i => {

                if (string.IsNullOrEmpty(i.ID) && string.IsNullOrEmpty(i.TOKEN) && string.IsNullOrEmpty(i.VALUE)) mem.Remove(i);
                return true;
            });

            foreach(var dat in mem) 
            {
                compilderdat.Rows.Add(dat.TOKEN, dat.ID, dat.VALUE);
            }
            foreach(var e in err)
            Analizadorxd.AppendText(e+"\n");

            lblComp.Text = $"Tiempo de compilación: {_Time}";
        }

        private void ConsoleForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            frm.OnCons -= PaintText;
        }
    }
}
