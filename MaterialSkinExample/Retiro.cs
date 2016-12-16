﻿using MaterialSkin;
using MaterialSkin.Controls;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialSkinExample
{
    public partial class Retiro : MaterialForm 
    {

        private readonly MaterialSkinManager materialSkinManager;
        public int  r1;


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
            groupBox1.Location = new Point(316, 254);
            groupBox1.Size = new Size(439, 381);
           
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
                        resp.Retiro();
                        PrintTicket();
                        //Process.Start("shutdown.exe", "-f");
                        // string errorcontraseña = MyMessageBox.ShowBox(" El equipo se Apagara.", "Mensaje");
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

            MainForm mainform = new MainForm();
            mainform.Show();
            this.Hide();
        }
        Response respuesta = new Response();
        //Nuevo Ticket
        void pd_PrintPage(object sender, PrintPageEventArgs e)
        {
            string hora1 = DateTime.Now.ToString("HH:mm:ss");
         /*   int cant1 = Convert.ToInt32(lblC1.Text);  // Conversión de datos de string a enteros
            int cant2 = Convert.ToInt32(lblC2.Text);  //..  
            int cant3 = Convert.ToInt32(lblC3.Text);  //..
            int cant4 = Convert.ToInt32(lblC4.Text);  //..
            int cant5 = Convert.ToInt32(lblC5.Text);  //..
            int cant6 = Convert.ToInt32(lblC6.Text);  // Conversión de datos de string a enteros
            int Billetes = cant1 + cant2 + cant3 + cant4 + cant5 + cant6;  //Suma total de cantidades
            */
            string cuenta = lblR_Cuenta.Text; //Número de cuenta cliente
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

            g.DrawString("Retiro de EFECTIVO\n" + "Ubicación: Suc. CEDA\n" + "Id Cajero: 201\n", fBody1, sb, 10, ESPACIO);
            g.DrawString("Transacción: " + numtransaccion, fBody1, sb, 10, ESPACIO + 60);
            g.DrawString("No. Cuenta: " + cuenta + "****", fBody1, sb, 10, ESPACIO + 75);
            g.DrawString("Fecha: " + DateTime.Now.ToString("dd/MM/yyyy") + "  Hora: " + /*DateTime.Now.ToString("hh:mm:ss")*/ hora1 + "\n", fBody1, sb, 10, ESPACIO + 90);
            g.DrawString("========================================\n", fBody1, sb, 10, ESPACIO + 105);
             g.DrawString("Cantidad     Denominación      Total\n", fBody1, sb, 10, ESPACIO + 125);

            //lblD1.Text = (respuesta.dm0).ToString();
            //lblD2.Text = (respuesta.dm1).ToString();
            //lblD3.Text = (respuesta.dm2).ToString();
            //lblD4.Text = (respuesta.dm3).ToString();
            //lblD5.Text = (respuesta.dm4).ToString();
            //lblD6.Text = (respuesta.dm5).ToString();


            // if (respuesta.dm0 >= 1)
            //{

           string text = (respuesta.dm0).ToString();

            g.DrawString( r1.ToString(), fBody1, sb, 30, ESPACIO + 145);
                g.DrawString("$ 20", fBody1, sb, 100, ESPACIO + 145);
               // g.DrawString(lblT1.Text, fBody1, sb, 180, ESPACIO + 145);
                Mov = Mov + 20;
          // }
        /*   if (cant2 >= 1)
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
        */
        //g.DrawString("Saldo Anterior:" + 12 + "\n", fBody1, sb, 10, ESPACIO + 145 + Mov); Mov = Mov + 20;
        //    g.DrawString("Total Retiro: " + "$" + txtRetiro.Text + "\n", fBody1, sb, 10, ESPACIO + 145 + Mov); Mov = Mov + 20;
        //    g.DrawString("========================================\n", fBody1, sb, 10, ESPACIO + 145 + Mov); Mov = Mov + 20;
            g.DrawString("Para aclaraciones de los depósitos realizados\nponemos a su disposición nuestras líneas de\natención.\n\nMéxico D.F.:\nMonterrey:\nGuadalajara:\nResto del país:\n", fBody1, sb, 10, ESPACIO + 145 + Mov);
        }
        // creacion de cadena json 


    }
}