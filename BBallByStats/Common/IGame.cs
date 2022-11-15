namespace BBallByStats.Interfaces
{
    public interface IGame
    {
        int totalPointsOnGame(int homeTeamPoints, int guestTeamPoints);
        int totalPointsByTeam(List<int> pointsByTeam);
    }
}
