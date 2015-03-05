using System;
using System.Text;
using System.Linq;
using System.Web.Security;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDocs.Models;

namespace MvcDocs.Controllers
{
    public class ContaController : BaseController
    {
        #region Objetos Criptografia
        TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider md5Crypto = new MD5CryptoServiceProvider();
        //Chave para criptografia
        String Chave = "AgilityWD";
        #endregion

        #region Actions
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Autenticar(FormCollection Fcollection)
        {
            if (Fcollection["Email"] != null && Fcollection["Senha"] != null)
            {
                string cEmail = Fcollection["Email"].ToString();
                string cSenha = Fcollection["Senha"].ToString();
                cSenha = CriptografarSenha(cSenha);

                UsuarioModel usuarioModel = new UsuarioModel();
                Usuario usuario = usuarioModel.AutenticaUsuario(cEmail, cSenha);
                if (usuario.Online == true)
                {
                    CriaSessionUsuario(usuario);
                    System.Web.Security.FormsAuthentication.SetAuthCookie(usuario.SenhaHash + usuario.EntidadeID, false);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult Registrar()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Registrar(FormCollection Fcollection)
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        #endregion

        #region Métodos
        protected String CriptografarSenha(string senha)
        {
            des.Key = md5Crypto.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Chave));
            des.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = des.CreateEncryptor();
            ASCIIEncoding MyASCIIEncoding = new ASCIIEncoding();
            var buff = ASCIIEncoding.ASCII.GetBytes(senha);
            senha = Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));

            return senha;
        }
        #endregion
    }
}
