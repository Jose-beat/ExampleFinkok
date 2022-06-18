
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Services;
using CFDI4_VB;


namespace FinkokFunctions.Stamp
{
    public class XMLGenerator
    {
        
        public string generateXML4(string certifiedRoot)
        {
            string response = "";
            string username = "uriel.rr@ticas.com.mx";
            string password = "The_Beatles1960";
            string path = certifiedRoot;
            string cerFile = Path.Combine(path, "CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753s.cer");
            string keyFile = Path.Combine(path, "CSD_Escuela_Kemper_Urgate_EKU9003173C9_20190617_131753.key");
            string keyPass = "1234567a";
            string ex = "";
            Exception er = new  Exception();

            CFDx  cfds = new CFDx();
            
            cfds.Comprobante(
                     Folio: "649",
                     Fecha: DateTime.Now,
                     SubTotal: "22500.00",
                     Moneda: "MXN",
                     Total: "0.00",
                     TipoDeComprobante: "I",
                     Exportacion: "02",
                     LugarDeExpedicion: "67800",
                     Serie: "A",
                     FormaPago: "01",
                     CondicionesDePago: "3 MESES",
                     Descuento: "22500.00",
                     TipoCambio: "1",
                     MetodoPago: "PUE",
                     Confirmacion: null);
      
            cfds.AgregarEmisor(
                Rfc: "",
                RegimenFiscal: "",
                Nombre : "",
                FacAtrAdquirente : ""
                );


            cfds.AgregarReceptor(
                Rfc : "",
                UsoCFDI : "",
                Nombre : "RAUL SALDIVAR",
                DomicilioFiscalReceptor : "domicilioFoscal",
                RegimenFiscalReceptor : "regimen",
                ResidenciaFiscal : "recidenciaFiscal",
                NumRegIdTrib : "num"
                );
            cfds.AgregarConcepto(
                 ClaveProdServ : "10101500",
                             Cantidad : "",
                             ClaveUnidad : "F52",
                             Descripcion : "ACERO",
                             ValorUnitario : "15000.00",
                             Importe : "22500.00",
                             ObjetoImp : "obje",
                             Descuento : "22500.00",
                             Unidad : "TONELADA",
                             NoIdentificacion : "00001"
                );






            if (
            cfds.CrearFacturaXML(
                username,
                password,
                keyFile,
                cerFile,
                keyPass,
                ref ex,
                Ruta: path,
                nameXML: "MiXml4",
                ErrorE: ref er
                ))
            {
                response = "XML Generado Correctamente";
            }
            else
            {
                response = "Error al generar XML";
            }


            
            return response;
        }
    }
}
