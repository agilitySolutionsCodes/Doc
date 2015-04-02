using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDocs.Models
{
    #region Project
    public class Project
    {
        private int entityID;
        private int managerID;
        private string managerName;
        private string projectName;
        private DateTime startDate;
        private DateTime endDate;
        private string projectStatus;
        private int hours;
        private string classification;
        private DateTime modifiedDate;

        public int EntityID { get { return entityID; } set { entityID = value; } }
        public int ManagerID { get { return managerID; } set { managerID = value; } }
        public string ManagerName { get { return managerName; } set { managerName = value; } }
        public string ProjectName { get { return projectName; } set { projectName = value; } }
        public DateTime StartDate{ get { return startDate; } set { startDate = value; } }
        public DateTime EndDate { get { return endDate; } set { endDate = value; } }
        public string ProjectStatus { get { return projectStatus; } set { projectStatus = value; } }
        public int Hours { get { return hours; } set { hours = value; } }
        public string Classification { get { return classification; } set { classification = value; } }
        public DateTime ModifiedDate { get { return modifiedDate; } set { modifiedDate = value; } }
    }
    #endregion
}