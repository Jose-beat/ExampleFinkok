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

        public CartaPorteMercanciasMercancia addMercancia()
        {
            CartaPorteMercanciasMercancia mercancia = new CartaPorteMercanciasMercancia();
            return mercancia;
        }
        public Comprobante complement(Comprobante comprobante)
        {
            //Carta Porte
            CartaPorte cartaPorte = new CartaPorte();
            cartaPorte.Version = "2.0";

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
            ubicacionOrigen.RFCRemitenteDestinatario = "";
            DateTime horaSalida = new DateTime(2022, 6, 10);
            ubicacionOrigen.FechaHoraSalidaLlegada = horaSalida.ToString("yyyy-MM-ddThh:mm:ss");

            CartaPorteUbicacion ubicacionDestino = new CartaPorteUbicacion();
            //Ubicacion Destinatario
            DateTime horaLlegada = new DateTime(2022, 6, 10);
            ubicacionDestino.TipoUbicacion = CartaPorteUbicacionTipoUbicacion.Destino;
            ubicacionDestino.RFCRemitenteDestinatario = "";
            ubicacionDestino.FechaHoraSalidaLlegada = horaLlegada.ToString("yyyy-MM-ddThh:mm:ss");

            ubicaciones.Add(ubicacionOrigen);
            ubicaciones.Add(ubicacionDestino);

            cartaPorte.Ubicaciones = ubicaciones.ToArray();




            comprobante.Complemento = new ComprobanteComplemento();
            
            XmlDocument compPorteMerc = new XmlDocument();
            XmlSerializerNamespaces xmlNamespaceMercPorte = new XmlSerializerNamespaces();

            using (XmlWriter writter = compPorteMerc.CreateNavigator().AppendChild())
            {
                new XmlSerializer(mercancias.GetType()).Serialize(writter, listMercancias, xmlNamespaceMercPorte);
            }

            XmlDocument compPorteUbc = new XmlDocument();
            XmlSerializerNamespaces xmlNamespaceUbcPorte = new XmlSerializerNamespaces();


            using(XmlWriter writter = compPorteUbc.CreateNavigator().AppendChild())
            {
                new XmlSerializer(ubicaciones.GetType()).Serialize(writter, listMercancias, xmlNamespaceUbcPorte);
            }
            comprobante.Complemento.Any = new XmlElement[2];
            comprobante.Complemento.Any[0] = compPorteMerc.DocumentElement;
            comprobante.Complemento.Any[1] = compPorteUbc.DocumentElement;



            return comprobante;

        }
    }
}
