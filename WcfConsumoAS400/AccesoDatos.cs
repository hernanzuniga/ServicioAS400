using IBM.Data.DB2.iSeries;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace WcfConsumoAS400
{
    public class AccesoDatos
    {
        iDB2Connection AS400ConnectionString = null;

        public AccesoDatos(string connectionstring)
        {
            try
            {
                AS400ConnectionString = new iDB2Connection();
                AS400ConnectionString.ConnectionString = connectionstring;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
            }
        }

        public bool conectar()
        {
            try
            {
                AS400ConnectionString.Open();
                AS400ConnectionString.Close();
                return true;
            }
            catch (Exception ex)
            {
                string error = ex.Message;
                return false;
            }
            
        }

        public string EjecutarSelectResult(string consulta)
        {
            try
            {
                AS400ConnectionString.Open();

                string datoDevuelto = string.Empty;

                iDB2Command command = new iDB2Command();
                command.Connection = AS400ConnectionString;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = consulta;
                command.CommandTimeout = 600000;

                iDB2DataReader dr = command.ExecuteReader();

                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        datoDevuelto = dr.GetString(0);
                        break;
                    }
                }
                return datoDevuelto;

            }
            catch (Exception ex)
            {
                return "0";
                //throw new Exception("Se produjo un problema al reaizar un select en AS400: ", ex);
            }
        }

        public DataTable ejecutarSelect(string consulta)
        {
            try
            {
                DataTable Datosconsulta = new DataTable("tabla");
                AS400ConnectionString.Open();

                iDB2Command command = new iDB2Command();
                command.Connection = AS400ConnectionString;
                command.CommandType = System.Data.CommandType.Text;
                command.CommandText = consulta;
                command.CommandTimeout = 600000;

                iDB2DataReader dr = command.ExecuteReader();

                Datosconsulta.Load(dr);
                return Datosconsulta;

            }
            catch (Exception ex)
            {

                throw new Exception("Se produjo un problema al reaizar un select en AS400: ", ex);
            }
        }
        public string ejecutarInsertUpdateDelete(string consulta)
        {
            try
            {
                int resul = 0;
                AS400ConnectionString.Open();
                iDB2Command command = AS400ConnectionString.CreateCommand();
                command = new iDB2Command(consulta, AS400ConnectionString);
                resul = command.ExecuteNonQuery();
                return resul.ToString();
            }
            catch (Exception ex)
            {
                throw new Exception("Se produjo un problema al realizar un insert en AS400: ", ex);

            }
            finally { AS400ConnectionString.Close(); }
        }
    }
}