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
using System.IO;

namespace MaterialSkinExample
{
    public partial class Deposito : MaterialForm 
    {

        private readonly MaterialSkinManager materialSkinManager;

        

        public Deposito()
        {
            InitializeComponent();

           

            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey900, Primary.BlueGrey100, Primary.gris500, Accent.LightBlue200, TextShade.WHITE);

            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            string filePath = @"logo2.png";


            Bitmap myBitmap = new Bitmap(filePath);
            myBitmap.MakeTransparent();
            this.pictureBox1.Image = myBitmap;
        }
        public string a, b, c;

        
        private void Retiro_Load_1(object sender, EventArgs e)
        {
            pictureBox8.Visible = false;
            pictureBox7.Visible = false;
            pictureBox6.Visible = false;
            pictureBox5.Visible = false;
            pictureBox4.Visible = false;
          //  pictureBox3.Visible = false;
            
            timer1.Start();

            this.WindowState = FormWindowState.Maximized;
           

        }

        private void tableLayoutPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
       
        private void materialFlatButton1_Click(object sender, EventArgs e)
        {

          ////borrar
        }

        public void JournalOperacionCancelarDeposito()
        {
            DateTime fecha = DateTime.Now;

            string m_archivo = fecha.Day.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Year.ToString() + ".journal";
            var archivo_Journal = (@"C:\Directorio SICE\" + m_archivo + "");
            using (StreamWriter w = File.AppendText(archivo_Journal))
            {
                Logcancel("SICE", w);
            }


        }

        public void Logcancel(string logMessage, TextWriter w)
        {
            string EstadoFecha = (DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString());
            string Operacion = "Deposito";
            w.WriteLine("--------------------------------------Estado Botón Cancelar Deposito------------------------------------------------");

            w.WriteLine(EstadoFecha);
            w.WriteLine("se cancelo opeación Deposito \t" + Operacion);
            w.WriteLine(lblDCuenta.Text.Substring(0, 4) + "****");

        }

        public static void validar() {
            MaterialSkin.Operaciones.WebTellerClient TCR2 = new MaterialSkin.Operaciones.WebTellerClient();

            MainForm metodo = new MainForm();
          //  metodo.Login();
        }

      //  public static string FechaInicio ;

        public void materialFlatButton2_Click(object sender, EventArgs e)
        {
////////borrar
        }

        public void JournalOperacionDeposito()
        {
            DateTime fecha = DateTime.Now;

            string m_archivo = fecha.Day.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Year.ToString() + ".journal";
            var archivo_Journal = (@"C:\Directorio SICE\" + m_archivo + "");
            using (StreamWriter w = File.AppendText(archivo_Journal))
            {
                Log("SICE", w);
            }


        }

        private void button2_Click(object sender, EventArgs e)
        {
            string result = MyMessageBox.ShowBox("Introduce los Billetes ", "Mensaje");
                         string FechaInicio = String.Format(" {0:s}  ", DateTime.Now + DateTime.Now.ToString("%K"));
                        C_Deposito.FechaInicio = FechaInicio;
                        C_Deposito.mCuenta = Convert.ToInt32(lblDCuenta.Text);
 WTcr wtcr = new WTcr();
            wtcr.PrepararDeposito();

            C_Deposito cdeposito = new C_Deposito();
            this.Hide();
            cdeposito.Show();
            cdeposito.label2.Text = FechaInicio;

            JournalOperacionDeposito();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.lblHCuenta.Text = lblDCuenta.Text;

            MainForm mainform = new MainForm();
           
            this.Hide();
            mainform.Show();
                      JournalOperacionCancelarDeposito();

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox9_Click(object sender, EventArgs e)
        {
            Home home = new Home();
            home.lblHCuenta.Text = lblDCuenta.Text;

            MainForm mainform = new MainForm();
            
            this.Hide();
            mainform.Show();
                        JournalOperacionCancelarDeposito();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (pictureBox3.Visible == true)
            {
                pictureBox3.Visible = false;
                pictureBox4.Visible = true;
            }

            else if (pictureBox4.Visible == true)
            {
                pictureBox4.Visible = false;
                pictureBox5.Visible = true;
            }

            else if (pictureBox5.Visible == true)
            {
                pictureBox5.Visible = false;
                pictureBox6.Visible = true;
            }

            else if (pictureBox6.Visible == true)
            {
                pictureBox6.Visible = false;
                pictureBox7.Visible = true;
            }
            else if (pictureBox7.Visible == true)
            {
                pictureBox7.Visible = false;
                pictureBox8.Visible = true;
            }

            else if (pictureBox8.Visible == true)
            {
                pictureBox8.Visible = false;
                //pictureBox1.Visible = true;
            }
            else
            {

              //  pictureBox1.Visible = true;
                pictureBox3.Visible = true;
              //  pictureBox2.Visible = true;

                pictureBox4.Visible = true;
                pictureBox5.Visible = true;
                pictureBox6.Visible = true;
                pictureBox7.Visible = true;
                pictureBox8.Visible = true;
            }
        }
       // Variables estaticas del servicio web 
        static string user = "TCR";
        static String Pasword = "12345";
        static string terminal = "123";
        static FullService.Autenticacao authData = new FullService.Autenticacao();
        static FullService.OperationsClient operaciones = new FullService.OperationsClient();
        static FullService.ConfiguracaoCassette casett = new FullService.ConfiguracaoCassette();
        static FullService.DadosCassete dados = new FullService.DadosCassete();


        private void button3_Click(object sender, EventArgs e)
        {
            authData.Usuario = user;
            authData.Terminal = terminal;
            authData.Senha = Pasword;
            string mensaje = operaciones.AbastecimentoIniciar(authData).Mensagem;
           // MessageBox.Show(mensaje);

            Deposito_D deposito_d = new Deposito_D();
            deposito_d.lblNo_Cuenta.Text = lblDCuenta.Text;
            deposito_d.Show();
            this.Hide();


 

        }

        public void Log(string logMessage, TextWriter w)
        {
            string EstadoFecha = (DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString());
            string Operacion = "Deposito";
            w.WriteLine("--------------------------------------Estado Botón Deposito------------------------------------------------");

            w.WriteLine(EstadoFecha);
            w.WriteLine("inicio de opeación Deposito \t" + Operacion);
            w.WriteLine(lblDCuenta.Text.Substring(0, 4) + "****");

        }

        private void Close_Form(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
