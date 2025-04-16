using FunctionAppDoc2Data.Constants;
using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using System;

namespace FunctionAppDoc2Data.Mappers;
public static class CompanyMapper
{
    public static Company MapToCompany(this UserRegisterModelDTO userRegister)
    {
        if (userRegister == null)
        {
            throw new ArgumentNullException(nameof(UserRegisterModelDTO));
        }
        return new Company()
        {
            CompanyName = userRegister.CompanyName,
            CreatedAt = DateTime.UtcNow,
            Address = userRegister.Address,
            TelephoneNumber = userRegister.TelephoneNumber,
            SubscriptionId = !String.IsNullOrEmpty(userRegister.CompanyName) ? Guid.Parse(CompanySubscriptionConstant.Premium) : Guid.Parse(CompanySubscriptionConstant.Super),
        };
    }
}
