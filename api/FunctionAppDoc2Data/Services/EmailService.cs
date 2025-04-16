using FunctionAppDoc2Data.Models;
using RestSharp;
using RestSharp.Authenticators;
using System;

namespace FunctionAppDoc2Data.Services;
static class EmailService
{
    private const string SenderAddress = "sender@xxxxxxxx.xxx";
    private const string SenderDisplayName = "Sender Name";
    private const string Tag = "sampleTag";

    public static IRestResponse SendEmail(UserEmailOptionsDTO userEmailOptions)
    {
        var apiKey = Environment.GetEnvironmentVariable("MailGunApiKey");
        var baseUrl = Environment.GetEnvironmentVariable("MailGunBaseUrl");
        var domain = Environment.GetEnvironmentVariable("MailGunDomain");

        RestClient client = new RestClient
        {
            BaseUrl = new Uri(baseUrl),
            Authenticator = new HttpBasicAuthenticator("api", apiKey)
        };

        RestRequest request = new RestRequest();
        request.AddParameter("domain", domain, ParameterType.UrlSegment);
        request.Resource = "{domain}/messages";
        request.AddParameter("from", $"{SenderDisplayName} <{SenderAddress}>");

        foreach (var toEmail in userEmailOptions.ToEmails)
        {
            request.AddParameter("to", toEmail);
        }

        request.AddParameter("subject", userEmailOptions.Subject);
        request.AddParameter("html", userEmailOptions.Body);
        request.AddParameter("o:tag", Tag);
        request.Method = Method.POST;
        return client.Execute(request);
    }
}
