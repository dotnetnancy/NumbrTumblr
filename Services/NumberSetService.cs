using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSNumberTumbler.Domain;
using VSNumberTumbler.Models;

namespace VSNumberTumbler.Services
{
    public class NumberSetService : INumberSetService
    {
        INumberSetDomain _numberSetDomain;

        public NumberSetService(INumberSetDomain numberSetDomain)
        {
            _numberSetDomain = numberSetDomain;
        }

        public IEnumerable<NumberSet> GetAllNumberSets()
        {
            return _numberSetDomain.GetAllNumberSets();
        }
        public NumberSet GetNumberSet(int numberSetId)
        {
            return _numberSetDomain.GetNumberSet(numberSetId);
        }
        public bool AddNumberSet(NumberSet newNumberSet)
        {
           return _numberSetDomain.AddNumberSet(newNumberSet);           
        }
        public bool EditNumberSet(NumberSet numberSetToEdit)
        {
            return _numberSetDomain.EditNumberSet(numberSetToEdit);
        }
        public bool DeleteNumberSet(int numberSetId)
        {
            return _numberSetDomain.DeleteNumberSet(numberSetId);
        }
        public bool AddNumberSetNumbers(IEnumerable<NumberSetNumber> numberSetNumbers)
        {
            return _numberSetDomain.AddNumberSetNumbers(numberSetNumbers);
        }
        public bool DeleteNumberSetNumbers(IEnumerable<NumberSetNumber> numberSetNumbers)
        {
            return _numberSetDomain.DeleteNumberSetNumbers(numberSetNumbers);
        }
    }
}
