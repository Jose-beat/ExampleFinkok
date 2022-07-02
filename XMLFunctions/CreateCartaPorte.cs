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
            
            //Añadir mercancias
            CartaPorteMercancias mercancias = new CartaPorteMercancias();
            mercancias.PesoBrutoTotal = 100m;
            mercancias.UnidadPeso = "XBX";
            mercancias.NumTotalMercancias = 1;


            List<CartaPorteMercanciasMercancia> listMercancias = new List<CartaPorteMercanciasMercancia>();
            CartaPorteMercanciasMercancia mercancia = new CartaPorteMercanciasMercancia();
            mercancia.BienesTransp = "50193201";
            mercancia.Descripcion = "Navajas y un chingo de chorizo";
            mercancia.Cantidad = 100m;
            mercancia.ClaveUnidad = "AS";
            mercancia.PesoEnKg = 100m;


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
            
            
            CartaPorteUbicacion ubicacionDestino = new CartaPorteUbicacion();
            //Ubicacion Destinatario
            DateTime horaLlegada = new DateTime(2022, 6, 10);
            ubicacionDestino.TipoUbicacion = CartaPorteUbicacionTipoUbicacion.Destino;
            ubicacionDestino.RFCRemitenteDestinatario = rRFC;
            ubicacionDestino.FechaHoraSalidaLlegada = horaLlegada.ToString("yyyy-MM-ddThh:mm:ss");

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
