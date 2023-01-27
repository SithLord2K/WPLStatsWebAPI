using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using WPLStatsWebAPI.Caching;
using WPLStatsWebAPI.Data;


namespace WPLStatsWebAPI.Controllers
{
    public class WeeksController : ApiController
    {
        private XWPLStatsEntity db = new XWPLStatsEntity();

        // GET: api/Weeks
        [CacheFilter(TimeDuration = 20)]
        public IQueryable<Week> GetWeeks()
        {
            return db.Weeks;
        }

        // GET: api/Weeks/5
        [ResponseType(typeof(Week))]
        [CacheFilter(TimeDuration = 20)]
        public async Task<IHttpActionResult> GetWeek(int id)
        {
            Week week = await db.Weeks.FindAsync(id);
            if (week == null)
            {
                return NotFound();
            }

            return Ok(week);
        }

        // PUT: api/Weeks/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutWeek(int id, Week week)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != week.Id)
            {
                return BadRequest();
            }

            db.Entry(week).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Weeks
        [ResponseType(typeof(Week))]
        public async Task<IHttpActionResult> PostWeek(Week week)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Weeks.Add(week);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = week.Id }, week);
        }

        // DELETE: api/Weeks/5
        [ResponseType(typeof(Week))]
        public async Task<IHttpActionResult> DeleteWeek(int id)
        {
            Week week = await db.Weeks.FindAsync(id);
            if (week == null)
            {
                return NotFound();
            }

            db.Weeks.Remove(week);
            await db.SaveChangesAsync();

            return Ok(week);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool WeekExists(int id)
        {
            return db.Weeks.Count(e => e.Id == id) > 0;
        }
    }
}