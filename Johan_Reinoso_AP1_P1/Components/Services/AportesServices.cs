using Johan_Reinoso_AP1_P1.Components.DAL;
using Johan_Reinoso_AP1_P1.Components.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Johan_Reinoso_AP1_P1.Components.Services;

public class AportesServices(IDbContextFactory<Contexto>DbFactory)
{
  
   public async Task<bool> Guardar(Aportes aportes)
    {
        if (!await ExisteAporteId(aportes.AporteId))
        { return (await InsertarAporte(aportes)); }

        else { return await ModificarAporte(aportes); }
    }

    public async Task<bool> ExisteAporteId(int aporteId)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        return await context.Aportes.AnyAsync(a => a.AporteId == aporteId);
    }

    public async Task<bool> ExisteAporteNombre(string Nombre)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        return await context.Aportes.AnyAsync(n => n.Nombres.ToLower() == Nombre.ToLower());
    }

    public async Task<bool> InsertarAporte(Aportes aportes)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        context.Aportes.Add(aportes);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> ModificarAporte(Aportes aporte)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        context.Aportes.Update(aporte);
        return await context.SaveChangesAsync() > 0;
    }

    public async Task<bool> Eliminar(int aporteId)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        return await context.Aportes.AsNoTracking().Where(a => a.AporteId == aporteId).ExecuteDeleteAsync() > 0;
    }

    public async Task<Aportes?> Buscar(int aporteId)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        return await context.Aportes.FirstOrDefaultAsync(a => a.AporteId == aporteId);
    }

    public async Task<List<Aportes>> Listar(Expression<Func<Aportes, bool>> criterio)
    {
        await using var context = await DbFactory.CreateDbContextAsync();
        return await context.Aportes.Where(criterio).AsNoTracking().ToListAsync();
    }

    public async Task<List<Aportes>> BuscarFiltradosAsync(
    string filtroCampo,
    string valorFiltro,
    DateTime? fechaDesde,
    DateTime? fechaHasta)
    {
        await using var context = await DbFactory.CreateDbContextAsync();

        IQueryable<Aportes> query = context.Aportes.AsNoTracking();

        if (!string.IsNullOrWhiteSpace(valorFiltro))
        {
            var valor = valorFiltro.ToLower();

            switch (filtroCampo)
            {
                case "AporteId":
                    if (int.TryParse(valorFiltro, out var aporteId))
                        query = query.Where(a => a.AporteId == aporteId);
                    break;

                case "Nombre":
                    query = query.Where(a => a.Nombres.ToLower().Contains(valor));
                    break;

                case "Monto":
                    if (double.TryParse(valorFiltro, out var monto))
                        query = query.Where(m => m.Monto == monto);
                    break;
            }
        }

        if (fechaDesde.HasValue)
            query = query.Where(f => f.Fecha >= fechaDesde.Value);

        if (fechaHasta.HasValue)
            query = query.Where(f => f.Fecha <= fechaHasta.Value);

        return await query.OrderBy(f => f.Fecha).ToListAsync();
    }
}