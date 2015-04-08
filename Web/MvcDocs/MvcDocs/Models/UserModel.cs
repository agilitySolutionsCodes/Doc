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
            SqlCommand sqlCmd = new SqlCommand("stp_AuthenticateUser", sqlCon);
            sqlCmd.CommandTimeout = sqlCon.ConnectionTimeout;
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.Parameters.Add(new SqlParameter("@p_Email", mEmail));
            sqlCmd.Parameters.Add(new SqlParameter("@p_Password", mPassword));

            SqlDataReader dr = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                user.EntityID = Convert.ToInt32(dr["EntityID"]);
                user.FirstName = dr["FirstName"].ToString();
                user.LastName = dr["LastName"].ToString();
                user.Email = dr["Email"].ToString();
                user.BirthDate = Convert.ToDateTime(dr["BirthDate"]);
                user.Avatar = dr["Avatar"].ToString();
                user.ProfileCode = dr["PersonProfile"].ToString();
                user.ModifiedDate = Convert.ToDateTime(dr["ModifiedDate"]);
                user.Online = true;
            }

            return user;
        }

        public User modelRegisterUser(User userModel)
        {
            connection = new Connection();
            SqlConnection sqlCon = connection.OpenConnection();
            SqlCommand sqlCmd = new SqlCommand("stp_RegisterUser", sqlCon);
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = sqlCon.ConnectionTimeout;
            sqlCmd.Parameters.Add(new SqlParameter("@p_FirstName", userModel.FirstName));
            sqlCmd.Parameters.Add(new SqlParameter("@p_LastName", userModel.LastName));
            sqlCmd.Parameters.Add(new SqlParameter("@p_Email", userModel.Email));
            sqlCmd.Parameters.Add(new SqlParameter("@p_BirthDate", userModel.BirthDate));
            sqlCmd.Parameters.Add(new SqlParameter("@p_Avatar", userModel.Avatar));
            sqlCmd.Parameters.Add(new SqlParameter("@p_UserProfile", userModel.Profile.ToString()));
            sqlCmd.Parameters.Add(new SqlParameter("@p_UserProfileCode", userModel.ProfileCode));
            sqlCmd.Parameters.Add(new SqlParameter("@p_PasswordHash", userModel.PasswordHash));
            sqlCmd.Parameters.Add("@p_UserID", SqlDbType.Int).Direction = ParameterDirection.Output;
            sqlCmd.Parameters.Add("@p_UserOnline", SqlDbType.Bit).Direction = ParameterDirection.Output;

            sqlCmd.ExecuteNonQuery();

            userModel.EntityID = Convert.ToInt32(sqlCmd.Parameters["@p_UserID"].Value);
            userModel.Online = Convert.ToBoolean(sqlCmd.Parameters["@p_UserOnline"].Value);

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

            sqlCmd.Parameters.Add(new SqlParameter("@p_Email", mEmail));
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