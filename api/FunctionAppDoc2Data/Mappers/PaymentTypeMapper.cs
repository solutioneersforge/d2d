using FunctionAppDoc2Data.DataContext;
using FunctionAppDoc2Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Mappers;
public static class PaymentTypeMapper
{
    public static IEnumerable<PaymentTypeDTO> MapToPaymentTypeDTO(this IEnumerable<PaymentType> paymentTypes)
    {
        try
        {
            if (paymentTypes == null)
                throw new ArgumentNullException(nameof(paymentTypes));

            return paymentTypes.Select(payment => new PaymentTypeDTO
            {
               PaymentType = payment.PaymentType1,
               PaymentTypeId = payment.PaymentTypeId
            });
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}


public static class CurrencyTypeMapper
{
    public static IEnumerable<CurrencyTypeDTO> MapToCurrencyTypeDTO(this IEnumerable<DataContext.Currency> currencyTypes)
    {
        try
        {
            if (currencyTypes == null)
                throw new ArgumentNullException(nameof(currencyTypes));

            return currencyTypes.Select(currency => new CurrencyTypeDTO
            {
                Code = currency.Code,
                CurrencyId = currency.CurrenctId,
                Name = currency.Name,
                Symbol = currency.Symbol
            });
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}


public static class MerchantDetailsMapper
{
    public static IEnumerable<MerchantDetailsDTO> MapToMerchantDetailsDTO(this IEnumerable<Merchant> merchants)
    {
        try
        {
            if (merchants == null)
                throw new ArgumentNullException(nameof(merchants));

            return merchants.Select(merchant => new MerchantDetailsDTO
            {
                MerchantAddress = merchant.Address,
                MerchantEmail = merchant.Email,
                MerchantName = merchant.Name,
                MerchantId  = merchant.MerchantId,
                MerchantPhone = merchant.Phone,
            });
        }
        catch (Exception ex)
        {
            return null;
        }
    }
}
