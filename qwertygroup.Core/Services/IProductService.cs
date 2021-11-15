using System.Collections.Generic;
using CompAssignmnetSDPSecurity.Core.Models;

namespace CompAssignmnetSDPSecurity.Core.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
    }
}