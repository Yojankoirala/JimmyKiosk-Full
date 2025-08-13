using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JimmysKioskWeb.Models;

namespace JimmysKioskWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly AppDbContext _context;

        public OrdersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderModel>>> GetOrders()
        {
            return await _context.Orders.ToListAsync();
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<OrderModel>> GetOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            return order;
        }

        // POST: api/Orders
        // 🔻 START: Enhanced PostOrder method with debug logging
        [HttpPost]
        public async Task<ActionResult<OrderModel>> PostOrder(OrderModel orderModel)
        {
            try
            {
                Console.WriteLine($"📥 Incoming Order: {orderModel.ItemsSummary}, {orderModel.TotalAmount}");

                _context.Orders.Add(orderModel);
                await _context.SaveChangesAsync();

                Console.WriteLine("✅ Order saved successfully.");
                return CreatedAtAction(nameof(GetOrder), new { id = orderModel.Id }, orderModel);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Order save failed: {ex.Message}");
                return BadRequest(new { error = ex.Message });
            }
        }
        // 🔺 END: Enhanced PostOrder method

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(int id, OrderModel order)
        {
            if (id != order.Id)
                return BadRequest();

            _context.Entry(order).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
                return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id) =>
            _context.Orders.Any(e => e.Id == id);
    }
}
