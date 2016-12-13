using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;

namespace MaterialSkinExample
{
    public partial class Deposito_D : MaterialForm
    {
        static string user = "TCR";
        static String Pasword = "12345";
        static string terminal = "123";
        static FullService.Autenticacao authData = new FullService.Autenticacao();
        static FullService.OperationsClient operaciones = new FullService.OperationsClient();
        static FullService.ConfiguracaoCassette casett = new FullService.ConfiguracaoCassette();
        static FullService.DadosCassete dados = new FullService.DadosCassete();




        public Deposito_D()
        {
            InitializeComponent();
            groupBox1.Location = new Point(325, 130);
            groupBox1.Size = new Size(420, 408);
        }

        private void Deposito_D_Load(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            //authData.Usuario = user;
            //authData.Terminal = terminal;
            //authData.Senha = Pasword;
            //string mensaje = operaciones.AbastecimentoEncerrar(authData).Mensagem;
            //MessageBox.Show(mensaje);
            Response respuesta = new Response();
            respuesta.TerminarDepositoDirecto();
            
           // int y = Convert.ToInt32(respuesta.cant0);



            lblD1.Text = (respuesta.dm0).ToString();
            lblD2.Text = (respuesta.dm1).ToString();
            lblD3.Text = (respuesta.dm2).ToString();
            lblD4.Text = (respuesta.dm3).ToString();
            lblD5.Text = (respuesta.dm4).ToString();
            lblD6.Text = (respuesta.dm5).ToString();


         int _dm0 = Convert.ToInt32(lblD1.Text);
         int _dm1 = Convert.ToInt32(lblD2.Text);
         int _dm2 = Convert.ToInt32(lblD3.Text);
         int _dm3 = Convert.ToInt32(lblD4.Text);
         int _dm4 = Convert.ToInt32(lblD5.Text);
         int _dm5 = Convert.ToInt32(lblD6.Text);

            lblC1.Text =( respuesta.cant0).ToString();
            lblC2.Text = (respuesta.cant1).ToString();
            lblC3.Text = (respuesta.cant2).ToString();
            lblC4.Text = (respuesta.cant3).ToString();
            lblC5.Text = (respuesta.cant4).ToString();
            lblC6.Text = (respuesta.cant5).ToString();



            //respuesta.cant0 = Convert.ToInt32(lblC1.Text);
            //respuesta.cant1 = Convert.ToInt32(lblC2.Text);
            //respuesta.cant2 = Convert.ToInt32(lblC3.Text);
            //respuesta.cant3 = Convert.ToInt32(lblC4.Text);
            //respuesta.cant4 = Convert.ToInt32(lblC5.Text);
            //respuesta.cant5 = Convert.ToInt32(lblC6.Text);


            //  MessageBox.Show("CANTIDADqwwerwrwqer " + y);
            int _lblC1 = Convert.ToInt32(respuesta.cant0);
            int _lblC2 = Convert.ToInt32(respuesta.cant1);
            int _lblC3 = Convert.ToInt32(respuesta.cant2);
            int _lblC4 = Convert.ToInt32(respuesta.cant3);
            int _lblC5 = Convert.ToInt32(respuesta.cant4);
            int _lblC6 = Convert.ToInt32(respuesta.cant5);




            int total0 = _dm0 * _lblC1;
            int total1 = _dm1 * _lblC2;
            int total2 = _dm2 * _lblC3;
            int total3 = _dm3 * _lblC4;
            int total4 = _dm4 * _lblC5;
            int total5 = _dm5 * _lblC6;


            lblT1.Text = ("$" + total0);
            lblT2.Text = ("$" + total1);
            lblT3.Text = ("$" + total2);
            lblT4.Text = ("$" + total3);
            lblT5.Text = ("$" + total4);
            lblT6.Text = ("$" + total5);

            lblTotal.Text = (total0 + total1 + total2 + total3 + total4 + total5).ToString();

            lbltotalbilletes.Text = (_lblC1+_lblC2+_lblC3+_lblC4+_lblC5+_lblC6).ToString();


        }

        private void lblD6_Click(object sender, EventArgs e)
        {

        }

        private void lblD5_Click(object sender, EventArgs e)
        {

        }

        private void lblD4_Click(object sender, EventArgs e)
        {

        }

        private void lblD3_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void lblD1_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel2_Click(object sender, EventArgs e)
        {

        }

        private void lblT1_Click(object sender, EventArgs e)
        {

        }

        private void lblT2_Click(object sender, EventArgs e)
        {

        }

        private void lblT3_Click(object sender, EventArgs e)
        {

        }

        private void lblT4_Click(object sender, EventArgs e)
        {

        }

        private void lblT5_Click(object sender, EventArgs e)
        {

        }

        private void lblT6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void lblC7_Click(object sender, EventArgs e)
        {

        }

        private void lbltotalbilletes_Click(object sender, EventArgs e)
        {

        }

        private void lblC6_Click(object sender, EventArgs e)
        {

        }

        private void lblC5_Click(object sender, EventArgs e)
        {

        }

        private void lblC4_Click(object sender, EventArgs e)
        {

        }

        private void lblC3_Click(object sender, EventArgs e)
        {

        }

        private void lblC2_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel24_Click(object sender, EventArgs e)
        {

        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }

        private void lblD2_Click(object sender, EventArgs e)
        {

        }
    }
}
