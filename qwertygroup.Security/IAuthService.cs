using System.Collections.Generic;
using CompAssignmnetSDPSecurity.Security.Models;

namespace CompAssignmnetSDPSecurity.Security
{
    public interface IAuthService
    {
        string GenerateJwtToken(LoginUser user);
        string Hash(string password);
        List<Permission> GetPermissions(int UserId);
    }
}