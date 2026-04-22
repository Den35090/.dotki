using System.Collections.Generic;
using System.Linq;

namespace ShopLibrary
{
    public class ProductService
    {
        private List<Product> _products;

        public ProductService(List<Product> products)
        {
            _products = products;
        }

        public Product[] GetAll() => _products.ToArray();

        public Product Find(string name) => 
            _products.FirstOrDefault(p => p.Name.Equals(name, System.StringComparison.OrdinalIgnoreCase));

        public List<Product> Find(decimal minPrice, decimal maxPrice) => 
            _products.Where(p => p.Price >= minPrice && p.Price <= maxPrice).ToList();

        public decimal GetTotalValue() => _products.Sum(p => p.Price * p.Quantity);

        public List<Product> GetExpensiveProducts(decimal price) => 
            _products.Where(p => p.Price > price).ToList();
    }
}