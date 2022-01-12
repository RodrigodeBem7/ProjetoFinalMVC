using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjetoFinalMVC.Models;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalMVC.Services.Exceptions;

namespace ProjetoFinalMVC.Services
{
    //Classe responsável pelas operações da classe e implementação das regras de negócio
    public class DoutorService
    {
        private readonly Contexto _contexto;
        public DoutorService(Contexto contexto)// injeção de dependência
        {
            _contexto = contexto;
        }
        public async Task<List<Doutor>> RetornaDoutoresAsync()// método que retorna os doutores cadastrados no banco
        {
            return await _contexto.Doutor.OrderBy(dr=> dr.Nome).ToListAsync();
        }
        public async Task InserirDoutorAsync(Doutor dr)// método para registrar o doutor na DB
        {
            _contexto.Add(dr);
            await _contexto.SaveChangesAsync();
        }
        public async Task<Doutor> EncontrarIdAsync(int id)// método para encontrar o dr pelo id que será deletado da DB
        {
            return await _contexto.Doutor.Include(dr => dr.Especializacao).FirstOrDefaultAsync(dr => dr.Id == id);
        }
        public async Task RemoverDoutorAsync(int id) // método para excluir o doutor da DB
        {
            try
            {
                var dr = await _contexto.Doutor.FindAsync(id);


                await _contexto.Doutor.Include(d => d.Consultas).FirstOrDefaultAsync(d => d.Id == dr.Id);

                _contexto.Doutor.Remove(dr);

                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateException e)
            {
                throw new IntegrityException(e.Message);
            }
        }
        public async Task AtualizarDoutorAsync(Doutor dr) // método que atualiza a classe
        {
            bool hasAny = await _contexto.Doutor.AnyAsync(x => x.Id == dr.Id);

            if (!hasAny)// verifica se o id do dr informado existe na DB
            {
                throw new NotFoundException("Id não encontrado na DB");
            }

            try
            {
                _contexto.Update(dr);
                await _contexto.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
        }
    }
}