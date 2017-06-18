using System.Collections.Generic;

namespace VSNumberTumbler.ViewModels
{
    public class NumberSetViewModel
    {       
        public int NumberSetID { get; set; }
        public string NumberSetName { get; set; }
        public string NumberSetDescription { get; set; }

        public int NumberSetMax { get; set; }
        public int NumberSetMin { get; set; }
      
        public List<NumberSetNumberViewModel> NumberSetNumbers { get; set; }
        public bool IsApplicationNumberPool { get; set; }
    }
}
