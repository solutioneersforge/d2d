using Microsoft.AspNetCore.Http;
using System;

namespace FunctionAppDoc2Data.Services;
public interface IValidatedTokenService
{
    (bool isSuccess, Guid userId) ValidateTokenRequest(HttpRequest req);
}