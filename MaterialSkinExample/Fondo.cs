using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialSkinExample
{
    public partial class Fondo : Form
    {
        public Fondo()
        {
            InitializeComponent();
            MainForm mainform = new MainForm();
            mainform.Show();
        }

        static string user = "TCR";
        static String Pasword = "12345";
        static string terminal = "123";
        static FullService.Autenticacao authData = new FullService.Autenticacao();
        static FullService.OperationsClient operaciones = new FullService.OperationsClient();
        static FullService.ConfiguracaoCassette casett = new FullService.ConfiguracaoCassette();
        static FullService.DadosCassete dados = new FullService.DadosCassete();
        static FullService.ContentClient[] contenido = new FullService.ContentClient[3];

        private void Fondo_Load(object sender, EventArgs e)
        {
        //   MainForm mainform = new MainForm();
        //mainform.Show();
        }
        private void ping()
        {
            var DailyTime = "15:10:00";

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

        }

    }
}
