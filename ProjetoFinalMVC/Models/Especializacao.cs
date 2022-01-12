using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoFinalMVC.Models
{
    public class Especializacao
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }
        public ICollection<Doutor> Doutores { get; set; } = new List<Doutor>();//Lista para armazenar os doutores de cada área de atuação
        public Especializacao()
        {
        }
        public Especializacao(int id, string nome)
        {
            Id = id;
            Nome = nome;
        }
        public void AddDoutor(Doutor doutor)
        {
            Doutores.Add(doutor);
        }

        public int TotalDeConsultas(DateTime dataInicial, DateTime dataFinal)// Método que retorna o total de consultas realizadas por uma especialização num determinado período
        {
            return Doutores.Sum(doutor => doutor.TotalDeConsultas(dataInicial, dataFinal));// Realiza a soma total das consultas dos drs. de uma determinada especialização
        }
    }
}