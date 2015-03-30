using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDocs.Models
{
    #region Connection
    public class Connection
    {
        #region connection
        private SqlConnection connection;
        #endregion

        #region Constructor
        public Connection()
        {
            AppSettingsReader oSettingsReader = new AppSettingsReader();
            string sEnvironment = oSettingsReader.GetValue("Environment", typeof(String)).ToString();
            connection = new SqlConnection(ConfigurationManager.ConnectionStrings[sEnvironment].ConnectionString);
        }
        #endregion

        #region Open Connection
        public SqlConnection OpenConnection()
        {
            try
            {
                if (connection.State == ConnectionState.Broken || connection.State == ConnectionState.Closed)
                {
                    connection.Open();
                }
                else
                {
                    connection.Close();
                }
            }

            catch (SqlException sqlEx)
            {
                throw sqlEx;
            }

            catch (Exception ex)
            {
                throw ex;
            }

            return connection;
        }
        #endregion
    }
    #endregion
}
