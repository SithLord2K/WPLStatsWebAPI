using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WPLStatsCoreWebAPI.Models;

namespace WPLStatsCoreWebAPI.Data
{
    [Route("api_v2/[controller]")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly WPLStatsDbContext _context;

        public PlayersController(WPLStatsDbContext context)
        {
            _context = context;
        }

        // GET: api/Players
        [HttpGet]
        [ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, NoStore = false)]
        [Authorize]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
          if (_context.Players == null)
          {
              return NotFound();
          }
            return await _context.Players.ToListAsync();
        }

        // GET: api/Players/5
        [HttpGet("{id}")]
        [Authorize]
        [ResponseCache(Duration = 20, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<ActionResult<Player>> GetPlayer(int id)
        {
          if (_context.Players == null)
          {
              return NotFound();
          }
            var player = await _context.Players.FindAsync(id);

            if (player == null)
            {
                return NotFound();
            }

            return player;
        }

        // PUT: api/Players/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> PutPlayer(int id, Player player)
        {
            if (id != player.EntryId)
            {
                return BadRequest();
            }

            _context.Entry(player).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerExists(id))
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

        // POST: api/Players
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<Player>> PostPlayer(Player player)
        {
          if (_context.Players == null)
          {
              return Problem("Entity set 'WPLStatsDbContext.Players'  is null.");
          }
            _context.Players.Add(player);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPlayer", new { id = player.EntryId }, player);
        }

        // DELETE: api/Players/5
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeletePlayer(int id)
        {
            if (_context.Players == null)
            {
                return NotFound();
            }
            var player = await _context.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            _context.Players.Remove(player);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PlayerExists(int id)
        {
            return (_context.Players?.Any(e => e.EntryId == id)).GetValueOrDefault();
        }
    }
}
