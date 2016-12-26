using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System.Dynamic;
using System.Drawing.Printing;
using System.Net;
using System.IO;
using System.Threading;
using tlockCajeros;

namespace MaterialSkinExample
{
    public partial class C_Deposito : MaterialForm
    {
        private readonly MaterialSkinManager materialSkinManager;

        public C_Deposito()
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

            btnPrinTicket.Visible = false;
        }

        private void C_Deposito_Load(object sender, EventArgs e)
        {
            string a = @"piano.gif";
            pictureBox_loading.SizeMode = PictureBoxSizeMode.StretchImage; //con  ejecutable error
            pictureBox_loading.Image = Image.FromFile(a);
            lblEspera.Visible = false;
            pictureBox_loading.Visible = false;

            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            this.WindowState = FormWindowState.Maximized;
            btnFinalizar.Visible = false;
            btnPrinTicket.Visible = false;
            metroButton3.Visible = false;
            lblCuenta.Text = mCuenta.ToString();

            lblMensageGuardar.Location = new Point(66, 235);
            lblMensageGuardar.Size = new Size(841, 424);


            lblscroll.Location = new Point(140, 220);
            lblscroll.Size = new Size(700, 370);

            lblscroll.Visible = false;
            pictureBox_loading.Location = new Point(100, 235);
            pictureBox_loading.Size = new Size(841, 341);

            lblEspera.Location = new Point(36, 610);
            lblEspera.Size = new Size(970, 73);

        }

        private void Close_Form(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            MainForm Continuar = new MainForm();
            Continuar.ContinuarDeposito();
        }

        private void metroButton2_Click(object sender, EventArgs e)
        {
            WTcr mwtcr = new WTcr();
            mwtcr.ContinuarDeposito();

        }

        private void metroButton3_Click(object sender, EventArgs e)
        {
            WTcr cancelar = new WTcr();
            cancelar.CancelarDeposito();
            metroButton3.Enabled = !metroButton3.Enabled;
            Home home = new Home();
            home.lblHCuenta.Text = mCuenta.ToString();
            this.Hide();
            home.Show();
            JournalCancelarDeposito();
        }


