using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfConsumoAS400
{
    // NOTA: puede usar el comando "Rename" del menú "Refactorizar" para cambiar el nombre de clase "Servicio" en el código, en svc y en el archivo de configuración a la vez.
    // NOTA: para iniciar el Cliente de prueba WCF para probar este servicio, seleccione Servicio.svc o Servicio.svc.cs en el Explorador de soluciones e inicie la depuración.

    public class Servicio : IServicio
    {
        public string VigenciaPolizaAs400(string nroPoliza)
        {
            string nroSucursal = string.Empty;
            string tipo = string.Empty;
            string numeroDocumento = string.Empty;

            try
            {
                nroSucursal = nroPoliza.Substring(0, 1);
                tipo = nroPoliza.Substring(1, 1);
                numeroDocumento = nroPoliza.Substring(2, nroPoliza.Length - 2);
            }
            catch (Exception ex)
            {
                return "Error: Formato Incorrecto de Poliza";
            }


            string datoDevuelto = "";

            try
            {
                AccesoDatos acceso = new AccesoDatos(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);
                string query = "     SELECT	COUNT(*) CONTADOR    " +
                               "     FROM	LEUGDID.SUAUGVIG " +
                               "     WHERE  CSUCUR_VIG = '" + nroSucursal + "' " +
                               "            AND CTPDOC_VIG = '" + tipo + "' " +
                               "            AND NDOCTO_VIG = " + numeroDocumento;

                int contador = 0;
                Int32.TryParse(acceso.EjecutarSelectResult(query), out contador);

                if (contador == 0)
                {
                    datoDevuelto = "NVIG";
                }
                else
                {
                    datoDevuelto = "VIG";
                }
            }
            catch (Exception ex)
            {
                datoDevuelto = "Error: " + ex.Message;
            }
            return datoDevuelto;
        }

        public List<Documento> VigenciaListadoPolizasAs400(string polizas)
        {
            string[] listadoPolizas = polizas.Replace('"', ' ').Trim().Split(',');

            string sucursalesJuntas = string.Empty;
            string tiposdocJuntas = string.Empty;
            string nrodocJuntas = string.Empty;

            try
            {
                foreach (var poliza in listadoPolizas)
                {
                    string nroSucursal = poliza.Substring(0, 1);
                    string tipo = poliza.Substring(1, 1);
                    string numeroDocumento = poliza.Substring(2, poliza.Length - 2);

                    sucursalesJuntas = sucursalesJuntas + "'" + nroSucursal + "',";
                    tiposdocJuntas = tiposdocJuntas + "'" + tipo + "',";
                    nrodocJuntas = nrodocJuntas + "'" + numeroDocumento + "',";
                }
            }
            catch (Exception)
            {
                return new List<Documento>();
            }


            sucursalesJuntas = sucursalesJuntas.Remove(sucursalesJuntas.Length - 1);
            tiposdocJuntas = tiposdocJuntas.Remove(tiposdocJuntas.Length - 1);
            nrodocJuntas = nrodocJuntas.Remove(nrodocJuntas.Length - 1);

            List<Documento> Listado = new List<Documento>();

            try
            {
                AccesoDatos acceso = new AccesoDatos(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);
                string query = "     SELECT CSUCUR_VIG || CTPDOC_VIG || NDOCTO_VIG AS DOCUMENTO,  FVIGFD_VIG||'-'|| FVIGFM_VIG||'-'|| FVIGFA_VIG  AS FECHA    " +
                               "     FROM	LEUGDID.SUAUGVIG " +
                               "     WHERE  CSUCUR_VIG IN (" + sucursalesJuntas + ") " +
                               "            AND CTPDOC_VIG IN (" + tiposdocJuntas + ") " +
                               "            AND NDOCTO_VIG IN (" + nrodocJuntas + ")";

                DataTable dt = new DataTable();
                dt = acceso.ejecutarSelect(query);

                foreach (DataRow item in dt.Rows)
                {
                    Documento doc = new Documento();
                    doc.NroDocumento = item[0].ToString();
                    doc.Fecha = item[1].ToString();
                    Listado.Add(doc);
                }

                return Listado;
            }
            catch (Exception ex)
            {
                return new List<Documento>();
            }
        }


        public List<Documento> TraerPolizasVencidasAS400(string year, string month)
        {
            try
            {
                AccesoDatos acceso = new AccesoDatos(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);

                string query = "     SELECT A.CSUCUR_DOC || A.CTPDOC_DOC || A.NDOCTO_DOC AS DOCUMENTO,  A.FVIGFD_DOC||'-'|| A.FVIGFM_DOC||'-'|| A.FVIGFA_DOC  AS FECHA   " +
                               "     FROM	LEUGDID.SUACSDOC A LEFT OUTER JOIN LEUGDID.SUAUGVIG B ON " +
                               "            A.NDOCTO_DOC = B.NDOCTO_VIG AND A.CSUCUR_DOC = B.CSUCUR_VIG INNER JOIN " +
                               "            LEUGDID.SUAUGNVI C ON A.NDOCTO_DOC = C.NDOCTO_NVIG AND A.CSUCUR_DOC = C.CSUCUR_NVIG" +
                               "     WHERE 	B.NDOCTO_VIG IS NULL" +
                               "            AND A.FVIGFA_DOC = " + year +
                               "            AND A.FVIGFM_DOC = " + month +
                               "            AND A.CTPDOC_DOC = 'P'";

                DataTable dt = new DataTable();
                dt = acceso.ejecutarSelect(query);

                List<Documento> Listado = new List<Documento>();
                foreach (DataRow item in dt.Rows)
                {
                    Documento doc = new Documento();
                    doc.NroDocumento = item[0].ToString();
                    doc.Fecha = item[1].ToString();
                    Listado.Add(doc);
                }

                return Listado;
            }
            catch (Exception ex)
            {
                return new List<Documento>();
            }
        }

        public List<Documento> Documentos(string entrada)
        {
            List<Documento> docu = new List<Documento>();
            docu.Add(new Documento { NroDocumento = "1", Fecha = "2016-01-24" });
            docu.Add(new Documento { NroDocumento = "2", Fecha = "2016-04-21" });
            docu.Add(new Documento { NroDocumento = "3", Fecha = "2016-03-2" });
            return docu;
        }
    }
}
