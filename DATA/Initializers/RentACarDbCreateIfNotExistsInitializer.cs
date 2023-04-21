using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Initializers
{
    public class RentACarDbCreateIfNotExistsInitializer : CreateDatabaseIfNotExists<RentACarDbContext>
    {
        protected override void Seed(RentACarDbContext context)
        {
            Seeder.Seed(context);

            base.Seed(context);
        }
    }
}
