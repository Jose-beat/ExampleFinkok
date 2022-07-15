using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace XMLFunctions
{
    public class CreateCartaPorte
    {


        public Comprobante complement(Comprobante comprobante, string rRFC, string eRFC)
        {
            //Carta Porte
            CartaPorte cartaPorte = new CartaPorte();
            cartaPorte.Version = "2.0";
            cartaPorte.TranspInternac = CartaPorteTranspInternac.No;
            cartaPorte.TotalDistRec = 200m;
           
            //Añadir mercancias
            CartaPorteMercancias mercancias = new CartaPorteMercancias();
            mercancias.PesoBrutoTotal = 100m;
            mercancias.UnidadPeso = "XBX";
            mercancias.NumTotalMercancias = 1;


            List<CartaPorteMercanciasMercancia> listMercancias = new List<CartaPorteMercanciasMercancia>();
            CartaPorteMercanciasMercancia mercancia = new CartaPorteMercanciasMercancia();
            mercancia.BienesTransp = "50193201";
            mercancia.Descripcion = "Navajas";
            mercancia.Cantidad = 100m;
            mercancia.ClaveUnidad = "AS";
            mercancia.PesoEnKg = 100m;

            //DATOS DE AUTOTRANSPORTE

            CartaPorteMercanciasAutotransporteIdentificacionVehicular identificacionVehicular = new CartaPorteMercanciasAutotransporteIdentificacionVehicular()
            {
                ConfigVehicular = c_ConfigAutotransporte.GPLATB,
                PlacaVM = "NAA7741",
                AnioModeloVM = 2004
                
            };

            CartaPorteMercanciasAutotransporteSeguros seguros = new CartaPorteMercanciasAutotransporteSeguros()
            {
                AseguraRespCivil = "Los angeles",
                PolizaRespCivil = "REUGI8RGERGYEGUR4429"
            };

            //OBTENER INFORMACION DE VEHICULO Y SUS PERFILES EN  TINISA 
            mercancias.Autotransporte = new CartaPorteMercanciasAutotransporte()
            {
                PermSCT = c_TipoPermiso.TPAF01,
                NumPermisoSCT = "435UYI354445GTGE345ERGEGERG",
                IdentificacionVehicular = identificacionVehicular,
                Seguros = seguros,
            };


            //FIGURAS DE AUTOTRANSPORTE

            List<CartaPorteTiposFigura> tiposFiguras = new List<CartaPorteTiposFigura>();


            CartaPorteTiposFiguraDomicilio domicilioFigura = new CartaPorteTiposFiguraDomicilio()
            {
                Calle = "3 Norte #23",
                Municipio = "205",
                Pais = c_Pais.MEX,
                NumeroExterior = "5",
                Estado = "PUE",
                CodigoPostal = "75670"


            };
            //OBTENER INFORMACION DE OPERADORES Y SUS PERFILES EN E TINISA 
            CartaPorteTiposFigura figuraTransporte = new CartaPorteTiposFigura()
            {
                TipoFigura = c_FiguraTransporte.Item01,
                RFCFigura = eRFC ,
                NombreFigura = "ADRIANA JUAREZ FERNANDEZ",
                NumLicencia = "PUE9988765",
                Domicilio= domicilioFigura
            };


            tiposFiguras.Add(figuraTransporte);
            cartaPorte.FiguraTransporte = tiposFiguras.ToArray();



            listMercancias.Add(mercancia);
            mercancias.Mercancia = listMercancias.ToArray();


            List<CartaPorteUbicacion> ubicaciones = new List<CartaPorteUbicacion>();
            //Ubicaciones
            CartaPorteUbicacion ubicacionOrigen = new CartaPorteUbicacion();
            //CartaPorteUbicacion Origen
            ubicacionOrigen.TipoUbicacion = CartaPorteUbicacionTipoUbicacion.Origen;
            ubicacionOrigen.RFCRemitenteDestinatario = eRFC;
            DateTime horaSalida = new DateTime(2022, 6, 10);
            ubicacionOrigen.FechaHoraSalidaLlegada = horaSalida.ToString("yyyy-MM-ddThh:mm:ss");
            

            CartaPorteUbicacionDomicilio domicilioOrigen = new CartaPorteUbicacionDomicilio()
            {
                Estado = "PUE",
                Pais = c_Pais.MEX,
                CodigoPostal = "75660"
                
            };
            ubicacionOrigen.Domicilio = domicilioOrigen;

            CartaPorteUbicacion ubicacionDestino = new CartaPorteUbicacion();
            //Ubicacion Destinatario
            DateTime horaLlegada = new DateTime(2022, 6, 10);
            ubicacionDestino.TipoUbicacion = CartaPorteUbicacionTipoUbicacion.Destino;
            ubicacionDestino.RFCRemitenteDestinatario = rRFC;
            ubicacionDestino.FechaHoraSalidaLlegada = horaLlegada.ToString("yyyy-MM-ddThh:mm:ss");
            ubicacionDestino.DistanciaRecorrida = 200;

            CartaPorteUbicacionDomicilio domicilioDestino = new CartaPorteUbicacionDomicilio()
            {
                Estado = "VER",
                Pais = c_Pais.MEX,
                CodigoPostal = "94690"

            };

            ubicacionDestino.Domicilio = domicilioDestino;

            ubicaciones.Add(ubicacionOrigen);
            ubicaciones.Add(ubicacionDestino);

            cartaPorte.Mercancias = mercancias;
            cartaPorte.Ubicaciones = ubicaciones.ToArray();




            comprobante.Complemento = new ComprobanteComplemento();
            
            XmlDocument compPorte = new XmlDocument();
            XmlSerializerNamespaces xmlNamespacePorte = new XmlSerializerNamespaces();
            xmlNamespacePorte.Add("cartaporte20", "http://www.sat.gob.mx/CartaPorte20");


            using (XmlWriter writter = compPorte.CreateNavigator().AppendChild())
            {
                new XmlSerializer(cartaPorte.GetType()).Serialize(writter, cartaPorte, xmlNamespacePorte);
                
            }
            
            comprobante.Complemento.Any = new XmlElement[1];
            comprobante.Complemento.Any[0] = compPorte.DocumentElement;



            return comprobante;

        }
    }
}
