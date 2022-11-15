using BBallByStats.Interfaces;

namespace BBallByStats.Models
{
    public class Game : IGame
    {
        private int _id;
        private int _gameId;
        private int _homeTeam;
        private int _guestTeam;
        private string _homeTeamName;
        private string _guestTeamName;
        private int _homeTeamPoints;
        private int _guestTeamPoints;
        private DateTime _gameDate;

        public Game(int gameId,int homeTeam, int guestTeam, int homeTeamPoints, int guestTeamPoints, DateTime gameDate,string homeTeamName, string guestTeamName)
        {
            _gameId = gameId;
            _homeTeam = homeTeam;
            _guestTeam = guestTeam;
            _homeTeamPoints = homeTeamPoints;
            _guestTeamPoints = guestTeamPoints;
            _gameDate = gameDate;
            _homeTeamName = homeTeamName;
            _guestTeamName = guestTeamName; 
        }

        public int Id { get => _id; set => _id = value; }
        public int GameId { get => _gameId; set => _gameId = value; }
        public int HomeTeam { get => _homeTeam; set => _homeTeam = value; }
        public int GuestTeam { get => _guestTeam; set => _guestTeam = value; }
        public int HomeTeamPoints { get => _homeTeamPoints; set => _homeTeamPoints = value; }
        public int GuestTeamPoints { get => _guestTeamPoints; set => _guestTeamPoints = value; }
        public DateTime GameDate { get => _gameDate; set => _gameDate = value; }
        public string GuestTeamName { get => _guestTeamName; set => _guestTeamName = value; }
        public string HomeTeamName { get => _homeTeamName; set => _homeTeamName = value; }

        public int totalPointsByTeam(List<int> pointsByTeam)
        {
            int sum = 0;
            foreach(var point in pointsByTeam)
            {
                sum += point;
            }
            return sum;
        }

        public int totalPointsOnGame(int homeTeamPoints, int guestTeamPoints)
        {
            return homeTeamPoints + guestTeamPoints;
        }
    }
}
