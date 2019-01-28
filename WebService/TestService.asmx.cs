using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace WebService
{
    /// <summary>
    /// Descripción breve de TestService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // Para permitir que se llame a este servicio web desde un script, usando ASP.NET AJAX, quite la marca de comentario de la línea siguiente. 
    // [System.Web.Script.Services.ScriptService]
    public class TestService : System.Web.Services.WebService
    {
        [WebMethod]
        public DataSet index()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter ad = new SqlDataAdapter();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["testWebServiceConnectionString"].ConnectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_read", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        ad.SelectCommand = cmd;
                        ad.Fill(ds);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }


            return ds;
        }

        [WebMethod]
        public int store(string name, string lastName, int areaId)
        {
            int retorno = 0;
              using( SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["testWebServiceConnectionString"].ConnectionString))
                  {    
                      try 
	                    {	        
		                    con.Open();
                            using (SqlCommand cmd = new SqlCommand("sp_insert", con))
                            {
                                cmd.CommandType = CommandType.StoredProcedure;
                                cmd.Parameters.Add("name", SqlDbType.VarChar, 100).Value = name;
                                cmd.Parameters.Add("last_name", SqlDbType.VarChar, 100).Value = lastName;
                                cmd.Parameters.Add("area_id", SqlDbType.Int, 1).Value = areaId;
                                retorno = cmd.ExecuteNonQuery();
                            }
	                    }
	                    catch (Exception)
	                    {
		                    throw;
	                    }finally
                        {
                          con.Close();
                        }
                    }


            return retorno;
        }

        [WebMethod]
        public DataSet areas()
        {
            DataSet ds = new DataSet();
            SqlDataAdapter ad = new SqlDataAdapter();

            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["testWebServiceConnectionString"].ConnectionString))
            {
                try
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("sp_read_areas", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        ad.SelectCommand = cmd;
                        ad.Fill(ds);
                    }
                }
                catch (Exception)
                {
                    throw;
                }
                finally
                {
                    con.Close();
                }
            }


            return ds;
        }
    }
}