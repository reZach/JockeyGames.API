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
    public class MatchesController : ApiController
    {
        private JockeyGamesAPIContext db = new JockeyGamesAPIContext();

        // GET: api/Matches
        public IQueryable<MatchDTO> GetMatches()
        {
            var query = (from m in db.Matches
                         select new MatchDTO
                         {
                             Id = m.Id,
                             DateTime = m.DateTime,
                             G1P1Score = m.G1P1Score,
                             G1P2Score = m.G1P2Score,
                             G2P1Score = m.G2P1Score,
                             G2P2Score = m.G2P2Score,
                             G3P1Score = m.G3P1Score,
                             G3P2Score = m.G3P2Score,
                             PlayerId1 = m.PlayerId1.Id,
                             PlayerId2 = m.PlayerId2.Id
                         });

            return query;
        }

        // GET: api/Matches/5
        [ResponseType(typeof(MatchDTO))]
        public async Task<IHttpActionResult> GetMatch(int id)
        {
            Match match = await db.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            MatchDTO matchDTO = new MatchDTO
            {
                Id = match.Id,
                DateTime = match.DateTime,
                G1P1Score = match.G1P1Score,
                G1P2Score = match.G1P2Score,
                G2P1Score = match.G2P1Score,
                G2P2Score = match.G2P2Score,
                G3P1Score = match.G3P1Score,
                G3P2Score = match.G3P2Score,
                PlayerId1 = match.PlayerId1.Id,
                PlayerId2 = match.PlayerId2.Id
            };

            return Ok(matchDTO);
        }

        // PUT: api/Matches/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutMatch(int id, MatchDTO matchDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != matchDTO.Id)
            {
                return BadRequest();
            }

            Player player1 = await db.Players.FindAsync(matchDTO.PlayerId1);
            Player player2 = await db.Players.FindAsync(matchDTO.PlayerId2);
            Match match = new Match
            {
                Id = matchDTO.Id,
                DateTime = matchDTO.DateTime,
                G1P1Score = matchDTO.G1P1Score,
                G1P2Score = matchDTO.G1P2Score,
                G2P1Score = matchDTO.G2P1Score,
                G2P2Score = matchDTO.G2P2Score,
                G3P1Score = matchDTO.G3P1Score,
                G3P2Score = matchDTO.G3P2Score,
                PlayerId1 = player1,
                PlayerId2 = player2
            };

            db.Entry(match).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
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

        // POST: api/Matches
        [ResponseType(typeof(MatchDTO))]
        public async Task<IHttpActionResult> PostMatch(MatchDTO matchDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Player player1 = await db.Players.FindAsync(matchDTO.PlayerId1);
            Player player2 = await db.Players.FindAsync(matchDTO.PlayerId2);
            Match match = new Match
            {
                Id = matchDTO.Id,
                DateTime = matchDTO.DateTime,
                G1P1Score = matchDTO.G1P1Score,
                G1P2Score = matchDTO.G1P2Score,
                G2P1Score = matchDTO.G2P1Score,
                G2P2Score = matchDTO.G2P2Score,
                G3P1Score = matchDTO.G3P1Score,
                G3P2Score = matchDTO.G3P2Score,
                PlayerId1 = player1,
                PlayerId2 = player2
            };

            db.Matches.Add(match);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = matchDTO.Id }, matchDTO);
        }

        // DELETE: api/Matches/5
        [ResponseType(typeof(MatchDTO))]
        public async Task<IHttpActionResult> DeleteMatch(int id)
        {
            Match match = await db.Matches.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            MatchDTO matchDTO = new MatchDTO
            {
                Id = match.Id,
                DateTime = match.DateTime,
                G1P1Score = match.G1P1Score,
                G1P2Score = match.G1P2Score,
                G2P1Score = match.G2P1Score,
                G2P2Score = match.G2P2Score,
                G3P1Score = match.G3P1Score,
                G3P2Score = match.G3P2Score,
                PlayerId1 = match.PlayerId1.Id,
                PlayerId2 = match.PlayerId2.Id
            };

            db.Matches.Remove(match);
            await db.SaveChangesAsync();

            return Ok(matchDTO);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MatchExists(int id)
        {
            return db.Matches.Count(e => e.Id == id) > 0;
        }
    }
}