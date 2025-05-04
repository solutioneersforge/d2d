using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Services;
public  class ForgetPasswordMailLink
{
    public async Task<bool> SendForgetPasswordLink(string emailAddress, Guid forgetPasswordLink)
    {
        string verificationUrl = $"https://app-doc2data.azurewebsites.net/resetpassword?forgetpasswordtoken={forgetPasswordLink}";
        //string verificationUrl = $"http://localhost:4200/resetpassword?forgetpasswordtoken={forgetPasswordLink}";
        string body = $@"
<html>
  <head>
    <meta charset=""UTF-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <title>Password Reset</title>
  </head>
  <body style=""margin: 0; padding: 0; background-color: #f5f7fa; font-family: Arial, sans-serif;"">
    <table width=""100%"" cellpadding=""0"" cellspacing=""0"" style=""background-color: #f5f7fa; padding: 40px 0;"">
      <tr>
        <td align=""center"">
          <table width=""600"" cellpadding=""0"" cellspacing=""0"" style=""background-color: #ffffff; border-radius: 6px; box-shadow: 0 2px 4px rgba(0,0,0,0.1);"">
            <tr>
              <td style=""padding: 40px; text-align: center;"">
                <h2 style=""color: #333; margin-bottom: 20px;"">Reset Your Password</h2>
                <p style=""font-size: 16px; color: #555; margin-bottom: 30px;"">
                  Hello, <br><br>
                  We received a request to reset the password associated with your account. If you made this request, you can reset your password using the button below.
                </p>
                <a href=""{{reset_link}}""
                   style=""background-color: #007BFF; color: #ffffff; text-decoration: none; padding: 12px 24px; border-radius: 4px; font-size: 16px; display: inline-block;"">
                  Reset Password
                </a>
                <p style=""font-size: 14px; color: #888; margin-top: 30px;"">
                  If you did not request a password reset, please disregard this message. This link will expire in 24 hours for your security.
                </p>
                <p style=""font-size: 14px; color: #888; margin-top: 30px;"">
                  Regards,<br>
                  The Solutioneers Forge Team
                </p>
              </td>
            </tr>
          </table>
          <p style=""font-size: 12px; color: #999; margin-top: 20px;"">
            © 2025 Solutioneers Forge. All rights reserved.
          </p>
        </td>
      </tr>
    </table>
  </body>
</html>
";
        string emailBody = body.Replace("{reset_link}", verificationUrl);
        var mailerSendRequest = new MailerSendRequest(
                From: new("noreply@solutioneersforge.com", "Solutioneers Forge"),
                To: new MailerSendTo[] { new MailerSendTo(emailAddress, emailAddress) },
                Subject: "Email Verification",
                Text: emailBody,
                Html: emailBody
            );
        return await ExternalMailService.SendMail(mailerSendRequest);
    }
}
