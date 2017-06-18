using System.Collections.Generic;
using System.Threading.Tasks;

namespace VSNumberTumbler.Models
{
    public interface INumberTumblerRepository
    {    
        Task<bool> SaveChangesAsync();
    }
}