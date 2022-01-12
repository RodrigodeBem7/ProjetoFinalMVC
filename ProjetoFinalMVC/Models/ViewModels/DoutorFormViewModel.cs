using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoFinalMVC.Models.ViewModels
{
    public class DoutorFormViewModel
    {
        //Classe que contém os dados para o cadastro dos doutores
        public Doutor Doutor { get; set; }
        public ICollection<Especializacao> Especializacoes { get; set; }
    }
}