        private void metroButton2_Click_1(object sender, EventArgs e)
        {
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

            string cuenta = lblCuenta.Text; //Número de cuenta cliente
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

            g.DrawString("Depósito de EFECTIVO\n" + "Ubicación: Suc. CEDA\n" + "Id Cajero: 2001\n", fBody1, sb, 10, ESPACIO);
            g.DrawString("Transacción: " + numtransaccion, fBody1, sb, 10, ESPACIO + 60);
            g.DrawString("No. Cuenta: " + cuenta, fBody1, sb, 10, ESPACIO + 75);
            g.DrawString("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy") + "  Hora: " + /*DateTime.Now.ToString("hh:mm:ss")*/ hora1 + "\n", fBody1, sb, 10, ESPACIO + 90);
            g.DrawString("========================================\n", fBody1, sb, 10, ESPACIO + 105);
            g.DrawString("Cantidad        Denominación          Total\n", fBody1, sb, 10, ESPACIO + 125);
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
            g.DrawString("Total Depositado: " + "$" + lblT7.Text + "\n", fBody1, sb, 10, ESPACIO + 145 + Mov); Mov = Mov + 20;
            g.DrawString("========================================\n", fBody1, sb, 10, ESPACIO + 145 + Mov); Mov = Mov + 20;
            g.DrawString("Para aclaraciones de los depósitos realizados\nponemos a su disposición nuestras líneas de\natención.\n\nMéxico D.F.:\nMonterrey:\nGuadalajara:\nResto del país:\n", fBody1, sb, 10, ESPACIO + 145 + Mov);
        }


        private void PrintTicket(object sender, EventArgs e)
        {
        }
        public void btnprintTicket()
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            pd.PrintController = new StandardPrintController();
            pd.Print();
        }
        // Variables que guardan informacion 
        public int save = 0;
        public int c1 = 0, c2 = 0, c3 = 0, c4 = 0, c5 = 0, c6 = 0;
        //capturar btton
        private void materialFlatButton1_Click_1(object sender, EventArgs e)
        { }


        public void salvartabla()
        {
            int no_cuenta = Convert.ToInt32(lblCuenta.Text);

            int id_cajero = Convert.ToInt32(lblCajero.Text);
            int MXN20 = Convert.ToInt32(lblC1.Text);
            int MXN50 = Convert.ToInt32(lblC2.Text);
            int MXN100 = Convert.ToInt32(lblC3.Text);
            int MXN200 = Convert.ToInt32(lblC4.Text);
            int MXN500 = Convert.ToInt32(lblC5.Text);
            int MXN1000 = Convert.ToInt32(lblC6.Text);
            int Total_Billetes = MXN20 + MXN50 + MXN100 + MXN200 + MXN500 + MXN1000;
            int mTotal = Convert.ToInt32(lblT7.Text);
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

        public static int mCuenta;



        public static string FechaInicio;
        public void json2()
        {


            Deposito deposito = new Deposito();
            int m_IdEstacion = Convert.ToInt32(lblCajero.Text);
            string m_Ubicacion = "CEDA";
            int m_Ciclo = 1;
            int m_Folio = 287;

            string m_FechaHoraFin = String.Format(" {0:s}  ", DateTime.Now + DateTime.Now.ToString("%K"));
            string m_IdMoneda = "MXN";
            string m_IdCliente = "1330";
            string m_Cliente = "N/A";
            string m_BancoCuenta = "N/A";
            string m_Cuenta = lblCuenta.Text;
            string m_Referencia = "";
            string m_ClaveOperadorLocal = "N/A";
            string m_NombreCompletoOperador = "N/A";
            string m_SaldoProcesado = lblT7.Text;
            int m_MontoDeclarado;
            int m_TotalIncidentes = 0;
            string m_Envases = "";
            MySqlConnection con = new MySqlConnection("Server=localhost; User=OKI; Password=OKI2016; database=tlock; port=3306;");
            MySqlCommand cmd = new MySqlCommand();
            con.Open();
            cmd.Connection = con;
            //select IFNULL(sum(total),0)
            cmd.CommandText = ("select IFNULL(sum(total),0) as total from transaccion where no_cuenta = '" + lblCuenta.Text + "'");
            string m_SaldoAnterior = cmd.ExecuteScalar().ToString();
            con.Close();
            //fin consulta 

            int saldoanterior = Convert.ToInt32(m_SaldoAnterior);
            int m_MontoProcesado = Convert.ToInt32(m_SaldoProcesado);
            m_MontoDeclarado = saldoanterior + m_MontoProcesado;

            string MXN20c = lblC1.Text;
            string MXN20d = lblD1.Text;

            string MXN50c = lblC2.Text;
            string MXN50d = lblD2.Text;

            string MXN100c = lblC3.Text;
            string MXN100d = lblD3.Text;

            string MXN200c = lblC4.Text;
            string MXN200d = lblD4.Text;

            string MXN500c = lblC5.Text;
            string MXN500d = lblD5.Text;

            string MXN1000c = lblC6.Text;
            string MXN1000d = lblD6.Text;

            try
            {

                string IdEstacion = "2001";
                string IdCategoriaMensaje = "1";
                string IdTipoMensaje = "1000";
                string VersionProtocolo = "23";
 //http://187.174.220.229/sol/publico/pd.aspx?IdEstacion=200&IdMensaje=233495&IdCategoriaMensaje=1&IdTipoMensaje=1000&VersionProtocolo=2&c=”

//string webAddr = "http://187.174.220.229/presol/publico/pd.aspx?&IdEstacion&IdCategoriaMensaje&IdTipoMensaje&VersionProtocolo";
                string webAddr = "http://187.174.220.229/sol/publico/pd.aspx?IdEstacion=2001&IdCategoriaMensaje=1&IdTipoMensaje=1000&VersionProtocolo=2&c=";


                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";



                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string json = @"{ m_IdEstacion: " + m_IdEstacion + ", m_Ubicacion: '" + m_Ubicacion +
"',m_Ciclo:" + m_Ciclo + ",m_Folio:" + m_Folio + ",m_FechaHoraInicio:'" + FechaInicio + "',m_FechaHoraFin:'" + m_FechaHoraFin +
"',m_IdMoneda:'" + m_IdMoneda + "',m_IdCliente:'" + m_IdCliente + "',m_Cliente:'" + m_Cliente +
"',m_BancoCuenta:'" + m_BancoCuenta + "',m_Cuenta:'" + m_Cuenta + "',m_Referencia:'" + m_Referencia +
"',m_ClaveOperadorLocal:'" + m_ClaveOperadorLocal + "',m_NombreCompletoOperador:'" + m_NombreCompletoOperador +
"',m_ClaveOperadorLocal:'" + m_ClaveOperadorLocal + "',m_SaldoAnterior:" + m_SaldoAnterior +
",m_MontoProcesado:" + m_MontoProcesado + ",m_MontoDeclarado:" + m_MontoDeclarado +
",m_TotalIncidentes:" + m_TotalIncidentes + ",   m_DenominacionContenedor:  {'1':" + "{'1000':" + MXN1000c + ",'500':" + MXN500c +
",'200':" + MXN200c + ",'100':" + MXN100c + ",'50':" + MXN50c + ",'20':" + MXN20c + "}" + "},m_Envases:{}}";


                    JObject jobj = JObject.Parse(json);

                    string cadena = jobj.ToString();

                    

                    var encrypt = tlockCajeros.codificaMensajes.Codificar(cadena);

                    streamWriter.Write(json); streamWriter.Flush();
                    var texto = jobj;

                    DateTime namefile = DateTime.Now;
                    string m_archivo = namefile.Day.ToString() + "-" + namefile.Month.ToString() + "-" + namefile.Year.ToString() + "h" + namefile.Hour.ToString() + "m" + namefile.Minute.ToString() + "s" + namefile.Second.ToString() + ".json";
                    //var texto = jobj;
                    StreamWriter file = new StreamWriter(@"C:\Directorio SICE\JSONS_N\" + m_archivo);
                    StreamWriter file2 = new StreamWriter(@"C:\Directorio SICE\encript.txt");

                    file.WriteLine(texto);
                    file.Close();

                    file2.WriteLine(encrypt);
                    file2.Close();

                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();

                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();

                    WebResponse response = httpWebRequest.GetResponse();

                    MessageBox.Show(responseText );
                    MessageBox.Show(((HttpWebResponse)response).StatusDescription);
                }

            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message+"error en ...");
            



            }
        }
    
        //Journals

        public void JournalContinuarDeposito()
        {
            DateTime fecha = DateTime.Now;

            string m_archivo = fecha.Day.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Year.ToString() + ".journal";
            var archivo_Journal = (@"C:\Directorio SICE\" + m_archivo + "");
            using (StreamWriter w = File.AppendText(archivo_Journal))
            {
                LogDeposito("SICE", w);
            }


        }

        public void LogDeposito(string logMessage, TextWriter w)
        {
            string hora1 = DateTime.Now.ToString("HH:mm:ss");

            string Operacion = "Depósito";
            w.WriteLine("--------------------------------------Estado Botón Continuar Depósito ------------------------------------------------");

            w.WriteLine(hora1);
            w.WriteLine("Inicio la operación de Depósito  \t" + Operacion);
            w.WriteLine(mCuenta.ToString().Substring(0, 4) + "****");
        }

        //journal cancelar deposito

        public void JournalCancelarDeposito()
        {
            DateTime fecha = DateTime.Now;

            string m_archivo = fecha.Day.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Year.ToString() + ".journal";
            var archivo_Journal = (@"C:\Directorio SICE\" + m_archivo + "");
            using (StreamWriter w = File.AppendText(archivo_Journal))
            {
                Logcancelar("SICE", w);
            }
        }

        private void lblCuenta_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {
            //borrar
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void lblT7_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click_1(object sender, EventArgs e)
        {

        }

        WTcr continuard = new WTcr();

        private void button1_Click_1(object sender, EventArgs e)
        {


            int scroll = c1 + c2 + c3 + c4 + c5 + c6;
            if (scroll == 2000)
            {
                lblscroll.Text = " Entrada máxima de billetes, Cancele o Finalice su operación  ";
                lblscroll.Visible = true;


                btnDeposito.Visible = false;

                btnCancelar.Location = new Point(154, 612);
                btnFinalizar.Location = new Point(527, 612);



                // string result = MyMessageBox.ShowBox("Por favor, Finalice su Transacción", "Mensaje");
            }
            else
            {
                pictureBox_loading.Visible = true;
                lblEspera.Visible = true;
                Thread th = new Thread(new ThreadStart(OperacionDeposito));
                th.Start();
            }

        }

        void OperacionDeposito()
        {

            lblC1.ForeColor = Color.Red;
            lblD1.ForeColor = Color.Red;
            lblT1.ForeColor = Color.Red;

            lblC2.ForeColor = Color.Red;
            lblD2.ForeColor = Color.Red;
            lblT2.ForeColor = Color.Red;

            lblC3.ForeColor = Color.Red;
            lblD3.ForeColor = Color.Red;
            lblT3.ForeColor = Color.Red;

            lblC4.ForeColor = Color.Red;
            lblD4.ForeColor = Color.Red;
            lblT4.ForeColor = Color.Red;

            lblC5.ForeColor = Color.Red;
            lblD5.ForeColor = Color.Red;
            lblT5.ForeColor = Color.Red;

            lblC6.ForeColor = Color.Red;
            lblD6.ForeColor = Color.Red;
            lblT6.ForeColor = Color.Red;

            lblT7.ForeColor = Color.Red;
            continuard.ContinuarDeposito();

            //Denomionaciones
            int _denom0 = Convert.ToInt32(continuard.denom0.ToString());
            int _denom1 = Convert.ToInt32(continuard.denom1.ToString());
            int _denom2 = Convert.ToInt32(continuard.denom2.ToString());
            int _denom3 = Convert.ToInt32(continuard.denom3.ToString());
            int _denom4 = Convert.ToInt32(continuard.denom4.ToString());
            int _denom5 = Convert.ToInt32(continuard.denom5.ToString());
            //Total
            int _Total = Convert.ToInt32(continuard.mTotal.ToString());

            //Cantidad por billetes 
            int _cantidad0 = Convert.ToInt32(continuard.cantidad0.ToString());
            int _cantidad1 = Convert.ToInt32(continuard.cantidad1.ToString());
            int _cantidad2 = Convert.ToInt32(continuard.cantidad2.ToString());
            int _cantidad3 = Convert.ToInt32(continuard.cantidad3.ToString());
            int _cantidad4 = Convert.ToInt32(continuard.cantidad4.ToString());
            int _cantidad5 = Convert.ToInt32(continuard.cantidad5.ToString());

            // Denominaciones 
            lblD1.Text = ("$" + _denom0.ToString());
            lblD2.Text = ("$" + _denom1.ToString());
            lblD3.Text = ("$" + _denom2.ToString());
            lblD4.Text = ("$" + _denom3.ToString());
            lblD5.Text = ("$" + _denom4.ToString());
            lblD6.Text = ("$" + _denom5.ToString());




            //Salvando  Totales 
            lblT7.Text = ("" + _Total);
            save += Convert.ToInt32(lblT7.Text);
            //  lblT7.Text = ("$" + save);
            lblT7.Text = ("" + save);

            //validar boton 

            if (save == 0)
            {
                btnFinalizar.Visible = false;
            }
            else
            {
                btnFinalizar.Visible = true;
            }

            if (save > 0)
            {
                // button3.Visible = false;
            }
            // Cantidades por unidad de billetes 
            //Salvando unidades por cantidad

            lblC1.Text = ("" + _cantidad0);
            c1 += Convert.ToInt32(lblC1.Text);
            lblC1.Text = ("" + c1);// Cantidades  guardadas por unidad de billetes en varible C1


            lblC2.Text = ("" + _cantidad1);
            c2 += Convert.ToInt32(lblC2.Text);
            lblC2.Text = ("" + c2);

            lblC3.Text = ("" + _cantidad2);
            c3 += Convert.ToInt32(lblC3.Text);
            lblC3.Text = ("" + c3);

            lblC4.Text = ("" + _cantidad3);
            c4 += Convert.ToInt32(lblC4.Text);
            lblC4.Text = ("" + c4);

            lblC5.Text = ("" + _cantidad4);
            c5 += Convert.ToInt32(lblC5.Text);
            lblC5.Text = ("" + c5);

            lblC6.Text = ("" + _cantidad5);
            c6 += Convert.ToInt32(lblC6.Text);
            lblC6.Text = ("" + c6);

            //Operaciones de totales por unidad 
            int total0 = c1 * _denom0;
            int total1 = c2 * _denom1;
            int total2 = c3 * _denom2;
            int total3 = c4 * _denom3;
            int total4 = c5 * _denom4;
            int total5 = c6 * _denom4;

            //Total Unidad por Cantidad 

            lblT1.Text = ("$" + total0);
            lblT2.Text = ("$" + total1);
            lblT3.Text = ("$" + total2);
            lblT4.Text = ("$" + total3);
            lblT5.Text = ("$" + total4);
            lblT6.Text = ("$" + total5);

            lbltotalbilletes.Text = (c1 + c2 + c3 + c4 + c5 + c6).ToString();

            JournalContinuarDeposito();

            pictureBox_loading.Visible = false;///picture box 
            lblEspera.Visible = false;
            if (continuard.Rechazos > 0)
            {
                string result = MyMessageBox.ShowBox("Billetes Rechazados \n Intente de nuevo ", "Mensaje");
            }
        }

        public static Thread th2 = null;

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Visible = false;
            lblscroll.Visible = false;
            btnFinalizar.Visible = false;
            btnDeposito.Visible = false;
            btnCancelar.Visible = false;

            lblMensageGuardar.Text = "Espere... \n Guardando su Depósito";
            lblMensageGuardar.Visible = true;
            th2 = new Thread(new ThreadStart(finalizarOperacion));
            th2.Start();
            th2.Join();
            MainForm mainform = new MainForm();
            this.Hide();
            mainform.Show();
        }


        public void finalizarOperacion()
        {
            WTcr finalizar = new WTcr();
            finalizar.FinDeposito();
            btnFinalizar.Visible = false;

            btnDeposito.Visible = false;
            metroButton3.Visible = false;
            json2();
            salvartabla();

            JournalFinalizarDeposito();
            lblMensageGuardar.Text = "";

            string result = MyMessageBox.ShowBox("Transacción Exitosa ... \n No olvide retirar su ticket", "Mensaje");

            btnprintTicket();

        }


        private void button3_Click(object sender, EventArgs e)
        {
            lblscroll.Size = new Size(841, 424);
            lblscroll.Visible = true;
            lblscroll.Text = "Espere... \n Cancelando operación ";
            btnCancelar.Visible = false;
            btnFinalizar.Visible = false;
            btnDeposito.Visible = false;

            Thread th = new Thread(new ThreadStart(cancelardeposito));
            th.Start();
            th.Join();

            MainForm main = new MainForm();
            this.Hide();
            main.Show();

        }

        public void cancelardeposito()
        {

            WTcr cancelar = new WTcr();
            cancelar.CancelarDeposito();

            JournalCancelarDeposito();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {
            lblEspera.Location = new Point(131, 596);
            lblEspera.Size = new Size(704, 63);
        }

        public void Logcancelar(string logMessage, TextWriter w)
        {

            string EstadoFecha = (DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString());
            string Operacion = "Deposito";
            w.WriteLine("--------------------------------------Estado Botón Cancelar Deposito------------------------------------------------");

            w.WriteLine(EstadoFecha);
            w.WriteLine("se cancelo opeación Depósito \t" + Operacion);
            w.WriteLine(mCuenta.ToString().Substring(0, 4) + "****");
        }



        //journal Finalizar deposito

        public void JournalFinalizarDeposito()
        {
            DateTime fecha = DateTime.Now;
            string m_archivo = fecha.Day.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Year.ToString() + ".journal";
            var archivo_Journal = (@"C:\Directorio SICE\" + m_archivo + "");
            using (StreamWriter w = File.AppendText(archivo_Journal))
            {
                LogFinalizar("SICE", w);
            }
        }

        public void LogFinalizar(string logMessage, TextWriter w)
        {
            string Total = lblT7.Text;
            int id_cajero = Convert.ToInt32(lblCajero.Text);
            int MXN20 = Convert.ToInt32(lblC1.Text);
            int MXN50 = Convert.ToInt32(lblC2.Text);
            int MXN100 = Convert.ToInt32(lblC3.Text);
            int MXN200 = Convert.ToInt32(lblC4.Text);
            int MXN500 = Convert.ToInt32(lblC5.Text);
            int MXN1000 = Convert.ToInt32(lblC6.Text);
            int Total_Billetes = MXN20 + MXN50 + MXN100 + MXN200 + MXN500 + MXN1000;

            dynamic Denom = new JObject();

            string _MXN20 = lblC1.Text;
            string _MXN20d = lblD1.Text;

            string _MXN50 = lblC2.Text;
            string _MXN50d = lblD2.Text;

            string _MXN100 = lblC3.Text;
            string _MXN100d = lblD3.Text;

            string _MXN200 = lblC4.Text;
            string _MXN200d = lblD4.Text;

            string _MXN500 = lblC5.Text;
            string _MXN500d = lblD5.Text;

            string _MXN1000 = lblC6.Text;
            string _MXN1000d = lblD6.Text;

            Denom.Tags = new JArray(
                _MXN20 + ":" + _MXN20d,
                _MXN50 + ":" + _MXN50d,
                _MXN100 + ":" + _MXN100d,
                _MXN200 + ":" + _MXN200d,
                _MXN500 + ":" + _MXN500d,
                _MXN1000 + ":" + _MXN1000d);
            var mDenom = Denom.Tags;


            string hora1 = DateTime.Now.ToString("HH:mm:ss");
            string Operacion = "Depósito";
            w.WriteLine("--------------------------------------Estado Botón Finalizar Depósito------------------------------------------------");
            w.WriteLine(hora1);
            w.WriteLine("Finalizo opeación Depósito \t" + Operacion);
            w.WriteLine(mCuenta.ToString().Substring(0, 4) + "****");
            w.WriteLine("Total de Billetes" + Total_Billetes);
            w.WriteLine("Denominaciones Ingresadas" + mDenom);
            w.WriteLine("Total" + Total);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Dispose();
        }
        private void escape(object sender, KeyPressEventArgs e)
        {

        }
    }
}
