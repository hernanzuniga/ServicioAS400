using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace WcfConsumoAS400
{
    [ServiceContract]
    public interface IServicio
    {
        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        RequestFormat = WebMessageFormat.Json,
        UriTemplate = "vigenciaPoliza/{nroPoliza}")]
        String VigenciaPolizaAs400(string nroPoliza);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        RequestFormat = WebMessageFormat.Json,
        UriTemplate = "listadovigenciaPoliza/{polizas}")]
        List<Documento> VigenciaListadoPolizasAs400(string polizas);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        RequestFormat = WebMessageFormat.Json,
        UriTemplate = "traerListado/{year}/{month}")]
        List<Documento> TraerPolizasVencidasAS400(string year, string month);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        RequestFormat = WebMessageFormat.Json,
        UriTemplate = "prueba/{entrada}")]
        List<Documento> Documentos(string entrada);
    }
}
