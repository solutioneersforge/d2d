using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionAppDoc2Data.Mappers;
public static class CompanyUserMapper
{
    public static List<CompanyUserDTO> GetCompanyUserDTOs(this List<CompanyMember> companyMembers)
    {
        if (companyMembers == null)
        {
            throw new ArgumentNullException(nameof(CompanyUserDTO));
        }
        return companyMembers.Select(m => new CompanyUserDTO()
        {
            Active = m.User.IsActive.GetValueOrDefault() ? "Yes" : "No",
            Email = m.User.Email,
            FirstName = m.User.FirstName,
            IsActive = m.User.IsActive.GetValueOrDefault(),
            LastName = m.User.LastName,
            RoleId = m.RoleId,
            RoleName = m.Role.RoleName,
            UserId = m.User.UserId,
        }).ToList();
    }
}
