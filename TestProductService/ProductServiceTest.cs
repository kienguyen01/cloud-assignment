using cloud_db.DAL.Service;
using cloud_db.Domain;
using cloud_db.Domain.DTO;
using cloud_db.Repository;
using Moq;
using System.Diagnostics;

namespace TestProductService
{
    public class Tests
    {
        private Mock<IProductRepository> _productRepository;
        private ProductService _productService;
        private Product _MockProduct;
        private List<Product> _MockProducts;

        [SetUp]
        public void Setup()
        {
            Trace.Listeners.Add(new ConsoleTraceListener());
            _productRepository = new Mock<IProductRepository>();

            _productService = new ProductService(_productRepository.Object);

            Product product1 = new Product()
            {
                Id = Guid.Parse("2fcf43fc-91d4-4ae8-8de5-79a7d4a112ad"),
                ProductName = "TestName1",
                Price = 250,
                ImageBlob = "image.png",
                Review = new List<string>(),
                OrderDetails = new List<OrderDetail>()
            };

            Product product2 = new Product()
            {
                Id = Guid.Parse("621a729e-4ae2-4dad-bf97-6a81d4f4db32"),
                ProductName = "TestName2",
                Price = 350,
                ImageBlob = "image2.png",
                Review = new List<string>(),
                OrderDetails = new List<OrderDetail>()
            };

            _MockProducts = new List<Product>();

            _MockProducts.Add(product1);
            _MockProducts.Add(product2);
           
        }

        [Test]
        public async Task GetProductById_Should_ReturnProduct_and_CallProductRepo()
        {
            //Arrange
            Guid productId = Guid.Parse("2fcf43fc-91d4-4ae8-8de5-79a7d4a112ad");
            //_productRepository.Setup(p => await p.GetSingle(productId)).Returns(_MockProduct);
            _productRepository.Setup(p => p.GetSingle(productId).Result).Returns(_MockProduct);

            //Act
            var result = _productService.GetProduct(productId).Result;

            //Assert
            Assert.That(result, Is.EqualTo(_MockProduct));
            _productRepository.Verify(p => p.GetSingle(productId), Times.Once);
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [TearDown]
        public void TestCleanUp()
        {
            _productRepository = null;
            _MockProducts = null;
        }
    }
}