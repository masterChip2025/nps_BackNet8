using System;
using ApiEcommerce.Repository.IRepository;
using ApiEcommerce.Models;

namespace ApiEcommerce.Repository;

public class CalificacionRepository: ICalificacionRepository
{
    
    private readonly ApplicationDbContext _db;

    public CalificacionRepository(ApplicationDbContext context)
    {
        _db = context;
    }

	public bool AlreadyRespondedAsync(string userName)
    {
        return _db.Calificaciones.Any(c => c.UserName == userName);
    }

    public async Task AddAsync(Calificacion calificacion)
    {
        _db.Calificaciones.Add(calificacion);
        await _db.SaveChangesAsync();
    }
}
