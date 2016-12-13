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

namespace MaterialSkinExample
{
    public partial class Admin : MaterialForm
    {
        byte state; // Variable que toma el valor en dado caso que sea elejido Administrador, este valor se guarda en la BD con 0 o 1
        private readonly MaterialSkinManager materialSkinManager;

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
            var archivo_Journal = (@"C:\Directorio SICE\" + m_archivo + "");

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
            var archivo_Journal = (@"C:\Directorio SICE\" + m_archivo + "");
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
            var archivo_Journal = (@"C:\Directorio SICE\" + m_archivo + "");
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
    }
}
