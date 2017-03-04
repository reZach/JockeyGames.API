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
    public class PlayerGamesController : ApiController
    {
        private JockeyGamesAPIContext db = new JockeyGamesAPIContext();

        // GET: api/PlayerGames1
        public IQueryable<PlayerGame> GetPlayerGames()
        {
            return db.PlayerGames;
        }

        // GET: api/PlayerGames1/5
        [ResponseType(typeof(PlayerGame))]
        public IHttpActionResult GetPlayerGame(int id)
        {
            PlayerGame playerGame = db.PlayerGames.Find(id);
            if (playerGame == null)
            {
                return NotFound();
            }

            return Ok(playerGame);
        }

        // PUT: api/PlayerGames1/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPlayerGame(int id, PlayerGame playerGame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != playerGame.Id)
            {
                return BadRequest();
            }

            db.Entry(playerGame).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PlayerGameExists(id))
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

        // POST: api/PlayerGames1
        [ResponseType(typeof(PlayerGame))]
        public IHttpActionResult PostPlayerGame(PlayerGame playerGame)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.PlayerGames.Add(playerGame);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = playerGame.Id }, playerGame);
        }

        // DELETE: api/PlayerGames1/5
        [ResponseType(typeof(PlayerGame))]
        public IHttpActionResult DeletePlayerGame(int id)
        {
            PlayerGame playerGame = db.PlayerGames.Find(id);
            if (playerGame == null)
            {
                return NotFound();
            }

            db.PlayerGames.Remove(playerGame);
            db.SaveChanges();

            return Ok(playerGame);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerGameExists(int id)
        {
            return db.PlayerGames.Count(e => e.Id == id) > 0;
        }
    }
}