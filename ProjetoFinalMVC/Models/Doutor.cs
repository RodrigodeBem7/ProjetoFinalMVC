using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoFinalMVC.Models
{
    public class Doutor
    {
        public int Id { get; set; }

        [Required]
        [StringLength(60,MinimumLength =3)]
        public string Nome { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required]
        [DataType(DataType.PhoneNumber)]
        public string Telefone { get; set; }

        public Especializacao Especializacao { get; set; }
        public int EspecializacaoId { get; set; }
        public ICollection<Consulta> Consultas { get; set; } = new List<Consulta>();// Lista para armazenar as consultas de cada doutor
        public Doutor()
        {
        }
        public Doutor(int id, string nome, string email, string telefone, Especializacao especializacao)
        {
            Id = id;
            Nome = nome;
            Email = email;
            Telefone = telefone;
            Especializacao = especializacao;
        }
        public void AddConsulta(Consulta consulta)
        {
            Consultas.Add(consulta);
        }
        public void RemoverConsulta(Consulta consulta)
        {
            Consultas.Remove(consulta);
        }
        public int TotalDeConsultas(DateTime dataInicial, DateTime dataFinal)// Método que retorna o total de consultas realizadas pelo Dr. num determinado período
        {
            int contaConsulta = 0;

            //Filtra a lista de consultas e verifica se datas da lista são as mesmas do método, caso forem, o contador é incrementado
            Consultas.Where(c => c.Data >= dataInicial && c.Data <= dataFinal);
            contaConsulta++;

            return contaConsulta;
        }
    }
}