using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDocs.Models
{
    #region UsuarioModel
    public class UsuarioModel
    {
        #region conexao;
        private Conexao conexao;
        #endregion

        #region Métodos
        public Usuario AutenticaUsuario(string mEmail, string mSenha)
        {
            Usuario usuario = new Usuario();
            conexao = new Conexao();
            SqlConnection sqlCon = conexao.AbrirConexao();
            SqlCommand sqlCmd = new SqlCommand("stp_AutenticarUsuario", sqlCon);
            sqlCmd.CommandTimeout = sqlCon.ConnectionTimeout;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add(new SqlParameter("@P_Email", mEmail));
            sqlCmd.Parameters.Add(new SqlParameter("@P_Senha", mSenha));

            SqlDataReader dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                usuario.EntidadeID = Convert.ToInt32(dr["entidadeID"]);
                usuario.Nome = dr["nome"].ToString();
                usuario.Sobrenome = dr["sobrenome"].ToString();
                usuario.Email = dr["email"].ToString();
                usuario.DataNascimento = Convert.ToDateTime(dr["dataNascimento"]);
                usuario.Avatar = dr["avatar"].ToString();
                usuario.Perfil = dr["perfil"].ToString();
                usuario.DataModificacao = Convert.ToDateTime(dr["dataModificacao"]);
                usuario.Online = true;
            }

            return usuario;
        }
        #endregion
    }
    #endregion
}