using BBallByStats.HttpClientModel;
using BBallByStats.Models;
using BBallByStats.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BBallByStats.Controllers
{
    [Route("api/players")]
    [ApiController]
    public class PlayersController : ControllerBase
    {
        private readonly PlayerContext _dbContext;

        public PlayersController(PlayerContext dbContext)
        {
            _dbContext = dbContext;
        }

        //GET: api/Players

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Player>>> GetPlayers()
        {
            if (_dbContext.Players == null)
            {
                return NotFound();
            }
            
            return await _dbContext.Players.ToListAsync();

        }
        

        protected async Task<Dictionary<int, List<Datum>>> GetPlayersFromUrlAsync(int pageNumber)
        {
            Dictionary<int, List<Datum>> players = new Dictionary<int, List<Datum>>();
            using (HttpClient client = new HttpClient())
            {
                Uri uri = new Uri($"https://www.balldontlie.io/api/v1/players?per_page=100&page={pageNumber}");
                var response = await client.GetFromJsonAsync<Root>(uri);
                if(response != null)
                players.Add(response.Meta.total_pages, response.Data.ToList());

                return players;
            }

         }
                

        //GET: api/Players/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Player>> GetPlayer(long id)
        {
            if (_dbContext.Players == null)
            {
                return NotFound();
            }
            var player = await _dbContext.Players.FindAsync(id);

            if(player == null)
            {
                return NotFound();
            }

            return player;

        }
    }
}
