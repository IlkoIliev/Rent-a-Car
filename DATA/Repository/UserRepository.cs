using DATA.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Repository
{
    public class UserRepository
    {
        private readonly RentACarDbContext _dbContext;

        public UserRepository(RentACarDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IQueryable<User> GetAll()
        {
            return this._dbContext.Users.AsQueryable();
        }

        public IQueryable<User> GetAll(Expression<Func<User, bool>> predicate)
        {
            return this._dbContext.Users.Where(predicate).AsQueryable();
        }

        public User GetOne(int id)
        {
            return this._dbContext.Users.Find(id);
        }

        public User GetOne(Expression<Func<User, bool>> predicate)
        {
            return this._dbContext.Users.FirstOrDefault(predicate);
        }

        public void Add(User entity)
        {
            this._dbContext.Users.Add(entity);
            this._dbContext.SaveChanges();
        }

        public void Update(User entity)
        {
            this._dbContext.Entry(entity).State = EntityState.Modified;
            this._dbContext.SaveChanges();
        }

        public void Remove(User entity)
        {
            this._dbContext.Users.Remove(entity);
            this._dbContext.SaveChanges();
        }
    }
}
