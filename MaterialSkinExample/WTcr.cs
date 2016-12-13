using Newtonsoft.Json;
using System.Collections.Generic;
 

namespace MaterialSkinExample
{
    class WTcr
    {
        // variables por default del login tcr
        string user = "TCR";
        string pass = "12345";
        string terminal = "1";
        // Abre el login del TCR

        //  Cargando Referecnias del WebService 
        MaterialSkin.Operaciones.AuthenticationType TCR = new MaterialSkin.Operaciones.AuthenticationType();
        MaterialSkin.Operaciones.DataResponse TCR1 = new MaterialSkin.Operaciones.DataResponse();
        MaterialSkin.Operaciones.WebTellerClient TCR2 = new MaterialSkin.Operaciones.WebTellerClient();


        public void LoginTcr() {
            
           
            TCR.sLogin = user;
            TCR.sPassword = pass;
            TCR.sTerminal = terminal;
        }

        // Preparando el Deposito 

        public void PrepararDeposito()
        {
            TCR.sLogin = user;
            TCR.sPassword = pass;
            TCR.sTerminal = terminal;
            TCR2.StartDeposit(TCR);
        }

        // Continuando con el Deposito
        public int denom0;
        public int denom1;
        public int denom2;
        public int denom3;
        public int denom4;
        public int denom5;
        // Total
        public int mTotal;

        //Cantidades por unidad
        public int cantidad0;
        public int cantidad1;
        public int cantidad2;
        public int cantidad3;
        public int cantidad4;
        public int cantidad5;

        public int Rechazos;

    //    public string vacio;

        public void ContinuarDeposito()
        {
            TCR.sLogin = user;
            TCR.sPassword = pass;
            TCR.sTerminal = terminal;
           
            string json = TCR2.ContinueDeposit(TCR).RetData;
         //vacio = TCR2.ContinueDeposit(TCR).Message;

            //    MessageBox.Show("" + TCR2.ContinueDeposit(TCR).RetData);
            TellerResponse webTellerResponse = JsonConvert.DeserializeObject<TellerResponse>(json);
         //  MessageBox.Show("" + webTellerResponse.rsmdata.Denom[1]);
         
            //Denominaciones
            denom0 = webTellerResponse.rsmdata.Denom[0];//Veinte
            denom1 = webTellerResponse.rsmdata.Denom[1];
            denom2 = webTellerResponse.rsmdata.Denom[2];
            denom3 = webTellerResponse.rsmdata.Denom[3];
            denom4 = webTellerResponse.rsmdata.Denom[4];
            denom5 = webTellerResponse.rsmdata.Denom[5];
            //Total Final
            mTotal = webTellerResponse.iTotalAmount;
            
            //Cantidad de billetes
            cantidad0=webTellerResponse.rsmdata.Notes[0];
            cantidad1 = webTellerResponse.rsmdata.Notes[1];
            cantidad2 = webTellerResponse.rsmdata.Notes[2];
            cantidad3 = webTellerResponse.rsmdata.Notes[3];
            cantidad4 = webTellerResponse.rsmdata.Notes[4];
            cantidad5 = webTellerResponse.rsmdata.Notes[5];

            //Rechazos 
            Rechazos = webTellerResponse.iArrayReject ;

         

        }

        // Finalizando el Deposito 
        public void FinDeposito (){

            TCR.sLogin = user;
            TCR.sPassword = pass;
            TCR.sTerminal = terminal;
            TCR2.EndDeposit(TCR);
        }


        // Cancelar el Deposito
        public void CancelarDeposito() {
             TCR.sLogin = user;
            TCR.sPassword = pass;
            TCR.sTerminal = terminal;         
            TCR2.CancelDeposit(TCR);
        }

            // Cargando  el retorno del json
        public class TellerResponse
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
