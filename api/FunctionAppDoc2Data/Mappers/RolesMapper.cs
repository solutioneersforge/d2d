using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionAppDoc2Data.Mappers;
public static class RolesMapper
{
    public static List<RolesDTO> MapToRolesDTO(this List<Role> roles)
    {
        if (roles == null)
            throw new ArgumentNullException(nameof(roles));

        var rolesDTOs = roles
            .Where(m => m.IsActive.GetValueOrDefault())
            .Select(m => new RolesDTO()
            {
                IsActive = m.IsActive.GetValueOrDefault(),
                RoleName = m.RoleName,
                RoleId = m.RoleId
            }).ToList();

        return rolesDTOs;
    }
}
