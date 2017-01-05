using MySql.Data.MySqlClient;
using MySql.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using System.IO;

using MaterialSkin;
using MaterialSkin.Controls;
using System.Threading;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Drawing.Printing;
using Newtonsoft.Json.Linq;
using System.Net;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Collections;
using Spire.Pdf.Annotations;
using Spire.Pdf.Widget;
using Spire.Pdf;
using System.Data.SqlClient;
using DocumentFormat.OpenXml;
using System.Xml;

namespace MaterialSkinExample
{
    public partial class Admin : MaterialForm
    {
        byte state; // Variable que toma el valor en dado caso que sea elejido Administrador, este valor se guarda en la BD con 0 o 1
        private readonly MaterialSkinManager materialSkinManager;



        static string user = "TCR";
        static String Pasword = "12345";
        static string terminal = "123";
        static FullService.Autenticacao authData = new FullService.Autenticacao();
        static FullService.OperationsClient operaciones = new FullService.OperationsClient();
        static FullService.ConfiguracaoCassette casett = new FullService.ConfiguracaoCassette();
        static FullService.DadosCassete dados = new FullService.DadosCassete();
        static FullService.ContentClient[] contenido = new FullService.ContentClient[3];
        public static DateTime fecha = DateTime.Now;

        public static string m_archivo = fecha.Day.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Year.ToString() + ".journal";


        public Admin()
        {
            InitializeComponent();
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;
            materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey900, Primary.BlueGrey100, Primary.GRIS200, Accent.LightBlue200, TextShade.WHITE);


        }
        private void Admin_Load(object sender, EventArgs e)
        {
            MainForm m = new MainForm();
            m.txtUser.Text = lblCuentaAdmin.Text;
            //Maximizando Pantalla
            this.WindowState = FormWindowState.Maximized;
            lblLoad.Visible = false;

            lblLoad.Location = new Point(0, 39);
            lblLoad.Size = new Size(956, 448);

            ptr.Visible = false;

            // estadotcr();
            xml();
        }



