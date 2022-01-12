using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoFinalMVC.Models;
using Microsoft.EntityFrameworkCore;

namespace ProjetoFinalMVC.Services
{
    public class EspecializacaoService
    {
        private readonly Contexto _contexto;
        public EspecializacaoService(Contexto contexto)// injeção de dependência
        {
            _contexto = contexto;
        }
        public async Task <List<Especializacao>> RetornaEspecializacoesAsync()// retorna as especializações ordernadas pelo nome
        {
            return await _contexto.Especializacao.OrderBy(x => x.Nome).ToListAsync();
        }
    }
}
