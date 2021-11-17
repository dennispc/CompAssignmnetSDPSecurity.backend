
using System.Collections.Generic;

namespace qwertygroup.WebApi.Dtos.Auth
{
    public class ProfileDto
    {
        public List<string> Permissions { get; set; }
        public string Name { get; set; }
    }
}