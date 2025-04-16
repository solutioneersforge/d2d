using System.Collections.Generic;

namespace FunctionAppDoc2Data.Models;
public class UserEmailOptionsDTO
{
    public List<string> ToEmails { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
