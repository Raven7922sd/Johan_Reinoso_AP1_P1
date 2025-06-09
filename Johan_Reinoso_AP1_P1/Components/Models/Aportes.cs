using System.ComponentModel.DataAnnotations;

namespace Johan_Reinoso_AP1_P1.Components.Models;

public class Aportes
{
    [Key]
    public int AporteId { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio.")]
    [StringLength(100, ErrorMessage = "El campo no puede exceder los 100 caracteres.")]
    public string Nombres { get; set; }=null!;

    [Required(ErrorMessage = "El campo es obligatorio.")]
    [Range(0, double.MaxValue, ErrorMessage = "El aporte debe ser un número positivo de 0 hasta 1000000.")]
    public double Monto { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
}

