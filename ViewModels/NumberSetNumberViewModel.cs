namespace VSNumberTumbler.ViewModels
{
    public class NumberSetNumberViewModel
    {        
        public int NumberSetNumberID { get; set; } 
        public int Number { get; set; }       
        public int NumberSetID { get; set; }      
        //when this is the in the viewmodel the json serialization causes stackoverflow
        //public NumberSetViewModel NumberSet { get; set; }       
        public bool SelectedNumber { get; set; }
    }
}
