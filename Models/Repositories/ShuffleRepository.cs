using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSNumberTumbler.Models.Repositories
{
    public class ShuffleRepository : NumberTumblerRepository, IShuffleRepository
    {
        NumberTumblerContext _context;

        public ShuffleRepository(NumberTumblerContext context) : base(context)
        {
            _context = context;
        }
        #region Shuffles

        public IEnumerable<Shuffle> GetAllShuffles()
        {
            return _context
                .Shuffles
                .Include(x => x.ShuffleNumbers)
                .ToList();
        }

        public IEnumerable<Shuffle> GetShufflesByNumberSetId(int numberSetId)
        {
            return _context
                .Shuffles
                .Include(x => x.ShuffleNumbers)
                .Where(y => y.NumberSetID == numberSetId)
                .ToList();
        }

        public Shuffle GetShuffle(int shuffleId)
        {
            return _context
                .Shuffles
                .Include(x => x.ShuffleNumbers)
                .Where(x => x.ShuffleID == shuffleId)
                .FirstOrDefault();
        }

        public void AddShuffle(Shuffle newShuffle)
        {
            _context.Add(newShuffle);
        }
        public Shuffle UpsertShuffle(int shuffleId, int numberSetId, string description)
        {
            var shuffle = _context.Shuffles.Where(x => x.ShuffleID == shuffleId).FirstOrDefault();
            if (shuffle == null)
            {
                shuffle = new Shuffle
                {
                    NumberSetID = numberSetId,
                    ShuffleDescription = description,
                    ShuffleDateTime = DateTime.UtcNow
                };
                _context.Shuffles.Add(shuffle);
            }
            return shuffle;
        }

        public void AddShuffleNumbers(IEnumerable<ShuffleNumber> shuffleNumbers)
        {

            foreach (ShuffleNumber shuffleNumber in shuffleNumbers)
            {
                var shuffleEntity = _context
                    .Shuffles
                    .Include(x => x.ShuffleNumbers)
                    .Where(x => x.ShuffleID == shuffleNumber.ShuffleID).FirstOrDefault();
                //cannot find an either created or updated shuffle
                if (shuffleEntity != null)
                {                 
                    if (shuffleEntity.ShuffleNumbers == null || (!shuffleEntity.ShuffleNumbers.Any())
                        || (!shuffleEntity
                        .ShuffleNumbers
                        .Select(x => x.Number).Any(y => y == shuffleNumber.Number)))
                    {
                        _context.ShuffleNumbers.Add(
                            new ShuffleNumber
                            {
                                Number = shuffleNumber.Number,
                                ShuffleID = shuffleNumber.ShuffleID,
                                SelectedNumber = shuffleNumber.SelectedNumber
                            });
                    }
                    //update shuffle number selected to whatever the selected state is passed in
                    else
                    {
                        var shuffleNumberEntity = shuffleEntity
                        .ShuffleNumbers
                        .Where(y => y.Number == shuffleNumber.Number).FirstOrDefault();

                        if (shuffleNumberEntity != null)
                        {
                            shuffleNumberEntity.SelectedNumber = shuffleNumber.SelectedNumber;
                        }
                    }
                }
            }
        }

        public void DeleteShuffleNumbers(IEnumerable<ShuffleNumber> shuffleNumbers)
        {
            foreach (ShuffleNumber shuffleNumber in shuffleNumbers)
            {
                var shuffleEntity = _context
                   .Shuffles
                   .Include(x => x.ShuffleNumbers)
                   .Where(x => x.ShuffleID == shuffleNumber.ShuffleID).FirstOrDefault();
                if (shuffleEntity == null)
                    continue; //shuffle not found

                if (!shuffleEntity.ShuffleNumbers.Select(x => x.Number).Any(y => y == shuffleNumber.Number))
                {
                    var shuffleNumberEntity =
                        _context.ShuffleNumbers.Find(shuffleNumber.ShuffleID, shuffleNumber.ShuffleNumberID);
                    if (shuffleNumberEntity == null)
                    {
                        _context.Entry(shuffleNumberEntity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    }
                }
            }
        }



        public void EditShuffle(Shuffle shuffleToEdit)
        {
            var shuffleEntity = _context
                    .Shuffles
                    .Include(x => x.ShuffleNumbers)
                    .Where(x => x.ShuffleID == shuffleToEdit.ShuffleID).FirstOrDefault();
            if (shuffleEntity == null)
                return;
            foreach (var shuffleNumberEntity in shuffleEntity.ShuffleNumbers)
            {
                _context
                    .Entry(shuffleNumberEntity)
                    .CurrentValues
                    .SetValues(shuffleToEdit.ShuffleNumbers.Where(x=> x.ShuffleNumberID == shuffleNumberEntity.ShuffleNumberID).FirstOrDefault());
            }
        }

        public void DeleteShuffle(int shuffleID)
        {
            var entity = _context.Shuffles.Find(shuffleID);
            if (entity == null)
                return;
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }
        #endregion Shuffles


    }
}
