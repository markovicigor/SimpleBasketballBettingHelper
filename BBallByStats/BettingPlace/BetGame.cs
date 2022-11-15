namespace BBallByStats.BettingPlace
{
    public class BetGame
    {
        private string homeTeam;
        private string awayTeam;
        private double limit;
        private double over;
        private double odd;
        private double under;
        private Dictionary<string, Percent> percents;
        public BetGame()
        {

        }
        public BetGame(string homeTeam, string awayTeam, double limit, double over, double under)
        {
            Percents = new Dictionary<string, Percent>();
            this.homeTeam = homeTeam;
            this.awayTeam = awayTeam;
            this.limit = limit;
            this.over = over;
            this.under = under;
        }

        public string HomeTeam { get => homeTeam; set => homeTeam = value; }
        public string AwayTeam { get => awayTeam; set => awayTeam = value; }
        public double Limit { get => limit; set => limit = value; }
        public double Over { get => over; set => over = value; }
        public double Under { get => under; set => under = value; }
        public double Odd { get => odd; set => odd = value; }
        public Dictionary<string, Percent> Percents { get => percents; set => percents = value; }
    }
}
