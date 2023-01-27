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
    public class TeamDetailsController : ApiController
    {
        private XWPLStatsEntity db = new XWPLStatsEntity();

        // GET: api/TeamDetails
        [CacheFilter(TimeDuration = 20)]
        public IQueryable<TeamDetail> GetTeamDetails()
        {
            return db.TeamDetails;
        }

        // GET: api/TeamDetails/5
        [ResponseType(typeof(TeamDetail))]
        [CacheFilter(TimeDuration = 20)]
        public async Task<IHttpActionResult> GetTeamDetail(int id)
        {
            TeamDetail teamDetail = await db.TeamDetails.FindAsync(id);
            if (teamDetail == null)
            {
                return NotFound();
            }

            return Ok(teamDetail);
        }

        // PUT: api/TeamDetails/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutTeamDetail(int id, TeamDetail teamDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teamDetail.ID)
            {
                return BadRequest();
            }

            db.Entry(teamDetail).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TeamDetailExists(id))
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

        // POST: api/TeamDetails
        [ResponseType(typeof(TeamDetail))]
        public async Task<IHttpActionResult> PostTeamDetail(TeamDetail teamDetail)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.TeamDetails.Add(teamDetail);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = teamDetail.ID }, teamDetail);
        }

        // DELETE: api/TeamDetails/5
        [ResponseType(typeof(TeamDetail))]
        public async Task<IHttpActionResult> DeleteTeamDetail(int id)
        {
            TeamDetail teamDetail = await db.TeamDetails.FindAsync(id);
            if (teamDetail == null)
            {
                return NotFound();
            }

            db.TeamDetails.Remove(teamDetail);
            await db.SaveChangesAsync();

            return Ok(teamDetail);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TeamDetailExists(int id)
        {
            return db.TeamDetails.Count(e => e.ID == id) > 0;
        }
    }
}