using System.Collections.Generic;
using CompAssignmnetSDPSecurity.Core.Models;

namespace CompAssignmnetSDPSecurity.Core.Services
{
    public interface IProductService
    {
        List<Product> GetProducts();
        Product GetProduct(int id);

        bool DeleteProduct(Product product);
        Product UpdateProduct(Product product);
        Product CreateProduct(Product product);
    }
}