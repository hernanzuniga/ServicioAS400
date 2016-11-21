using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace WcfConsumoAS400
{
    [DataContract]
    public class Documento
    {
        [DataMember]
        public string NroDocumento { get; set; }

        [DataMember]
        public string Fecha { get; set; }
    }
}