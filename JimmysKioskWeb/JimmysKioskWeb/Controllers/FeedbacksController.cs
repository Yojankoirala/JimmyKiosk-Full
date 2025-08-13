using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JimmysKioskWeb.Models;

namespace JimmysKioskWeb.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbacksController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FeedbacksController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/Feedbacks
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FeedbackModel>>> GetFeedbacks()
        {
            return await _context.Feedbacks.ToListAsync();
        }

        // GET: api/Feedbacks/5
        [HttpGet("{id}")]
        public async Task<ActionResult<FeedbackModel>> GetFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);

            if (feedback == null)
                return NotFound();

            return feedback;
        }

        // POST: api/Feedbacks
        [HttpPost]
        public async Task<ActionResult<FeedbackModel>> PostFeedback(FeedbackModel feedback)
        {
            try
            {
                Console.WriteLine($"📥 Incoming Feedback: {feedback.CustomerName} - {feedback.Message}");

                _context.Feedbacks.Add(feedback);
                await _context.SaveChangesAsync();

                Console.WriteLine("✅ Feedback saved.");
                return CreatedAtAction(nameof(GetFeedback), new { id = feedback.Id }, feedback);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Feedback save failed: {ex.Message}");
                return BadRequest(new { error = ex.Message });
            }
        }

        // PUT: api/Feedbacks/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFeedback(int id, FeedbackModel feedback)
        {
            if (id != feedback.Id)
                return BadRequest();

            _context.Entry(feedback).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FeedbackExists(id))
                    return NotFound();
                else
                    throw;
            }

            return NoContent();
        }

        // DELETE: api/Feedbacks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFeedback(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback == null)
                return NotFound();

            _context.Feedbacks.Remove(feedback);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FeedbackExists(int id) =>
            _context.Feedbacks.Any(e => e.Id == id);
    }
}
