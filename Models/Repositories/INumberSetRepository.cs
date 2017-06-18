using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSNumberTumbler.Models.Repositories
{
    public interface INumberSetRepository
    {
        #region NumberSets
        IEnumerable<NumberSet> GetAllNumberSets();
        NumberSet GetNumberSet(int numberSetId);
        void AddNumberSet(NumberSet newNumberSet);
        void EditNumberSet(NumberSet numberSetToEdit);
        void DeleteNumberSet(int numberSetId);
        void AddNumberSetNumbers(IEnumerable<NumberSetNumber> NumberSetNumbers);
        void DeleteNumberSetNumbers(IEnumerable<NumberSetNumber> numberSetNumbers);
        #endregion NumberSets

    }
}
