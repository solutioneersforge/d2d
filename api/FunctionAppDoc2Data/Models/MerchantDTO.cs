using System;

namespace FunctionAppDoc2Data.Models;
public class MerchantDTO
{
    public int MerchantId { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
    public int CountryId { get; set; }
    public DateTime CreatedOn { get; set; }
    public bool IsActive { get; set; }
    public string Website { get; set; }
    public string CompanyRegNo { get; set; }
    public string TaxCompanyRegNo { get; set; }
}
