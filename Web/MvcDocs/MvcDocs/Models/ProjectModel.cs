using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDocs.Models
{
    #region Projeto Model
    public class ProjectModel
    {
        #region Connection
        private Connection connection;
        #endregion

        #region Methods

        public Project modelRegisterProject(Project projectModel)
        {
            connection = new Connection();
            SqlConnection sqlCon = connection.OpenConnection();
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.StoredProcedure;
            sqlCmd.CommandTimeout = sqlCon.ConnectionTimeout;

            sqlCmd.Parameters.Add(new SqlParameter("@p_ManagerID", projectModel.ManagerID));
            sqlCmd.Parameters.Add(new SqlParameter("@p_ProjectName", projectModel.ProjectName));
            sqlCmd.Parameters.Add(new SqlParameter("@p_StartDate", projectModel.StartDate));
            sqlCmd.Parameters.Add(new SqlParameter("@p_EndDate", projectModel.EndDate));
            sqlCmd.Parameters.Add(new SqlParameter("@p_ProjectStatus", projectModel.ProjectStatus));
            sqlCmd.Parameters.Add(new SqlParameter("@p_TermHours", projectModel.Hours));
            sqlCmd.Parameters.Add(new SqlParameter("@p_ProjectClassification", projectModel.Classification));
            sqlCmd.Parameters.Add("@p_ProjectID", SqlDbType.Int).Direction = ParameterDirection.Output;

            sqlCmd.ExecuteNonQuery();

            projectModel.EntityID = Convert.ToInt32(sqlCmd.Parameters["@p_ProjectID"].Value);
            
            return projectModel;
        }

        #endregion
    }
    #endregion
}