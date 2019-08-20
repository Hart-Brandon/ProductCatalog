using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace ProductCatalog.Models
{
    public class Products : IProduct, IEnumerable<Product>
    {
        private List<Product> ProductList;

        public Products()
        {
            ProductList = new List<Product>() { };
        }

        public IEnumerable<Product> GetProducts()
        {
            return ProductList;
        }

        public Product GetProduct(int id)
        {
            return ProductList.FirstOrDefault(p => p.Id == id);
        }

        public Product AddProduct(Product product)
        {
            product.Id = (ProductList.Count() > 0) ? ProductList.Max(p => p.Id) + 1 : 0;

            ProductList.Add(product);
            return product;
        }

        public bool IsDuplicateName(string name)
        {
            if (ProductList.Any(p => name == p.Name))
                return true;

            return false;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new System.NotImplementedException();
        }

        public IEnumerator<Product> GetEnumerator()
        {
            throw new System.NotImplementedException();
        }
    }
}
