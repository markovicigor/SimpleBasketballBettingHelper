namespace BBallByStats.HttpClientModel
{
    public class Datum
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public int? height_feet { get; set; }
        public int? height_inches { get; set; }
        public string last_name { get; set; }
        public string position { get; set; }
        public Team team { get; set; }
        public int? weight_pounds { get; set; }
    }

    public class Meta
    {
        public int total_pages { get; set; }
        public int current_page { get; set; }
        public int next_page { get; set; }
        public int per_page { get; set; }
        public int total_count { get; set; }
    }

    public class Root
    {
        public  List<Datum> Data { get; set; }
        public  Meta Meta { get; set; }
    }

    public class Team
    {
        public int id { get; set; }
        public string abbreviation { get; set; }
        public string city { get; set; }
        public string conference { get; set; }
        public string division { get; set; }
        public string full_name { get; set; }
        public string name { get; set; }
    }
}
