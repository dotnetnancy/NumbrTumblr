using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VSNumberTumbler.Models;
using VSNumberTumbler.ViewModels;

namespace VSNumberTumbler.Domain
{
    public interface IShuffleDomain
    {
        ICollection<ShuffleNumber> ShuffleSelectedNumbers(ICollection<NumberSetNumber> numberSetNumbers);

        ICollection<ShuffleNumber> ShuffleAllNumbers(ICollection<NumberSetNumber> numberSetNumbers);

        IEnumerable<Shuffle> GetAllShuffles();
        IEnumerable<Shuffle> GetShufflesByNumberSetId(int numberSetId);
        Shuffle GetShuffle(int shuffleId);
        bool AddShuffle(Shuffle newShuffle);
        bool EditShuffle(Shuffle shuffleToEdit);
        bool DeleteShuffle(int shuffleId);
        bool AddShuffleNumbers(IEnumerable<ShuffleNumber> ShuffleNumbers);
        bool DeleteShuffleNumbers(IEnumerable<ShuffleNumber> shuffleNumbers);
        Shuffle UpsertShuffle(int shuffleId, int numberSetId,string shuffleDescription);
        Task<bool> SaveData();
       
    }
}
