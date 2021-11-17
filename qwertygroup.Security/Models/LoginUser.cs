using System.Collections.Generic;

namespace CompAssignmnetSDPSecurity.Security.Models
{
    public class LoginUser
    {
        public int Id{get;set;}
        public string UserName{get;set;}
        public string HashedPassword{get;set;}

        public List<Permission> permissions {get;set;}
        public int DbUserId{get;set;}
    }
}