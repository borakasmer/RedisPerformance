using System;
using System.Collections.Generic;

namespace RedisExample.Entities
{
    public partial class DbUser
    {
        public DbUser()
        {
            DbSecurityUserActions = new HashSet<DbSecurityUserAction>();
        }

        public int IdUser { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Gsm { get; set; }
        public bool IsDeleted { get; set; }
        public bool? IsAdmin { get; set; }
        public int? CreUser { get; set; }
        public DateTime CreDate { get; set; }
        public int? ModUser { get; set; }
        public DateTime? ModDate { get; set; }
        public bool Deleted { get; set; }

        public virtual ICollection<DbSecurityUserAction> DbSecurityUserActions { get; set; }
    }
}
