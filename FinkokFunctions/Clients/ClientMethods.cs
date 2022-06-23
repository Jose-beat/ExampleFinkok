using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using ServiceClients;
using ServiceFinkok;


namespace FinkokFunctions.Clients
{
    public class ClientMethods
    {

        public string GetRegistrationClient()
        {
            string user = "uriel.rr@ticas.com.mx";
            string password = "The_Beatles1960";
            string RFC = "";

            ServiceClients.ApplicationClient ws = new ServiceClients.ApplicationClient();
            get agregar = new get();
            getResponse1 response = new getResponse1();

            agregar.reseller_username = user;
            agregar.reseller_password = password;
            agregar.taxpayer_id = RFC;



            //
            response = ws.getAsync(agregar).Result;
            String url = "C:\\XML\\";
            StreamWriter objStreamWriter = new StreamWriter(url + "Reporte_get.xml");
            XmlSerializer x = new XmlSerializer(response.getResponse.getResult.GetType());
            x.Serialize(objStreamWriter, response.getResponse.getResult);
            objStreamWriter.Close();
            return response.getResponse.getResult.message;


        }

       
    }
}
