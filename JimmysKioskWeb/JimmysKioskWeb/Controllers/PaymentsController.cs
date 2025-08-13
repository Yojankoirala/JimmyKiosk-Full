using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JimmysKioskWeb;        // or wherever your AppDbContext is
using JimmysKioskWeb.Models;

namespace JimmysKioskWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public PaymentsController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentModel>>> GetPayments()
        {
            return await _context.Payments.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentModel>> GetPaymentModel(int id)
        {
            var paymentModel = await _context.Payments.FindAsync(id);
            if (paymentModel == null)
                return NotFound();

            return paymentModel;
        }

        [HttpPost]
        public async Task<ActionResult<PaymentModel>> PostPaymentModel(PaymentModel paymentModel)
        {
            _context.Payments.Add(paymentModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPaymentModel), new { id = paymentModel.Id }, paymentModel);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentModel(int id, PaymentModel paymentModel)
        {
            if (id != paymentModel.Id)
                return BadRequest();

            _context.Entry(paymentModel).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePaymentModel(int id)
        {
            var payment = await _context.Payments.FindAsync(id);
            if (payment == null)
                return NotFound();

            _context.Payments.Remove(payment);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaymentModelExists(int id) =>
            _context.Payments.Any(e => e.Id == id);
    }
}
