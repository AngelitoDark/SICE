using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MaterialSkinExample
{
    class SendJSon
    {
        

        public void json() {
            dynamic myObject = new ExpandoObject();

            myObject.name = "SICE";
            //   myObject.website = "http://ourcodeworld.com";
            // myObject.language = "en-US";

            List<string> articles = new List<string>();
            articles.Add("m_IdEstacion:" + 70);
            articles.Add("M_Ubicacion" + "Banorte");
            articles.Add("MCiclo:" + 27);
            articles.Add("M_Folio" + 287);
            articles.Add("M_fechaHorarioInicio" + "Fecha");


            myObject.articles = articles;

            string json = JsonConvert.SerializeObject(myObject);

         

          
            MessageBox.Show(json);




        }

    }
}
