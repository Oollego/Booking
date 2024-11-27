using Booking.Domain.Entity;
using Booking.Domain.Interfaces.Repositories;
using Booking.Domain.Interfaces.UnitsOfWork;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.DAL.UnitOfWork
{
    public class AuthUnitOfWork : IAuthUnitOfWork
    {
        public IBaseRepository<User> Users { get; set; }
        public IBaseRepository<Role> Roles { get; set; }
        public IBaseRepository<UserRole> UserRoles { get; set; }
        public IBaseRepository<UserProfile> UserProfiles { get; set; }
        public IBaseRepository<UserToken> UserTokens { get; set; }


        private readonly ApplicationDbContext _context;

        public AuthUnitOfWork(ApplicationDbContext context, IBaseRepository<User> users,
            IBaseRepository<Role> roles, IBaseRepository<UserRole> userRoles, IBaseRepository<UserProfile> userProfiles, 
            IBaseRepository<UserToken> userTokens)
        {
            _context = context;
            Users = users;
            Roles = roles;
            UserRoles = userRoles;
            UserProfiles = userProfiles;
            UserTokens = userTokens;
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync()
        {
            return await _context.Database.BeginTransactionAsync();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
