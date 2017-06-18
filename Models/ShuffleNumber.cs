
using System.ComponentModel.DataAnnotations;

namespace VSNumberTumbler.Models
{
    public class ShuffleNumber
    {
        [Key]
        public int ShuffleNumberID { get; set; }
        public int Order { get; set; }
        public int Number { get; set; }
        public bool SelectedNumber { get; set; }
        public int ShuffleID { get; set; }
        public virtual Shuffle Shuffle {get;set;}
    }
}
