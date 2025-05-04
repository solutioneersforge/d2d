using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionAppDoc2Data.Models;
public class ResetPasswordDTO
{
    public Guid ForgetPasswordToken { get; set; }
    public string ResetPassword { get; set; }
}
