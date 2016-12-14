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
using MySql.Data.MySqlClient;
using System.Drawing.Printing;
using System.Threading;

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
            groupBox1.Location = new Point(350, 130);
            groupBox1.Size = new Size(420, 408);
        }

        private void Deposito_D_Load(object sender, EventArgs e)
        {
            groupBox2.Visible = false;
            btnprint.Visible = false;
        }


        private void button3_Click(object sender, EventArgs e)
        {
            groupBox1.Visible = false;
            groupBox2.Visible = true;
            btnprint.Visible = true;

            Response respuesta = new Response();
            respuesta.TerminarDepositoDirecto();

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

            lblC1.Text = (respuesta.cant0).ToString();
            lblC2.Text = (respuesta.cant1).ToString();
            lblC3.Text = (respuesta.cant2).ToString();
            lblC4.Text = (respuesta.cant3).ToString();
            lblC5.Text = (respuesta.cant4).ToString();
            lblC6.Text = (respuesta.cant5).ToString();

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
            lbltotalbilletes.Text = (_lblC1 + _lblC2 + _lblC3 + _lblC4 + _lblC5 + _lblC6).ToString();

            //for (int i = 0; i < 100; i++)
            //{
            //    Thread.Sleep(10);
            //    SaveTable();
            //    PrintTicket();
            //}

        }


        public void SaveTable()
        {
            try
            {
                int no_cuenta = Convert.ToInt32(lblNo_Cuenta.Text);

                int id_cajero = Convert.ToInt32(lbl_Cajero.Text);
                int MXN20 = Convert.ToInt32(lblC1.Text);
                int MXN50 = Convert.ToInt32(lblC2.Text);
                int MXN100 = Convert.ToInt32(lblC3.Text);
                int MXN200 = Convert.ToInt32(lblC4.Text);
                int MXN500 = Convert.ToInt32(lblC5.Text);
                int MXN1000 = Convert.ToInt32(lblC6.Text);
                int Total_Billetes = MXN20 + MXN50 + MXN100 + MXN200 + MXN500 + MXN1000;
                int mTotal = Convert.ToInt32(lblTotal.Text);
                MySqlConnection con = new MySqlConnection("Server=localhost; User=OKI; Password=OKI2016; database=tlock; port=3306;");
                MySqlCommand cmd = new MySqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "Insert into  transaccion ( id_cajero, no_Cuenta, MXN20, MXN50, MXN100, MXN200, MXN500,MXN1000,    totalBilletes,total) VALUES ( '" + id_cajero + "','" + no_cuenta + "','" +
                        MXN20 + "','" +
                        MXN50 + "','" +
                        MXN100 + "','" +
                        MXN200 + "','" +
                      MXN500 + "','" +
                       MXN1000 + "','" +
                        Total_Billetes + "','" +
                        mTotal + "')";
                cmd.ExecuteScalar();
                con.Close();

            }
            catch (Exception)
            {

                MessageBox.Show("error de conection ");
            }
        }



        public void PrintTicket()
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            pd.PrintController = new StandardPrintController();
            pd.Print();

            MainForm mainform = new MainForm();
            mainform.Show();
            this.Hide();
        }
        //Nuevo Ticket
        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            string hora1 = DateTime.Now.ToString("HH:mm:ss");
            int cant1 = Convert.ToInt32(lblC1.Text);  // Conversión de datos de string a enteros
            int cant2 = Convert.ToInt32(lblC2.Text);  //..  
            int cant3 = Convert.ToInt32(lblC3.Text);  //..
            int cant4 = Convert.ToInt32(lblC4.Text);  //..
            int cant5 = Convert.ToInt32(lblC5.Text);  //..
            int cant6 = Convert.ToInt32(lblC6.Text);  // Conversión de datos de string a enteros
            int Billetes = cant1 + cant2 + cant3 + cant4 + cant5 + cant6;  //Suma total de cantidades

            string cuenta = lblNo_Cuenta.Text; //Número de cuenta cliente
            int Mov = 0;
            //Consulta SQL  
            MySqlConnection con = new MySqlConnection("Server=localhost; User=OKI; Password=OKI2016; database=tlock; port=3306;");
            MySqlCommand cmd = new MySqlCommand();
            con.Open();
            cmd.Connection = con;

            cmd.CommandText = ("select max(id_transaccion)from transaccion");

            string numtransaccion = cmd.ExecuteScalar().ToString();

            con.Close();
            // Fin de Consulta SQL

            int ESPACIO = 85;  // Interlineado estandar entre texto
            //string logo = Application.StartupPath + "\\logo.png";  //Carga de logotipo
            Graphics g = e.Graphics;  //Uso de graficos para impresión de ticket

            string logo = Application.StartupPath + "\\logo.jpg";
            Font fBody = new Font("Arial Narrow", 9, FontStyle.Bold); //Cambio de estilo 
            Font fBody1 = new Font("Arial Narrow", 9, FontStyle.Regular); //Estilo utilizado en ticket
            SolidBrush sb = new SolidBrush(Color.Black);  //Color de texto
            g.DrawImage(Image.FromFile(logo), 130, -10, 160, 90);

            g.DrawString("Depósito de EFECTIVO\n" + "Ubicación: Suc. CEDA\n" + "Id Cajero: 201\n", fBody1, sb, 10, ESPACIO);
            g.DrawString("Transacción: " + numtransaccion, fBody1, sb, 10, ESPACIO + 60);
            g.DrawString("No. Cuenta: " + cuenta, fBody1, sb, 10, ESPACIO + 75);
            g.DrawString("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy") + "  Hora: " + /*DateTime.Now.ToString("hh:mm:ss")*/ hora1 + "\n", fBody1, sb, 10, ESPACIO + 90);
            g.DrawString("========================================\n", fBody1, sb, 10, ESPACIO + 105);
            g.DrawString("Cantidad     Denominación      Total\n", fBody1, sb, 10, ESPACIO + 125);
            if (cant1 >= 1)
            {
                g.DrawString(lblC1.Text, fBody1, sb, 30, ESPACIO + 145);
                g.DrawString(lblD1.Text, fBody1, sb, 100, ESPACIO + 145);
                g.DrawString(lblT1.Text, fBody1, sb, 180, ESPACIO + 145);
                Mov = Mov + 20;
            }
            if (cant2 >= 1)
            {
                g.DrawString(lblC2.Text, fBody1, sb, 30, ESPACIO + 145 + Mov);
                g.DrawString(lblD2.Text, fBody1, sb, 100, ESPACIO + 145 + Mov);
                g.DrawString(lblT2.Text, fBody1, sb, 180, ESPACIO + 145 + Mov);
                Mov = Mov + 20;
            }
            if (cant3 >= 1)
            {
                g.DrawString(lblC3.Text, fBody1, sb, 30, ESPACIO + 145 + Mov);
                g.DrawString(lblD3.Text, fBody1, sb, 100, ESPACIO + 145 + Mov);
                g.DrawString(lblT3.Text, fBody1, sb, 180, ESPACIO + 145 + Mov);
                Mov = Mov + 20;
            }
            if (cant4 >= 1)
            {
                g.DrawString(lblC4.Text, fBody1, sb, 30, ESPACIO + 145 + Mov);
                g.DrawString(lblD4.Text, fBody1, sb, 100, ESPACIO + 145 + Mov);
                g.DrawString(lblT4.Text, fBody1, sb, 180, ESPACIO + 145 + Mov);
                Mov = Mov + 20;
            }
            if (cant5 >= 1)
            {
                g.DrawString(lblC5.Text, fBody1, sb, 30, ESPACIO + 145 + Mov);
                g.DrawString(lblD5.Text, fBody1, sb, 100, ESPACIO + 145 + Mov);
                g.DrawString(lblT5.Text, fBody1, sb, 180, ESPACIO + 145 + Mov);
                Mov = Mov + 20;
            }
            if (cant6 >= 1)
            {
                g.DrawString(lblC6.Text, fBody1, sb, 30, ESPACIO + 145 + Mov);
                g.DrawString(lblD6.Text, fBody1, sb, 100, ESPACIO + 145 + Mov);
                g.DrawString(lblT6.Text, fBody1, sb, 180, ESPACIO + 145 + Mov);
                Mov = Mov + 20;
            }
            g.DrawString("========================================\n", fBody1, sb, 10, ESPACIO + 145 + Mov); Mov = Mov + 20;
            g.DrawString("Billetes Totales:" + Billetes + "\n", fBody1, sb, 10, ESPACIO + 145 + Mov); Mov = Mov + 20;
            g.DrawString("Total Depositado: " + "$" + lblTotal.Text + "\n", fBody1, sb, 10, ESPACIO + 145 + Mov); Mov = Mov + 20;
            g.DrawString("========================================\n", fBody1, sb, 10, ESPACIO + 145 + Mov); Mov = Mov + 20;
            g.DrawString("Para aclaraciones de los depósitos realizados\nponemos a su disposición nuestras líneas de\natención.\n\nMéxico D.F.:\nMonterrey:\nGuadalajara:\nResto del país:\n", fBody1, sb, 10, ESPACIO + 145 + Mov);
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

        private void close(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            SaveTable();
             PrintTicket();
        }
    }
}
