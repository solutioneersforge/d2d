using System;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class CompanyMember
    {
        public Guid MemberId { get; set; }
        public Guid CompanyId { get; set; }
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool? IsActive { get; set; }

        public virtual Company Company { get; set; }
        public virtual Role Role { get; set; }
        public virtual User User { get; set; }
    }
}
