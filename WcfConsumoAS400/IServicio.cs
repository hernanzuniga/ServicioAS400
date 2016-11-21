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
        UriTemplate = "traerListado/{year}/{month}/{day}")]        
        List<Documento> TraerPolizasVencidasAS400(string year, string month, string day);

        [OperationContract]
        [WebInvoke(Method = "GET",
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        RequestFormat = WebMessageFormat.Json,
        UriTemplate = "prueba/{entrada}")]
        List<Documento> Documentos(string entrada);
    }
}
