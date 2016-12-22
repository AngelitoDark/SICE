using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialSkinExample
{
    public partial class Retiro : MaterialForm
    {

        static string user = "TCR";
        static String Pasword = "12345";
        static string terminal = "123";
        static FullService.Autenticacao authData = new FullService.Autenticacao();
        static FullService.OperationsClient operaciones = new FullService.OperationsClient();
        static FullService.ConfiguracaoCassette casett = new FullService.ConfiguracaoCassette();
        static FullService.DadosCassete dados = new FullService.DadosCassete();


        private readonly MaterialSkinManager materialSkinManager;
        // public int  r1;
        // Continuando con el Deposito
        int dm0;
        int dm1;
        int dm2;
        int dm3;
        int dm4;
        int dm5;

        //Cantidades por unidad
        int cant0;
        int cant1;
        int cant2;
        int cant3;
        int cant4;
        int cant5;
        int totalretiro;
        public static string FechaInicio;


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
            lblR_Cuenta.Text = groupBox1.Text;

            groupBox1.Visible = false;

            groupBox1.Location = new Point(13, 281);
            groupBox1.Size = new Size(425, 248);



            //   197; 72
            loading.Visible = false;

            loading.Location = new Point(150,170);
            loading.Size = new Size(642, 400);


            gruporetiro.Text = lblR_Cuenta.Text;

            this.ActiveControl = null;


        }

        private void button4_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtRetiro.Text))
            {
                string result = MyMessageBox.ShowBox("Ingresa monto a retirar.", "Mensaje");

            }
            else
            {

                resp._retiro = Convert.ToInt32(txtRetiro.Text);
                groupBox1.Visible = true;
            }


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

        private void button2_Click(object sender, EventArgs e)
        {




            operacion_retiro();



        }
        public void operacion_retiro()
        {
            if (string.IsNullOrEmpty(txt_Contraseña.Text))
            {
                string result = MyMessageBox.ShowBox("Debes Ingresar Contraseña.", "Mensaje");

            }
            else

                try
                {
                    string Consulta = string.Format("select * from Usuarios WHERE No_cuenta='{0}' AND contraseña ='{1}'", lblR_Cuenta.Text.Trim(), txt_Contraseña.Text.Trim());

                    DataSet ds = BuscarAdministrador.Consultar(Consulta);

                    string Cuenta = ds.Tables[0].Rows[0]["No_cuenta"].ToString().Trim();
                    string Contraseña = ds.Tables[0].Rows[0]["contraseña"].ToString().Trim();

                    if (Cuenta == lblR_Cuenta.Text.Trim() && Contraseña == txt_Contraseña.Text.Trim())
                    {

                        try
                        {

                            MySqlConnection _con = new MySqlConnection("Server=localhost; User=OKI; Password=OKI2016; database=tlock; port=3306;");
                            MySqlCommand _cmd = new MySqlCommand();
                            MySqlCommand R_cmd = new MySqlCommand();
                            _con.Open();
                            _cmd.Connection = _con;
                            R_cmd.Connection = _con;
                            DateTime fecha = DateTime.Now;

                            string formato = (fecha.Year + "-" + fecha.Month + "-" + fecha.Day) + "%";
                            _cmd.CommandText = ("select IFNULL(sum(total),0) as total from transaccion where fecha_Transaccion LIKE '" + formato + "' and no_cuenta ='" + lblR_Cuenta.Text + "'");
                            R_cmd.CommandText = ("select IFNULL(sum(total),0) as total from retiro where fecha_Transaccion LIKE '" + formato + "' and no_cuenta ='" + lblR_Cuenta.Text + "'");
                            // cmd.CommandText = ("select IFNULL(sum(total),0) as total from transaccion where no_cuenta = '" + lblNo_Cuenta.Text + "'");
                            string cons = R_cmd.ExecuteScalar().ToString();
                            int valor = Convert.ToInt32(cons);
                            MessageBox.Show("" + valor);
                            int _txtRetiro = Convert.ToInt32(txtRetiro.Text);
                            int saldo_D = Convert.ToInt32(_cmd.ExecuteScalar());
                            _con.Close();

                            if (_txtRetiro > saldo_D)
                            {
                                MyMessageBox.ShowBox("La cantidad solicitada no está Disponible");
                            }
                            else
                            {
                                if (saldo_D == valor)
                                {
                                    MyMessageBox.ShowBox("Saldo insuficiente para retiro ");
                                }
                                else
                                {

                                    loading.Visible = true;
                                    tclNumerico.Visible = false;
                                    gruporetiro.Visible = false;

                                    PrintTicket();

                                    MainForm mainform = new MainForm();
                                    this.Hide();
                                    mainform.Show();
                                }

                            }


                        }
                        catch (Exception)
                        {

                            MessageBox.Show("error en la consulta ...");
                        }

                    }

                }
                catch
              (Exception er)
                {

                    string errorcontraseña = MyMessageBox.ShowBox(" Contraseña Incorrecta.", "Mensaje");
                    txt_Contraseña.Clear();
                }

        }


        public void PrintTicket()
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            pd.PrintController = new StandardPrintController();
            pd.Print();

        }
        //    Response respuesta = new Response();
        //Nuevo Ticket
        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {

            authData.Usuario = user;
            authData.Terminal = terminal;
            authData.Senha = Pasword;
            int RETIRO = Convert.ToInt32(txtRetiro.Text);
            string json = operaciones.Sacar(authData, RETIRO).Retorno;

            TResponse wtResponse = JsonConvert.DeserializeObject<TResponse>(json);

            dm0 = wtResponse.rsmdata.Denom[0];
            dm1 = wtResponse.rsmdata.Denom[1];
            dm2 = wtResponse.rsmdata.Denom[2];
            dm3 = wtResponse.rsmdata.Denom[3];
            dm4 = wtResponse.rsmdata.Denom[4];
            dm5 = wtResponse.rsmdata.Denom[5];

            cant0 = wtResponse.rsmdata.Notes[0];
            cant1 = wtResponse.rsmdata.Notes[1];
            cant2 = wtResponse.rsmdata.Notes[2];
            cant3 = wtResponse.rsmdata.Notes[3];
            cant4 = wtResponse.rsmdata.Notes[4];
            cant5 = wtResponse.rsmdata.Notes[5];


            int total1 = cant0 * dm0;
            int total2 = cant1 * dm1;
            int total3 = cant2 * dm2;
            int total4 = cant3 * dm3;
            int total5 = cant4 * dm4;
            int total6 = cant5 * dm5;

            //Salvando las tablas 

            int no_cuenta = Convert.ToInt32(lblR_Cuenta.Text);

            int id_cajero = Convert.ToInt32(lblNo_Cajero.Text);
            int MXN20 = cant0;
            int MXN50 = cant1;
            int MXN100 = cant2;
            int MXN200 = cant3;
            int MXN500 = cant4;
            int MXN1000 = cant5;
            int Total_Billetes = MXN20 + MXN50 + MXN100 + MXN200 + MXN500 + MXN1000;
            //totalretiro = 
            int mTotal = wtResponse.iTotalAmount;
            MySqlConnection conn = new MySqlConnection("Server=localhost; User=OKI; Password=OKI2016; database=tlock; port=3306;");
            MySqlCommand cmdd = new MySqlCommand();
            conn.Open();
            cmdd.Connection = conn;
            cmdd.CommandText = "Insert into  retiro ( id_cajero, no_Cuenta, MXN20, MXN50, MXN100, MXN200, MXN500,MXN1000,    totalBilletes,total) VALUES ( '" + id_cajero + "','" + no_cuenta + "','" +
                    MXN20 + "','" +
                    MXN50 + "','" +
                    MXN100 + "','" +
                    MXN200 + "','" +
                  MXN500 + "','" +
                   MXN1000 + "','" +
                    Total_Billetes + "','" +
                    mTotal + "')";
            cmdd.ExecuteScalar();
            conn.Close();

            ///--------------------------
            string hora1 = DateTime.Now.ToString("HH:mm:ss");

            string cuenta = lblR_Cuenta.Text; //Número de cuenta cliente
            int Mov = 0;
            //Consulta SQL  
            MySqlConnection con = new MySqlConnection("Server=localhost; User=OKI; Password=OKI2016; database=tlock; port=3306;");
            MySqlCommand cmd = new MySqlCommand();
            con.Open();
            cmd.Connection = con;

            cmd.CommandText = ("select max(id_transaccion)from retiro");

            string numtransaccion = cmd.ExecuteScalar().ToString();

            con.Close();
            // Fin de Consulta SQL

            int ESPACIO = 85;  // Interlineado estandar entre texto
            Graphics g = e.Graphics;  //Uso de graficos para impresión de ticket

            string logo = Application.StartupPath + "\\logo.jpg";
            Font fBody = new Font("Arial Narrow", 9, FontStyle.Bold); //Cambio de estilo 
            Font fBody1 = new Font("Arial Narrow", 10, FontStyle.Regular); //Estilo utilizado en ticket
            SolidBrush sb = new SolidBrush(Color.Black);  //Color de texto
            g.DrawImage(Image.FromFile(logo), 130, -10, 160, 90);

            g.DrawString("Retiro de EFECTIVO\n" + "Ubicación: Suc. CEDA\n" + "Id Cajero: 201\n", fBody1, sb, 10, ESPACIO);
            g.DrawString("Transacción: " + numtransaccion, fBody1, sb, 10, ESPACIO + 60);
            g.DrawString("No. Cuenta: " + cuenta, fBody1, sb, 10, ESPACIO + 75);
            g.DrawString("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy") + "  Hora: " + /*DateTime.Now.ToString("hh:mm:ss")*/ hora1 + "\n", fBody1, sb, 10, ESPACIO + 90);
            g.DrawString("========================================\n", fBody1, sb, 10, ESPACIO + 105);
            g.DrawString("Cantidad         Denominación          Total\n", fBody1, sb, 10, ESPACIO + 125);





            if (cant0 >= 1)
            {

                g.DrawString(cant0.ToString(), fBody1, sb, 30, ESPACIO + 145);
                g.DrawString("$ " + dm0, fBody1, sb, 100, ESPACIO + 145);
                g.DrawString("$ " + total1.ToString("#,#", CultureInfo.InvariantCulture), fBody1, sb, 180, ESPACIO + 145);
                Mov = Mov + 20;
            }
            if (cant1 >= 1)
            {
                g.DrawString(cant1.ToString(), fBody1, sb, 30, ESPACIO + 145 + Mov);
                g.DrawString("$ " + dm1, fBody1, sb, 100, ESPACIO + 145 + Mov);
                g.DrawString("$ " + total2.ToString("#,#", CultureInfo.InvariantCulture), fBody1, sb, 180, ESPACIO + 145 + Mov);
                Mov = Mov + 20;
            }
            if (cant2 >= 1)
            {
                g.DrawString(cant2.ToString(), fBody1, sb, 30, ESPACIO + 145 + Mov);
                g.DrawString("$ " + dm2, fBody1, sb, 100, ESPACIO + 145 + Mov);
                g.DrawString("$ " + total3.ToString("#,#", CultureInfo.InvariantCulture), fBody1, sb, 180, ESPACIO + 145 + Mov);
                Mov = Mov + 20;
            }
            if (cant3 >= 1)
            {
                g.DrawString(cant3.ToString(), fBody1, sb, 30, ESPACIO + 145 + Mov);
                g.DrawString("$ " + dm3, fBody1, sb, 100, ESPACIO + 145 + Mov);
                g.DrawString("$ " + total4.ToString("#,#", CultureInfo.InvariantCulture), fBody1, sb, 180, ESPACIO + 145 + Mov);
                Mov = Mov + 20;
            }
            if (cant4 >= 1)
            {
                g.DrawString(cant4.ToString(), fBody1, sb, 30, ESPACIO + 145 + Mov);
                g.DrawString("$ " + dm4, fBody1, sb, 100, ESPACIO + 145 + Mov);
                g.DrawString("$ " + total5.ToString("#,#", CultureInfo.InvariantCulture), fBody1, sb, 180, ESPACIO + 145 + Mov);
                Mov = Mov + 20;
            }
            if (cant5 >= 1)
            {
                g.DrawString(cant5.ToString(), fBody1, sb, 30, ESPACIO + 145 + Mov);
                g.DrawString("$ " + dm5, fBody1, sb, 100, ESPACIO + 145 + Mov);
                g.DrawString("$ " + total6.ToString("#,#", CultureInfo.InvariantCulture), fBody1, sb, 180, ESPACIO + 145 + Mov);
                Mov = Mov + 20;
            }
            g.DrawString("========================================\n", fBody1, sb, 10, ESPACIO + 145 + Mov); Mov = Mov + 20;
            totalretiro = wtResponse.iTotalAmount;
            if (wtResponse.iTotalAmount == wtResponse.iTotalAmount)
            {

            }
            else
            {
                if (RETIRO != totalretiro)
                {
                    MyMessageBox.ShowBox("Retiro incompleto");
                    g.DrawString(" ----- Retiro incompleto cajero vacio ----- " + "\n", fBody1, sb, 10, ESPACIO + 145 + Mov); Mov = Mov + 20;
                    operaciones.ResetTCR(authData);//reseteto del TCR
                }
                else
                {
                    MyMessageBox.ShowBox("Operacion exitosa no olvide retirar su ticket");
                }
            }


    //        MyMessageBox.ShowBox("Operacion exitosa no olvide retirar su ticket");

            g.DrawString("Retiro Efectuado:" + "$ " + totalretiro.ToString("#,#", CultureInfo.InvariantCulture) + "\n", fBody1, sb, 10, ESPACIO + 145 + Mov); Mov = Mov + 20;
            //    g.DrawString("Total Retiro: " + "$" + txtRetiro.Text + "\n", fBody1, sb, 10, ESPACIO + 145 + Mov); Mov = Mov + 20;
            g.DrawString("========================================\n", fBody1, sb, 10, ESPACIO + 145 + Mov); Mov = Mov + 20;
            g.DrawString("Para aclaraciones de los depósitos realizados\nponemos a su disposición nuestras líneas de\natención.\n\nMéxico D.F.:\nMonterrey:\nGuadalajara:\nResto del país:\n", fBody1, sb, 10, ESPACIO + 145 + Mov);

            /////////////////////////////////////////////////////////////////////////////////
            

            Deposito deposito = new Deposito();
            int m_IdEstacion = Convert.ToInt32(lblNo_Cajero.Text);
            string m_Ubicacion = "CEDA";
            int m_Ciclo = 1;
            int m_Folio = 287;
            int IdCategoriaMensaje = 1;
            int IdTipoMensaje = 1000;
            int VersionProtocolo = 3;

            string m_FechaHoraFin = String.Format(" {0:s}  ", DateTime.Now + DateTime.Now.ToString("%K"));
            string m_IdMoneda = "MXN";
            string m_IdCliente = "1330";
            string m_Cliente = "Banorte-586082157";
            string m_BancoCuenta = "Banorte";
            string m_Cuenta = lblR_Cuenta.Text;
            string m_Referencia = "";
            string m_ClaveOperadorLocal = "586082157";
            string m_NombreCompletoOperador = "Banorte-586082157";
            string m_SaldoProcesado; ;
            int m_MontoDeclarado;
            int m_TotalIncidentes = 0;
            string m_Envases = "";
            MySqlConnection conx = new MySqlConnection("Server=localhost; User=OKI; Password=OKI2016; database=tlock; port=3306;");
            MySqlCommand cmdx = new MySqlCommand();
            MySqlCommand cmdt = new MySqlCommand();
            MySqlCommand cmdr = new MySqlCommand();
            conx.Open();
            cmdx.Connection = conx;
           
            cmdr.Connection = conx;
            DateTime fecha = DateTime.Now;
            string formato = (fecha.Year + "-" + fecha.Month + "-" + fecha.Day) + "%";

            // consulta 
            //SELECT IFNULL(SUM(total), 0) AS total from transaccion where no_cuenta =12113008;
            cmdx.CommandText = ("select IFNULL(sum(total),0) as total from transaccion where fecha_Transaccion LIKE '" + formato + "' and no_cuenta ='" + lblR_Cuenta.Text + "'");
            cmdr.CommandText = ("select IFNULL(sum(total),0) as total from retiro where fecha_Transaccion LIKE '" + formato + "' and no_cuenta ='" + lblR_Cuenta.Text + "'");
             
            int dep = Convert.ToInt32(cmdx.ExecuteScalar().ToString());
            int ret = Convert.ToInt32(cmdr.ExecuteScalar().ToString());

          //  string m_SaldoAnterior = cmdx.ExecuteScalar().ToString();

            conx.Close();
            
            int m_SaldoAnterior = dep - ret;
            int m_MontoProcesado = totalretiro;
            //    int     m_MontoProcesado = 1;
            m_MontoDeclarado = m_SaldoAnterior;

            int MXN20c = cant0; // lblC1.Text;
            int MXN20d = dm0; //lblD1.Text;

            int MXN50c = cant1;
            int MXN50d = dm1;

            int MXN100c = cant2;
            int MXN100d = dm2;

            int MXN200c = cant3;
            int MXN200d = dm3;

            int MXN500c = cant4;
            int MXN500d = dm4;

            int MXN1000c = cant5;
            int MXN1000d = dm5;

            try
            {
                string webAddr = "http://187.174.220.229/presol/publico/pd.aspx";
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(webAddr);
                httpWebRequest.ContentType = "application/json; charset=utf-8";
                httpWebRequest.Method = "POST";

                using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
                {
                    string jsonn = @"{ m_IdEstacion: " + m_IdEstacion + ",IdCategoriaMensaje: '" + IdCategoriaMensaje + "',IdTipoMensaje:'" + IdTipoMensaje + "',VersionProtocolo:'" + VersionProtocolo + "',  m_Ubicacion: '" + m_Ubicacion +
"',m_Ciclo:" + m_Ciclo + ",m_Folio:" + m_Folio + ",m_FechaHoraInicio:'" + FechaInicio + "',m_FechaHoraFin:'" + m_FechaHoraFin +
"',m_IdMoneda:'" + m_IdMoneda + "',m_IdCliente:'" + m_IdCliente + "',m_Cliente:'" + m_Cliente +
"',m_BancoCuenta:'" + m_BancoCuenta + "',m_Cuenta:'" + m_Cuenta + "',m_Referencia:'" + m_Referencia +
"',m_ClaveOperadorLocal:'" + m_ClaveOperadorLocal + "',m_NombreCompletoOperador:'" + m_NombreCompletoOperador +
"',m_ClaveOperadorLocal:'" + m_ClaveOperadorLocal + "',m_SaldoAnterior:" + m_SaldoAnterior +
",m_MontoProcesado:" + m_MontoProcesado + ",m_MontoDeclarado:" + m_MontoDeclarado +
",m_TotalIncidentes:" + m_TotalIncidentes + ",   m_DenominacionContenedor:  {'1':" + "{'1000':" + MXN1000c + ",'500':" + MXN500c +
",'200':" + MXN200c + ",'100':" + MXN100c + ",'50':" + MXN50c + ",'20':" + MXN20c + "}" + "},m_Envases:{}}";

                    JObject jobj = JObject.Parse(jsonn);
                    streamWriter.Write(jsonn); streamWriter.Flush();

                    DateTime namefile = DateTime.Now;
                    int i = 58;

                    char c = (char)i;

                    string m_archivo = namefile.Day.ToString() + "-" + namefile.Month.ToString() + "-" + namefile.Year.ToString() + "h" + namefile.Hour.ToString() + "m" + namefile.Minute.ToString() + "s" + namefile.Second.ToString() + ".json";

                    var texto = jobj;
                    StreamWriter file = new StreamWriter(@"C:\Directorio SICE\JSONS_R\ " + m_archivo);


                    file.WriteLine(texto);
                    file.Close();

                }
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responseText = streamReader.ReadToEnd();
                }

            }
            catch (WebException ex)
            {
                MessageBox.Show(ex.Message);
            }
            

            ///////////////////////////////////////////////////////////////////////////////////////////////////


        }
        bool T1 = false;


        private void txtRetiro_TextChanged(object sender, EventArgs e)
        {

        }

        private void txt_Contraseña_TextChanged(object sender, EventArgs e)
        {

        }

        private void onclick_retiro(object sender, EventArgs e)
        {
            T1 = true;
            //   tclNumerico.Visible = true;
            //   s
        }

        private void onclick_pass(object sender, EventArgs e)
        {
            T1 = false;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtRetiro.Text = txtRetiro.Text + "1";
            }
            else
            {
                txt_Contraseña.Text = txt_Contraseña.Text + "1";
            }

        }

        private void btn2_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtRetiro.Text = txtRetiro.Text + "2";
            }
            else
            {
                txt_Contraseña.Text = txt_Contraseña.Text + "2";
            }
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtRetiro.Text = txtRetiro.Text + "3";
            }
            else
            {
                txt_Contraseña.Text = txt_Contraseña.Text + "3";
            }
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtRetiro.Text = txtRetiro.Text + "4";
            }
            else
            {
                txt_Contraseña.Text = txt_Contraseña.Text + "4";
            }
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtRetiro.Text = txtRetiro.Text + "5";
            }
            else
            {
                txt_Contraseña.Text = txt_Contraseña.Text + "5";
            }
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtRetiro.Text = txtRetiro.Text + "6";
            }
            else
            {
                txt_Contraseña.Text = txt_Contraseña.Text + "6";
            }
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtRetiro.Text = txtRetiro.Text + "7";
            }
            else
            {
                txt_Contraseña.Text = txt_Contraseña.Text + "7";
            }
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtRetiro.Text = txtRetiro.Text + "8";
            }
            else
            {
                txt_Contraseña.Text = txt_Contraseña.Text + "8";
            }
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtRetiro.Text = txtRetiro.Text + "9";
            }
            else
            {
                txt_Contraseña.Text = txt_Contraseña.Text + "9";
            }
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtRetiro.Text = txtRetiro.Text + "0";
            }
            else
            {
                txt_Contraseña.Text = txt_Contraseña.Text + "0";
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnBorrarNum_Click(object sender, EventArgs e)
        {

            if (T1 == true)
            {
                if (txtRetiro.Text.Length == 0)
                {
                    txtRetiro.Text = "";
                }
                else
                {
                    txtRetiro.Text = txtRetiro.Text.Substring(0, txtRetiro.Text.Length - 1);
                }
            }
            else
            {

                if (txt_Contraseña.Text.Length == 0)
                {
                    txt_Contraseña.Text = "";
                }
                else
                {
                    txt_Contraseña.Text = txt_Contraseña.Text.Substring(0, txt_Contraseña.Text.Length - 1);
                }
                //   txt_Contraseña.Text = txt_Contraseña.Text + "0";
            }
        }

        private void loading_Click(object sender, EventArgs e)
        {

        }
    }


    public class TResponse
    {
        public int iTotalAmount { get; set; }
        public string szCurrency { get; set; }
        public int iArrayReject { get; set; }
        public RsmData rsmdata { get; set; }
        public RejectData rejecdata { get; set; }
    }
    public class RsmData
    {
        public string szCurrency { get; set; }
        public int iArrayReject { get; set; }
        public IList<int> Denom { get; set; }
        public IList<int> Notes { get; set; }
        public IList<Codes> CurrencyCode { get; set; }
    }

    public class Codes
    {
        public string Data { get; set; }
    }

    public class RejectData
    {
        public IList<int> Denom { get; set; }
        public IList<int> Notes { get; set; }
        public IList<int> bCurrencyCode { get; set; }
        public IList<int> bCategory { get; set; }
        public IList<int> bRjCode { get; set; }
    }
    // }
}
