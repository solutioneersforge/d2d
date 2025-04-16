using System;

namespace FunctionAppDoc2Data.Models;
public class CompanyUserDTO
{
    public Guid UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Active { get; set; }
    public bool IsActive { get; set; }
    public string RoleName { get; set; }
    public Guid RoleId { get; set; }
}
