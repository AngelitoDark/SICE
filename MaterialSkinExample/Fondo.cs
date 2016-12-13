using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialSkinExample
{
    public partial class Fondo : Form
    {
        public Fondo()
        {
            InitializeComponent();
            MainForm mainform = new MainForm();
            mainform.Show();
        }

        private void Fondo_Load(object sender, EventArgs e)
        {
        //   MainForm mainform = new MainForm();
        //mainform.Show();
        }
    }
}
