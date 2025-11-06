using System.ComponentModel.DataAnnotations;
using SpendWise.Core.Entities;

namespace SpendWise.Web.Models.Requests
{
    public record CreateTransactionRequest(
        [Required(ErrorMessage = "El monto es obligatorio.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "El monto debe ser mayor a 0.")]
        decimal Amount,

        [Required(ErrorMessage = "El tipo de transaccion es obligatorio (Ingreso o Gasto).")]
        string Type,

        [Required(ErrorMessage = "La categoria es obligatoria.")]
        Category Category,

        [Required(ErrorMessage = "La fecha es obligatoria.")]
        DateTime Date,

        string? Description = null
    );
}
