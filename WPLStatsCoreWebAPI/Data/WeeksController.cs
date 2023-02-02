using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPLStatsCoreWebAPI.Models;

namespace WPLStatsCoreWebAPI.Data
{
    [Route("/[controller]")]
    [ApiController]
    public class WeeksController : ControllerBase
    {
        private readonly WPLStatsDbContext _context;

        public WeeksController(WPLStatsDbContext context)
        {
            _context = context;
        }

        // GET: api/Weeks
        [HttpGet]
        [ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any,NoStore =false)]
        public async Task<ActionResult<IEnumerable<Week>>> GetWeeks()
        {
          if (_context.Weeks == null)
          {
              return NotFound();
          }
            return await _context.Weeks.ToListAsync();
        }

        // GET: api/Weeks/5
        [HttpGet("{id}")]
        [ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<Week>> GetWeek(int id)
        {
          if (_context.Weeks == null)
          {
              return NotFound();
          }
            var week = await _context.Weeks.FindAsync(id);

            if (week == null)
            {
                return NotFound();
            }

            return week;
        }

        // PUT: api/Weeks/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWeek(int id, Week week)
        {
            if (id != week.WeekNumber)
            {
                return BadRequest();
            }

            _context.Entry(week).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WeekExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Weeks
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Week>> PostWeek(Week week)
        {
          if (_context.Weeks == null)
          {
              return Problem("Entity set 'WPLStatsDbContext.Weeks'  is null.");
          }
            _context.Weeks.Add(week);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WeekExists(week.WeekNumber))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWeek", new { id = week.WeekNumber }, week);
        }

        // DELETE: api/Weeks/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteWeek(int id)
        {
            if (_context.Weeks == null)
            {
                return NotFound();
            }
            var week = await _context.Weeks.FindAsync(id);
            if (week == null)
            {
                return NotFound();
            }

            _context.Weeks.Remove(week);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool WeekExists(int id)
        {
            return (_context.Weeks?.Any(e => e.WeekNumber == id)).GetValueOrDefault();
        }
    }
}
