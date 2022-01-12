using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalMVC.Models;
using ProjetoFinalMVC.Models.ViewModels;

namespace ProjetoFinalMVC.Services
{
    public class ConsultasService
    {
        private readonly Contexto _contexto;
        public ConsultasService(Contexto contexto)// injeção de dependência
        {
            _contexto = contexto;
        }
        public async Task<List<Consulta>> EncontrarDataAsync(DateTime? minDate, DateTime? maxDate)// método para realizar a busca genérica das consultas
        {
            var resultado = from obj in _contexto.Consulta select obj;
            if (minDate.HasValue)
            {
                resultado = resultado.Where(x => x.Data >= minDate.Value);
            }
            if (maxDate.HasValue)
            {
                resultado = resultado.Where(x => x.Data <= maxDate.Value);
            }

            return await resultado
                .Include(x => x.Doutor)
                .Include(x => x.Doutor.Especializacao)
                .OrderByDescending(x => x.Data)
                .ToListAsync();
        }
        public async Task InserirConsultaAsync(Consulta consulta)// método para cadastrar a consulta na db
        {
            _contexto.Add(consulta);
            await _contexto.SaveChangesAsync();
        }
        public async Task<Consulta> EncontrarIdAsync(int id)// método para encontrar a consulta pelo id
        {
            return await _contexto.Consulta.Include(d => d.Doutor).FirstOrDefaultAsync(d => d.Id == id);
        }
        public async Task AtualizarConsultaAsync(Consulta consulta)// método que atualiza a consulta
        {
            bool hasAny = await _contexto.Consulta.AnyAsync(x => x.Id == consulta.Id);

            try
            {
                _contexto.Update(consulta);
                await _contexto.SaveChangesAsync();
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public async Task RemoverConsultaAsync(int id)
        {
            try
            {
                var consulta = await _contexto.Consulta.FindAsync(id);

                _contexto.Consulta.Remove(consulta);

                await _contexto.SaveChangesAsync();
            }
            catch (ApplicationException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        public async Task<Dictionary<Especializacao, List<Consulta>>> EncontrarGrupoAsync(DateTime? minDate, DateTime? maxDate) // método para agrupar as consultas por especializações
        {

            var consultas = await EncontrarDataAsync(minDate, maxDate);
            var especializacoes = await _contexto.Especializacao.ToListAsync();

            var grupo = new Dictionary<Especializacao, List<Consulta>>();

            especializacoes.ForEach(e =>
            {
                var consulta = consultas.Where(c => c.Doutor.Especializacao == e).ToList();
                grupo.Add(e, consulta);
            });

            return grupo;
        }
    }
}