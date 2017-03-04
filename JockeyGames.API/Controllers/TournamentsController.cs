using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using JockeyGames.API.Models;
using JockeyGames.Models.PingPong;

namespace JockeyGames.API.Controllers
{
    public class TournamentsController : ApiController
    {
        private JockeyGamesAPIContext db = new JockeyGamesAPIContext();

        // GET: api/Tournaments1
        public IQueryable<Tournament> GetTournaments()
        {
            return db.Tournaments;
        }

        // GET: api/Tournaments1/5
        [ResponseType(typeof(Tournament))]
        public IHttpActionResult GetTournament(int id)
        {
            Tournament tournament = db.Tournaments.Find(id);
            if (tournament == null)
            {
                return NotFound();
            }

            return Ok(tournament);
        }

        // PUT: api/Tournaments1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutTournament(int id, Tournament tournament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tournament.Id)
            {
                return BadRequest();
            }

            db.Entry(tournament).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TournamentExists(id))
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

        // POST: api/Tournaments1
        [ResponseType(typeof(Tournament))]
        public IHttpActionResult PostTournament(Tournament tournament)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Tournaments.Add(tournament);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tournament.Id }, tournament);
        }

        // DELETE: api/Tournaments1/5
        [ResponseType(typeof(Tournament))]
        public IHttpActionResult DeleteTournament(int id)
        {
            Tournament tournament = db.Tournaments.Find(id);
            if (tournament == null)
            {
                return NotFound();
            }

            db.Tournaments.Remove(tournament);
            db.SaveChanges();

            return Ok(tournament);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TournamentExists(int id)
        {
            return db.Tournaments.Count(e => e.Id == id) > 0;
        }
    }
}