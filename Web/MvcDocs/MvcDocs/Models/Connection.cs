using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDocs.Models
{
    #region Conexao
    public class Conexao
    {
        #region connection
        private SqlConnection conexao;
        #endregion

        #region Construtor
        public Conexao()
        {
            AppSettingsReader oSettingsReader = new AppSettingsReader();
            string sAmbiente = oSettingsReader.GetValue("Ambiente", typeof(String)).ToString();
            conexao = new SqlConnection(ConfigurationManager.ConnectionStrings[sAmbiente].ConnectionString);
        }
        #endregion

        #region Abrir Conexão
        public SqlConnection AbrirConexao()
        {
            try
            {
                if (conexao.State == ConnectionState.Broken || conexao.State == ConnectionState.Closed)
                {
                    conexao.Open();
                }
                else
                {
                    conexao.Close();
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

            return conexao;
        }
        #endregion
    }
    #endregion
}
