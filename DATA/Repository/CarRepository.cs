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
    public class CarRepository
    {
        private readonly RentACarDbContext _dbContext;

        public CarRepository(RentACarDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public IQueryable<Car> GetAll()
        {
            return this._dbContext.Cars.AsQueryable();
        }

        public IQueryable<Car> GetAllCarsInRent()
        {
            var cars =
            from c in this._dbContext.Cars
            join r in this._dbContext.Rents
           on c.Id equals r.CarId
            where r.EndDate <= DateTime.Today
            select new Car
            {
                Id = c.Id,
            };

            return cars.AsQueryable();
        }

        public IQueryable<Car> GetAll(Expression<Func<Car, bool>> predicate)
        {
            return this._dbContext.Cars.Where(predicate).AsQueryable();
        }

        public Car GetOne(int id)
        {
            return this._dbContext.Cars.Find(id);
        }

        public Car GetOne(Expression<Func<Car, bool>> predicate)
        {
            return this._dbContext.Cars.FirstOrDefault(predicate);
        }

        public void Add(Car entity)
        {
            this._dbContext.Cars.Add(entity);
            this._dbContext.SaveChanges();
        }

        public void Update(Car entity)
        {
            this._dbContext.Entry(entity).State = EntityState.Modified;
            this._dbContext.SaveChanges();
        }

        public void Remove(Car entity)
        {
            this._dbContext.Cars.Remove(entity);
            this._dbContext.SaveChanges();
        }
    }
}
