namespace BBallByStats.BettingPlace
{
    public class HttpClientMaxBet
    {
    }
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class ImatchCBL
    {
        public object matchBlockadesCode { get; set; }
        public int matchConditionsCode { get; set; }
        public int matchLimitsCode { get; set; }
        public int matchAuthParamsCode { get; set; }
        public object desctription { get; set; }
    }

    public class IMatchCBL2
    {
        public object matchBlockadesCode { get; set; }
        public int matchConditionsCode { get; set; }
        public int matchLimitsCode { get; set; }
        public int matchAuthParamsCode { get; set; }
        public object desctription { get; set; }
    }

    public class MatchList
    {
        public int id { get; set; }
        public int round { get; set; }
        public int matchCode { get; set; }
        public int leagueId { get; set; }
        public string home { get; set; }
        public string away { get; set; }
        public object kickOffTime { get; set; }
        public string kickOffTimeString { get; set; }
        public int status { get; set; }
        public object single { get; set; }
        public bool blocked { get; set; }
        public bool favorite { get; set; }
        public ImatchCBL iMatchCBL { get; set; }
        public object odds { get; set; }
        public object @params { get; set; }
        public List<OdBetPickGroup> odBetPickGroups { get; set; }
        public bool haveOdds { get; set; }
        public int leagueCode { get; set; }
        public string sport { get; set; }
        public int tipTypeGroupsCount { get; set; }
        public string leagueName { get; set; }
        public object sportName { get; set; }
        public int oddsCount { get; set; }
        public object leagueType { get; set; }
        public string leagueShortName { get; set; }
        public object countryCode { get; set; }
        public int homeId { get; set; }
        public int awayId { get; set; }
        public int? brMatchId { get; set; }
        public bool virtualSport { get; set; }
        public int ticketPrintType { get; set; }
        public string matchInfo { get; set; }
        public int leagueRiskLevel { get; set; }
        public object leagueSortValue { get; set; }
        public bool bonusDisabled { get; set; }
        
    }

    public class OdBetPickGroup
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public object sport { get; set; }
        public List<TipType> tipTypes { get; set; }
        public string handicapParam { get; set; }
        public string handicapParamValue { get; set; }
        public bool active { get; set; }
        public bool favorite { get; set; }
        public int orderNumber { get; set; }
        public string handicapParamDisplay { get; set; }
        public string handicapParamName { get; set; }
        public object lastChangedTime { get; set; }
        public bool showOnMain { get; set; }
        public bool showOnMobileMain { get; set; }
        public bool showOnSpecial { get; set; }
        public bool handicapParamMulti { get; set; }
        public string specialBetValueType { get; set; }
        public object formatCode { get; set; }
        public int lineCode { get; set; }
        public bool hideHeader { get; set; }
        public string specialValuePosition { get; set; }
        public object picksPerRow { get; set; }
        public object instanceCode { get; set; }
        public object sportTypeValue { get; set; }
        public bool showOnSuper { get; set; }
        public object tmstmp { get; set; }
        public bool hidePicksWithoutOdd { get; set; }
        public object betMedTranslation { get; set; }
        public List<object> picks { get; set; }
        public object displaySpecifiers { get; set; }
        public bool showOnHeader { get; set; }
    }

    public class Root
    {
        public int betLeagueId { get; set; }
        public string name { get; set; }
        public int leagueCode { get; set; }
        public object? description { get; set; }
        public string sport { get; set; }
        public bool active { get; set; }
        public bool favorite { get; set; }
        public int orderNumber { get; set; }
        public bool single { get; set; }
        public bool blocked { get; set; }
        public ImatchCBL imatchCBL { get; set; }
        public int flagId { get; set; }
        public int numOfMatches { get; set; }
        public object leagueType { get; set; }
        public bool prepaymentDisabled { get; set; }
        public object sortValue { get; set; }
        public string sportName { get; set; }
        public List<MatchList> matchList { get; set; }
    }

    public class TipType
    {
        public string tipType { get; set; }
        public int tipTypeId { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string caption { get; set; }
        public double value { get; set; }
        public object tipTypeTag { get; set; }
        public bool mainType { get; set; }
    }


}
