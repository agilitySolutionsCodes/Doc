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
            SqlCommand sqlCmd = new SqlCommand("SELECT U.entidadeID, U.nome, U.sobrenome, U.email, U.dataNascimento, U.avatar, S.senhaHash, P.nome as perfil, " + 
                                               "U.dataModificacao FROM usuario U JOIN senha S ON U.entidadeID = S.entidadeID JOIN perfil P ON U.entidadeID = P.entidadeID " + 
                                               "WHERE U.email = '" + mEmail + "' AND S.senhaHash = '" + mSenha + "'", sqlCon);
            sqlCmd.CommandTimeout = sqlCon.ConnectionTimeout;

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