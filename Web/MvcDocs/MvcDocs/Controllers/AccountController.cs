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
    #region AccountController
    public class AccountController : BaseController
    {
        #region Objects Crypto
        TripleDESCryptoServiceProvider des = new TripleDESCryptoServiceProvider();
        MD5CryptoServiceProvider md5Crypto = new MD5CryptoServiceProvider();
        //Key
        String Key = "AgilityWD";
        #endregion

        #region Actions
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public JsonResult AuthenticateUser(FormCollection Fcollection)
        {
            User ObjUser = new User();
            if (Fcollection["Login"] != null && Fcollection["Password"] != null)
            {
                string cEmail = Fcollection["Login"].ToString();
                string cPassword = Fcollection["Password"].ToString();
                bool cRemindMe = false;

                if (Fcollection["RemindMe"] != null && Convert.ToBoolean(Fcollection["RemindMe"].Contains("true")))
                { cRemindMe = true; }

                cPassword = CryptographyPassword(cPassword);

                UserModel ObjUserModel = new UserModel();

                ObjUser = ObjUserModel.modelAuthenticateUser(cEmail, cPassword);
                if (ObjUser.Online == true)
                {
                    CreateUserSession(ObjUser);
                    System.Web.Security.FormsAuthentication.SetAuthCookie(ObjUser.PasswordHash + ObjUser.EntityID, cRemindMe);
                }
            }

            RedirectToRoute("Authenticate");
            return Json(ObjUser.Online);
            
            //return RedirectToRoute("Authenticate");
            //return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            System.Web.Security.FormsAuthentication.SignOut();

            return View("Login");
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(FormCollection Fcollection)
        {
            if (Fcollection != null)
            {
                //Create new object user
                User ObjUser = CreateUser(Fcollection);
                //New object Model
                UserModel ObjUserModel = new UserModel();
                ObjUser = ObjUserModel.modelRegisterUser(ObjUser);
                //Upload File
                if (Request.Files["file"].ContentLength > 0)
                {
                    HttpPostedFileBase ObjFile = Request.Files["file"];
                    SaveUploadedFile(ObjFile);
                }
            }

            return AuthenticateUser(Fcollection);
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

        #region Methods
        protected String CryptographyPassword(string password)
        {
            des.Key = md5Crypto.ComputeHash(ASCIIEncoding.ASCII.GetBytes(Key));
            des.Mode = CipherMode.ECB;
            ICryptoTransform desdencrypt = des.CreateEncryptor();
            ASCIIEncoding MyASCIIEncoding = new ASCIIEncoding();
            var buff = ASCIIEncoding.ASCII.GetBytes(password);
            password = Convert.ToBase64String(desdencrypt.TransformFinalBlock(buff, 0, buff.Length));

            return password;
        }

        protected User CreateUser(FormCollection collection)
        {
            User user = new User();
            user.Name = collection["Name"];
            user.LastName = collection["LastName"];
            user.Email = collection["Email"];
            user.BirthDate = Convert.ToDateTime(collection["BirthDate"]);
            user.PasswordHash = CryptographyPassword(collection["ConfirmPassword"]);
            user.Profile = ((User.Profiles)Convert.ToInt32(collection["Profile"]));
            user.ProfileCode = collection["Perfil"];
            user.Avatar = Request.Files["File"].FileName;
            return user;
        }

        public JsonResult CheckEmailExist(string cEmail)
        {
            bool cOk = false;
            UserModel userModel = new UserModel();
            cOk = userModel.modelCheckEmailsExists(cEmail);

            return Json(cOk);
        }

        public ActionResult SaveUploadedFile(HttpPostedFileBase file)
        {
            bool isSavedSuccessfully = true;
            string sFileName = "";
            foreach (string fileName in Request.Files)
            {
                HttpPostedFileBase Objfile = Request.Files[fileName];
                //Save file content goes here
                sFileName = Objfile.FileName;
                if (Objfile != null && Objfile.ContentLength > 0)
                {
                    var originalDirectory = new DirectoryInfo(string.Format("{0}Uploads", Server.MapPath(@"\")));
                    string pathString = System.IO.Path.Combine(originalDirectory.ToString(), "Avatar");
                    var fileNameOne = Path.GetFileName(Objfile.FileName);

                    bool isExists = System.IO.Directory.Exists(pathString);
                    if (!isExists)
                        System.IO.Directory.CreateDirectory(pathString);
                    var path = string.Format("{0}\\{1}", pathString, Objfile.FileName);
                    Objfile.SaveAs(path);
                }
            }

            if (isSavedSuccessfully)
            {
                return Json(new { Message = sFileName });
            }
            else
            {
                return Json(new { Message = "Error in saving file" });
            }
        }
        #endregion
    }
    #endregion
}
