using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class Role
    {
        public Role()
        {
            CompanyMembers = new HashSet<CompanyMember>();
        }

        public Guid RoleId { get; set; }
        public string RoleName { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<CompanyMember> CompanyMembers { get; set; }
    }
}
