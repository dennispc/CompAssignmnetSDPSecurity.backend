using System.Collections.Generic;
using CompAssignmnetSDPSecurity.Core.Models;

namespace CompAssignmnetSDPSecurity.Domain
{
    public interface IProductRepository
    {
        List<Product> FindAll();
        bool Delete(Product product);

        Product Create(Product product);
        Product ReadById(int id);
        Product Update(Product product);
    }
}