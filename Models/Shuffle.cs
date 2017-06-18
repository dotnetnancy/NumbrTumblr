using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace VSNumberTumbler.Models
{
    public class Shuffle
    {      
        [Key]
        public int ShuffleID { get; set; }
        public DateTime ShuffleDateTime { get; set; }   
        public string ShuffleDescription { get; set; }
        public int NumberSetID { get; set; }     
        public NumberSet NumberSet { get; set; }       
        public ICollection<ShuffleNumber> ShuffleNumbers { get; set; }
       
    //    public string ResultOfShuffleConcatenated
    //    {
    //        get
    //        {
    //            if(ResultOfShuffle != null && ResultOfShuffle.Count > 0)
    //            {
    //                return string.Join(",", ResultOfShuffle.Select(x => x.Number.ToString()));
    //            }
    //            return string.Empty;
    //        }
    //    }
       
    //    public string ResultOfShufflePicksConcatenated
    //    {
    //        get
    //        {
    //            if(ResultOfShuffle != null && ResultOfShuffle.Count > 0)
    //            {
    //                return string.Join(",", ResultOfShuffle.Where(y=> y.Picked).Select(x => x.Number.ToString()));
    //            }
    //            return string.Empty;
    //        }
    //    }
    }
}
