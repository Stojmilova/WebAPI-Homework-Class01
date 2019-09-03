using DataModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess
{
    public class UserRepository :IRepository<UserDto>
    {
        private readonly MovieAppDbContext _context;
        public UserRepository(MovieAppDbContext context)
        {
            _context = context;
        }

        public void Add(UserDto entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        public void Delete(UserDto entity)
        {
            _context.Users.Remove(entity);
            _context.SaveChanges();
        }

        public IEnumerable<UserDto> GetAll()
        {
            return _context.Users;
        }

        public void Update(UserDto update)
        {
            _context.Users.Update(update);
            _context.SaveChanges();

        }
    }
}
