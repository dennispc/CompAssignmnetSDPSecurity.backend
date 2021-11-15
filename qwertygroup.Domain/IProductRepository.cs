using System.Collections.Generic;
using CompAssignmnetSDPSecurity.Core.Models;

namespace CompAssignmnetSDPSecurity.Domain
{
    public interface IProductRepository
    {
        List<Product> FindAll();
    }
}