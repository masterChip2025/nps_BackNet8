using System;
using System.ComponentModel.DataAnnotations;

namespace ApiEcommerce.Models.Dtos.Calificacion;

public class CalificacionDto
{
    [Required]
    [Range(1, 10, ErrorMessage = "La calificación debe estar entre 1 y 10.")]
    public int CalificacionValor { get; set; }
}
