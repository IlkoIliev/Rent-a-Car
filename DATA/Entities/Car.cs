using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Entities
{
    [Description("Съхранява данни за всички автомобили")]
    public partial class Car
    {
        public Car()
        {
            Rent = new HashSet<Rent>();
        }

        [Description("Първичен ключ")]
        public int Id { get; set; }
        [Description("Марка на автомобила")]
        public string Brand { get; set; }
        [Description("Модел на автомобила")]
        public string Model { get; set; }
        [Description("Година на производство на автомобила")]
        public short Year { get; set; }
        [Description("Брой пасажерски места")]
        public byte Seats { get; set; }
        [Description("Наем на автомобила")]
        public decimal Price { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Rent> Rent { get; set; }
    }
}
