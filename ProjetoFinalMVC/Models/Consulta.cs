using System;
using System.ComponentModel.DataAnnotations;

namespace ProjetoFinalMVC.Models
{
    public class Consulta
    {
        public int Id { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString ="{0:dd/MM/yyyy}")]
        public DateTime Data { get; set; }

        [Required]
        [DisplayFormat(DataFormatString ="{0:F2}")]
        public double Valor { get; set; }

        public Doutor Doutor { get; set; }
        public int DoutorId { get; set; }
        public Consulta()
        {
        }
        public Consulta(int id, DateTime data, double valor, Doutor doutor)
        {
            Id = id;
            Data = data;
            Valor = valor;
            Doutor = doutor;
        }
    }
}