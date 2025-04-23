using FunctionAppDoc2Data.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Services;
public static class ExternalMailService
{
    static readonly HttpClient httpClient = new();
    public async static Task<bool> SendMail(MailerSendRequest mailerSendRequest)
    {
        string MailAPIKey = ExternalMailServiceConst.MailApiKey;
        string MailBaseUrl = ExternalMailServiceConst.MailBaseUrl;
        using var request = new HttpRequestMessage
        {
            Method = HttpMethod.Post,
            RequestUri = new Uri(MailBaseUrl),
            Headers = { { "Authorization", $"Bearer {MailAPIKey}" } },
            Content = JsonContent.Create(mailerSendRequest)
        };

        var response = await httpClient.SendAsync(request);
        return response.IsSuccessStatusCode;
    }
}

public record MailerSendRequest(
       MailerSendFrom From,
       MailerSendTo[] To,
       string Subject,
       string Text,
       string Html
   );

public record MailerSendFrom(
    string Email,
    string Name
);

public record MailerSendTo(
    string Email,
    string Name
);


