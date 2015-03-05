﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDocs.Models
{
    #region Usuario
    public class Usuario
    {
        private int entidadeID;
        private string nome;
        private string sobrenome;
        private string email;
        private DateTime dataNascimento;
        private string avatar;
        private string senhaHash;
        private string perfil;
        private DateTime dataModificacao;
        private bool online;

        public int EntidadeID { get { return entidadeID; } set { entidadeID = value; } }
        public string Nome { get { return nome; } set { nome = value; } }
        public string Sobrenome { get { return sobrenome; } set { sobrenome = value; } }
        public string Email { get { return email; } set { email = value; } }
        public DateTime DataNascimento { get { return dataNascimento; } set { dataNascimento = value; } }
        public string Avatar { get { return avatar; } set { avatar = value; } }
        public string SenhaHash { get { return senhaHash; } set { senhaHash = value; } }
        public string Perfil { get { return perfil; } set { perfil = value; } }
        public DateTime DataModificacao { get { return dataModificacao; } set { dataModificacao = value; } }
        public bool Online { get { return online; } set { online = value; } }

        public Usuario()
        {
            this.EntidadeID = 0;
            this.Nome = "";
            this.Sobrenome = "";
            this.Email = "";
            this.DataNascimento = DateTime.Now;
            this.Avatar = "";
            this.SenhaHash = "";
            this.Perfil = "";
            this.DataModificacao = DateTime.Now;
            this.online = false;
        }
    }
    #endregion 
}