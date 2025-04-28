using System;

namespace FunctionAppDoc2Data.Models;
public class UserRegisterModelDTO
{
    public Guid? CompanyId { get; set; }
    public string CompanyName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Guid? RoleId { get; set; }
    public string TelephoneNumber { get; set; }
    public string Address { get; set; }
    public string CompanyEmail { get; set; }
}


public class UserValidateModelDTO
{
    public string Email { get; set; }
    public string Password { get; set; }
}
