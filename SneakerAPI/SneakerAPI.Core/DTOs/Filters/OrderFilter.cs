namespace SneakerAPI.Core.Models.Filters
{   
    public class RangeDateTime{
        public DateTime? From {get;set;} = null;
        public DateTime? To {get;set;} = null;
    }
    public class OrderFilter
    {
        public int? Order__Status {get;set;}=null;
        public int? Payment__Status {get;set;}=null;
        public RangePrice? RangePrice {get;set;}
        public RangeDateTime? RangeDateTime {get;set;}
    }
}