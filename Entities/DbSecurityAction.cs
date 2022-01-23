using System;
using System.Collections.Generic;

namespace RedisExample.Entities
{
    public partial class DbSecurityAction
    {
        public int IdSecurityAction { get; set; }
        public long? ActionNumber { get; set; }
        public string ActionName { get; set; }
        public int? IdSecurityController { get; set; }
        public bool? IsDeleted { get; set; }
        public int? CreUser { get; set; }
        public DateTime CreDate { get; set; }
        public int? ModUser { get; set; }
        public DateTime? ModDate { get; set; }
        public bool Deleted { get; set; }

        public virtual DbSecurityController IdSecurityControllerNavigation { get; set; }
    }
}
