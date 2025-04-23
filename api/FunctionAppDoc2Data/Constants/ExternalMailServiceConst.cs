using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Constants;
public static class ExternalMailServiceConst
{
    public static string MailApiKey { get { return Environment.GetEnvironmentVariable("MailApiKey"); } }
    public static string MailBaseUrl { get { return Environment.GetEnvironmentVariable("MailBaseUrl"); } }
}
