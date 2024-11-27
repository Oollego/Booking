using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Result
{
    public class OperationResult<T>
    {
        public bool Success  => ErrorMessage == null;
        public T? Data { get; set; }
        public string ErrorMessage { get; set; } = null!;
    }
}

