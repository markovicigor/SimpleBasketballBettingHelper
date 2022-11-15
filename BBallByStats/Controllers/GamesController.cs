using BBallByStats.BettingPlace;
using BBallByStats.Common;
using BBallByStats.Models;
using BBallByStats.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
namespace BBallByStats.Controllers
{
    [Route("api/Games")]
    [ApiController]
    public class GamesController : ControllerBase
    {   
      static Dictionary<string, Percent> awayTeams = new Dictionary<string, Percent>();
        private readonly PlayerContext _dbContext;
        public GamesController(PlayerContext dbContext)
        {

            _dbContext = dbContext;
        }
        
       
        [HttpGet("/getPointsByTeam")]
        public async Task<ActionResult<Dictionary<string,Percent>>> getPointsByTeam()
        {
            var pointsByTeam = await getPercentOfUnderLimits();
            if (pointsByTeam == null)
            {
                return NotFound();
            }
            return pointsByTeam;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames()
        {
            //await WriteGamesToDB();
           
            //await getGameLimitsFromBettingSite("MaxBet"); 
            //await getPercentOfUnderLimits();
            if (_dbContext.Games == null)
            {
                return NotFound();
            }

            return await _dbContext.Games.ToListAsync();

        }
        
        //Writing game score in DataBase
        protected async Task WriteGamesToDB()
        {
            List<Datum> GamesToDB = await getGamesFromUrl();

            foreach(var game in GamesToDB)
            {
                Game g = new Game(game.id, game.home_team.id, game.visitor_team.id, game.home_team_score, game.visitor_team_score, game.date,game.home_team.full_name,game.visitor_team.full_name);
                try
                {
                    if(_dbContext.Games.ToList().Find(x=>x.GameId == game.id) == null)
                    {
                        if(game.home_team_score != 0 || game.visitor_team_score !=0)
                        _dbContext.Games.Add(g); 
                    }
                    
                    
                  
                    
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }

            }
           await _dbContext.SaveChangesAsync();

        }
        //Extract games score from external api
        protected async Task<List<Datum>> getGamesFromUrl()
        {
           
            List<Datum> gamesFromResponse = new List<Datum>();
            HttpClient client = new HttpClient();

                string startDate = getDate(DateTime.Today.AddDays(-2));
                string endDate = getDate(DateTime.Today.AddDays(+2));   
                Uri uri = new Uri($"https://www.balldontlie.io/api/v1/games?start_date={startDate}&end_date={endDate}&per_page=100");
                var response = await client.GetFromJsonAsync<Common.Root>(uri);
                if (response != null)
                {
                    foreach(var g in response.data.ToList())
                    {
                        gamesFromResponse.Add(g);
                    }
                }

                return gamesFromResponse;
            
        }
        //Extract game odds from MaxBet site
        protected async Task<List<MatchList>> getGamesFromMaxBet()
        {

            List<MatchList> gamesFromResponse = new List<MatchList>();
            HttpClient client = new HttpClient();

            var options = new JsonSerializerOptions()
            {
                NumberHandling = JsonNumberHandling.AllowReadingFromString |
                JsonNumberHandling.WriteAsString
            };
            Uri uri = new Uri($"https://www.maxbet.rs/ibet/offer/league/144532/-1/0/false.json?v=4.48.26&locale=sr&ttgIds=");
            try
            {
                string str = await client.GetStringAsync(uri);

                JObject json = JObject.Parse(str);

                var response = json.ToObject<BettingPlace.Root>();


                //var response = await client.GetFromJsonAsync<BettingPlace.Root>(uri,options);
              // var response = await JsonSerializer.Deserialize<BettingPlace.Root>(uri., options);
                if (response != null)
                {
                    foreach (var g in response.matchList.ToList())
                    {
                        gamesFromResponse.Add(g);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }



            return gamesFromResponse;

        }
        protected async Task getGameLimitsFromBettingSite(string bettingSiteName)
        {
            switch(bettingSiteName)
            {
                case "MaxBet":
                    await getGamesToCompareFromMaxBet();
                    break;

            }
        }
        //convert MaxBet games from json type to BetGame type
        
        protected async Task<List<BetGame>> getGamesToCompareFromMaxBet()
        {
            double limit = 0;
            double under = 0;
            double over = 0;
            List<BetGame> betGames = new List<BetGame>();   
            List<MatchList> gamesFromResponse = await getGamesFromMaxBet();
            List<MatchList> filteredGames = new List<MatchList>();
            filteredGames = gamesFromResponse.Where(x => x.odBetPickGroups.Where(x => x.name == "Ukupno poena") != null).ToList();
            foreach (var game in filteredGames)
            {
                var odBet = game.odBetPickGroups.Find(x=>x.handicapParamValue != null)!;
                limit = Convert.ToDouble(odBet.handicapParamValue);

                var types = game.odBetPickGroups.SelectMany(b => b.tipTypes).Distinct();
                under = types.ToList().Find(x => x.tipType == "G_MINUS")!.value;
                over = types.ToList().Find(x => x.tipType == "G_PLUS")!.value;

                BetGame betGame = new BetGame(game.home, game.away, limit, over, under);
                //betGame.Percents = await getPercentOfUnderLimits();
                betGames.Add(betGame);


            }
            return  betGames;
        }
        protected async Task<Dictionary<string,Percent>> getPercentOfUnderLimits()
        {
            
            
            List<Game> gamesFromDatabase = _dbContext.Games.ToList();
            List<BetGame> betGames = await getGamesToCompareFromMaxBet();
            Dictionary<string, Percent> percentByTeam = new Dictionary<string, Percent>();
            Dictionary<string, Percent> filteredDictionary = new Dictionary<string, Percent>();

            foreach(Game game in gamesFromDatabase)
            {
                if(!percentByTeam.ContainsKey(game.HomeTeamName))
                {
                    Percent percent = new Percent();
                    percent.Name = game.HomeTeamName;
                    percent.TotalPoints.Add(game.HomeTeamPoints + game.GuestTeamPoints);
                    percentByTeam.Add(game.HomeTeamName, percent);
                }
                else
                {
                    var team = percentByTeam[game.HomeTeamName];
                    team.TotalPoints.Add(game.HomeTeamPoints + game.GuestTeamPoints);
                    percentByTeam[game.HomeTeamName] = team;
                }
                if (!percentByTeam.ContainsKey(game.GuestTeamName))
                {
                    Percent percent = new Percent();
                    percent.Name = game.GuestTeamName;
                    percent.TotalPoints.Add(game.HomeTeamPoints + game.GuestTeamPoints);
                    percentByTeam.Add(game.GuestTeamName, percent);
                }
                else
                {
                    var team = percentByTeam[game.GuestTeamName];
                    team.TotalPoints.Add(game.HomeTeamPoints + game.GuestTeamPoints);
                    percentByTeam[game.GuestTeamName] = team;
                }
            }


            foreach(BetGame betGame in betGames)
            {
                
                foreach(KeyValuePair<string,Percent> kvp in percentByTeam)
                {
                    if(compareTwoNames(kvp.Key,betGame.HomeTeam,betGame.AwayTeam))
                    { 
                        
                        var underGames = kvp.Value.TotalPoints.Where(x => x < betGame.Limit).Count();
                        percentByTeam[kvp.Key].Under = underGames;
                       
                        if(percentByTeam.Values.ToList().Find(x => x.BetGame.HomeTeam == betGame.HomeTeam) == null)
                            percentByTeam[kvp.Key].BetGame = betGame;
                        percentByTeam[kvp.Key].UnderPercent = (percentByTeam[kvp.Key].Under / percentByTeam[kvp.Key].TotalPoints.Count())*100;
                       
                    }
                
                }
            }
            foreach (KeyValuePair<string, Percent> kvp in percentByTeam)
            {

               if(kvp.Value.BetGame.HomeTeam != null)
                {
                    if(kvp.Value.BetGame.HomeTeam == "Houston")
                    {
                        Console.WriteLine();
                    }
                    string guestTeamName = kvp.Value.BetGame.AwayTeam;
                    string homeTeamName = kvp.Value.BetGame.HomeTeam;
                    string guestTeamModified = stringModifier(guestTeamName);
                    string homeTeamModified = stringModifier(homeTeamName);
                    percentByTeam[homeTeamModified].UnderBoth = percentByTeam[guestTeamModified].Under + percentByTeam[homeTeamModified].Under;
                    percentByTeam[guestTeamModified].UnderBoth = percentByTeam[homeTeamModified].UnderBoth;
                    var bothGames = percentByTeam[guestTeamModified].TotalPoints.Count() + percentByTeam[homeTeamModified].TotalPoints.Count();

                    percentByTeam[homeTeamModified].PercentBoth = Math.Round((percentByTeam[homeTeamModified].UnderBoth / bothGames) * 100,2);
                    percentByTeam[guestTeamModified].PercentBoth = Math.Round((percentByTeam[guestTeamModified].UnderBoth / bothGames) * 100,2);
                    var clipers = percentByTeam[homeTeamModified];
                    //var dict = await homeAwayTeam(kvp.Value.BetGame.HomeTeam, percentByTeam[guestTeamModified]);
                }

            }
            


            return percentByTeam;
        }
        protected  string stringModifier(string name)
        {
           
            string ret = "";
            List<Team> teams =  _dbContext.Teams.ToList();
            switch(name)
            {
                case "LA Lakers":
                    name = "Los Angeles Lakers";
                    break;
               
            }
           

            foreach (Team t in teams)
            {
                if(t.Name.Contains(name))
                {
                    ret = t.Name;
                    break;
                }
            }
            return ret;
        }
       
        protected string getDate(DateTime date)
        {
          

            int day = date.Day;
            int month = date.Month;
            int year = date.Year;

            return $"{year}-{month}-{day}";
        }
      
        protected bool compareTwoNames(string name1, string name2,string name3)
        {
            if(name2 == "LA Lakers" )
            {
                name2 = "Los Angeles Lakers";
            }
            if (name3 == "LA Lakers")
            {
                name3 = "Los Angeles Lakers";
            }

           /*string nameSplit = name1.Split(' ').FirstOrDefault()!;
            string name2Split = name2.Split(' ').FirstOrDefault()!;
            string name3Split = name3.Split(' ').FirstOrDefault()!;*/

            if (name1.Contains(name2) || name1.Contains(name3))
            {
                
                return true;
            }
            else
            {
               
                    return false;
            }
                
        }
        
    }
}
