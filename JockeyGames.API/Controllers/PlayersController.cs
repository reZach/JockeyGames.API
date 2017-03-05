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
using JockeyGames.API.Models;
using JockeyGames.Models.PingPong;
using JockeyGames.Models.DTOs;

namespace JockeyGames.API.Controllers
{
    public class PlayersController : ApiController
    {
        private JockeyGamesAPIContext db = new JockeyGamesAPIContext();

        // GET: api/Players
        public IQueryable<PlayerDTO> GetPlayers()
        {
            var query = (from p in db.Players
                         select new PlayerDTO
                         {
                             Id = p.Id,
                             Name = p.Name
                         });

            return query;
        }

        // GET: api/Players/5
        [ResponseType(typeof(PlayerDTO))]
        public async Task<IHttpActionResult> GetPlayer(int id)
        {
            Player player = await db.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            PlayerDTO playerDTO = new PlayerDTO
            {
                Id = player.Id,
                Name = player.Name
            };

            return Ok(playerDTO);
        }

        // PUT: api/Players/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutPlayer(int id, PlayerDTO playerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != playerDTO.Id)
            {
                return BadRequest();
            }

            Player player = new Player
            {
                Id = playerDTO.Id,
                Name = playerDTO.Name
            };

            db.Entry(player).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
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

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Players
        [ResponseType(typeof(PlayerDTO))]
        public async Task<IHttpActionResult> PostPlayer(PlayerDTO playerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Player player = new Player
            {
                Id = playerDTO.Id,
                Name = playerDTO.Name
            };

            db.Players.Add(player);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = playerDTO.Id }, playerDTO);
        }

        // DELETE: api/Players/5
        [ResponseType(typeof(PlayerDTO))]
        public async Task<IHttpActionResult> DeletePlayer(int id)
        {
            Player player = await db.Players.FindAsync(id);
            if (player == null)
            {
                return NotFound();
            }

            PlayerDTO playerDTO = new PlayerDTO
            {
                Id = player.Id,
                Name = player.Name
            };

            db.Players.Remove(player);
            await db.SaveChangesAsync();

            return Ok(playerDTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PlayerExists(int id)
        {
            return db.Players.Count(e => e.Id == id) > 0;
        }
    }
}