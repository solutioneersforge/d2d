using System;

namespace FunctionAppDoc2Data.Constants;
public static class ExternalMailServiceConst
{
    public static string MailApiKey { get { return Environment.GetEnvironmentVariable("MailApiKey"); } }
    public static string MailBaseUrl { get { return Environment.GetEnvironmentVariable("MailBaseUrl"); } }
}
