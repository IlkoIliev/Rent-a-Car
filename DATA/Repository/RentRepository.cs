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
    public class RentRepository
    {
        private readonly RentACarDbContext _dbContext;

        public RentRepository(RentACarDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IQueryable<Rent> GetAll()
        {
            return this._dbContext.Rents.AsQueryable();
        }

        public IQueryable<Rent> GetAll(Expression<Func<Rent, bool>> predicate)
        {
            return this._dbContext.Rents.Where(predicate).AsQueryable();
        }
        
        public List<Rent> GetOneRentDetails(int id)
        {
            var rents = _dbContext.Rents
                        .Include(r => r.User)
                        .Include(r => r.Car)
                        .Where(r => r.Id == id
                        ).ToList();

            return rents;
        }
        
        public Rent GetOne(int id)
        {
            return this._dbContext.Rents.Find(id);
        }

        public Rent GetOne(Expression<Func<Rent, bool>> predicate)
        {
            return this._dbContext.Rents.FirstOrDefault(predicate);
        }

        public void Add(Rent entity)
        {
            this._dbContext.Rents.Add(entity);
            this._dbContext.SaveChanges();
        }

        public void Update(Rent entity)
        {
            this._dbContext.Entry(entity).State = EntityState.Modified;
            this._dbContext.SaveChanges();
        }

        public void Remove(Rent entity)
        {
            this._dbContext.Rents.Remove(entity);
            this._dbContext.SaveChanges();
        }
    }
}
