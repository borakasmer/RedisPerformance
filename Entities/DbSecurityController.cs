using System;
using System.Collections.Generic;

namespace RedisExample.Entities
{
    public partial class DbSecurityController
    {
        public DbSecurityController()
        {
            DbSecurityActions = new HashSet<DbSecurityAction>();
            DbSecurityUserActions = new HashSet<DbSecurityUserAction>();
        }

        public int IdSecurityController { get; set; }
        public string ControllerName { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreUser { get; set; }
        public DateTime CreDate { get; set; }
        public int? ModUser { get; set; }
        public DateTime? ModDate { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<DbSecurityAction> DbSecurityActions { get; set; }
        public virtual ICollection<DbSecurityUserAction> DbSecurityUserActions { get; set; }
    }
}
