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
    public class RoleUnitOfWork : IRoleUnitOfWork
    {
        public IBaseRepository<User> Users { get; set; } = null!;
        public IBaseRepository<Role> Roles { get; set; } = null!;
        public IBaseRepository<UserRole> UserRoles { get; set; } = null!;

        private readonly ApplicationDbContext _context;

        public RoleUnitOfWork(ApplicationDbContext context, IBaseRepository<User> users,
            IBaseRepository<Role> roles, IBaseRepository<UserRole> userRoles)
        {
            _context = context;
            Users = users;
            Roles = roles;
            UserRoles = userRoles;
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
