using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSNumberTumbler.Models;
using VSNumberTumbler.ViewModels;

namespace VSNumberTumbler.Services
{
    public interface IShuffleService
    {
        ICollection<ShuffleNumber> ShuffleNumbersByShuffleType(int numberSetId, string shuffleIncludeType);
        IEnumerable<Shuffle> GetAllShuffles();
        IEnumerable<Shuffle> GetShufflesByNumberSetId(int numberSetId);
        Shuffle GetShuffle(int shuffleId);
        bool AddShuffle(Shuffle newShuffle);
        bool EditShuffle(Shuffle shuffleToEdit);
        bool DeleteShuffle(int shuffleId);
        bool AddShuffleNumbers(IEnumerable<ShuffleNumber> ShuffleNumbers);
        bool DeleteShuffleNumbers(IEnumerable<ShuffleNumber> shuffleNumbers);
        Shuffle UpsertShuffle(IEnumerable<ShuffleNumber> shuffleNumbers, int numberSetId, string shuffleDescription);
    }
}
