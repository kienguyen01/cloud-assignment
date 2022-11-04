using cloud_db.Domain;
using cloud_db.Domain.DTO;

namespace cloud_db.DAL.Service
{
    public interface IProductService
    {
        Task<Product> AddProductReview(AddProductReviewDTO addProductReviewDTO);
        Task<Product> AddProduct(AddProductDTO addProductDTO);
        Task<Product> GetProduct(Guid productId);

    }
}
