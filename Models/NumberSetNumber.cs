using System.ComponentModel.DataAnnotations;

namespace VSNumberTumbler.Models
{
    public class NumberSetNumber
    {    
        [Key]
        public int NumberSetNumberID { get; set; } 
        public int Number { get; set; }       
        public int NumberSetID { get; set; }   
        public bool SelectedNumber { get; set; }
        public virtual NumberSet NumberSet { get; set; }
    }
}
