﻿using System;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Threading;
using System.IO;
using System.Data;
using System.Drawing;
using System.Management;
using System.Xml;
using System.Threading.Tasks;
using System.Globalization;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Text;

namespace MaterialSkinExample
{
    public partial class MainForm : MaterialForm
    {
        Boolean Valor = false; //Se considera T o F dependiendo de que caja esta en foco (txtUsuario o txtContraseña)
        Thread th;
        private readonly MaterialSkinManager materialSkinManager;

        static string user = "GO";
        static String Pasword = "go";
        static string terminal = "123";
        static FullService.Autenticacao authData = new FullService.Autenticacao();
        static FullService.OperationsClient operaciones = new FullService.OperationsClient();
        static FullService.ConfiguracaoCassette casett = new FullService.ConfiguracaoCassette();
        static FullService.DadosCassete dados = new FullService.DadosCassete();
        static FullService.ContentClient[] contenido = new FullService.ContentClient[3];
      //  public static DateTime fecha = DateTime.Now;

    //    public static string m_archivo = fecha.Day.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Year.ToString() + ".journal";


        public MainForm()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey900, Primary.BlueGrey100, Primary.GRIS200, Accent.LightBlue200, TextShade.WHITE);
            pictureBox2.SizeMode = PictureBoxSizeMode.StretchImage;
            string filePath = @"logo.JPg";

