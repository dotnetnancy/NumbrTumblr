using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSNumberTumbler.Domain;
using VSNumberTumbler.Models;
using VSNumberTumbler.ViewModels;

namespace VSNumberTumbler.Services
{
    public class ShuffleService : IShuffleService
    {
        IShuffleDomain _shuffleDomain;
        INumberSetDomain _numberSetDomain;

        public ShuffleService(IShuffleDomain shuffleDomain,INumberSetDomain numberSetDomain)
        {
            _shuffleDomain = shuffleDomain;
            _numberSetDomain = numberSetDomain;
        }

        public IEnumerable<Shuffle> GetAllShuffles()
        {
            return _shuffleDomain.GetAllShuffles();
        }
        public IEnumerable<Shuffle> GetShufflesByNumberSetId(int numberSetId)
        {
            return _shuffleDomain.GetShufflesByNumberSetId(numberSetId);
        }
        public Shuffle GetShuffle(int shuffleId)
        {
            return _shuffleDomain.GetShuffle(shuffleId);
        }
        public bool AddShuffle(Shuffle newShuffle)
        {
            return _shuffleDomain.AddShuffle(newShuffle);
        }
        public bool EditShuffle(Shuffle shuffleToEdit)
        {
            return _shuffleDomain.EditShuffle(shuffleToEdit);
        }
        public bool DeleteShuffle(int shuffleId)
        {
            return _shuffleDomain.DeleteShuffle(shuffleId);
        }
        public bool AddShuffleNumbers(IEnumerable<ShuffleNumber> shuffleNumbers)
        {
            return _shuffleDomain.AddShuffleNumbers(shuffleNumbers);
        }
        public bool DeleteShuffleNumbers(IEnumerable<ShuffleNumber> shuffleNumbers)
        {
            return _shuffleDomain.DeleteShuffleNumbers(shuffleNumbers);
        }
        public Shuffle UpsertShuffle(IEnumerable<ShuffleNumber> shuffleNumbers, int numberSetId,string shuffleDescription)
        {
            Shuffle shuffle = null;
            int shuffleId = default(int);

            if (shuffleNumbers != null && shuffleNumbers.Any())
            {
                shuffleId = shuffleNumbers.First().ShuffleID;

                shuffle =  _shuffleDomain.UpsertShuffle(shuffleId, numberSetId,shuffleDescription);
            }
            return shuffle;
        }

        public ICollection<ShuffleNumber> ShuffleNumbersByShuffleType(int numberSetId, string shuffleIncludeType)
        {
            ShuffleIncludeType includeType = (ShuffleIncludeType)Enum.Parse(typeof(ShuffleIncludeType), shuffleIncludeType);
            var numberSet = _numberSetDomain.GetNumberSet(numberSetId);

            ICollection<ShuffleNumber> shuffleResult = new List<ShuffleNumber>();

            if (numberSet != null && numberSet.NumberSetNumbers != null && numberSet.NumberSetNumbers.Any())
            {
                switch (includeType)
                {
                    case ShuffleIncludeType.All:
                        {
                            shuffleResult = _shuffleDomain.ShuffleAllNumbers(numberSet.NumberSetNumbers);
                            break;
                        }
                    case ShuffleIncludeType.Selected:
                        {
                            shuffleResult = _shuffleDomain.ShuffleSelectedNumbers(numberSet.NumberSetNumbers);
                            break;
                        }                    
                    default:
                        {
                            break;
                        }
                }
            }
            return shuffleResult;
        }
    }
}
