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
        #region Cria Seção
        public void CriaSessionUsuario(Usuario objUsuario)
        {
            System.Web.HttpContext.Current.Session.Add("objUsuario", objUsuario);
        }
        #endregion

        #region Método Get
        public Usuario GetUsuarioInfo
        {
            get
            {
                if (System.Web.HttpContext.Current.Session["objUsuario"] == null || string.IsNullOrEmpty(System.Web.HttpContext.Current.Session["objUsuario"].ToString()))
                {
                    return GetUsuarioInfo;
                }

                else
                {
                    return (Usuario)Session["objUsuario"];
                }
            }
        }
        #endregion 
    }
    #endregion
}
