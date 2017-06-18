using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSNumberTumbler.Models.Repositories
{
    public interface IShuffleRepository
    {
        #region Shuffles
        IEnumerable<Shuffle> GetAllShuffles();
        IEnumerable<Shuffle> GetShufflesByNumberSetId(int numberSetId);
        Shuffle GetShuffle(int shuffleId);
        void AddShuffle(Shuffle newShuffle);
        void EditShuffle(Shuffle shuffleToEdit);
        void DeleteShuffle(int shuffleId);
        void AddShuffleNumbers(IEnumerable<ShuffleNumber> ShuffleNumbers);
        void DeleteShuffleNumbers(IEnumerable<ShuffleNumber> shuffleNumbers);
        Shuffle UpsertShuffle(int shuffleId, int numberSetId,string description);
        #endregion Shuffles
    }
}
