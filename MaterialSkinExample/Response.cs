using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialSkinExample
{
     class Response
    {
        static string user = "TCR";
        static String Pasword = "12345";
        static string terminal = "123";
        static FullService.Autenticacao authData = new FullService.Autenticacao();
        static FullService.OperationsClient operaciones = new FullService.OperationsClient();
        static FullService.ConfiguracaoCassette casett = new FullService.ConfiguracaoCassette();
        static FullService.DadosCassete dados = new FullService.DadosCassete();


        public int Total_depositados;

        // Continuando con el Deposito
        public int dm0;
        public int dm1;
        public int dm2;
        public int dm3;
        public int dm4;
        public int dm5;
         
        //Cantidades por unidad
        public int cant0;
        public int cant1;
        public int cant2;
        public int cant3;
        public int cant4;
        public int cant5;

        


        public void TerminarDepositoDirecto()
        {
            authData.Usuario = user;
            authData.Terminal = terminal;
            authData.Senha = Pasword;
            string json= operaciones.AbastecimentoEncerrar(authData).Retorno;
            

            //string json = TCR2.ContinueDeposit(TCR).RetData;
            //vacio = TCR2.ContinueDeposit(TCR).Message;

            //    MessageBox.Show("" + TCR2.ContinueDeposit(TCR).RetData);
            TResponse wtResponse = JsonConvert.DeserializeObject<TResponse>(json);
            //  MessageBox.Show("" + webTellerResponse.rsmdata.Denom[1]);
            Total_depositados = wtResponse.iTotalAmount;
        //    MyMessageBox.ShowBox(""+Total_depositados);

           

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
           // MessageBox.Show("CANTIDAD " + cant0);
            //  MyMessageBox.ShowBox("" + dm0);
        }
        public int _retiro;

        public void Retiro() {

            authData.Usuario = user;
            authData.Terminal = terminal;
            authData.Senha = Pasword;

            string json  = operaciones.Sacar(authData, _retiro).Retorno;
 
            TResponse wtResponse = JsonConvert.DeserializeObject<TResponse>(json);
            //  MessageBox.Show("" + webTellerResponse.rsmdata.Denom[1]);
            Total_depositados = wtResponse.iTotalAmount;
            //    MyMessageBox.ShowBox(""+Total_depositados);



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

         
            //retiro.r1 = dm0;
            //dm0 = retiro.r1;

            //    MyMessageBox.ShowBox("y" + dm0+"x"+cant0);

            // string mensaje =   operaciones.Sacar(authData, _retiro).Retorno;
            // MessageBox.Show(mensaje);
        }


        // Cargando  el retorno del json
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
        // Fin clases del retorno json

    }
}