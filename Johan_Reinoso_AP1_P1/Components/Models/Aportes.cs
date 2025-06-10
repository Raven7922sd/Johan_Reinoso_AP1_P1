using System.ComponentModel.DataAnnotations;

namespace Johan_Reinoso_AP1_P1.Components.Models;

public class Aportes
{
    [Key]
    public int AporteId { get; set; }

    [Required(ErrorMessage = "El campo es obligatorio.")]
    [StringLength(100, ErrorMessage = "El campo no puede exceder los 100 caracteres.")]
    [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "El campo solo puede contener letras y espacios.")]
    public string Nombres { get; set; }=null!;

    [Required(ErrorMessage = "El campo es obligatorio.")]
    [Range(0.1, 1000000, ErrorMessage = "El aporte debe ser un número positivo de 0.1 hasta 1,000,000.00")]
    public double Monto { get; set; }
    public DateTime Fecha { get; set; } = DateTime.Now;
}