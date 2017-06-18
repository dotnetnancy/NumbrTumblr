using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSNumberTumbler.Models;
using VSNumberTumbler.Models.Repositories;

namespace VSNumberTumbler.Domain
{
    public class NumberSetDomain : INumberSetDomain
    {
        INumberSetRepository _numberSetRepository;
        public NumberSetDomain(INumberSetRepository numberSetRepository)
        {
            _numberSetRepository = numberSetRepository;
        }

        public IEnumerable<NumberSet> GetAllNumberSets()
        {
            return _numberSetRepository.GetAllNumberSets();
        }
        public NumberSet GetNumberSet(int numberSetId)
        {
            return _numberSetRepository.GetNumberSet(numberSetId);
        }
        public bool AddNumberSet(NumberSet newNumberSet)
        {
            _numberSetRepository.AddNumberSet(newNumberSet);
            return SaveData().Result;
        }
        public bool EditNumberSet(NumberSet numberSetToEdit)
        {
            _numberSetRepository.EditNumberSet(numberSetToEdit);
            return SaveData().Result;
        }
        public bool DeleteNumberSet(int numberSetId)
        {
            _numberSetRepository.DeleteNumberSet(numberSetId);
            return SaveData().Result;
        }
        public bool AddNumberSetNumbers(IEnumerable<NumberSetNumber> numberSetNumbers)
        {
            _numberSetRepository.AddNumberSetNumbers(numberSetNumbers);
            return SaveData().Result;
        }
        public bool DeleteNumberSetNumbers(IEnumerable<NumberSetNumber> numberSetNumbers)
        {
            _numberSetRepository.DeleteNumberSetNumbers(numberSetNumbers);
            return SaveData().Result;
        }

        public async Task<bool> SaveData()
        {
            if (await (_numberSetRepository as NumberTumblerRepository).SaveChangesAsync())
            {
                return true;
            }
            return false;
        }
    }
}
