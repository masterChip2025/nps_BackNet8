using System;
using ApiEcommerce.Models;

namespace ApiEcommerce.Repository.IRepository;

public interface ICalificacionRepository
{
    Task<object> ObtenerEstadisticasAsync();

    Task AddAsync(Calificacion calificacion);

    Task<bool> AlreadyRespondedAsync(string userName);
}
