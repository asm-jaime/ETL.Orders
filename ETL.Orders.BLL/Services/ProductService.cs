using ETL.Orders.DAL;
namespace ETL.Orders.BLL.Services;

public class ProductService(IProductRepository productRepository)
{
    IProductRepository _productRepository = productRepository;
}
