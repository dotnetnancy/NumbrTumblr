using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSNumberTumbler.Models;

namespace VSNumberTumbler.Services
{
    public interface INumberSetService
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
