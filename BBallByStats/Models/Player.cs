namespace BBallByStats.Models
{
    public class Player
    {
        private long _id;
        private string _name;
        private string _club;
        private double _points;

        public long Id { get => _id; set => _id = value; }
        public string Name { get => _name; set => _name = value; }
        public string Club { get => _club; set => _club = value; }
        public double Points { get => _points; set => _points = value; }


    }
}
