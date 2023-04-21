using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.Entities
{
    public partial class Role
    {
        [Description("Роли на потребителите")]
        public Role()
        {
            User = new HashSet<User>();
        }

        [Description("Първичен ключ")]
        public int Id { get; set; }
        [Description("Име на ролята")]
        public string Name { get; set; }

        public virtual ICollection<User> User { get; set; }
    }
}
