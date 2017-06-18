using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSNumberTumbler.Models.Repositories
{
    public class ShuffleRepository :NumberTumblerRepository, IShuffleRepository
    {
        NumberTumblerContext _context;

        public ShuffleRepository(NumberTumblerContext context):base(context)
        {
            _context = context;
        }

    }
}
