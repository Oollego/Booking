using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Entity
{
    public class User
    {
        public long Id { get; set; }
        public string UserEmail { get; set; } = null!;
        public string PasswordSalt { get; set; } = null!;
        public string PasswordDk { get; set;} = null!;
        public DateTime RegisteredAt { get; set; } = DateTime.Now;
        public DateTime? DeletedAt { get; set; }
        public long UpdatedBy {  get; set; }
        public UserProfile UserProfile { get; set; } = null!;
        public UserToken UserToken { get; set; } = null!;
        public List<Role> Roles { get; set; } = null!;
        public List<Book> Books { get; set; } = null!;
        public List<Review> Reviews { get; set; } = null!;
    }
}
