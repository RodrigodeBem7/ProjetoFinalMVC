using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ProjetoFinalMVC.Models;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options)
        : base(options)
    {
    }
    //DB Set de cada entidade presente no projeto
    public DbSet<Especializacao> Especializacao { get; set; }
    public DbSet<Doutor> Doutor { get; set; }
    public DbSet<Consulta> Consulta { get; set; }
}