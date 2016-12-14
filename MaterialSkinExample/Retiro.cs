using MaterialSkin;
using MaterialSkin.Controls;
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
    public partial class Retiro : MaterialForm 
    {

        private readonly MaterialSkinManager materialSkinManager;



    public Retiro()
    {
        InitializeComponent();



        materialSkinManager = MaterialSkinManager.Instance;
        materialSkinManager.AddFormToManage(this);
        materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
        materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey900, Primary.BlueGrey100, Primary.gris500, Accent.LightBlue200, TextShade.WHITE);

    }
        Response resp = new Response();
        private void Retiro_Load(object sender, EventArgs e)
        {
            

           
        }

        private void button4_Click(object sender, EventArgs e)
        {
            resp._retiro = Convert.ToInt32(txtRetiro.Text);
            //// resp._retiro = Convert.ToInt32(txtRetiro.Text);
            //int x = Convert.ToInt32(txtRetiro.Text);
            //x = resp._retiro;
            resp.Retiro();


                        
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Deposito deposito = new Deposito();
            deposito.Show();
            this.Hide();
        }

        private void Retiro_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
