using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using System;

namespace FunctionAppDoc2Data.Mappers;
public static class MerchantMapper
{
    public static Merchant MapToMerchantDetails(this MerchantDTO merchant, int merchantId = 0)
    {
        if (merchant == null)
            throw new ArgumentNullException(nameof(MerchantDTO));

        var mappedMerchant = new Merchant
        {
            Address = merchant.Address,
            CompanyRegNo = merchant.CompanyRegNo,
            CountryId = merchant.CountryId,
            CreatedOn = merchant.CreatedOn,
            Email = merchant.Email,
            IsActive = merchant.IsActive,
            Name = merchant.Name,
            Phone = merchant.Phone,
            TaxCompanyRegNo = merchant.TaxCompanyRegNo,
            Website = merchant.Website
        };

        if (merchantId != 0)
        {
            mappedMerchant.MerchantId = merchant.MerchantId;
        }

        return mappedMerchant;
    }

}
