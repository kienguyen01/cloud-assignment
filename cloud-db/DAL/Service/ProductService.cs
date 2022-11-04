using cloud_db.Domain;
using cloud_db.Domain.DTO;
using cloud_db.Repository;

namespace cloud_db.DAL.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> AddProduct(AddProductDTO addProductDTO)
        {

            Product product = new Product()
            {
                Id = Guid.NewGuid(),
                ProductName = addProductDTO.ProductName,
                Price = addProductDTO.Price,
                Review = new List<string>(),
                ImageBlob = addProductDTO.ProductPictureName,
                OrderDetails = new List<OrderDetail>(),
            };

            _productRepository.Add(product);
            _productRepository.Commit();

            return product;
        }

        public async Task<Product> AddProductReview(AddProductReviewDTO addProductReviewDTO)
        {
            var product = await _productRepository.GetSingle(addProductReviewDTO.Id);

            product.Review.Add(addProductReviewDTO.Review);

            _productRepository.Commit();

            return product;

        }

        public async Task<Product> GetProduct(Guid productId)
        {
            var product = await _productRepository.GetSingle(productId);

            return product;
        }
    }
}
