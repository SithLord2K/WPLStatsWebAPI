using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPLStatsCoreWebAPI.Filters;
using WPLStatsCoreWebAPI.Models;

namespace WPLStatsCoreWebAPI.Data
{
    [APIKey]
    [Route("/[controller]")]
    [ApiController]
    public class LoggingController : ControllerBase
    {
        private readonly WPLStatsDbContext _context;

        public LoggingController(WPLStatsDbContext context)
        {
            _context = context;
        }

        // GET: api/Logging
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Logging>>> GetLogging()
        {
          if (_context.Logging == null)
          {
              return NotFound();
          }
            return await _context.Logging.ToListAsync();
        }

        // GET: api/Logging/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Logging>> GetLogging(int id)
        {
          if (_context.Logging == null)
          {
              return NotFound();
          }
            var logging = await _context.Logging.FindAsync(id);

            if (logging == null)
            {
                return NotFound();
            }

            return logging;
        }

        // PUT: api/Logging/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLogging(int id, Logging logging)
        {
            if (id != logging.ID)
            {
                return BadRequest();
            }

            _context.Entry(logging).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoggingExists(id))
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

        // POST: api/Logging
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Logging>> PostLogging(Logging logging)
        {
          if (_context.Logging == null)
          {
              return Problem("Entity set 'WPLStatsDbContext.Logging'  is null.");
          }
            _context.Logging.Add(logging);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLogging", new { id = logging.ID }, logging);
        }

        // DELETE: api/Logging/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLogging(int id)
        {
            if (_context.Logging == null)
            {
                return NotFound();
            }
            var logging = await _context.Logging.FindAsync(id);
            if (logging == null)
            {
                return NotFound();
            }

            _context.Logging.Remove(logging);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LoggingExists(int id)
        {
            return (_context.Logging?.Any(e => e.ID == id)).GetValueOrDefault();
        }
    }
}
