using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpendWise.Web.Services;

namespace SpendWise.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionSummaryController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionSummaryController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Suma total de todas las transacciones (ingresos/gastos).
        [HttpGet("total")]
        public async Task<IActionResult> GetTotalAmount()
        {
            var transactions = await _transactionService.GetByUserIdAsync();
            if (transactions == null)
                return NotFound("No hay transacciones registradas.");

            var total = transactions.Sum(t =>
                t.Type.Equals("Gasto", StringComparison.OrdinalIgnoreCase) ? -t.Amount : t.Amount
            );

            return Ok(new
            {
                total,
                cantidad = transactions.Count(),
                mensaje = "Suma total de todas las transacciones (ingresos positivos, gastos negativos)."
            });
        }

        // Devuelve la suma separada de ingresos y gastos.
        [HttpGet("resumen")]
        public async Task<IActionResult> GetIncomeExpenseSummary()
        {
            var transactions = await _transactionService.GetByUserIdAsync();
            if (transactions == null)
                return NotFound("No hay transacciones registradas.");

            var totalIngresos = transactions
                .Where(t => t.Type.Equals("Ingreso", StringComparison.OrdinalIgnoreCase))
                .Sum(t => t.Amount);

            var totalGastos = transactions
                .Where(t => t.Type.Equals("Gasto", StringComparison.OrdinalIgnoreCase))
                .Sum(t => t.Amount);

            var balance = totalIngresos - totalGastos;

            return Ok(new
            {
                totalIngresos,
                totalGastos,
                balance,
                mensaje = "Resumen general de ingresos y gastos."
            });
        }
    }
}
