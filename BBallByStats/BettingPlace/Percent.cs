namespace BBallByStats.BettingPlace
{
    public class Percent
    {
        private string name;
        private double over;
        private double under;
        private double underPercent;
        private double overPercent;
        private List<int> totalPoints;
        private string opponentName;
        private double opponentPercentUnder;
        private BetGame betGame;
        private double underBoth;
        private double percentBoth;

        public Percent()
        {
            TotalPoints = new List<int>();
            BetGame = new BetGame();
        }
        public Percent(string name,double homeTeamPercent, double awayTeamPercent)
        {
            this.name = name;
            this.over = homeTeamPercent;
            this.under = awayTeamPercent;
        }

        public double Over{ get => over; set => over= value; }
        public double Under { get => under; set => under = value; }
        public string Name { get => name; set => name = value; }
        public List<int> TotalPoints { get => totalPoints; set => totalPoints = value; }
        public double UnderPercent { get => underPercent; set => underPercent = value; }
        public double OverPercent { get => overPercent; set => overPercent = value; }
        public BetGame BetGame { get => betGame; set => betGame = value; }
        public string OpponentName { get => opponentName; set => opponentName = value; }
        public double OpponentPercentUnder { get => opponentPercentUnder; set => opponentPercentUnder = value; }
        public double PercentBoth { get => percentBoth; set => percentBoth = value; }
        public double UnderBoth { get => underBoth; set => underBoth = value; }
    }
}
