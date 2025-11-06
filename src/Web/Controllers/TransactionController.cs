using Microsoft.AspNetCore.Mvc;
using SpendWise.Core.DTOs;
using SpendWise.Web.Services;


// Controlador para gestionar transacciones
// Proporciona endpoints para operaciones CRUD utilizando TransactionService 


namespace SpendWise.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionService _transactionService;

        public TransactionController(TransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // api/Transaction
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var transactions = await _transactionService.GetAllAsync();
            return Ok(transactions);
        }

        // api/Transaction/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var transaction = await _transactionService.GetByIdAsync(id);
            if (transaction == null)
                return NotFound($"No se encontró la transacción con ID {id}");

            return Ok(transaction);
        }

        // api/Transaction/user/{userId}
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetByUserId(int userId)
        {
            var transactions = await _transactionService.GetByUserIdAsync(userId);
            return Ok(transactions);
        }

        // api/Transaction
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] TransactionDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            await _transactionService.AddAsync(dto);
            return Ok("Transacción creada correctamente.");
        }

        // api/Transaction/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] TransactionDto dto)
        {
            var updated = await _transactionService.UpdateAsync(id, dto);
            if (!updated)
                return NotFound($"No se encontró la transacción con ID {id}");

            return Ok("Transacción actualizada correctamente.");
        }

        // api/Transaction/{id}
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
