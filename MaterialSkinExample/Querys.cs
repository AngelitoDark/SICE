using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialSkinExample
{
    public static class Querys
    {

        public static DataTable Buscar(string cuenta)
        {
            DataTable dt = new DataTable();
            Admin admin = new Admin();
            MySqlConnection conexion = new MySqlConnection("Server=localhost; User=OKI; Password=OKI2016; database=tlock; port=3306;");
            string consulta = ("select usuarios.no_cuenta, transaccion.id_transaccion ,transaccion.id_cajero , transaccion.MXN20 ,transaccion.MXN50,transaccion.MXN100, transaccion.MXN200,transaccion.MXN500 ,transaccion.MXN1000,  transaccion.totalBilletes , transaccion.total, transaccion.fecha_transaccion from usuarios inner join transaccion on transaccion.no_cuenta = usuarios.no_cuenta  where usuarios.no_cuenta = '" + cuenta + "'");
            MySqlCommand cmd = new MySqlCommand(consulta, conexion);
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@cuenta", cuenta);
            MySqlDataAdapter adap = new MySqlDataAdapter(comando);
            adap.Fill(dt);
            return dt;
        }


        public class utilidades
        {
            public static DataSet Ejecutar(string cmd)
            {
                MySqlConnection con = new MySqlConnection("Server=localhost; User=OKI; Password=OKI2016; database=tlock; port=3306;");
                con.Open();
                DataSet DS = new DataSet();
                MySqlDataAdapter DP = new MySqlDataAdapter(cmd, con);
                DP.Fill(DS);
                con.Close();
                return DS;
            }
        }

        public static DataTable BuscarUser(string cuenta)
        {
            // select *from usuarios where no_cuenta=12113007;
            DataTable dt = new DataTable();
            Admin admin = new Admin();
            MySqlConnection conexion = new MySqlConnection("Server=localhost; User=OKI; Password=OKI2016; database=tlock; port=3306;");
            string consulta = ("select usuarios.no_cuenta, transaccion.id_transaccion ,transaccion.id_cajero , transaccion.MXN20 ,transaccion.MXN50,transaccion.MXN100, transaccion.MXN200,transaccion.MXN500 ,transaccion.MXN1000,  transaccion.totalBilletes , transaccion.total, transaccion.fecha_transaccion from usuarios inner join transaccion on transaccion.no_cuenta = usuarios.no_cuenta  where usuarios.no_cuenta = '" + cuenta + "'");
            MySqlCommand cmd = new MySqlCommand(consulta, conexion);
            MySqlCommand comando = new MySqlCommand(consulta, conexion);
            comando.Parameters.AddWithValue("@cuenta", cuenta);
            MySqlDataAdapter adap = new MySqlDataAdapter(comando);
            adap.Fill(dt);
            return dt;
        }

        //Consulta de  transaccion 
        //select last_insert_id();
        //mysql> select max(id_transaccion)from transaccion;
     
        public static void idTransaccion() {
            MySqlConnection con = new MySqlConnection("Server=localhost; User=OKI; Password=OKI2016; database=tlock; port=3306;");
            MySqlCommand cmd = new MySqlCommand();
            con.Open();
            cmd.Connection = con;
            try
            {
                cmd.CommandText = ("select max(id_transaccion)from transaccion");

            string    numtransaccion = cmd.ExecuteScalar().ToString() ;
           }
            catch (Exception exception)
            {
                MessageBox.Show("A Ocurrido un Error" + exception);

            }
            con.Close();
        }

    }
    public class BuscarAdministrador
    {
        public static DataSet Consultar(string cmd)
        {
            MySqlConnection con = new MySqlConnection("Server=localhost; User=OKI; Password=OKI2016; database=tlock; port=3306;");
            con.Open();
            DataSet DS = new DataSet();
            MySqlDataAdapter DP = new MySqlDataAdapter(cmd, con);
            DP.Fill(DS);
            con.Close();
            return DS;
        }
    }

}
