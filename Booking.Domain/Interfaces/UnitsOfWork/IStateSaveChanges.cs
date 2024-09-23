using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Interfaces.UnitsOfWork
{
    public interface IStateSaveChanges
    {
        Task<int> SaveChangesAsync();
    }
}
