using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Newtonsoft.Json.Linq;

 
namespace ContenidoPING
{
    class Program
    {
        static string user = "GO";
        static String Pasword = "go";
        static string terminal = "123";
        static FullService.Autenticacao authData = new FullService.Autenticacao();
        static FullService.OperationsClient operaciones = new FullService.OperationsClient();
        static FullService.ConfiguracaoCassette casett = new FullService.ConfiguracaoCassette();
        static FullService.DadosCassete dados = new FullService.DadosCassete();
        static FullService.ContentClient[] contenido = new FullService.ContentClient[3];

        static void Main(string[] args)
        {
        //Console.WriteLine("hola");
             jsontcr();
            //Console.ReadKey();
        }

        public static void jsontcr()
        {
            try
            {

                int i = 0;
                while (true)
                {
                    i++;


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

                    xDoc.Load(@"../../../MaterialSkinExample/Configuration files/config.xml");
                    XmlNodeList tcr = xDoc.GetElementsByTagName("tcr");
                    string m_Ubicacion = ((XmlElement)tcr[0]).GetElementsByTagName("sucursal")[0].InnerText;

                  //  Console.WriteLine(m_Ubicacion);
                    int m_IdEstacion = 2001;

                    int m_Ciclo = 1;
                    int m_Folio = 368459;

                    DateTime fecha = DateTime.Now;
                    CultureInfo ci = CultureInfo.InvariantCulture;

                    string hora = String.Format(fecha.ToString("HH:mm:ss.ff", ci));
                    var zona = String.Format(DateTime.Now.ToString("%K"));
                    string m_FechaHoraFin = (fecha.Year + "-" + fecha.Month + "-" + fecha.Day) + ("T" + hora + zona);
                    string FechaInicio = (fecha.Year + "-" + fecha.Month + "-" + fecha.Day) + ("T" + hora + zona);//

                    string json = @"{ m_IdEstacion: " + m_IdEstacion + ", m_Ubicacion: '" + m_Ubicacion + "',m_Folio:" + m_Folio + ",m_FechaHoraInicio:'" + FechaInicio + "',m_FechaHoraFin:'" + m_FechaHoraFin +
        "',   m_DenominacionContenedor:  {'1':" + "{'1000':" + contenidoF + ",'500':" + contenidoE +
    ",'200':" + contenidoD + ",'100':" + contenidoC + ",'50':" + contenidoB + ",'20':" +
    contenidoA + "} } }";

                    JObject jobj = JObject.Parse(json);
                    //   MessageBox.Show(o.ToString());


                    var texto = jobj;
                  //  Console.WriteLine(texto);
                    // MessageBox.Show(texto+"");
                    // Console.ReadKey();

                    DateTime namefile = DateTime.Now;
                    string m_archivo = namefile.Day.ToString() + "-" + namefile.Month.ToString() + "-" + namefile.Year.ToString() + "h" + namefile.Hour.ToString() + "m" + namefile.Minute.ToString() + "s" + namefile.Second.ToString() + ".json";

                    System.IO.StreamWriter file = new System.IO.StreamWriter(@"C:\Directorio SICE\" + m_archivo + "");
                    file.WriteLine(texto);
                    file.Close();



                    if (i == 1)
                    {
                        i = 0;

                        int Minutos = 60000 * 5;

                        System.Threading.Thread.Sleep(Minutos);
                        //break;
                    }
                }
            }
            catch (Exception)
            {
                Console.WriteLine("proceso ocupado ....");
                   // throw;
            }

        }
    }
}
