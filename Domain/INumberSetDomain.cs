using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSNumberTumbler.Models;

namespace VSNumberTumbler.Domain
{
    public interface INumberSetDomain
    {
        IEnumerable<NumberSet> GetAllNumberSets();
        NumberSet GetNumberSet(int numberSetId);
        bool AddNumberSet(NumberSet newNumberSet);
        bool EditNumberSet(NumberSet numberSetToEdit);
        bool DeleteNumberSet(int numberSetId);
        bool AddNumberSetNumbers(IEnumerable<NumberSetNumber> NumberSetNumbers);
        bool DeleteNumberSetNumbers(IEnumerable<NumberSetNumber> numberSetNumbers);
    }
}
