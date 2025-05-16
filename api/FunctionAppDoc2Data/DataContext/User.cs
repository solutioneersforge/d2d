using System;
using System.Collections.Generic;

namespace FunctionAppDoc2Data.DataContext
{
    public partial class User
    {
        public User()
        {
            CompanyMembers = new HashSet<CompanyMember>();
            ExpenseCategories = new HashSet<ExpenseCategory>();
            ReceiptApprovedByNavigations = new HashSet<Receipt>();
            ReceiptModifiedByNavigations = new HashSet<Receipt>();
            ReceiptUsers = new HashSet<Receipt>();
        }

        public Guid UserId { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public byte[] PasswordHash { get; set; }
        public DateTime CreatedAt { get; set; }
        public byte[] PasswordSalt { get; set; }
        public Guid? AuthenticationKey { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public bool IsTwoFactorEnabled { get; set; }
        public int? FailedLoginAttempts { get; set; }
        public Guid? ForgetPasswordKey { get; set; }
        public int? ForgetPasswordRetry { get; set; }
        public DateTime? LockoutEnd { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? ExpiredForgetPasswordKey { get; set; }
        public bool? IsExpiredKeyUsed { get; set; }

        public virtual ICollection<CompanyMember> CompanyMembers { get; set; }
        public virtual ICollection<ExpenseCategory> ExpenseCategories { get; set; }
        public virtual ICollection<Receipt> ReceiptApprovedByNavigations { get; set; }
        public virtual ICollection<Receipt> ReceiptModifiedByNavigations { get; set; }
        public virtual ICollection<Receipt> ReceiptUsers { get; set; }
    }
}
