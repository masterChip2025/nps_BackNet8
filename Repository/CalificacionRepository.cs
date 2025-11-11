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

    // public bool AlreadyRespondedAsync(string userName)
    // {
    //     return _db.Calificaciones.Any(c => c.UserName == userName);
    // }

    public async Task AddAsync(Calificacion calificacion)
    {
        _db.Calificaciones.Add(calificacion);
        await _db.SaveChangesAsync();
    }

    public async Task<bool> AlreadyRespondedAsync(string userName)
    {
        return await _db.Calificaciones.AnyAsync(c => c.UserName == userName);
    }
}
