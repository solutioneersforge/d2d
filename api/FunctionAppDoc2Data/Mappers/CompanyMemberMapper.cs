using FunctionAppDoc2Data.Constants;
using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using System;

namespace FunctionAppDoc2Data.Mappers;
public static class CompanyMemberMapper
{
    public static CompanyMember MapToCompanyMember(this UserRegisterModelDTO userRegister, Guid userId)
    {
        if (userRegister == null)
        {
            throw new ArgumentNullException(nameof(UserRegisterModelDTO));
        }
        return new CompanyMember()
        {
            CompanyId = userRegister.CompanyId.GetValueOrDefault(),
            CreatedAt = DateTime.UtcNow,
            RoleId = !String.IsNullOrEmpty(userRegister.CompanyName) ? Guid.Parse(RolesConstant.Manager) : userRegister.RoleId.GetValueOrDefault(),
            UserId = userId
        };
    }
}
