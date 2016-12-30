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
            authData.Usuario = user;
            authData.Terminal = terminal;
            authData.Senha = Pasword;
            string mensaje = operaciones.ResetTCR(authData).CodigoResposta;
            string mensaje1 = operaciones.ResetTCR(authData).Mensagem;

            //   richTextBox1.Text = (operaciones.AbastecimentoEncerrar(authData).ExtensionData).ToString();
            //            lblMensaje.Text = TCR2.ContinueDeposit(TCR).RetData;
            //  lblMensaje.Text = TCR2.StartDeposit(TCR).Message;
            MessageBox.Show(mensaje);
        }




        public void estadotcr()
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
            lbldenomF.Text = "$ " + denomF.ToString();
            int contadorF = Convert.ToInt32(tcr2.ConsultarConteudoTCR(authData).InformacoesCassetes[5].Contador);
            lblcantF.Text = contadorF.ToString();
            int totalF = denomF * contadorF;
            lbltotalF.Text = "$ " + totalF;

            lbltotalcontenido.Text = "Contenido: $" + (totalA + totalB +
                totalC + totalD + totalE + totalF +
                10000).ToString("#,#", CultureInfo.InvariantCulture);

            lblT_Billetes.Text = "Total de Billetes" + (contadorA + contadorB + contadorC + contadorD + contadorE + contadorF);

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

        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (metroTabControl1.SelectedTab == metroTabControl1.TabPages["contenidoTCR"])//Nombre del Tab
            {
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
            Font f = new Font("Arial Narrow", 10, FontStyle.Regular); //Estilo utilizado en ticket
            SolidBrush b = new SolidBrush(Color.Black);  //Color de texto
            g.DrawImage(Image.FromFile(logo), 130, -5, 160, 90);
            g.DrawString("Impresión de contenido TCR   ", f, b, 10, 95);
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



    }
}
