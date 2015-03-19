using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcDocs.Models
{
    #region Projeto
    public class Projeto
    {
        private int entidadeID;
        private int gerenteID;
        private string gerenteNome;
        private string nomeProjeto;
        private DateTime dataInicio;
        private DateTime dataTermino;
        private string statusProjeto;
        private int horas;
        private DateTime dataModificacao;

        public int EntidadeID { get { return entidadeID; } set { entidadeID = value; } }
        public int GerenteID { get { return gerenteID; } set { gerenteID = value; } }
        public string GerenteNome { get { return gerenteNome; } set { gerenteNome = value; } }
        public string NomeProjeto { get { return nomeProjeto; } set { nomeProjeto = value; } }
        public DateTime DataInicio { get { return dataInicio; } set { dataInicio = value; } }
        public DateTime DataTermino { get { return dataTermino; } set { dataTermino = value; } }
        public string StatusProjeto { get { return statusProjeto; } set { statusProjeto = value; } }
        public int Horas { get { return horas; } set { horas = value; } }
        public DateTime DataModificacao { get { return dataModificacao; } set { dataModificacao = value; } }
    }
    #endregion
}