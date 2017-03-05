using JockeyGames.API.Models;
using JockeyGames.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace JockeyGames.API.Controllers
{
    public class StatsController : ApiController
    {
        private JockeyGamesAPIContext db = new JockeyGamesAPIContext();

        // GET: api/Stats/5        
        public IQueryable<MatchDTO> GetPlayer(int id)
        {
            var query = (from m in db.Matches
                         where m.PlayerId1.Id == id || m.PlayerId2.Id == id
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

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}