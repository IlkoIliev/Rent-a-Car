using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Entities
{
    [Description("Резервации на автомобили")]
    public partial class Rent
    {
        [Description("Първичен ключ")]
        public int Id { get; set; }
        [Description("Вторичен ключ сочещ към нает автомобил")]
        public int CarId { get; set; }
        [Description("Начална дата на наем на автомобил")]
        public DateTime StartDate { get; set; }
        [Description("Крайна дата на наем на автомобил")]
        public DateTime EndDate { get; set; }
        [Description("Вторичен ключ сочещ към потребител наел автомобила")]
        public int UserId { get; set; }
        [DefaultValue(0)]
        [Description("Състояние на питането за наем")]
        public byte State { get; set; }

        public virtual Car Car { get; set; }
        public virtual User User { get; set; }
        //public object Price { get; internal set; }
    }
}
