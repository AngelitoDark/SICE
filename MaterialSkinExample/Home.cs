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
using MetroFramework.Forms;
using MySql.Data.MySqlClient;
using System.Data.Common;
using System.IO;
using System.Threading;

namespace MaterialSkinExample
{
    public partial class Home : MaterialForm
    {

        private readonly MaterialSkinManager materialSkinManager;



        public Home()
        {
            InitializeComponent();

            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey900, Primary.BlueGrey100, Primary.gris500, Accent.LightBlue200, TextShade.WHITE);
            pictureBox1.SizeMode = PictureBoxSizeMode.StretchImage;
            string filePath = @"logo2.png";


            Bitmap myBitmap = new Bitmap(filePath);
            myBitmap.MakeTransparent();
            this.pictureBox1.Image = myBitmap;
        }
        public static string mlbl;


        private void Home_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
        }

        private void ClosedForm(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void btnRetiro_Click(object sender, EventArgs e)
        {
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {

        }

        private void iTalk_Button_11_Click(object sender, EventArgs e)
        {

        }

        private void btnDepos_Click(object sender, EventArgs e)
        {

        }
        public void JournalOpeDep()
        {
            DateTime fecha = DateTime.Now;

            string m_archivo = fecha.Day.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Year.ToString() + ".journal";
            var archivo_Journal = (@"C:\Directorio SICE\Journals\" + m_archivo + "");
            using (StreamWriter w = File.AppendText(archivo_Journal))
            {
                Log("SICE", w);
            }


        }

        public void Log(string logMessage, TextWriter w)
        {
            string EstadoFecha = (DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString());
            string Operacion = "Deposito";
            w.WriteLine("--------------------------------------Selección Botón Deposito------------------------------------------------");

            w.WriteLine(EstadoFecha);
            w.WriteLine("Selección de opeación \t" + Operacion);
            w.WriteLine(lblHCuenta.Text.Substring(0, 4) + "****");

        }

        public void journalSalirApp()
        {
            DateTime fecha = DateTime.Now;

            string m_archivo = fecha.Day.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Year.ToString() + ".journal";
            var archivo_Journal = (@"C:\Directorio SICE\Journals\" + m_archivo + "");
            using (StreamWriter w = File.AppendText(archivo_Journal))
            {
                LogSalir("SICE", w);
            }


        }

        public void LogSalir(string logMessage, TextWriter w)
        {
            string EstadoFecha = (DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString());
            string Operacion = "Cerrar Sesión";
            w.WriteLine("--------------------------------------Estado Selección Botón Salir------------------------------------------------");

            w.WriteLine(EstadoFecha);
            w.WriteLine("Selección de opeación \t" + Operacion);
            w.WriteLine(" Cerro Sesión por el usuario " + lblHCuenta.Text.Substring(0, 4) + "****");

        }
        private void iTalk_Button_11_Click_1(object sender, EventArgs e)
        {
            Querys.idTransaccion();

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        public void salir_App()
        {

            Thread.Sleep(60000);
            MainForm mainform = new MainForm();
            this.Hide();
            mainform.Show();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            JournalOpeDep();
            Deposito deposito = new Deposito();
            deposito.lblDCuenta.Text = lblHCuenta.Text;
            this.Hide();
            deposito.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            journalSalirApp();
            MainForm main = new MainForm();
            this.Hide();
            main.Show();
        }

        private void pictureBox2_Click_1(object sender, EventArgs e)
        {

        }
        public int timeLeft { get; set; }
        private void timer1_Tick(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            journalSalirApp();
            MainForm main = new MainForm();
            this.Hide();
            main.Show();
        }
    }
}
