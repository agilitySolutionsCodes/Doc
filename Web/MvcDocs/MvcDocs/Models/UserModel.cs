using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDocs.Models
{
    #region UserModel
    public class UserModel
    {
        #region Connection
        private Connection connection;
        #endregion

        #region Métodos
        public User modelAuthenticateUser(string mEmail, string mPassword)
        {
            User user = new User();
            connection = new Connection();
            SqlConnection sqlCon = connection.OpenConnection();
            SqlCommand sqlCmd = new SqlCommand("stp_AutenticarUsuario", sqlCon);
            sqlCmd.CommandTimeout = sqlCon.ConnectionTimeout;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add(new SqlParameter("@P_Email", mEmail));
            sqlCmd.Parameters.Add(new SqlParameter("@P_Senha", mPassword));

            SqlDataReader dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                user.EntityID = Convert.ToInt32(dr["entidadeID"]);
                user.Name = dr["nome"].ToString();
                user.LastName = dr["sobrenome"].ToString();
                user.Email = dr["email"].ToString();
                user.BirthDate = Convert.ToDateTime(dr["dataNascimento"]);
                user.Avatar = dr["avatar"].ToString();
                user.ProfileCode = dr["perfil"].ToString();
                user.ModifiedDate = Convert.ToDateTime(dr["dataModificacao"]);
                user.Online = true;
            }

            return user;
        }

        public User modelRegisterUser(User userModel)
        {
            connection = new Connection();
            SqlConnection sqlCon = connection.OpenConnection();
            SqlCommand sqlCmd = new SqlCommand("stp_RegistraNovoUsuario", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = sqlCon.ConnectionTimeout;
            sqlCmd.Parameters.Add(new SqlParameter("@P_Nome", userModel.Name));
            sqlCmd.Parameters.Add(new SqlParameter("@P_Sobrenome", userModel.LastName));
            sqlCmd.Parameters.Add(new SqlParameter("@P_Email", userModel.Email));
            sqlCmd.Parameters.Add(new SqlParameter("@P_DataNascimento", userModel.BirthDate));
            sqlCmd.Parameters.Add(new SqlParameter("@P_Avatar", userModel.Avatar));
            sqlCmd.Parameters.Add(new SqlParameter("@P_PerfilUsuario", userModel.Profile.ToString()));
            sqlCmd.Parameters.Add(new SqlParameter("@P_PerfilCodigoUsuario", userModel.ProfileCode));
            sqlCmd.Parameters.Add(new SqlParameter("@P_SenhaHash", userModel.PasswordHash));
            sqlCmd.Parameters.Add("@P_IdUsuario", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("@P_UsuarioOnline", SqlDbType.Bit).Direction = ParameterDirection.Output;

            sqlCmd.ExecuteNonQuery();

            userModel.EntityID = Convert.ToInt32(sqlCmd.Parameters["@P_IdUsuario"].Value);
            userModel.Online = Convert.ToBoolean(sqlCmd.Parameters["@P_UsuarioOnline"].Value);

            return userModel;
        }

        public Boolean modelCheckEmailsExists(string mEmail)
        {
            bool emailExist = false;
            connection = new Connection();
            SqlConnection sqlCon = connection.OpenConnection();
            SqlCommand sqlCmd = new SqlCommand("stp_CheckEmailExist", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = sqlCon.ConnectionTimeout;

            sqlCmd.Parameters.Add(new SqlParameter("@P_Email", mEmail));
            SqlDataReader dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                if (dr["Email"].ToString() != "")
                {
                    emailExist = true;    
                }
            }

            return emailExist;
        }

        #endregion
    }
    #endregion
}