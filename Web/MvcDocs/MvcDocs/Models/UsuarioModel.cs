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
                usuario.PerfilCodigo = dr["perfil"].ToString();
                usuario.DataModificacao = Convert.ToDateTime(dr["dataModificacao"]);
                usuario.Online = true;
            }

            return usuario;
        }

        public Usuario RegistrarUsuario(Usuario usuarioModel)
        {
            conexao = new Conexao();
            SqlConnection sqlCon = conexao.AbrirConexao();
            SqlCommand sqlCmd = new SqlCommand("stp_RegistraNovoUsuario", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = sqlCon.ConnectionTimeout;
            sqlCmd.Parameters.Add(new SqlParameter("@P_Nome", usuarioModel.Nome));
            sqlCmd.Parameters.Add(new SqlParameter("@P_Sobrenome", usuarioModel.Sobrenome));
            sqlCmd.Parameters.Add(new SqlParameter("@P_Email", usuarioModel.Email));
            sqlCmd.Parameters.Add(new SqlParameter("@P_DataNascimento", usuarioModel.DataNascimento));
            sqlCmd.Parameters.Add(new SqlParameter("@P_Avatar", usuarioModel.Avatar));
            sqlCmd.Parameters.Add(new SqlParameter("@P_PerfilUsuario", usuarioModel.Perfil));
            sqlCmd.Parameters.Add(new SqlParameter("@P_PerfilCodigoUsuario", usuarioModel.PerfilCodigo));
            sqlCmd.Parameters.Add(new SqlParameter("@P_SenhaHash", usuarioModel.SenhaHash));
            sqlCmd.Parameters.Add("@P_IdUsuario", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("@P_UsuarioOnline", SqlDbType.Bit).Direction = ParameterDirection.Output;

            sqlCmd.ExecuteNonQuery();

            usuarioModel.EntidadeID = Convert.ToInt32(sqlCmd.Parameters["@P_IdUsuario"].Value);
            usuarioModel.Online = Convert.ToBoolean(sqlCmd.Parameters["@P_UsuarioOnline"].Value);

            return usuarioModel;
        }
        #endregion
    }
    #endregion
}