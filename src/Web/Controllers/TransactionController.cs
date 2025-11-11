using Microsoft.AspNetCore.Mvc;
using SpendWise.Core.DTOs;
using SpendWise.Web.Services;
using SpendWise.Web.Models.Requests;
using Microsoft.AspNetCore.Authorization;

namespace SpendWise.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // Obtiene todas las transacciones
        [HttpGet("info")]
        public async Task<IActionResult> GetAllTransactionsInfo()
        {
            var transactions = await _transactionService.GetAllAsync();
            if (transactions == null || transactions.Count == 0)
                return NotFound("No hay transacciones registradas.");
            
            return Ok(transactions);
        }

        // Obtiene una transacción por su ID.
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _transactionService.GetByIdAsync(id);
            if (transaction is null)
                return NotFound($"No se encontró la transacción con ID {id}");

            return Ok(transaction);
        }
        // Crea una nueva transacción.
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionDto dto)
        {
            var transaction = await _transactionService.AddAsync(dto);

            return CreatedAtAction(nameof(GetById), new {id = transaction.Id }, transaction);
        }
        
        // Actualiza una transacción existente.
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TransactionDto dto)
        {
            var updated = await _transactionService.UpdateAsync(id, dto);

            if (!updated)
                return NotFound($"No se encontró la transacción con ID {id}");

            return Ok("Transacción actualizada correctamente.");
        }
        // Elimina una transacción por su ID.
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _transactionService.DeleteAsync(id);
            if (!deleted)
                return NotFound($"No se encontró la transacción con ID {id}");

            return Ok("Transacción eliminada correctamente.");
        }

    }
}
