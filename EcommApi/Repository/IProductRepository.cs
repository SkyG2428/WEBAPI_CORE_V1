using EcommApi.Models;

namespace EcommApi.Repository
{
    public interface IProductRepository
    {
        IQueryable<Product> GetAll();
        Product GetProductById(int id);
        void Insert(Product product);
        void Update(Product product);
        void Delete(Product product);
        
    }
}
