using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaterialSkinExample
{
    class Retornos
    {

        public static void mRetornos() {

            MaterialSkin.Operaciones.AuthenticationType TCR = new MaterialSkin.Operaciones.AuthenticationType();
            MaterialSkin.Operaciones.DataResponse TCR1 = new MaterialSkin.Operaciones.DataResponse();
            MaterialSkin.Operaciones.WebTellerClient TCR2 = new MaterialSkin.Operaciones.WebTellerClient();


            string json = TCR2.ContinueDeposit(TCR).RetData;

        }

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

     


    }
}
