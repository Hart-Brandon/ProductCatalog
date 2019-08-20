using System.Collections.Generic;

namespace ProductCatalog.Models
{
    public interface IProduct
    {
        Product GetProduct(int id);
        Product AddProduct(Product product);
        IEnumerable<Product> GetProducts();
        bool IsDuplicateName(string name);
    }
}
