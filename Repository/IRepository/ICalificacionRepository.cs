using System;
using ApiEcommerce.Models;

namespace ApiEcommerce.Repository.IRepository;

public interface ICalificacionRepository
{
    // bool AlreadyRespondedAsync(string userName);

    Task AddAsync(Calificacion calificacion);

    Task<bool> AlreadyRespondedAsync(string userName);
}
