using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDocs.Models;

namespace MvcDocs.Controllers
{
    #region Base Controller
    public class BaseController : Controller
    {
        #region Create Session
        public void CreateUserSession(Usuario ObjUser)
        {
            System.Web.HttpContext.Current.Session.Add("ObjUserSession", ObjUser);
        }
        #endregion

        #region Get Method
        public Usuario GetUserInfo
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["ObjUserSession"] == null || string.IsNullOrEmpty(System.Web.HttpContext.Current.Session["ObjUserSession"].ToString()))
                {
                    return GetUserInfo;
                }

                else
                {
                    return (Usuario)Session["ObjUserSession"];
                }
            }
        }
        #endregion
    }
    #endregion
}
