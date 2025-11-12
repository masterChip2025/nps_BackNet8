using System;

namespace ApiEcommerce.Models;

public class Calificacion
{
    public int Id { get; set; }
    public int CalificacionValor { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public DateTime FechaCreacion { get; set; }
}
