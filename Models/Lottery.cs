
namespace VSNumberTumbler.Models
{    
    public class Lottery
    {        
        public int LotteryID { get; set; }
        public string LotteryName { get; set; }
        public string LotteryDescription { get; set; }
        public int? LotteryNumberMax { get; set; }
        public int? LotteryNumberMin { get; set; }      
    }
}