            Bitmap myBitmap = new Bitmap(filePath);
            myBitmap.MakeTransparent();
            this.pictureBox2.Image = myBitmap;
            Teller();
        }
        public void Teller()
        {
           
                MaterialSkin.Operaciones.AuthenticationType TCR = new MaterialSkin.Operaciones.AuthenticationType();
                MaterialSkin.Operaciones.DataResponse TCR1 = new MaterialSkin.Operaciones.DataResponse();
                MaterialSkin.Operaciones.WebTellerClient TCR2 = new MaterialSkin.Operaciones.WebTellerClient();
                string user = "TCR";
                string pass = "12345";
                string terminal = "1";


                TCR.sLogin = user;
                TCR.sPassword = pass;
                TCR.sTerminal = terminal;

                string ultimodia = TCR2.TransactionDayCurrent(TCR).RetData;
                string fecha_actual = DateTime.Now.ToString("dd");
                int dia_actual = Convert.ToInt32(fecha_actual);

                string msj02 = "02 - Día Inicializado";
                string msj03 = "03 - Día Activado";//TransactionDayOpen

                if (TCR2.TransactionDayOpen(TCR, dia_actual).Message == msj03)
                {
                    TCR2.TransactionDayClose(TCR, Convert.ToInt32(ultimodia.Substring(0, 2)));
                }
                else
                { }
          
        }
    

        private void materialFlatButton4_Click(object sender, EventArgs e)
        {

            EstadoForm();
        }
        private void openForm(object obj)
        {
            Application.Run(new Deposito());
        }
        //Carga de Nueva Ventana
        public void EstadoForm()
        {
            this.Close();
            th = new Thread(openForm);
            th.SetApartmentState(ApartmentState.STA);
            th.Start();


        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            txtUsuario.Select();  //Posiciona cursor
            txtUsuario.Focus();  //Posiciona cursor
            txtUsuario.MaxLength = 8;  //Maximo de posiciones en caja de texto

            Cursor.Hide();
            tclNumerico.Visible = true;

            Teller();
            Directorio();
            //txtUser.Focus();
            this.WindowState = FormWindowState.Maximized;

            // Thread th = new Thread(new ThreadStart(jsontcr));
            //th.Start();
          ///  th.Join();

            //   ping();
          

        }

        private void ping()
        {
            var DailyTime = "19:09:00";

            //  var DailyTime = (DateTime.Now.Hour + ":" + DateTime.Now.Minute + ":" + DateTime.Now.Second).ToString();
            var timeParts = DailyTime.Split(new char[1] { ':' });

            var dateNow = DateTime.Now;
            var date = new DateTime(dateNow.Year, dateNow.Month, dateNow.Day,
                       int.Parse(timeParts[0]), int.Parse(timeParts[1]), int.Parse(timeParts[2]));


            TimeSpan ts;
            if (date > dateNow)
                ts = date - dateNow;
            else
            {
                date = date.AddDays(1);
                ts = date - dateNow;
            }
            //waits certan time and run the code
            Task.Delay(ts).ContinueWith((x) => json());
           
        }
        ////////////////////////////////////////////////



        public void jsontcr() {
           int i = 0;
            while (true)
            {  i++;
                

                FullService.EstadoCassette estadocasett = new FullService.EstadoCassette();
                FullService.DadosConteudoTCR tcr1 = new FullService.DadosConteudoTCR();
                FullService.ContentClient tcr2 = new FullService.ContentClient();


                FullService.DadosCassete tcr3 = new FullService.DadosCassete();

                authData.Usuario = user;
                authData.Terminal = terminal;
                authData.Senha = Pasword;

                var x = tcr2.ConsultarConteudoTCR(authData).CodigoResposta;

                string contadorA = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[0].Contador;//A
                string contadorB = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[1].Contador;//B
                string contadorC = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[2].Contador;
                string contadorD = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[3].Contador;
                string contadorE = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[4].Contador;
                string contadorF = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[5].Contador;
                int valorA = Convert.ToInt32(contadorA);
                int valorB = Convert.ToInt32(contadorB);
                int valorC = Convert.ToInt32(contadorC);
                int valorD = Convert.ToInt32(contadorD);
                int valorE = Convert.ToInt32(contadorE);
                int valorF = Convert.ToInt32(contadorF);

                int contenidoA = valorA * 20;
                int contenidoB = valorB * 50;
                int contenidoC = valorC * 100;
                int contenidoD = valorD * 200;
                int contenidoE = valorE * 500;
                int contenidoF = valorF * 1000;


                int estadotcr = contenidoA + contenidoB + contenidoC + contenidoD + contenidoE + contenidoF;

                XmlDocument xDoc = new XmlDocument();

                xDoc.Load(@"../../Configuration files/config.xml");
                XmlNodeList tcr = xDoc.GetElementsByTagName("tcr");
                string m_Ubicacion = ((XmlElement)tcr[0]).GetElementsByTagName("sucursal")[0].InnerText;

                Deposito deposito = new Deposito();
                int m_IdEstacion = 2001;

               // string m_Ubicacion = "CLIENTE";
                int m_Ciclo = 1;
                int m_Folio = 368459;

                DateTime fecha = DateTime.Now;
                CultureInfo ci = CultureInfo.InvariantCulture;

                string hora = String.Format(fecha.ToString("HH:mm:ss.ff", ci));
                var zona = String.Format(DateTime.Now.ToString("%K"));
                string m_FechaHoraFin = (fecha.Year + "-" + fecha.Month + "-" + fecha.Day) + ("T" + hora + zona);
                string FechaInicio = (fecha.Year + "-" + fecha.Month + "-" + fecha.Day) + ("T" + hora + zona);//



                //try
                //{
                string json = @"{ m_IdEstacion: " + m_IdEstacion + ", m_Ubicacion: '" + m_Ubicacion + "',m_Folio:" + m_Folio + ",m_FechaHoraInicio:'" + FechaInicio + "',m_FechaHoraFin:'" + m_FechaHoraFin +
    "',   m_DenominacionContenedor:  {'1':" + "{'1000':" + contenidoF + ",'500':" + contenidoE +
",'200':" + contenidoD + ",'100':" + contenidoC + ",'50':" + contenidoB + ",'20':" +
contenidoA + "} } }";

                JObject jobj = JObject.Parse(json);
                //   MessageBox.Show(o.ToString());


                var texto = jobj;
                // MessageBox.Show(texto+"");
                System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:transaccion.json");
                file.WriteLine(texto);
                file.Close();


                /*
                    JObject jobj = JObject.Parse(json);
                    string cadena = jobj.ToString();
                    var encrypt = tlockCajeros.codificaMensajes.Codificar(cadena);

                    //Parametros del POST 
                    //   string url = "http://187.174.220.229/presol/publico/pd.aspx?IdEstacion=2001&IdMensaje=233495&IdCategoriaMensaje=1&IdTipoMensaje=1000&VersionProtocolo=2&c=" + encrypt;
                    string url = "http://187.174.220.229/presol/publico/pd.aspx?";
                    String paramsPost = encrypt;

                    HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                    httpRequest.ContentType = "application/x-www-form-urlencoded";
                    httpRequest.Method = "POST";
                    // Cambiamos la version de peticion por HTTP 1,0 
                    httpRequest.ProtocolVersion = new Version(1, 0);
                    httpRequest.ContentLength = paramsPost.Length;
                    Stream stream = httpRequest.GetRequestStream();
                    stream.Write(Encoding.ASCII.GetBytes(paramsPost), 0, paramsPost.Length);
                    stream.Flush();
                    stream.Close();

                    var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    var streamReader = new StreamReader(httpResponse.GetResponseStream());
                    String resultHtml = streamReader.ReadToEnd();

                    MessageBox.Show(resultHtml);
                    DateTime namefile = DateTime.Now;
                    string m_archivo = namefile.Day.ToString() + "-" + namefile.Month.ToString() + "-" + namefile.Year.ToString() + "h" + namefile.Hour.ToString() + "m" + namefile.Minute.ToString() + "s" + namefile.Second.ToString() + ".json";
                    var texto = jobj;
                    StreamWriter file = new StreamWriter(@"C:\Directorio SICE\JSONS_N\" + m_archivo);
                    StreamWriter file2 = new StreamWriter(@"C:\Directorio SICE\encript.txt");

                    file.WriteLine(texto);
                    file.Close();

                    file2.WriteLine(encrypt + resultHtml);
                    file2.Close();
                    */

                //}
                //catch (Exception e)
                //{
                //    MessageBox.Show(e + "Servidor no  disponible  ");
                //}

                ////----------------------------

                if (i == 3)
                {
                    i = 0;

                    int Minutos = 60000 * 5;
                    System.Threading.Thread.Sleep(Minutos);
                    //break;
                }
            }

        }
    
        /// <summary>
        /// ///////////////////////////////////////////////////////////
        public void json()
        {

            FullService.EstadoCassette estadocasett = new FullService.EstadoCassette();
            FullService.DadosConteudoTCR tcr1 = new FullService.DadosConteudoTCR();
            FullService.ContentClient tcr2 = new FullService.ContentClient();


            FullService.DadosCassete tcr3 = new FullService.DadosCassete();

            authData.Usuario = user;
            authData.Terminal = terminal;
            authData.Senha = Pasword;

            var x = tcr2.ConsultarConteudoTCR(authData).CodigoResposta;

            string contadorA = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[0].Contador;//A
            string contadorB = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[1].Contador;//B
            string contadorC = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[2].Contador;
            string contadorD = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[3].Contador;
            string contadorE = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[4].Contador;
            string contadorF = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[5].Contador;
            int valorA = Convert.ToInt32(contadorA);
            int valorB = Convert.ToInt32(contadorB);
            int valorC = Convert.ToInt32(contadorC);
            int valorD = Convert.ToInt32(contadorD);
            int valorE = Convert.ToInt32(contadorE);
            int valorF = Convert.ToInt32(contadorF);

            int contenidoA = valorA * 20;
            int contenidoB = valorB * 50;
            int contenidoC = valorC * 100;
            int contenidoD = valorD * 200;
            int contenidoE = valorE * 500;
            int contenidoF = valorF * 1000;


            int estadotcr = contenidoA + contenidoB + contenidoC + contenidoD + contenidoE + contenidoF;

          //  MessageBox.Show("El TCR contien $" + estadotcr + "total");


/*

            XmlDocument xDoc = new XmlDocument();
            xDoc.Load(@"../../Configuration files/config.xml");
            XmlNodeList tcr = xDoc.GetElementsByTagName("tcr");
            XmlNodeList lista =
    ((XmlElement)tcr[0]).GetElementsByTagName("configuracion");
            foreach (XmlElement nodo in lista)

            {
                XmlNodeList IdEstacion =
                   nodo.GetElementsByTagName("IdEstacion");
                XmlNodeList VersionProtocolo =
nodo.GetElementsByTagName("VersionProtocolo");

                string estacion = IdEstacion[0].InnerText;
                string id = VersionProtocolo[0].InnerText;
                //lblEstacion.Text = estacion;
            }
            */

            Deposito deposito = new Deposito();
            int m_IdEstacion = 2001;
           
            string m_Ubicacion = "CLIENTE";
            int m_Ciclo = 1;
            int m_Folio = 368459;

            DateTime fecha = DateTime.Now;
            CultureInfo ci = CultureInfo.InvariantCulture;

            string hora = String.Format(fecha.ToString("HH:mm:ss.ff", ci));
            var zona = String.Format(DateTime.Now.ToString("%K"));
            string m_FechaHoraFin = (fecha.Year + "-" + fecha.Month + "-" + fecha.Day) + ("T" + hora + zona);
            string FechaInicio = (fecha.Year + "-" + fecha.Month + "-" + fecha.Day) + ("T" + hora + zona);//



            //try
            //{
                string json = @"{ m_IdEstacion: " + m_IdEstacion + ", m_Ubicacion: '" + m_Ubicacion +"',m_Folio:" + m_Folio + ",m_FechaHoraInicio:'" + FechaInicio + "',m_FechaHoraFin:'" + m_FechaHoraFin +
    "',   m_DenominacionContenedor:  {'1':" + "{'1000':" + contenidoF + ",'500':" + contenidoE +
",'200':" + contenidoD + ",'100':" + contenidoC + ",'50':" + contenidoB + ",'20':" +
contenidoA + "} } }";

            JObject jobj = JObject.Parse(json);
            //   MessageBox.Show(o.ToString());
           

            var texto = jobj;
           // MessageBox.Show(texto+"");
            System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:transaccion.json");
            file.WriteLine(texto);
            file.Close();


            /*
                JObject jobj = JObject.Parse(json);
                string cadena = jobj.ToString();
                var encrypt = tlockCajeros.codificaMensajes.Codificar(cadena);

                //Parametros del POST 
                //   string url = "http://187.174.220.229/presol/publico/pd.aspx?IdEstacion=2001&IdMensaje=233495&IdCategoriaMensaje=1&IdTipoMensaje=1000&VersionProtocolo=2&c=" + encrypt;
                string url = "http://187.174.220.229/presol/publico/pd.aspx?";
                String paramsPost = encrypt;

                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.ContentType = "application/x-www-form-urlencoded";
                httpRequest.Method = "POST";
                // Cambiamos la version de peticion por HTTP 1,0 
                httpRequest.ProtocolVersion = new Version(1, 0);
                httpRequest.ContentLength = paramsPost.Length;
                Stream stream = httpRequest.GetRequestStream();
                stream.Write(Encoding.ASCII.GetBytes(paramsPost), 0, paramsPost.Length);
                stream.Flush();
                stream.Close();

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                var streamReader = new StreamReader(httpResponse.GetResponseStream());
                String resultHtml = streamReader.ReadToEnd();

                MessageBox.Show(resultHtml);
                DateTime namefile = DateTime.Now;
                string m_archivo = namefile.Day.ToString() + "-" + namefile.Month.ToString() + "-" + namefile.Year.ToString() + "h" + namefile.Hour.ToString() + "m" + namefile.Minute.ToString() + "s" + namefile.Second.ToString() + ".json";
                var texto = jobj;
                StreamWriter file = new StreamWriter(@"C:\Directorio SICE\JSONS_N\" + m_archivo);
                StreamWriter file2 = new StreamWriter(@"C:\Directorio SICE\encript.txt");

                file.WriteLine(texto);
                file.Close();

                file2.WriteLine(encrypt + resultHtml);
                file2.Close();
                */

            //}
            //catch (Exception e)
            //{
            //    MessageBox.Show(e + "Servidor no  disponible  ");
            //}

        }
        public void ContinuarDeposito()
        {
            MaterialSkin.Operaciones.AuthenticationType TCR = new MaterialSkin.Operaciones.AuthenticationType();
            MaterialSkin.Operaciones.DataResponse TCR1 = new MaterialSkin.Operaciones.DataResponse();
            MaterialSkin.Operaciones.WebTellerClient TCR2 = new MaterialSkin.Operaciones.WebTellerClient();
            TCR.sLogin = txtUser.Text;
            TCR.sPassword = txtPass.Text;
            // TCR.sTerminal = txtTerminal.Text;


            string json = TCR2.ContinueDeposit(TCR).RetData;
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void materialSingleLineTextField1_Click(object sender, EventArgs e)
        {

        }


        public bool Validartxt()
        {
            bool error = true;
            if (txtUser.Text == string.Empty)
            {
                IconError.SetError(txtUser, "Ingresa Usuario");
                error = false;
            }
            else
            {
                try
                {
                    int i = Convert.ToInt32(txtPass.Text);

                }
                catch //(Exception)
                {
                    IconError.Clear();
                    IconError.SetError(txtPass, "Error de Contraseña");
                    return false;
                }

            }
            return error;
        }

        //Comparacion 
        public static string Codigo = "";


        private void materialSingleLineTextField1_Click_1(object sender, EventArgs e)
        {

        }

        private void materialLabel2_Click(object sender, EventArgs e)
        {

        }

        private void Closed_Form(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            this.Dispose();

        }

        private void PressEsc(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Dispose();
            }
        }

        private void Escape(object sender, KeyPressEventArgs es)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
        }



        public void Journal_Login()
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
            string fechaLogin = (DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString());

            string usuario = txtUser.Text;

            if (usuario == string.Empty)
            {
                usuario = "Null";
            }

            int menor = 4;
            if (txtUser.Text.Length < menor)
            {
                w.WriteLine("Error no ingreso numero de cuenta completa  ");
            }
            else
                if (txtUser.Text.Length > menor)
            {
                string cadena = usuario.Substring(0, 4) + "****";
                w.WriteLine("Usuario \t" + cadena);
            }

            w.WriteLine("--------------------------------------------------------------------------------------------------------------------------------");
            w.WriteLine("\n");
            w.WriteLine("\t\t\t\t\t\t S I C E");
            w.WriteLine("\n");
            w.WriteLine("\t\t\t\t SISTEMA INTEGRAL DE CONTROL DE EFECTIVO");
            w.WriteLine("\n");
            w.WriteLine("---------------------------------------------------Botón de inicio de Sesión----------------------------------------------------");
            w.WriteLine(fechaLogin);
            w.WriteLine("Inicio de Secion Boton Login");
        }


        public void Directorio()
        {
            //  creando carpeta en el disco C\SICE
            string path = @"C:\Directorio SICE";
            string path_D = @"C:\Directorio SICE\JSONS_D";
            string path_N = @"C:\Directorio SICE\JSONS_N";
            string path_R = @"C:\Directorio SICE\JSONS_R";
            string path_file = @"C:\Directorio SICE\Reportes tlock";
            string path_journal = @"C:\Directorio SICE\Journals";
            string path_config = @"C:\Directorio SICE\config";


            try
            {
                //Deternimar si el directorio existe 
                if (Directory.Exists(path) && Directory.Exists(path_D) && Directory.Exists(path_N) && Directory.Exists(path_R) && Directory.Exists(path_file) && Directory.Exists(path_journal)&& Directory.Exists(path_config))
                {
                    //  Console.WriteLine("That path exists already.");
                    return;
                }

                // Try crea el directorio.
                DirectoryInfo di = Directory.CreateDirectory(path);
                DirectoryInfo di_d = Directory.CreateDirectory(path_D);
                DirectoryInfo di_N = Directory.CreateDirectory(path_N);
                DirectoryInfo di_r = Directory.CreateDirectory(path_R);
                DirectoryInfo di_f = Directory.CreateDirectory(path_file);
                DirectoryInfo di_j = Directory.CreateDirectory(path_journal);
                DirectoryInfo di_confi = Directory.CreateDirectory(path_config);
                //Directorio creado exitosamente
            }
            catch (Exception e)
            {
                //  MessageBox.Show("The process failed: {0}", e.ToString());
            }
            finally { }
        }


        private void pictureBox2_Click(object sender, EventArgs e)
        {
            string logo = Application.StartupPath + @"\logo.jpg";

            pictureBox2.Image = Image.FromFile(logo);
        }

        private void Footer_TextChanged(object sender, EventArgs e)
        {

        }

        private void iTalk_HeaderLabel1_Click(object sender, EventArgs e)
        {

        }
        public void btnLogin()
        {
            Teller();

            try
            {
                string CMD = string.Format("select * from Usuarios WHERE No_cuenta='{0}' AND contraseña ='{1}'", txtUsuario.Text.Trim(), txtContraseña.Text.Trim());
                DataSet ds = Querys.utilidades.Ejecutar(CMD);
                string cuenta = ds.Tables[0].Rows[0]["No_cuenta"].ToString().Trim();
                string contra = ds.Tables[0].Rows[0]["contraseña"].ToString().Trim();

                if (cuenta == txtUsuario.Text.Trim() && contra == txtContraseña.Text.Trim())
                {
                    if (Convert.ToBoolean(ds.Tables[0].Rows[0]["Estado"]) == true)//Administrador
                    {
                        Admin admin = new Admin();
                        admin.lblCuentaAdmin.Text = txtUsuario.Text;
                        this.Hide();//hide esconder
                        admin.Show();
                    }
                    else //Usuario
                    {
                        Deposito deposito = new Deposito();
                        deposito.lblDCuenta.Text = txtUsuario.Text;
                        this.Hide();
                        deposito.Show();
                        //Home home = new Home();
                        //home.lblHCuenta.Text = txtUser.Text;
                        //this.Hide();
                        //home.Show();
                    }
                }

            }
            catch
          (Exception er)
            {
                msg();
            }

            if (Validartxt())
            {
                IconError.Clear();
            }
            //Agregando el Jounal 
            Journal_Login();

            //   cPro.stopProgress();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            btnLogin();
        }

        public void msg()
        {
            txtUser.Text = "";
            txtPass.Text = "";

            string result = MyMessageBox.ShowBox("Usuario y Contraseña Incorrectos ", "Mensaje");
        }





        private void iTalk_Label1_Click(object sender, EventArgs e)
        {

        }
        public bool T = false;
        private void txtUser_Click_1(object sender, EventArgs e)
        {
            //tclNumerico.Visible = true;
          //  T = false;

            Teller();

        }

        private void btn1_Click(object sender, EventArgs e)
        {
            //if (T == false)
            //{
            //    txtUser.Text = txtUser.Text + "1";
            //}
            //else
            //{
            //    txtPass.Text = txtPass.Text + "1";
            //}


            //Ingreso de Valor 1
            if (txtUsuario.TextLength == txtUsuario.MaxLength)
            {
                Valor = false;
                txtContraseña.Text = txtContraseña.Text + "1";
            }
            else
            {
                txtUsuario.Text = txtUsuario.Text + "1";
            }
            //Finaliza ingreso

        }

        private void txtPass_Click(object sender, EventArgs e)
        {
            tclNumerico.Visible = true;
            T = true;
            Teller();

        }

        private void btn2_Click(object sender, EventArgs e)
        {
            //if (T == false)
            //{
            //    txtUser.Text = txtUser.Text + "2";
            //}
            //else
            //{
            //    txtPass.Text = txtPass.Text + "2";
            //}


            //Ingreso de Valor 2
            if (txtUsuario.TextLength == txtUsuario.MaxLength)
            {
                Valor = false;
                txtContraseña.Text = txtContraseña.Text + "2";
            }
            else
            {
                txtUsuario.Text = txtUsuario.Text + "2";
            }
            //Finaliza ingreso
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            //if (T == false)
            //{
            //    txtUser.Text = txtUser.Text + "3";
            //}
            //else
            //{
            //    txtPass.Text = txtPass.Text + "3";
            //}


            //Ingreso de Valor 3
            if (txtUsuario.TextLength == txtUsuario.MaxLength)
            {
                Valor = false;
                txtContraseña.Text = txtContraseña.Text + "3";
            }
            else
            {
                txtUsuario.Text = txtUsuario.Text + "3";
            }
            //Finaliza ingreso
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            //if (T == false)
            //{
            //    txtUser.Text = txtUser.Text + "4";
            //}
            //else
            //{
            //    txtPass.Text = txtPass.Text + "4";
            //}


            //Ingreso de Valor 4
            if (txtUsuario.TextLength == txtUsuario.MaxLength)
            {
                Valor = false;
                txtContraseña.Text = txtContraseña.Text + "4";
            }
            else
            {
                txtUsuario.Text = txtUsuario.Text + "4";
            }
            //Finaliza ingreso
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            //if (T == false)
            //{
            //    txtUser.Text = txtUser.Text + "5";
            //}
            //else
            //{
            //    txtPass.Text = txtPass.Text + "5";
            //}

            //Ingreso de Valor 5
            if (txtUsuario.TextLength == txtUsuario.MaxLength)
            {
                Valor = false;
                txtContraseña.Text = txtContraseña.Text + "5";
            }
            else
            {
                txtUsuario.Text = txtUsuario.Text + "5";
            }
            //Finaliza ingreso
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            //if (T == false)
            //{
            //    txtUser.Text = txtUser.Text + "6";
            //}
            //else
            //{
            //    txtPass.Text = txtPass.Text + "6";
            //}


            //Ingreso de Valor 6
            if (txtUsuario.TextLength == txtUsuario.MaxLength)
            {
                Valor = false;
                txtContraseña.Text = txtContraseña.Text + "6";
            }
            else
            {
                txtUsuario.Text = txtUsuario.Text + "6";
            }
            //Finaliza ingreso
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            //if (T == false)
            //{
            //    txtUser.Text = txtUser.Text + "7";
            //}
            //else
            //{
            //    txtPass.Text = txtPass.Text + "7";
            //}


            //Ingreso de Valor 7
            if (txtUsuario.TextLength == txtUsuario.MaxLength)
            {
                Valor = false;
                txtContraseña.Text = txtContraseña.Text + "7";
            }
            else
            {
                txtUsuario.Text = txtUsuario.Text + "7";
            }
            //Finaliza ingreso
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            //if (T == false)
            //{
            //    txtUser.Text = txtUser.Text + "8";
            //}
            //else
            //{
            //    txtPass.Text = txtPass.Text + "8";
            //}


            //Ingreso de Valor 8
            if (txtUsuario.TextLength == txtUsuario.MaxLength)
            {
                Valor = false;
                txtContraseña.Text = txtContraseña.Text + "8";
            }
            else
            {
                txtUsuario.Text = txtUsuario.Text + "8";
            }
            //Finaliza ingreso
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            //if (T == false)
            //{
            //    txtUser.Text = txtUser.Text + "9";
            //}
            //else
            //{
            //    txtPass.Text = txtPass.Text + "9";
            //}


            //Ingreso de Valor 9
            if (txtUsuario.TextLength == txtUsuario.MaxLength)
            {
                Valor = false;
                txtContraseña.Text = txtContraseña.Text + "9";
            }
            else
            {
                txtUsuario.Text = txtUsuario.Text + "9";
            }
            //Finaliza ingreso
        }

        private void btn0_Click(object sender, EventArgs e)
        {
            //if (T == false)
            //{
            //    txtUser.Text = txtUser.Text + "0";
            //}
            //else
            //{
            //    txtPass.Text = txtPass.Text + "0";
            //}


            //Ingreso de Valor 0
            if (txtUsuario.TextLength == txtUsuario.MaxLength)
            {
                Valor = false;
                txtContraseña.Text = txtContraseña.Text + "0";
            }
            else
            {
                txtUsuario.Text = txtUsuario.Text + "0";
            }
            //Finaliza ingreso
        }

        private void btnBorrarNum_Click(object sender, EventArgs e)
        {
            //if (T == false)
            //{
            //    if (txtUser.Text.Length == 0)
            //    {
            //        txtUser.Text = "";
            //    }
            //    else
            //    {
            //        txtUser.Text = txtUser.Text.Substring(0, txtUser.Text.Length - 1);

            //    }

            //}
            //else
            //{
            //    if (txtPass.Text.Length == 0)
            //    {
            //        txtPass.Text = "";
            //    }
            //    else
            //    {
            //        txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.Length - 1);

            //    }

            //}

            if (Valor == true)
            {
                if (txtUsuario.Text.Length == 0)
                {
                    txtUsuario.Text = "";
                }
                else
                {
                    txtUsuario.Text = txtUsuario.Text.Substring(0, txtUsuario.Text.Length - 1);
                    txtUsuario.BackColor = Color.White;
                }
            }
            else
            {
                if (txtContraseña.Text.Length == 0)
                {
                    txtContraseña.Text = "";
                }
                else
                {
                    txtContraseña.Text = txtContraseña.Text.Substring(0, txtContraseña.Text.Length - 1);
                }
            }
            //Fin de Borrado

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Teller();
            tclNumerico.Visible = false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {

        }


        private void myButton_MouseEnter(object sender, System.EventArgs e)
        {

            Cursor.Hide();
        }

        private void Mouse(object sender, MouseEventArgs e)
        {
            Cursor.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void txtUsuario_TextChanged(object sender, EventArgs e)
        {
            //Coloca txtUsuario en color blanco debido al borrado de caracter, ademas de borrar completamente la contraseña
            if (txtUsuario.TextLength == txtUsuario.MaxLength)
            {
                txtUsuario.BackColor = Color.AntiqueWhite;
            }
            else
            {
                txtContraseña.Text = "";
            }
        }

        private void txtUsuario_Enter(object sender, EventArgs e)
        {
            Valor = true; //Da valor verdadero para que el teclado escriba en txtUsuario
        }

        private void txtContraseña_Enter(object sender, EventArgs e)
        {
            if (txtUsuario.TextLength != txtUsuario.MaxLength)
            {
                string result = MyMessageBox.ShowBox("Número de Usuario Incompleto ", "Mensaje");
            }
            else
            {
                Valor = false;
            }
        }
    }
}
