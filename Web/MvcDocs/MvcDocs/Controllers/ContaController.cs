using System;
using System.IO;
using System.Text;
using System.Linq;
using System.Web.Security;
using System.Collections.Generic;
using System.Security.Cryptography;
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
                bool cLembrar = false;

                if (Fcollection["Lembrar"] != null && Convert.ToBoolean(Fcollection["Lembrar"].Contains("true")))
                { cLembrar = true; }

                cSenha = CriptografarSenha(cSenha);

                UsuarioModel usuarioModel = new UsuarioModel();
                Usuario usuario = usuarioModel.AutenticaUsuario(cEmail, cSenha);
                if (usuario.Online == true)
                {
                    CriaSessionUsuario(usuario);
                    System.Web.Security.FormsAuthentication.SetAuthCookie(usuario.SenhaHash + usuario.EntidadeID, cLembrar);
                }
            }

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            System.Web.Security.FormsAuthentication.SignOut();

            return View("Login");
        }

        public ActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Registrar(FormCollection Fcollection)
        {
            if (Fcollection != null)
            {
                //Obj usuário
                Usuario usuario = CriaUsuario(Fcollection);
                //Model
                UsuarioModel usuarioModel = new UsuarioModel();
                usuario = usuarioModel.RegistrarUsuario(usuario);
                //Upload Arquivo
                if (Request.Files["file"].ContentLength > 0)
                {
                    HttpPostedFileBase arquivo = Request.Files["file"];
                    SaveUploadedFile(arquivo);
                }
            }

            return Autenticar(Fcollection);
        }

        public ActionResult Details(int id)
        {
            return View();
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

        protected Usuario CriaUsuario(FormCollection collection)
        {
            Usuario usuario = new Usuario();
            usuario.Nome = collection["Nome"];
            usuario.Sobrenome = collection["Sobrenome"];
            usuario.Email = collection["Email"];
            usuario.DataNascimento = Convert.ToDateTime(collection["DataNascimento"]);
            usuario.SenhaHash = CriptografarSenha(collection["ConfirmacaoSenha"]);
            usuario.Perfil = ((Usuario.Perfis)Convert.ToInt32(collection["Perfil"]));
            usuario.PerfilCodigo = collection["Perfil"];
            usuario.Avatar = Request.Files["file"].FileName;
            return usuario;
        }

        public ActionResult SaveUploadedFile(HttpPostedFileBase arquivo)
        {
            bool isSavedSuccessfully = true;
            string fName = "";
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[fileName];
                //Save file content goes here
                fName = file.FileName;
                if (file != null && file.ContentLength > 0)
                {
                    var originalDirectory = new DirectoryInfo(string.Format("{0}Content\\Uploads", Server.MapPath(@"\")));
                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "Avatar");
                    var fileName1 = Path.GetFileName(file.FileName);

                    bool isExists = System.IO.Directory.Exists(pathString);
                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);
                    var path = string.Format("{0}\\{1}", pathString, file.FileName);
                    file.SaveAs(path);
                }
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = fName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
        #endregion
    }
}
