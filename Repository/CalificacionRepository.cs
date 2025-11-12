using System;
using ApiEcommerce.Models;
using ApiEcommerce.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace ApiEcommerce.Repository;

public class CalificacionRepository : ICalificacionRepository
{
    private readonly ApplicationDbContext _db;

    public CalificacionRepository(ApplicationDbContext context)
    {
        _db = context;
    }

    public async Task AddAsync(Calificacion calificacion)
    {
        _db.Calificaciones.Add(calificacion);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> AlreadyRespondedAsync(string userName)
    {
        return await _db.Calificaciones.AnyAsync(c => c.UserName == userName);
    }

    public async Task<object> ObtenerEstadisticasAsync()
    {
        var total = await _db.Calificaciones.CountAsync();

        if (total == 0)
        {
            return new
            {
                total = 0,
                promotores = 0,
                neutros = 0,
                detractores = 0,
                nps = 0,
            };
        }

        var promotores = await _db.Calificaciones.CountAsync(c => c.Categoria == "Promotor");
        var neutros = await _db.Calificaciones.CountAsync(c => c.Categoria == "Neutro");
        var detractores = await _db.Calificaciones.CountAsync(c => c.Categoria == "Detractor");

        var nps = ((double)(promotores - detractores) / total) * 100;

        return new
        {
            total,
            promotores = promotores * 100.0 / total,
            neutros = neutros * 100.0 / total,
            detractores = detractores * 100.0 / total,
            nps = Math.Round(nps, 2),
        };
    }
}
