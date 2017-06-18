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
      
    }
}
