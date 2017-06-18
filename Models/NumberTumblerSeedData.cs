using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSNumberTumbler.Models
{
    public class NumberTumblerSeedData
    {
        NumberTumblerContext _context;

        public NumberTumblerSeedData(NumberTumblerContext context)
        {
            _context = context;
        }

        public async Task EnsureSeedData()
        {
            if (!_context.NumberSets.Any())
            {
                //if there are no number sets then we need to seed our canned
                //numbersets
                var numberSetExample = new NumberSet()
                {
                    //NumberSetID = 1,
                    NumberSetName = "Example Number Set",
                    NumberSetDescription = "Delete this later if you wish",
                    NumberSetMax = 75,
                    NumberSetMin = 1,                    
                    IsApplicationNumberPool = false
                };
                numberSetExample.NumberSetNumbers = GetExampleNumberSetNumbers(numberSetExample);
                _context.NumberSets.Add(numberSetExample);
                _context.NumberSetNumbers.AddRange(numberSetExample.NumberSetNumbers);
            }
            await _context.SaveChangesAsync();
           
        }

        private ICollection<NumberSetNumber> GetExampleNumberSetNumbers(NumberSet numberSet)
        {
            var numberSetNumbers = new List<NumberSetNumber>();

            for(int i = 0; i < 75; i++)
            {
                numberSetNumbers.Add(new NumberSetNumber()
                {
                    Number = i++,                    
                    NumberSetID = numberSet.NumberSetID,
                   // NumberSetNumberID = i                     
                });
            }
            return numberSetNumbers;
        }
    }
}
