using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSNumberTumbler.Models;
using VSNumberTumbler.Models.Repositories;
using VSNumberTumbler.ViewModels;
using VSNumberTumbler.Common.ExtensionMethods;

namespace VSNumberTumbler.Domain
{
    public class ShuffleDomain : IShuffleDomain
    {
        IShuffleRepository _shuffleRepository;
        INumberSetRepository _numberSetRepository;


        public ShuffleDomain(IShuffleRepository shuffleRepository, INumberSetRepository numberSetRepository)
        {
            _shuffleRepository = shuffleRepository;
            _numberSetRepository = numberSetRepository;
        }

        public IEnumerable<Shuffle> GetAllShuffles()
        {
            return _shuffleRepository.GetAllShuffles();
        }
        public IEnumerable<Shuffle> GetShufflesByNumberSetId(int numberSetId)
        {
            return _shuffleRepository.GetShufflesByNumberSetId(numberSetId);
        }
        public Shuffle GetShuffle(int shuffleId)
        {
            return _shuffleRepository.GetShuffle(shuffleId);
        }
        public bool AddShuffle(Shuffle newShuffle)
        {
            _shuffleRepository.AddShuffle(newShuffle);
            return SaveData().Result;
        }
        public Shuffle UpsertShuffle(int shuffleId, int numberSetId, string description)
        {
            var shuffle = _shuffleRepository.UpsertShuffle(shuffleId, numberSetId, description);
            var result = SaveData().Result;
            return shuffle;
        }
        public bool EditShuffle(Shuffle shuffleToEdit)
        {
            _shuffleRepository.EditShuffle(shuffleToEdit);
            var result = SaveData().Result;
            //there may or may not be records changed
            return true;
        }
        public bool DeleteShuffle(int shuffleId)
        {
            _shuffleRepository.DeleteShuffle(shuffleId);
            return SaveData().Result;
        }

        public bool AddShuffleNumbers(IEnumerable<ShuffleNumber> shuffleNumbers)
        {
            _shuffleRepository.AddShuffleNumbers(shuffleNumbers);
            return SaveData().Result;
        }
        public bool DeleteShuffleNumbers(IEnumerable<ShuffleNumber> shuffleNumbers)
        {
            _shuffleRepository.DeleteShuffleNumbers(shuffleNumbers);
            return SaveData().Result;
        }

        public async Task<bool> SaveData()
        {
            if (await (_shuffleRepository as NumberTumblerRepository).SaveChangesAsync())
            {
                return true;
            }
            return false;
        }

        //pseudocode:  check if numbersetnumbers has values, create a new shuffleviewmodel object,
        //call to the extension method to return appropriately shuffled items
        //set the shufflenumbers property on the new shuffleviewmodel object to the set of shuffled numbers
        //then return the new object
        public ICollection<ShuffleNumber> ShuffleSelectedNumbers(ICollection<NumberSetNumber> numberSetNumbers)
        {
            ICollection<ShuffleNumber> result = new List<ShuffleNumber>();

            if (numberSetNumbers.Any())
            {
                var selectedNumberSetNumbers = numberSetNumbers.Where(x => x.SelectedNumber);
                if (selectedNumberSetNumbers.Any())
                {
                    result = ShuffleAllNumbers(selectedNumberSetNumbers.ToList());
                }
            }
            return result;
        }

        public ICollection<ShuffleNumber> ShuffleAllNumbers(ICollection<NumberSetNumber> numberSetNumbers)
        {
            ICollection<ShuffleNumber> result = new List<ShuffleNumber>();

            if (numberSetNumbers.Any())
            {
                var numberSetNumbersList = numberSetNumbers.ToList();
                numberSetNumbersList.FisherYatesShuffle(new Random(numberSetNumbers.FirstOrDefault().Number));
                int index = 0;
                var query = from pro in numberSetNumbersList
                            select new ShuffleNumber() { Order = index++, Number = pro.Number, SelectedNumber = pro.SelectedNumber };

                result = query.ToList();
            }
            return result;
        }
    }
}
