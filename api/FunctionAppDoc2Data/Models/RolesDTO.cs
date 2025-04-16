using System;

namespace FunctionAppDoc2Data.Models;
public class RolesDTO
{
    public Guid RoleId { get; set; }
    public string RoleName { get; set; }
    public bool IsActive { get; set; }
}
