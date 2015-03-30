using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace MvcDocs.Models
{
    #region User
    public class User
    {
        private int entityID;
        private string name;
        private string lastName;
        private string email;
        private DateTime birthDate;
        private string avatar;
        private string passwordHash;
        public enum Profiles
        {
            Selecione,
            [Description("Gerente de Projetos")]
            Gerente = 1,
            [Description("Controlador de Documentações")]
            Controlador = 2,
            [Description("Usuário padrão")]
            Usuario = 3
        };
        private Profiles profile;
        private string profileCode;
        private DateTime modifiedDate;
        private bool online;

        public int EntityID { get { return entityID; } set { entityID = value; } }
        public string Name { get { return name; } set { name = value; } }
        public string LastName { get { return lastName; } set { lastName = value; } }
        public string Email { get { return email; } set { email = value; } }
        public DateTime BirthDate { get { return birthDate; } set { birthDate = value; } }
        public string Avatar { get { return avatar; } set { avatar = value; } }
        public string PasswordHash { get { return passwordHash; } set { passwordHash = value; } }
        public Profiles Profile { get { return profile; } set { profile = value; } }
        public string ProfileCode { get { return profileCode; } set { profileCode = value; } }
        public DateTime ModifiedDate { get { return modifiedDate; } set { modifiedDate = value; } }
        public bool Online { get { return online; } set { online = value; } }
    }
    #endregion
}