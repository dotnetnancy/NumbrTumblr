using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VSNumberTumbler.Models
{
    public class NumberTumblerRepository : INumberTumblerRepository
    {
        NumberTumblerContext _context;
        public NumberTumblerRepository(NumberTumblerContext context)
        {
            _context = context;            
        }

        public async Task<bool> SaveChangesAsync()
        {
            int result = default(int);
            result = await _context.SaveChangesAsync();
            return result > 0;
        }
    }
}
