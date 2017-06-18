using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VSNumberTumbler.Models
{
    public class NumberSet
    {       
        [Key]
        public int NumberSetID { get; set; }
        public string NumberSetName { get; set; }
        public string NumberSetDescription { get; set; }

        public int NumberSetMax { get; set; }
        public int NumberSetMin { get; set; }
      
        public ICollection<NumberSetNumber> NumberSetNumbers { get; set; }
        public bool IsApplicationNumberPool { get; set; }
    }
}
