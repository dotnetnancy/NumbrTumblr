using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSNumberTumbler.Models.Repositories
{
    public class NumberSetRepository : NumberTumblerRepository, INumberSetRepository
    {

        NumberTumblerContext _context;
        public NumberSetRepository(NumberTumblerContext context):base(context)
        {
            _context = context;
        }
        #region NumberSets
        public IEnumerable<NumberSet> GetAllNumberSets()
        {
            return _context
                .NumberSets
                .Include(x => x.NumberSetNumbers)
                .ToList();
        }

        public NumberSet GetNumberSet(int numberSetId)
        {
            return _context
                .NumberSets
                .Include(x => x.NumberSetNumbers)
                .Where(x => x.NumberSetID == numberSetId)
                .FirstOrDefault();
        }

        public void AddNumberSet(NumberSet newNumberSet)
        {
            _context.Add(newNumberSet);
        }

        public void AddNumberSetNumbers(IEnumerable<NumberSetNumber> numberSetNumbers)
        {
            foreach (NumberSetNumber numberSetNumber in numberSetNumbers)
            {
                var numberSetEntity = _context
                    .NumberSets
                    .Include(x => x.NumberSetNumbers)
                    .Where(x => x.NumberSetID == numberSetNumber.NumberSetID).FirstOrDefault();
                //cannot find number set bad
                if (numberSetEntity == null)
                    continue; //numberSet not found

                //add number set number cause it does not exist
                if (numberSetEntity.NumberSetNumbers == null || (!numberSetEntity.NumberSetNumbers.Any())
                    || (!numberSetEntity
                    .NumberSetNumbers
                    .Select(x => x.Number).Any(y => y == numberSetNumber.Number)))
                {
                    _context.NumberSetNumbers.Add(
                        new NumberSetNumber
                        {
                            Number = numberSetNumber.Number,
                            NumberSetID = numberSetNumber.NumberSetID,
                            SelectedNumber = numberSetNumber.SelectedNumber
                        });
                }
                //update number set number to whatever the selected state is passed in
                else
                {
                    var numberSetNumberEntity = numberSetEntity
                    .NumberSetNumbers
                    .Where(y => y.Number == numberSetNumber.Number).FirstOrDefault();

                    if (numberSetNumberEntity != null)
                    {
                        numberSetNumberEntity.SelectedNumber = numberSetNumber.SelectedNumber;
                    }
                }
            }
        }

        public void DeleteNumberSetNumbers(IEnumerable<NumberSetNumber> numberSetNumbers)
        {
            foreach (NumberSetNumber numberSetNumber in numberSetNumbers)
            {
                var numberSetEntity = _context.NumberSets.Find(numberSetNumber.NumberSetID);
                if (numberSetEntity == null)
                    continue; //numberSet not found

                if (!numberSetEntity.NumberSetNumbers.Select(x => x.Number).Any(y => y == numberSetNumber.Number))
                {
                    var numberSetNumberEntity =
                        _context.NumberSetNumbers.Find(numberSetNumber.NumberSetID, numberSetNumber.NumberSetNumberID);
                    if (numberSetNumberEntity == null)
                    {
                        _context.Entry(numberSetNumberEntity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
                    }
                }
            }
        }



        public void EditNumberSet(NumberSet numberSetToEdit)
        {
            var entity = _context.NumberSets.Find(numberSetToEdit.NumberSetID);
            if (entity == null)
                return;
            _context.Entry(entity).CurrentValues.SetValues(numberSetToEdit);
        }

        public void DeleteNumberSet(int numberSetID)
        {
            var entity = _context.NumberSets.Find(numberSetID);
            if (entity == null)
                return;
            _context.Entry(entity).State = Microsoft.EntityFrameworkCore.EntityState.Deleted;
        }
        #endregion NumberSets   


    }
}