        public void xml()
        {
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
                lblEstacion.Text = estacion;
            }

        }



        // Validar Campos de caja de texto
        MainForm mainform = new MainForm();

        public void insert()
        {


        }

        /// <summary>
        /// ////////////////////////////////////////
        /// 
        //  public void Journal_Login()
        //{
        //    //DateTime fecha = DateTime.Now;
        //    //string m_archivo = fecha.Day.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Year.ToString() + ".journal";
        //    //var archivo_Journal = (@"C:\Directorio SICE\" + m_archivo + "");

        //    //using (StreamWriter w = File.AppendText(archivo_Journal))
        //    //{
        //    //    Log("SICE", w);


        //    //}


        //}



        /////////////////////////////
        /// </summary>
        /// <returns></returns>
        public bool Validartxt()
        {

            DateTime fecha = DateTime.Now;
            string m_archivo = fecha.Day.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Year.ToString() + ".journal";
            var archivo_Journal = (@"C:\Directorio SICE\Journals\" + m_archivo + "");

            using (StreamWriter w = File.AppendText(archivo_Journal))
            {
                string user = txtUser.Text;
                string pass = txtPass.Text;
                string rpas = txtRepitepass.Text;
                bool error = true;
                if (txtUser.Text == string.Empty)
                {
                    mainform.IconError.SetError(txtUser, "Ingresa Usuario");
                    error = false;
                }
                else
                {
                    try
                    {
                        int i = Convert.ToInt32(txtPass.Text);
                        int j = Convert.ToInt32(txtRepitepass.Text);

                        if (txtPass.Text == txtRepitepass.Text)
                        {
                            MySqlConnection con = new MySqlConnection("Server=localhost; User=OKI; Password=OKI2016; database=tlock; port=3306;");
                            MySqlCommand cmd = new MySqlCommand();
                            con.Open();
                            cmd.Connection = con;
                            int caracter = 4;
                            cmd.CommandText = "Insert into  usuarios (No_cuenta,Contraseña,estado) VALUES ('" + user + "','" + pass + "'," + state + ")"; //Se agregan usuarios Estandar y Administradores
                            if (txtUser.Text.Length >= caracter)
                            {
                                cmd.ExecuteScalar();
                                //cmd.ExecuteReader();
                                con.Close();


                                string result = MyMessageBox.ShowBox("Usuario Agregado.", "Mensaje");


                                //MessageBox.Show("Usuario Agregado. ");
                                LogAddU("Usuario Agregado con éxtito." + txtUser.Text.Substring(0, 4) + "****", w);
                            }
                            else
                            {
                                LogAddU("Cuenta incorrecta al intentar Agregar usuario ", w);
                                //MessageBox.Show("Cuenta no valida. ");
                                string result = MyMessageBox.ShowBox("Cuenta Invalida.", "Mensaje");
                            }


                            txtUser.Clear();
                            txtPass.Clear();
                            txtRepitepass.Clear();
                            chkAdmin.Checked = false; //Limpia casilla para crear usuarios Administrador
                        }
                        else
                        {
                            //   MessageBox.Show("Las contraseñas no Coinciden. Inténtalo de nuevo. ");

                            string result = MyMessageBox.ShowBox("Las contraseñas no Coinciden. \n Inténtalo de nuevo.", "Mensaje");
                            LogAddU("Las contraseñas no Coinciden al intentar agregar nuevo usuario.", w);
                        }


                    }
                    catch //(Exception)
                    {
                        mainform.IconError.Clear();
                        mainform.IconError.SetError(txtPass, "Error de contraseña");
                        LogAddU("Error de contraseña al intentar agregar nuevo usuario.", w);

                        return false;
                    }

                }
                return error;
            } //fin log
        }

        //---------



        public bool Validartxt2()
        {
            bool error = true;

            if (txtCuenta.Text == string.Empty)
            {
                mainform.IconError.SetError(txtCuenta, "Ingresa cuenta");
                error = false;
            }
            else
            {
                mainform.IconError.Clear();
                mainform.IconError.SetError(txtCuenta, "Error de contraseña");

            }
            //  txtCuenta.Clear();



            return error;


        }

        //fin log

        //-----------------------------
        public void LogAddU(string msjadd, TextWriter w)
        {

            string fechaadduser = (DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString());

            w.WriteLine("--------------------------------------Estado Botón Agregar Nuevo usuario------------------------------------------------");

            w.WriteLine("\n");
            //w.WriteLine("\t\t S I C E");
            //w.WriteLine("\t\t SISTEMA INTEGRAL DE CONTROL DE EFECTIVO");
            w.WriteLine(fechaadduser);
            w.WriteLine(msjadd);



        }

        private void iTalk_Label1_Click(object sender, EventArgs e)
        {

        }
        //boton consulta

        private void btnConsulta_Click(object sender, EventArgs e)
        {
            /*string no_cuenta,
                 id_transaccion,
                 id_cajero,
                 MXN20, MXN50, MXN100, MXN200, MXN500, MXN1000,
                 totalBilletes,
                 total,
                 fecha_transaccion;

            DataTable dt = Querys.Buscar(txtCuenta.Text);//envio dato a buscar
                                                         //si encuentra el dato guardo los datos en las variables
            try
            {

                if (dt.Rows.Count > 0)
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        {
                            //   dt.Rows[dt.Rows.Count - 1][1] odt.Rows[0][1]

                            DataRow row = dt.Rows[i];

                            //guardo datos en variables
                            no_cuenta = Convert.ToString(row["no_cuenta"]);
                            id_transaccion = Convert.ToString(row["id_transaccion"]);
                            id_cajero = Convert.ToString(row["id_cajero"]);
                            MXN20 = Convert.ToString(row["MXN20"]);
                            MXN50 = Convert.ToString(row["MXN50"]);
                            MXN100 = Convert.ToString(row["MXN100"]);
                            MXN200 = Convert.ToString(row["MXN200"]);
                            MXN500 = Convert.ToString(row["MXN500"]);
                            MXN1000 = Convert.ToString(row["MXN1000"]);

                            totalBilletes = Convert.ToString(row["totalBilletes"]);
                            total = Convert.ToString(row["total"]);
                            fecha_transaccion = Convert.ToString(row["fecha_transaccion"]);
                            //ListViewItem lista = new ListViewItem(no_cuenta);
                            //lista.SubItems.Add(id_transaccion);
                            //lista.SubItems.Add(id_cajero);
                            //lista.SubItems.Add(MXN20);
                            //lista.SubItems.Add(MXN50);
                            //lista.SubItems.Add(MXN100);
                            //lista.SubItems.Add(MXN200);
                            //lista.SubItems.Add(MXN500);
                            //lista.SubItems.Add(MXN1000);
                            //lista.SubItems.Add(totalBilletes);
                            //lista.SubItems.Add(total);
                            //lista.SubItems.Add(fecha_transaccion);
                            mListView.Items.Add(lista);
                            MessageBox.Show(id_transaccion = Convert.ToString(row["id_transaccion"]));
                        }
                    }
                else
                    //  MetroMessageBox.Show(Owner,"No Existe","Registro",MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    MessageBox.Show("No Hay Valores Agregados");

            }
            catch (Exception ex)
            {

                MessageBox.Show("error en la consulta" + ex);
            }
           
           */

        }


        public void JournalConsultas()
        {
            DateTime fecha = DateTime.Now;

            string m_archivo = fecha.Day.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Year.ToString() + ".journal";
            var archivo_Journal = (@"C:\Directorio SICE\Journals\" + m_archivo + "");
            using (StreamWriter w = File.AppendText(archivo_Journal))
            {
                LogConsulta("SICE", w);
            }


        }

        public void LogConsulta(string logMessage, TextWriter w)
        {


            string EstadoFecha = (DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString());
            string Operacion = "Consulta";
            w.WriteLine("--------------------------------------Estado Consulta------------------------------------------------");

            w.WriteLine(EstadoFecha);
            w.WriteLine("Inicio la operacion de  \t" + Operacion);
            w.WriteLine("Consulta realizada por  Administrador \t");
            w.WriteLine("Consulto a ...\t");
            int menor = 4;
            if (txtCuenta.Text.Length < menor)
            {
                w.WriteLine("Error no ingreso numero de cuenta completa  ");
            }
            else
                if (txtCuenta.Text.Length > menor)
            {
                w.WriteLine(txtCuenta.Text.Substring(0, 4) + "****");
            }



            string no_cuenta,
                 id_transaccion,
                 id_cajero,
                 MXN20, MXN50, MXN100, MXN200, MXN500, MXN1000,
                 totalBilletes,
                 total,
                 fecha_transaccion;
            mListView.Items.Clear();/// limpia antes de la consulta

            DataTable dt = Querys.Buscar(txtCuenta.Text);//envio dato a buscar
                                                         //si encuentra el dato guardo los datos en las variables
            try
            {



                if (dt.Rows.Count > 0)
                {


                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        DataRow row = dt.Rows[i];

                        //guardo datos en variables
                        no_cuenta = Convert.ToString(row["no_cuenta"]);
                        id_transaccion = Convert.ToString(row["id_transaccion"]);
                        id_cajero = Convert.ToString(row["id_cajero"]);
                        MXN20 = Convert.ToString(row["MXN20"]);
                        MXN50 = Convert.ToString(row["MXN50"]);
                        MXN100 = Convert.ToString(row["MXN100"]);
                        MXN200 = Convert.ToString(row["MXN200"]);
                        MXN500 = Convert.ToString(row["MXN500"]);
                        MXN1000 = Convert.ToString(row["MXN1000"]);

                        totalBilletes = Convert.ToString(row["totalBilletes"]);
                        total = Convert.ToString(row["total"]);
                        fecha_transaccion = Convert.ToString(row["fecha_transaccion"]);
                        ListViewItem lista = new ListViewItem(no_cuenta);
                        lista.SubItems.Add(id_transaccion);
                        lista.SubItems.Add(id_cajero);
                        lista.SubItems.Add(MXN20);
                        lista.SubItems.Add(MXN50);
                        lista.SubItems.Add(MXN100);
                        lista.SubItems.Add(MXN200);
                        lista.SubItems.Add(MXN500);
                        lista.SubItems.Add(MXN1000);
                        lista.SubItems.Add(totalBilletes);
                        lista.SubItems.Add(total);
                        lista.SubItems.Add(fecha_transaccion);
                        mListView.Items.Add(lista);
                        //    MessageBox.Show(id_transaccion = Convert.ToString(row["id_transaccion"]));
                    }
                }

                else
                {

                    string result = MyMessageBox.ShowBox("La Cuenta es Incorrecta. ", "Mensaje");
                    //     string result = MyMessageBox.ShowBox("Numero de Cuenta Incorrecta.", "Mensaje");



                    //   MetroMessageBox.Show(Owner, "No Existe", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);




                    w.WriteLine("Error en la consulta Cuenta incorrecta ");
                    //  MessageBox.Show("Verifica la consulta");
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show("error en la consulta" + ex);
                w.WriteLine("error en la consulta........................");
                //  mListView.Items.Clear();
            }

        }
        private void Admin_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            this.Dispose();
            this.Close();
        }

        private void materialLabel1_Click(object sender, EventArgs e)
        {

        }





        private void btnSalir_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            this.Hide();//////////////////
            main.Show();
            journalSalirAdmin();
        }
        //journal salir de administrador 
        public void journalSalirAdmin()
        {
            DateTime fecha = DateTime.Now;

            string m_archivo = fecha.Day.ToString() + "-" + fecha.Month.ToString() + "-" + fecha.Year.ToString() + ".journal";
            var archivo_Journal = (@"C:\Directorio SICE\Journals\" + m_archivo + "");
            using (StreamWriter w = File.AppendText(archivo_Journal))
            {
                Logsalir("SICE", w);
            }


        }

        public void Logsalir(string logMessage, TextWriter w)
        {
            string EstadoFecha = (DateTime.Now.ToLongDateString() + DateTime.Now.ToLongTimeString());
            string Operacion = "Salir Vista del Administrador";
            w.WriteLine("--------------------------------------Estado Selección Botón Cerrar Sesión------------------------------------------------");

            w.WriteLine(EstadoFecha);
            w.WriteLine("Selección de opeación \t" + Operacion);


        }

        private void metroTabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {//btn Agregar usuarios
            if (Validartxt())
            {

                mainform.IconError.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            this.Hide();
            main.Show();
            journalSalirAdmin();
        }

        private void mListView_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {//btn consulta

            if (Validartxt2())
            {

                mainform.IconError.Clear();
            }



            JournalConsultas();


        }

        private void button4_Click(object sender, EventArgs e)
        {
            mListView.Items.Clear();
            tclNumerico.Visible = false;
        }
        // public bool Teclado = false;
        private void txtCuenta_Click(object sender, EventArgs e)
        {
            tclNumerico.Visible = true;
            //    Teclado = true;
        }

        private void btn1_Click(object sender, EventArgs e)
        {
            txtCuenta.Text = txtCuenta.Text + btn1.Text;
        }

        private void btn2_Click(object sender, EventArgs e)
        {
            txtCuenta.Text = txtCuenta.Text + btn2.Text;
        }

        private void btn3_Click(object sender, EventArgs e)
        {
            txtCuenta.Text = txtCuenta.Text + btn3.Text;
        }

        private void btn4_Click(object sender, EventArgs e)
        {
            txtCuenta.Text = txtCuenta.Text + btn4.Text;
        }

        private void btn5_Click(object sender, EventArgs e)
        {
            txtCuenta.Text = txtCuenta.Text + btn5.Text;
        }

        private void btn6_Click(object sender, EventArgs e)
        {
            txtCuenta.Text = txtCuenta.Text + btn6.Text;
        }

        private void btn7_Click(object sender, EventArgs e)
        {
            txtCuenta.Text = txtCuenta.Text + btn7.Text;
        }

        private void btn8_Click(object sender, EventArgs e)
        {
            txtCuenta.Text = txtCuenta.Text + btn8.Text;
        }

        private void btn9_Click(object sender, EventArgs e)
        {
            txtCuenta.Text = txtCuenta.Text + btn9.Text;

        }

        private void btn0_Click(object sender, EventArgs e)
        {
            txtCuenta.Text = txtCuenta.Text + btn0.Text;
        }

        private void btnBorrarNum_Click(object sender, EventArgs e)
        {

            if (txtCuenta.Text.Length == 0)
            {
                txtCuenta.Text = "";
            }
            else
            {
                txtCuenta.Text = txtCuenta.Text.Substring(0, txtCuenta.Text.Length - 1);

            }


        }

        private void btnAceptar_Click(object sender, EventArgs e)
        {

            if (txtCuenta.Text.Length == 0)
            {
                string result = MyMessageBox.ShowBox("Ingresa numero de Cuenta.", "Mensaje");
            }
            tclNumerico.Visible = false;
            



            if (Validartxt2())
            {

                mainform.IconError.Clear();
            }



            JournalConsultas();

           

        }

        //vista agregar usuarios

        public bool T1 = false;

        
        private void txtUser_Click(object sender, EventArgs e)
        {
            tclNumerico2.Visible = true;
            T1 = true;
            tclNumeric3.Visible = false;
            mListView.Items.Clear();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtUser.Text = txtUser.Text + "1";
            }
            else
            {
                txtPass.Text = txtPass.Text + "1";
            }

        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtUser.Text = txtUser.Text + "2";
            }
            else
            {
                txtPass.Text = txtPass.Text + "2";
            }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtUser.Text = txtUser.Text + "3";
            }
            else
            {
                txtPass.Text = txtPass.Text + "3";
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtUser.Text = txtUser.Text + "4";
            }
            else
            {
                txtPass.Text = txtPass.Text + "4";
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtUser.Text = txtUser.Text + "5";
            }
            else
            {
                txtPass.Text = txtPass.Text + "5";
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtUser.Text = txtUser.Text + "6";
            }
            else
            {
                txtPass.Text = txtPass.Text + "6";
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtUser.Text = txtUser.Text + "7";
            }
            else
            {
                txtPass.Text = txtPass.Text + "7";
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtUser.Text = txtUser.Text + "8";
            }
            else
            {
                txtPass.Text = txtPass.Text + "8";
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtUser.Text = txtUser.Text + "9";
            }
            else
            {
                txtPass.Text = txtPass.Text + "9";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                txtUser.Text = txtUser.Text + "0";
            }
            else
            {
                txtPass.Text = txtPass.Text + "0";
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (T1 == true)
            {
                if (txtUser.Text.Length == 0)
                {
                    txtUser.Text = "";
                }
                else
                {
                    txtUser.Text = txtUser.Text.Substring(0, txtUser.Text.Length - 1);

                }

            }
            else
            {
                if (txtPass.Text.Length == 0)
                {
                    txtPass.Text = "";
                }
                else
                {
                    txtPass.Text = txtPass.Text.Substring(0, txtPass.Text.Length - 1);

                }

            }
        }
        //Aceptar
        private void button5_Click(object sender, EventArgs e)
        {
            tclNumerico2.Visible = false;

        }

        private void txtPass_Click(object sender, EventArgs e)
        {
            tclNumerico2.Visible = true;
            //tclNumerico2.Visible = true;
            tclNumeric3.Visible = false;
            T1 = false;
        }
        //teclado 3
        private void txtRepitepass_Click(object sender, EventArgs e)
        {
            //  tclNumerico2.Visible = true;
            tclNumerico2.Visible = false;
            tclNumeric3.Visible = true;

        }

        private void button28_Click(object sender, EventArgs e)
        {
            txtRepitepass.Text = txtRepitepass.Text + "1";
        }

        private void button27_Click(object sender, EventArgs e)
        {
            txtRepitepass.Text = txtRepitepass.Text + "2";

        }

        private void button26_Click(object sender, EventArgs e)
        {
            txtRepitepass.Text = txtRepitepass.Text + "3";
        }

        private void button25_Click(object sender, EventArgs e)
        {
            txtRepitepass.Text = txtRepitepass.Text + "4";
        }

        private void button24_Click(object sender, EventArgs e)
        {
            txtRepitepass.Text = txtRepitepass.Text + "5";
        }

        private void button23_Click(object sender, EventArgs e)
        {
            txtRepitepass.Text = txtRepitepass.Text + "6";
        }

        private void button22_Click(object sender, EventArgs e)
        {
            txtRepitepass.Text = txtRepitepass.Text + "7";
        }

        private void button21_Click(object sender, EventArgs e)
        {
            txtRepitepass.Text = txtRepitepass.Text + "8";
        }

        private void button20_Click(object sender, EventArgs e)
        {
            txtRepitepass.Text = txtRepitepass.Text + "9";

        }

        private void button19_Click(object sender, EventArgs e)
        {
            txtRepitepass.Text = txtRepitepass.Text + "0";
        }

        private void button18_Click(object sender, EventArgs e)
        {

            if (txtRepitepass.Text.Length == 0)
            {
                txtRepitepass.Text = "";
            }
            else
            {
                txtRepitepass.Text = txtRepitepass.Text.Substring(0, txtRepitepass.Text.Length - 1);

            }


        }

        private void button17_Click(object sender, EventArgs e)
        {
            tclNumeric3.Visible = false;
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {
            MainForm main = new MainForm();
            this.Hide();
            main.Show();
            journalSalirAdmin();
        }

        private void button30_Click(object sender, EventArgs e)
        {
            button30.Visible = false;
            button29.Visible = false;
            lbl_etiqueta.Text = "Introduzca Contraseña para Confirmar Apagado";
            GrupoApagar.Visible = true;
            GrupoApagar.Location = new Point(235, 3);
            GrupoApagar.Size = new Size(537, 490);


            button33.Visible = true;
            btnReiniciar.Visible = false;

            btnReset.Visible = false;

        }

        private void button29_Click(object sender, EventArgs e)
        {
            button30.Visible = false;
            button29.Visible = false;
            lbl_etiqueta.Text = "Introduzca Contraseña para Confirmar Reinicio";
            GrupoApagar.Visible = true;
            GrupoApagar.Location = new Point(235, 3);
            GrupoApagar.Size = new Size(537, 490);

            button33.Visible = false;
            btnReiniciar.Visible = true;

            btnReset.Visible = false;



        }

        private void materialSingleLineTextField1_Click(object sender, EventArgs e)
        {
            TecladoAd.Visible = true;
        }

        private void metroTabPage3_Click(object sender, EventArgs e)
        {

        }

        private void button31_Click(object sender, EventArgs e)
        {//boton apagar -Aceptar-


        }



        private void button32_Click(object sender, EventArgs e)
        {
            button29.Visible = true;
            button30.Visible = true;
            GrupoApagar.Visible = false;
            btnReset.Visible = true;

        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtcontraseñaAd.Text))
            {
                string result = MyMessageBox.ShowBox("Debes Ingresar Contraseña.", "Mensaje");

            }
            else

                try
                {
                    string Consulta = string.Format("select * from Usuarios WHERE No_cuenta='{0}' AND contraseña ='{1}'", lblCuentaAdmin.Text.Trim(), txtcontraseñaAd.Text.Trim());

                    DataSet ds = BuscarAdministrador.Consultar(Consulta);

                    string Cuenta = ds.Tables[0].Rows[0]["No_cuenta"].ToString().Trim();
                    string Contraseña = ds.Tables[0].Rows[0]["contraseña"].ToString().Trim();

                    if (Cuenta == lblCuentaAdmin.Text.Trim() && Contraseña == txtcontraseñaAd.Text.Trim())
                    {
                        Process.Start("shutdown.exe", "-f");
                        string errorcontraseña = MyMessageBox.ShowBox(" El equipo se Apagara.", "Mensaje");
                    }

                }
                catch
              (Exception er)
                {

                    string errorcontraseña = MyMessageBox.ShowBox(" Contraseña Incorrecta.", "Mensaje");
                    txtcontraseñaAd.Clear();
                }


            // string errorcontraseña = MyMessageBox.ShowBox(" Contraseña Incorrecta.", "Mensaje");

            TecladoAd.Visible = false;
        }

        private void button44_Click(object sender, EventArgs e)
        {
            txtcontraseñaAd.Text = txtcontraseñaAd.Text + "1";
        }

        private void button43_Click(object sender, EventArgs e)
        {
            txtcontraseñaAd.Text = txtcontraseñaAd.Text + "2";
        }

        private void button42_Click(object sender, EventArgs e)
        {
            txtcontraseñaAd.Text = txtcontraseñaAd.Text + "3";
        }

        private void button41_Click(object sender, EventArgs e)
        {
            txtcontraseñaAd.Text = txtcontraseñaAd.Text + "4";
        }

        private void button40_Click(object sender, EventArgs e)
        {
            txtcontraseñaAd.Text = txtcontraseñaAd.Text + "5";
        }

        private void button39_Click(object sender, EventArgs e)
        {
            txtcontraseñaAd.Text = txtcontraseñaAd.Text + "6";
        }

        private void button38_Click(object sender, EventArgs e)
        {
            txtcontraseñaAd.Text = txtcontraseñaAd.Text + "7";
        }

        private void button37_Click(object sender, EventArgs e)
        {
            txtcontraseñaAd.Text = txtcontraseñaAd.Text + "8";
        }

        private void button36_Click(object sender, EventArgs e)
        {
            txtcontraseñaAd.Text = txtcontraseñaAd.Text + "9";
        }

        private void button35_Click(object sender, EventArgs e)
        {
            txtcontraseñaAd.Text = txtcontraseñaAd.Text + "0";
        }

        private void button34_Click(object sender, EventArgs e)
        {
            txtcontraseñaAd.Text = "";
        }

        private void GrupoApagar_Enter(object sender, EventArgs e)
        {

        }

        private void btnReiniciar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtcontraseñaAd.Text))
            {
                string result = MyMessageBox.ShowBox("Debes Ingresar Contraseña.", "Mensaje");

            }
            else

                try
                {
                    string Consulta = string.Format("select * from Usuarios WHERE No_cuenta='{0}' AND contraseña ='{1}'", lblCuentaAdmin.Text.Trim(), txtcontraseñaAd.Text.Trim());

                    DataSet ds = BuscarAdministrador.Consultar(Consulta);

                    string Cuenta = ds.Tables[0].Rows[0]["No_cuenta"].ToString().Trim();
                    string Contraseña = ds.Tables[0].Rows[0]["contraseña"].ToString().Trim();

                    if (Cuenta == lblCuentaAdmin.Text.Trim() && Contraseña == txtcontraseñaAd.Text.Trim())
                    {
                        Process.Start("shutdown.exe", "-r -t 00");
                        string errorcontraseña = MyMessageBox.ShowBox(" El equipo se reiniciara.", "Mensaje");
                    }

                }
                catch
              (Exception er)
                {

                    string errorcontraseña = MyMessageBox.ShowBox(" Contraseña Incorrecta.", "Mensaje");
                    txtcontraseñaAd.Clear();
                }



            TecladoAd.Visible = false;

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            // Se valido el estado de chado de la casilla para crear un usuario Estandar y Administrador
            if (chkAdmin.Checked == true)
            {
                state = 1;//Para crear usuario Administrador
            }
            else
            {
                state = 0;//Para crear usuario Estandar
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void button31_Click_1(object sender, EventArgs e)
        {
            try
            {
                authData.Usuario = user;
                authData.Terminal = terminal;
                authData.Senha = Pasword;
               // string mensaje = operaciones.ResetTCR(authData).CodigoResposta;
         operaciones.ResetTCR(authData) ;

                //   richTextBox1.Text = (operaciones.AbastecimentoEncerrar(authData).ExtensionData).ToString();
                //            lblMensaje.Text = TCR2.ContinueDeposit(TCR).RetData;
                //  lblMensaje.Text = TCR2.StartDeposit(TCR).Message;
          MyMessageBox.ShowBox("Operación ejecutada con éxito");
            }
            catch (Exception)
            {

                MyMessageBox.ShowBox("Intenta de nuevo ");
            }
       // }
        }




        public void estadotcr()
        {
            try
            {
                FullService.EstadoCassette estadocasett = new FullService.EstadoCassette();
                FullService.DadosConteudoTCR tcr1 = new FullService.DadosConteudoTCR();
                FullService.ContentClient tcr2 = new FullService.ContentClient();

                FullService.DadosCassete tcr3 = new FullService.DadosCassete();
                authData.Usuario = user;
                authData.Terminal = terminal;
                authData.Senha = Pasword;

                //Caset A
                lblidA.Text = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[0].Id;
                int denomA = Convert.ToInt32(tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[0].Denominacao);
                lbldenomA.Text = "$ " + denomA.ToString();
                int contadorA = Convert.ToInt32(tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[0].Contador);
                lblcantA.Text = contadorA.ToString();
                int totalA = denomA * contadorA;
                lbltotalA.Text = "$ " + totalA;


                //caset B
                lblidB.Text = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[1].Id;
                int denomB = Convert.ToInt32(tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[1].Denominacao);
                lbldenomB.Text = "$ " + denomB.ToString();
                int contadorB = Convert.ToInt32(tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[1].Contador);
                lblcantB.Text = contadorB.ToString();
                int totalB = denomB * contadorB;
                lbltotalB.Text = "$ " + totalB;

                //caset C
                lblidC.Text = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[2].Id;
                int denomC = Convert.ToInt32(tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[2].Denominacao);
                lbldenomC.Text = "$ " + denomC.ToString();
                int contadorC = Convert.ToInt32(tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[2].Contador);
                lblcantC.Text = contadorC.ToString();
                int totalC = denomC * contadorC;
                lbltotalC.Text = "$ " + totalC;


                //caset D
                lblidD.Text = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[3].Id;
                int denomD = Convert.ToInt32(tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[3].Denominacao);
                lbldenomD.Text = "$ " + denomD.ToString();
                int contadorD = Convert.ToInt32(tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[3].Contador);
                lblcantD.Text = contadorD.ToString();
                int totalD = denomD * contadorD;
                lbltotalD.Text = "$ " + totalD;


                //caset E
                lblidE.Text = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[4].Id;
                int denomE = Convert.ToInt32(tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[4].Denominacao);
                lbldenomE.Text = "$ " + denomE.ToString();
                int contadorE = Convert.ToInt32(tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[4].Contador);
                lblcantE.Text = contadorE.ToString();
                int totalE = denomE * contadorE;
                lbltotalE.Text = "$ " + totalE;


                //caset F
                lblidF.Text = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[5].Id;
                int denomF = Convert.ToInt32(tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[5].Denominacao);
                // lbldenomF.Text = "$ " + denomF.ToString();// original
                lbldenomF.Text = "$ 0";
                int contadorF = Convert.ToInt32(tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[5].Contador);
                lblcantF.Text = contadorF.ToString();
                int totalF = denomF * contadorF;
                lbltotalF.Text = "$ " + totalF;

                lbltotalcontenido.Text = "Contenido: $ " + (totalA + totalB +
                    totalC + totalD + totalE + totalF  ).ToString("#,#", CultureInfo.InvariantCulture);

                lblT_Billetes.Text = "Total de Billetes:  " + (contadorA + contadorB + contadorC + contadorD + contadorE + contadorF);

                lblLoad.Visible = false;
                chcContenido.Visible = true;
                /*
                // tcr1.InformacoesCassetes;
                var x = tcr2.ConsultarConteudoTCR(authData).CodigoResposta;

                string casetA = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[0].Posicao;//A
                string casetB = tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[1].Posicao;//B

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
                */
                //  MessageBox.Show("El TCR contien $" + estadotcr + "total");

            }
            catch (Exception ex)
            {

                MyMessageBox.ShowBox("Intenta de nuevo ");
                lblLoad.Visible = false;
                chcContenido.Visible = true;
            }
        }

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (metroTabControl1.SelectedTab == metroTabControl1.TabPages["contenidoTCR"])//Nombre del Tab
            {
                mListView.Items.Clear();

                var archivo_Journal = (@"C:\Directorio SICE\Journals\" + m_archivo + "");

                using (StreamWriter w = File.AppendText(archivo_Journal))
                {
                    w.WriteLine("--------------------------------------Selección barra del contenido Estado TCR ------------------------------------------------");
                    w.WriteLine("\n");
                    w.WriteLine("Selección realizada por usuario Administrador: " + lblCuentaAdmin.Text);
                    w.WriteLine("Hora de selección:   " + fecha);
                }
            }

            if (metroTabControl1.SelectedTab == metroTabControl1.TabPages["Herramientas"])//Nombre del Tab
            {
                mListView.Items.Clear();
                var archivo_Journal = (@"C:\Directorio SICE\Journals\" + m_archivo + "");

                using (StreamWriter w = File.AppendText(archivo_Journal))
                {
                    w.WriteLine("--------------------------------------Selección  Herramientas ------------------------------------------------");
                    w.WriteLine("\n");
                    w.WriteLine("Selección realizada por usuario Administrador: " + lblCuentaAdmin.Text);
                    w.WriteLine("Hora de selección:   " + fecha);
                }// 
            }

            if (metroTabControl1.SelectedTab == metroTabControl1.TabPages["Consultas"])//Nombre del Tab
            {
              //  mListView.Items.Clear();
                var archivo_Journal = (@"C:\Directorio SICE\Journals\" + m_archivo + "");

                using (StreamWriter w = File.AppendText(archivo_Journal))
                {
                    w.WriteLine("--------------------------------------Selección Consultas ------------------------------------------------");
                    w.WriteLine("\n");
                    w.WriteLine("Selección realizada por usuario Administrador: " + lblCuentaAdmin.Text);
                    w.WriteLine("Hora de selección:   " + fecha);
                }
            }

            if (metroTabControl1.SelectedTab == metroTabControl1.TabPages["Add"])//Nombre del Tab
            {
                mListView.Items.Clear();
                var archivo_Journal = (@"C:\Directorio SICE\Journals\" + m_archivo + "");

                using (StreamWriter w = File.AppendText(archivo_Journal))
                {
                    w.WriteLine("--------------------------------------Selección barra Ahgregar Usuarios ------------------------------------------------");
                    w.WriteLine("\n");
                    w.WriteLine("Selección realizada por usuario Administrador: " + lblCuentaAdmin.Text);
                    w.WriteLine("Hora de selección:   " + fecha);
                }
            }




        }

        private void materialCheckBox1_CheckedChanged(object sender, EventArgs e)
        {
             
                if (chcContenido.Checked)

                {
                    var archivo_Journal = (@"C:\Directorio SICE\Journals\" + m_archivo + "");
                    using (StreamWriter w = File.AppendText(archivo_Journal))
                    {
                        w.WriteLine("-------------------------------------- CheckBox checked------------------------------------------------");
                        w.WriteLine("\n");
                        w.WriteLine("Selección realizada por usuario Administrador: " + lblCuentaAdmin.Text);
                        w.WriteLine("Hora de selección:   " + fecha);
                    }
                    ptr.Visible = true;
                    lblLoad.Visible = true;
                    chcContenido.Visible = false;
                    //groupBox2.Visible = true;
                    Thread th = new Thread(new ThreadStart(estadotcr));
                    th.Start();
                    th.Join();

                }
             
            

        }

        private void contenidoTCR_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {
            if (lblidA.Text == string.Empty)
            {
                chcContenido.Checked = true;
                //  MyMessageBox.ShowBox(no_cuenta );
            }
            else
            {
                btnprintTicket();
            }



        }

        public void btnprintTicket()
        {
            PrintDocument pd = new PrintDocument();
            pd.PrintPage += new PrintPageEventHandler(pd_PrintPage);
            pd.PrintController = new StandardPrintController();
            pd.Print();
        }
        //Ticket de contenido TCR

        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {

            Graphics g = e.Graphics;  //Uso de graficos para impresión de ticket
            string logo = Application.StartupPath + "\\logo.jpg";
            System.Drawing.Font f = new System.Drawing.Font("Arial Narrow", 10, FontStyle.Regular); //Estilo utilizado en ticket
            SolidBrush b = new SolidBrush(Color.Black);  //Color de texto
            g.DrawImage(System.Drawing.Image.FromFile(logo), 130, -5, 160, 90);
            g.DrawString("Impresión de contenido TCR   ", f, b, 10, 80);
            g.DrawString("Administrador: "+lblCuentaAdmin.Text, f, b, 10, 95);

            g.DrawString("Fecha: " + DateTime.Now.ToString(), f, b, 10, 110);

            g.DrawString("------------------ CASSETTE A------------------", f, b, 10, 125);
            g.DrawString("Moneda: MXN", f, b, 10, +140);
            g.DrawString("ID: " + lblidA.Text, f, b, 10, 155);
            g.DrawString("Denominación: " + lbldenomA.Text, f, b, 10, 170);
            g.DrawString("Cantidad: " + lblcantA.Text, f, b, 10, 185);
            g.DrawString("Total: " + lbltotalA.Text, f, b, 10, 200);

            g.DrawString("------------------ CASSETTE B------------------", f, b, 10, 215);
            g.DrawString("Moneda: MXN", f, b, 10, +230);
            g.DrawString("ID: " + lblidB.Text, f, b, 10, 245);
            g.DrawString("Denominación: " + lbldenomB.Text, f, b, 10, 260);
            g.DrawString("Cantidad: " + lblcantB.Text, f, b, 10, 275);
            g.DrawString("Total: " + lbltotalB.Text, f, b, 10, 290);

            g.DrawString("------------------ CASSETTE C------------------", f, b, 10, 305);
            g.DrawString("Moneda: MXN", f, b, 10, +320);
            g.DrawString("ID: " + lblidC.Text, f, b, 10, 335);
            g.DrawString("Denominación: " + lbldenomC.Text, f, b, 10, 350);
            g.DrawString("Cantidad: " + lblcantC.Text, f, b, 10, 365);
            g.DrawString("Total: " + lbltotalC.Text, f, b, 10, 380);

            g.DrawString("------------------ CASSETTE D------------------", f, b, 10, 395);
            g.DrawString("Moneda: MXN", f, b, 10, +410);
            g.DrawString("ID: " + lblidD.Text, f, b, 10, 425);
            g.DrawString("Denominación: " + lbldenomD.Text, f, b, 10, 440);
            g.DrawString("Cantidad: " + lblcantD.Text, f, b, 10, 455);
            g.DrawString("Total: " + lbltotalD.Text, f, b, 10, 470);
            g.DrawString("------------------ CASSETTE E------------------", f, b, 10, 485);
            g.DrawString("Moneda: MXN", f, b, 10, +500);
            g.DrawString("ID: " + lblidE.Text, f, b, 10, 515);
            g.DrawString("Denominación: " + lbldenomE.Text, f, b, 10, 530);
            g.DrawString("Cantidad: " + lblcantE.Text, f, b, 10, 545);
            g.DrawString("Total: " + lbltotalE.Text, f, b, 10, 560);
            g.DrawString("------------------ CASSETTE F------------------", f, b, 10, 575);
            g.DrawString("Moneda: MXN", f, b, 10, +590);
            g.DrawString("ID: " + lblidF.Text, f, b, 10, 605);
            g.DrawString("Denominación: " + lbldenomF.Text, f, b, 10, 620);
            g.DrawString("Cantidad: " + lblcantF.Text, f, b, 10, 635);
            g.DrawString("Total: " + lbltotalF.Text, f, b, 10, 650);
            g.DrawString(lblT_Billetes.Text, f, b, 10, 665);
            //  g.DrawString("", f, b, 10, 890);

            var archivo_Journal = (@"C:\Directorio SICE\Journals\" + m_archivo + "");

            using (StreamWriter w = File.AppendText(archivo_Journal))
            {
                w.WriteLine("--------------------------------------Impresión de contenido TCR  TICKET ------------------------------------------------");
                w.WriteLine("\n");
                w.WriteLine("Selección realizada por usuario Administrador: " + lblCuentaAdmin.Text);
                w.WriteLine("Hora de selección:   " + fecha);


                w.WriteLine("Fecha: " + DateTime.Now.ToString(), f, b, 10, 110);

                w.WriteLine("------------------ CASSETTE A------------------", f, b, 10, 125);
                w.WriteLine("Moneda: MXN", f, b, 10, +140);
                w.WriteLine("ID: " + lblidA.Text, f, b, 10, 155);
                w.WriteLine("Denominación: " + lbldenomA.Text, f, b, 10, 170);
                w.WriteLine("Cantidad: " + lblcantA.Text, f, b, 10, 185);
                w.WriteLine("Total: " + lbltotalA.Text, f, b, 10, 200);

                w.WriteLine("------------------ CASSETTE B------------------", f, b, 10, 215);
                w.WriteLine("Moneda: MXN", f, b, 10, +230);
                w.WriteLine("ID: " + lblidB.Text, f, b, 10, 245);
                w.WriteLine("Denominación: " + lbldenomB.Text, f, b, 10, 260);
                w.WriteLine("Cantidad: " + lblcantB.Text, f, b, 10, 275);
                w.WriteLine("Total: " + lbltotalB.Text, f, b, 10, 290);
                //@"C:\Directorio SICE\journals\"
                w.WriteLine("------------------ CASSETTE C------------------", f, b, 10, 305);
                w.WriteLine("Moneda: MXN", f, b, 10, +320);
                w.WriteLine("ID: " + lblidC.Text, f, b, 10, 335);
                w.WriteLine("Denominación: " + lbldenomC.Text, f, b, 10, 350);
                w.WriteLine("Cantidad: " + lblcantC.Text, f, b, 10, 365);
                w.WriteLine("Total: " + lbltotalC.Text, f, b, 10, 380);

                w.WriteLine("------------------ CASSETTE D------------------", f, b, 10, 395);
                w.WriteLine("Moneda: MXN", f, b, 10, +410);
                w.WriteLine("ID: " + lblidD.Text, f, b, 10, 425);
                w.WriteLine("Denominación: " + lbldenomD.Text, f, b, 10, 440);
                w.WriteLine("Cantidad: " + lblcantD.Text, f, b, 10, 455);
                w.WriteLine("Total: " + lbltotalD.Text, f, b, 10, 470);
                w.WriteLine("------------------ CASSETTE E------------------", f, b, 10, 485);
                w.WriteLine("Moneda: MXN", f, b, 10, +500);
                w.WriteLine("ID: " + lblidE.Text, f, b, 10, 515);
                w.WriteLine("Denominación: " + lbldenomE.Text, f, b, 10, 530);
                w.WriteLine("Cantidad: " + lblcantE.Text, f, b, 10, 545);
                w.WriteLine("Total: " + lbltotalE.Text, f, b, 10, 560);
                w.WriteLine("------------------ CASSETTE F------------------", f, b, 10, 575);
                w.WriteLine("Moneda: MXN", f, b, 10, +590);
                w.WriteLine("ID: " + lblidF.Text, f, b, 10, 605);
                w.WriteLine("Denominación: " + lbldenomF.Text, f, b, 10, 620);
                w.WriteLine("Cantidad: " + lblcantF.Text, f, b, 10, 635);
                RectangleF rectF1 = new RectangleF(30, 10, 100, 122);
                w.WriteLine("Total: " + lbltotalF.Text, f, b, 10, 650, rectF1);
                w.WriteLine(lblT_Billetes.Text, f, b, 10, 665);


                //  e.Graphics.DrawString(text1, font1, Brushes.Blue, rectF1);




            }

        }

        private void button46_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtCuenta.Text==String.Empty)
                {
                    MyMessageBox.ShowBox("Ingresa un numero de cuenta ");
                }
                else
                {
                    MySqlConnection sqlConnection = new MySqlConnection("Server=localhost; User=OKI; Password=OKI2016; database=tlock; port=3306;");
                    MySqlCommand cmd = new MySqlCommand();
                    sqlConnection.Open();
                    cmd.Connection = sqlConnection;
                    cmd.CommandType = CommandType.Text;
                    DateTime fecha = DateTime.Now;
                    string f1 = dtp1.Value.ToString("yyyy-MM-dd");
                    string f2 = dtp2.Value.ToString("yyyy-MM-dd");

                    mListView.Items.Clear();//Limpia antes de la consulta

                    cmd.CommandText = (" SELECT * FROM  transaccion WHERE  fecha_Transaccion >= '" + f1 + "%' AND fecha_Transaccion <= '" + f2 + "%' and no_cuenta= '" + txtCuenta.Text + "'");
                    MySqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {  while (dr.Read())
                    {
                        ListViewItem lista = new ListViewItem(dr["no_cuenta"].ToString());
                        lista.SubItems.Add(dr["id_transaccion"].ToString());
                        lista.SubItems.Add(dr["id_cajero"].ToString());
                        lista.SubItems.Add(dr["MXN20"].ToString());
                        lista.SubItems.Add(dr["MXN50"].ToString());
                        lista.SubItems.Add(dr["MXN100"].ToString());
                        lista.SubItems.Add(dr["MXN200"].ToString());
                        lista.SubItems.Add(dr["MXN500"].ToString());
                        lista.SubItems.Add(dr["MXN1000"].ToString());
                        lista.SubItems.Add(dr["totalBilletes"].ToString());
                        lista.SubItems.Add(dr["total"].ToString());
                        lista.SubItems.Add(dr["fecha_transaccion"].ToString());
                        mListView.Items.Add(lista);

                    }
                    }
                    else
                    {
                        
                        MyMessageBox.ShowBox("No se econtro registros en las fechas");
                        dtp1.CalendarMonthBackground = Color.Azure;
                        dtp2.CalendarMonthBackground = Color.Azure;
                    }
                    tclNumerico.Visible = false;

                    sqlConnection.Close();
                }
            }
            catch (Exception ex)
            {

                MyMessageBox.ShowBox("Error de comunicacion con BD"+ex.Message);
            }

            //if (mListView.SelectedItems!= null)
            //{

            //}

            tclNumerico.Visible = false;

        }
       
        private void button47_Click(object sender, EventArgs e)
        {

            //for (int i = 0; i < mListView.Items.Count; i++)
            //{
            //}
            if (mListView.Items.Count == 0)
            {

                MyMessageBox.ShowBox("No hay registros en la tabla");

            }
            else
            {
               
            try
            {
                //if (txtCuenta.Text == String.Empty)
                //{
                //    MyMessageBox.ShowBox("Ingresa numero de Cuenta.","Mensaje");
                //}
               
                //else
                //{
                    DateTime namefile = DateTime.Now;
                    string m_archivo1 = namefile.Day.ToString() + "-" + namefile.Month.ToString() + "-" + namefile.Year.ToString() + "h" + namefile.Hour.ToString() + "m" + namefile.Minute.ToString() + "s" + namefile.Second.ToString() + ".xls";

                    using (SaveFileDialog sfd = new SaveFileDialog() { FileName = @"C:\Directorio SICE\Reportes tlock\" + m_archivo1 + "", ValidateNames = true }
                    )
                    {


                        Microsoft.Office.Interop.Excel.Application app = new Microsoft.Office.Interop.Excel.Application();


                        Microsoft.Office.Interop.Excel.Workbook wb =
                        app.Workbooks.Add(Microsoft.Office.Interop.Excel.XlSheetType.xlWorksheet);
                        Microsoft.Office.Interop.Excel.Worksheet ws = (Microsoft.Office.Interop.Excel.Worksheet)app.ActiveSheet;

                        app.Visible = false;

                        ws.Cells[1, 1] = "no_cuenta";
                        ws.Cells[1, 2] = "id_transaccion";
                        ws.Cells[1, 3] = "id_cajero";
                        ws.Cells[1, 4] = "MXN20";
                        ws.Cells[1, 5] = "MXN50";
                        ws.Cells[1, 6] = "MXN100";
                        ws.Cells[1, 7] = "MXN200";
                        ws.Cells[1, 8] = "MXN500";
                        ws.Cells[1, 9] = "MXN1000";
                        ws.Cells[1, 10] = "totalBilletes";
                        ws.Cells[1, 11] = "total";
                        ws.Cells[1, 12] = "fecha_transaccion";

                        ws.Range["A1:L1"].Columns.AutoFit();

                        int i = 1;
                        int i2 = 2;
                        int i3 = 3;
                        foreach (ListViewItem lista in mListView.Items)
                        {
                            i = 1;
                            foreach (ListViewItem.ListViewSubItem mListView in lista.SubItems)
                            {
                                ws.Cells[i2, i] = mListView.Text;
                                i++;
                            }
                            i2++;
                            i3++;

                        }



                        wb.SaveAs(sfd.FileName, Microsoft.Office.Interop.Excel.XlFileFormat.xlWorkbookNormal, Type.Missing,
                           Type.Missing, true, false, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange,
                            Microsoft.Office.Interop.Excel.XlSaveConflictResolution.xlLocalSessionChanges, Type.Missing,
                           Type.Missing);
                        // wb.Close(true);
                        app.Quit();

                        string result = MyMessageBox.ShowBox("Excel Creado.", "Mensaje");

                    }
              //  }
               
                
            }
            catch (Exception E)
            {
                MessageBox.Show("EXCEL ERROR" + E.Message);
            }
            }//fin else
            tclNumerico.Visible = false;
        }

        DataTable MakeDataTable()
        {  DataTable friend = new DataTable();
             //Define columns
            friend.Columns.Add("no_cuenta");
            friend.Columns.Add("id_transaccion");
            friend.Columns.Add("id_cajero");
            friend.Columns.Add("MXN20");
            friend.Columns.Add("MXN50");
            friend.Columns.Add("MXN100");
            friend.Columns.Add("MXN200");
            friend.Columns.Add("MXN500");
            friend.Columns.Add("MXN1000");
            friend.Columns.Add("totalBilletes");
            friend.Columns.Add("total");
            friend.Columns.Add("fecha_transaccion");

            for (int i = 0; i < mListView.Items.Count; i++)
            {
                friend.Rows.Add(mListView.Items[i].SubItems[0].Text, mListView.Items[i].SubItems[1].Text, mListView.Items[i].SubItems[2].Text,
                    mListView.Items[i].SubItems[3].Text, mListView.Items[i].SubItems[4].Text, mListView.Items[i].SubItems[5].Text,
                    mListView.Items[i].SubItems[6].Text, mListView.Items[i].SubItems[7].Text, mListView.Items[i].SubItems[8].Text,
                    mListView.Items[i].SubItems[9].Text, mListView.Items[i].SubItems[10].Text, mListView.Items[i].SubItems[11].Text);


            }

            return friend;
        }

        public void ExportDataTableToPdf(DataTable dtblTable, String strPdfPath, string strHeader)
        {

            System.IO.FileStream fs = new FileStream(strPdfPath, FileMode.Create, FileAccess.Write, FileShare.None);
            Document document = new Document(iTextSharp.text.PageSize.A3.Rotate());
            PdfWriter writer = PdfWriter.GetInstance(document, fs);
            
            document.Open();

            BaseFont bfntHead = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntHead = new iTextSharp.text.Font(bfntHead, 12, 1, BaseColor.GRAY);
            Paragraph prgHeading = new Paragraph();
            prgHeading.Alignment = Element.ALIGN_CENTER;
            prgHeading.Add(new Chunk(strHeader.ToUpper(), fntHead));
            document.Add(prgHeading);

            Paragraph prgAuthor = new Paragraph();
            iTextSharp.text.Font fntAuthor = new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 12, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
            prgAuthor.Alignment = Element.ALIGN_RIGHT;
            prgAuthor.Add(new Chunk("No. CUENTA :" + txtCuenta.Text, fntAuthor));
            prgAuthor.Add(new Chunk("\n FECHA : " + DateTime.Now.ToShortDateString(), fntAuthor));
            document.Add(prgAuthor);
            document.AddCreator(lblCuentaAdmin.Text);

            Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
            document.Add(p);
            document.Add(new Chunk("\n", fntHead));

            PdfPTable table = new PdfPTable(dtblTable.Columns.Count);
            BaseFont btnColumnHeader = BaseFont.CreateFont(BaseFont.TIMES_ROMAN, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
            iTextSharp.text.Font fntColumnHeader = new iTextSharp.text.Font(btnColumnHeader, 10, 1, BaseColor.WHITE);
            for (int i = 0; i < dtblTable.Columns.Count; i++)
            {
                PdfPCell cell = new PdfPCell();
                cell.BackgroundColor = BaseColor.LIGHT_GRAY;
                cell.AddElement(new Chunk(dtblTable.Columns[i].ColumnName.ToUpper(), fntColumnHeader));
                table.AddCell(cell);
            }
            for (int i = 0; i < dtblTable.Rows.Count; i++)
            {
                for (int j = 0; j < dtblTable.Columns.Count; j++)
                {
                    table.AddCell(dtblTable.Rows[i][j].ToString());
                }
            }

            document.Add(table);
            document.Close();
            writer.Close();
            fs.Close();

        }

        private void button45_Click(object sender, EventArgs e)
        {
            if (mListView.Items.Count == 0)
            {
                MyMessageBox.ShowBox("No hay registros en la tabla");
            }
            else
            {
                try
                {
                    DateTime namefile = DateTime.Now;
                    string m_archivo1 = namefile.Day.ToString() + "-" + namefile.Month.ToString() + "-" + namefile.Year.ToString() + "h" + namefile.Hour.ToString() + "m" + namefile.Minute.ToString() + "s" + namefile.Second.ToString() + ".pdf";

                    DataTable dtbl = MakeDataTable();
                    ExportDataTableToPdf(dtbl, @"C:\Directorio SICE\Reportes tlock\" + m_archivo1 + "", "CONSULTAS LOCK");
                    string result = MyMessageBox.ShowBox("PDF Creado.", "Mensaje");
        }
                catch (Exception ex)
                {
                    string result = MyMessageBox.ShowBox("ERROR DE PDF.", "Mensaje" + ex);

                }
            }
            tclNumerico.Visible = false;
        }

      

        private void lblEstacion_Click(object sender, EventArgs e)
        {

        }

        private void onclick_Ptr(object sender, EventArgs e)
        {

            if (mListView.Items.Count == 0)
            {
                MyMessageBox.ShowBox("No hay registros en la tabla");
            }
            else
            {
                try
                {
                    DataTable dtbl = MakeDataTable();
                    ExportDataTableToPdf(dtbl, @"C:\Directorio SICE\Reportes tlock\temporal.pdf", "CONSULTAS LOCK");
                    Spire.Pdf.PdfDocument doc = new Spire.Pdf.PdfDocument();
                    doc.LoadFromFile(@"C:\Directorio SICE\Reportes tlock\temporal.pdf");

                    PrintDialog dialogPrint = new PrintDialog();
                    dialogPrint.AllowPrintToFile = true;
                    dialogPrint.AllowSomePages = true;
                    dialogPrint.PrinterSettings.MinimumPage = 1;
                    dialogPrint.PrinterSettings.MaximumPage = doc.Pages.Count;
                    dialogPrint.PrinterSettings.FromPage = 1;
                    dialogPrint.PrinterSettings.ToPage = doc.Pages.Count;

                    if (dialogPrint.ShowDialog() == DialogResult.OK)
                    {
                        //Set the pagenumber which you choose as the start page to print
                        doc.PrintFromPage = dialogPrint.PrinterSettings.FromPage;
                        //Set the pagenumber which you choose as the final page to print
                        doc.PrintToPage = dialogPrint.PrinterSettings.ToPage;
                        //Set the name of the printer which is to print the PDF
                        doc.PrinterName = dialogPrint.PrinterSettings.PrinterName;

                        PrintDocument printDoc = doc.PrintDocument;
                        dialogPrint.Document = printDoc;
                        printDoc.Print();

                        string result = MyMessageBox.ShowBox("Imprimiendo documento", "Mensaje");
                    }
                }
                catch (Exception ex)
                {
                    string result = MyMessageBox.ShowBox(ex.Message + "Error de impresión");

                }
                if (System.IO.File.Exists(@"C:\Directorio SICE\Reportes tlock\temporal.pdf"))
                {
                    try
                    {
                        System.IO.File.Delete(@"C:\Directorio SICE\Reportes tlock\temporal.pdf");
                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show("Error de proceso");
                    }
                }
            }//fin else
            tclNumerico.Visible = false;

        }

        private void materialRadioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (materialRadioButton1.Checked)
            {



                btnAceptar.Visible = true;
                btnAceptar.Location = new Point(1, 80);
                button46.Visible = false;
                tclNumerico.Visible = false;
                mListView.Items.Clear();

            }
        }

        private void materialRadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (materialRadioButton2.Checked)
            {

                tclNumerico.Visible = false;
                btnAceptar.Visible = false;
                button46.Location = new Point(1, 80);
                button46.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                dtp1.Visible = true;
                dtp2.Visible = true;
                mListView.Items.Clear();

            }
        }
    }
}
















  
