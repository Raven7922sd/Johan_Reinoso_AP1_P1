using Johan_Reinoso_AP1_P1.Components.Models;
using Microsoft.EntityFrameworkCore;

namespace Johan_Reinoso_AP1_P1.Components.DAL;

public class Contexto:DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public DbSet<Aportes>Aportes{get; set; }
}
