using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoFinalMVC.Models.ViewModels
{
    public class ConsultFormViewModel
    {
        //Classe que contém os dados para o cadastro das consultas
        public Consulta consulta { get; set; }
        public ICollection<Doutor> Doutores { get; set; }
    }
}
