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
using JockeyGames.Models.Shared;
using JockeyGames.Models.DTOs;
using JockeyGames.Models.PingPong;

namespace JockeyGames.API.Controllers
{
    public class GamesController : ApiController
    {
        private JockeyGamesAPIContext db = new JockeyGamesAPIContext();

        // GET: api/Games
        public IQueryable<GameDTO> GetGames()
        {
            List<int> gameIds = (from g in db.Games
                                 select g.Id).ToList();
            
            return db.Games;
        }

        // GET: api/Games/5
        [ResponseType(typeof(GameDTO))]
        public IHttpActionResult GetGame(int id)
        {
            var query = (from g in db.Games
                         where g.Id == id
                         select new GameDTO
                         {
                             Id = g.Id
                         });

            if (query == null)
            {
                return NotFound();
            }

            GameDTO game = query.First();

            int matchId = db.Games.Where(g => g.Id == id).Select(g => g.Match.Id).First();
            Match match = db.Matches.Where(m => m.Id == matchId).First();

            //int tournamentId = db.Matches.Where(m => m.)
            //Tournament tournament = db.Tournaments.Where(t => t.Id == )
            List<PlayerGame> playerGames = db.PlayerGames.Where(p => p.GameId == game.Id).ToList();

            game.PlayerGames = new List<PlayerGameDTO>();
            foreach (PlayerGame pg in playerGames)
            {
                PlayerDTO playerDTO = new PlayerDTO()
                {
                    Id = pg.PlayerId,
                    Name = pg.Player.Name
                };

                game.PlayerGames.Add(new PlayerGameDTO()
                {
                    Score = pg.Score,
                    Player = playerDTO
                });
            }

            MatchDTO matchDTO = new MatchDTO()
            {
                Id = match.Id,
                DateTime = match.DateTime
            };

            return Ok(game);
        }

        // PUT: api/Games/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutGame(int id, GameDTO gameDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != gameDTO.Id)
            {
                return BadRequest();
            }

            db.Entry(gameDTO).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameExists(id))
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

        // POST: api/Games
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> PostGame(Game game)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Games.Add(game);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = game.Id }, game);
        }

        // DELETE: api/Games/5
        [ResponseType(typeof(Game))]
        public async Task<IHttpActionResult> DeleteGame(int id)
        {
            Game game = await db.Games.FindAsync(id);
            if (game == null)
            {
                return NotFound();
            }

            db.Games.Remove(game);
            await db.SaveChangesAsync();

            return Ok(game);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GameExists(int id)
        {
            return db.Games.Count(e => e.Id == id) > 0;
        }
    }
}