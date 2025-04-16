using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

namespace FunctionAppDoc2Data.Helpers;
public static class DocDataHelper
{
    public static decimal GetNumberFromString(decimal value, string contentString, string price = "")
    {
        try
        {
            if (!string.IsNullOrEmpty(contentString) || !string.IsNullOrEmpty(price))
            {
                if (!string.IsNullOrEmpty(value.ToString()) && value != 0)
                {
                    return value;
                }
                else if (!string.IsNullOrEmpty(contentString))
                {
                    contentString = Regex.Replace(contentString, @"[^\d.,-]", "");
                    if (decimal.TryParse(contentString, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
                    {
                        return result;
                    }
                    if (decimal.TryParse(contentString, NumberStyles.Any, CultureInfo.CurrentCulture, out result))
                    {
                        return result;
                    }
                }
                else
                {
                    price = Regex.Replace(price, @"[^\d.,-]", "");
                    if (decimal.TryParse(price, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal result))
                    {
                        return result;
                    }
                    if (decimal.TryParse(price, NumberStyles.Any, CultureInfo.CurrentCulture, out result))
                    {
                        return result;
                    }
                }
            }
            return 0;
        }
        catch
        {
            return 0;
        }
    }



    public static List<DateTime?> ListParseStringsToDateTime(params string[] dateStrings)
    {
        string[] dateFormats = new[]
        {
            "yyyy-MM-dd",
            "MM/dd/yyyy",
            "dd/MM/yyyy",
            "yyyyMMdd",
            "dd-MMM-yyyy",
            "yyyy/MM/dd",
            "MM-dd-yyyy",
            "dd.MM.yyyy",
            "yyyy.MM.dd",
            "MMM dd, yyyy",
            "dd MMM yyyy",
            "yyyy MMM dd"
        };

        List<DateTime?> parsedDates = new List<DateTime?>();

        foreach (var dateString in dateStrings)
        {
            string cleanedString = Regex.Replace(dateString, @"[^0-9a-zA-Z\/\-\.\s,]", "");

            DateTime parsedDate;
            bool success = DateTime.TryParseExact(cleanedString, dateFormats,
                                                  CultureInfo.InvariantCulture,
                                                  DateTimeStyles.None,
                                                  out parsedDate);

            if (success)
            {
                parsedDates.Add(parsedDate);
            }
            else
            {
                parsedDates.Add(null);
            }
        }

        return parsedDates;
    }

    public static DateTime? ParseStringsToDateTime(string dateStrings)
    {
        // Define the possible date formats you expect
        string[] dateFormats = new[]
        {
            "yyyy-MM-dd",
            "MM/dd/yyyy",
            "dd/MM/yyyy",
            "yyyyMMdd",
            "dd-MMM-yyyy",
            "yyyy/MM/dd",
            "MM-dd-yyyy",
            "dd.MM.yyyy",
            "yyyy.MM.dd",
            "MMM dd, yyyy",
            "dd MMM yyyy",
            "yyyy MMM dd"
        };

        List<DateTime?> parsedDates = new List<DateTime?>();
        string cleanedString = Regex.Replace(dateStrings, @"[^0-9a-zA-Z\/\-\.\s,]", "");

        DateTime parsedDate;
        bool success = DateTime.TryParseExact(cleanedString, dateFormats,
                                              CultureInfo.InvariantCulture,
                                              DateTimeStyles.None,
                                              out parsedDate);

        if (success)
        {
            return parsedDate;
        }

        return null;
    }

    public static string GetFirstNonEmptyValue(this string value, params string[] names)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return value;
        }
        if (names == null || names.Length == 0)
        {
            return string.Empty;
        }
        return names.FirstOrDefault(name => !string.IsNullOrEmpty(name)) ?? string.Empty;
    }

    public static DateTime GetFirstNonEmptyDateTime(this DateTime value, params DateTime[] names)
    {
        if (value != DateTime.MinValue)
        {
            return value;
        }

        foreach (var name in names)
        {
            if (name != DateTime.MinValue)
            {
                return name;
            }
        }

        return DateTime.UtcNow;
    }
}
