using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Net;
using ZeissProducts.Business.Models;
using ZeissProducts.Business.Services.Products;
using ZeissProducts.Data.Models;
using ZeissProducts.WebAPI.Controllers;

namespace ZeissProducts.Tests
{
    [TestClass]
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _productService;
        private readonly ProductsController _productsController;

        public ProductsControllerTests()
        {
            _productService = new Mock<IProductService>();
            _productsController = new ProductsController(_productService.Object);
        }


        [TestMethod]
        public void GetProductsAsync_Returns_Products()
        {
            //Arrange
            _productService.Setup(m => m.GetProductsAsync()).ReturnsAsync(GetProducts());

            //Act
            var result  = _productsController.GetProductsAsync().Result as OkObjectResult;
            var products = result?.Value as List<Product>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK,result.StatusCode);
            Assert.AreEqual(3, products?.Count);
        }

        [TestMethod]
        public void GetProductAsync_Returns_Product()
        {
            //Arrange
            var product = new Product()
            {
                Id = 100000,
                Name = "Contact Lenses",
                Description = "A thin plastic lens placed directly on the surface of the eye to correct visual defects.",
                ExpiryDate = new DateTime(2027, 02, 10),
                Inventory = 200,
                ManufactureDate = new DateTime(2025, 02, 10),
                Price = 20000
            };

            _productService.Setup(m => m.GetProductByIdAsync(product.Id)).ReturnsAsync(product);

            //Act
            var result = _productsController.GetProductAsync(product.Id).Result as OkObjectResult;
            var actualProduct = result?.Value as Product;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(product.Name, actualProduct?.Name);
        }

        [TestMethod]
        public void UpdateProductAsync_Returns_SucessResult()
        {
            //Arrange
            var product = new Product()
            {
                Id = 100001,
                Name = "Sunglasses",
                Description = "Glasses tinted to protect the eyes from sunlight or glare.",
                ExpiryDate = new DateTime(2027, 02, 28),
                Inventory = 200,
                ManufactureDate = new DateTime(2025, 01, 31),
                Price = 5000
            };

            _productService.Setup(m => m.UpdateProductAsync(product.Id, product)).ReturnsAsync(ZeissResult<Product>.Success());

            //Act
            var result = _productsController.UpdateProductAsync(product.Id, product).Result as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void RemoveProductStocksAsync_Returns_SuccessResult()
        {
            //Arrange
            int quantityToBeRemoved = 20;
            var product = new Product()
            {
                Id = 100002,
                Name = "Glass Bottles",
                Description = "A glass bottle can vary in size considerably, but are most commonly found in sizes ranging between about 200 millilitres and 1.5 litres.",
                ExpiryDate = new DateTime(2027, 01, 31),
                Inventory = 200,
                ManufactureDate = new DateTime(2025, 01, 10),
                Price = 1500
            };

            _productService.Setup(m => m.RemoveProductStocksAsync(product.Id, quantityToBeRemoved)).ReturnsAsync(ZeissResult<Product>.Success());

            //Act
            var result = _productsController.RemoveProductStocksAsync(product.Id, quantityToBeRemoved).Result as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void AddProductAsync_Returns_AddedProduct()
        {
            //Arrange
            var product = new Product()
            {
                Id = 100002,
                Name = "Glass Bottles",
                Description = "A glass bottle can vary in size considerably, but are most commonly found in sizes ranging between about 200 millilitres and 1.5 litres.",
                ExpiryDate = new DateTime(2027, 01, 31),
                Inventory = 200,
                ManufactureDate = new DateTime(2025, 01, 10),
                Price = 1500
            };

            _productService.Setup(m => m.AddProductAsync(product)).ReturnsAsync(product);

            //Act
            var result = _productsController.AddProductAsync(product).Result as OkObjectResult;
            var actualProduct = result?.Value as Product;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
            Assert.AreEqual(product.Name, actualProduct?.Name);
        }

        [TestMethod]
        public void AddProductStocksAsync_Returns_SuccessResult()
        {
            //Arrange
            int quantityToAdd = 50;
            var product = new Product()
            {
                Id = 100002,
                Name = "Glass Bottles",
                Description = "A glass bottle can vary in size considerably, but are most commonly found in sizes ranging between about 200 millilitres and 1.5 litres.",
                ExpiryDate = new DateTime(2027, 01, 31),
                Inventory = 200,
                ManufactureDate = new DateTime(2025, 01, 10),
                Price = 1500
            };

            _productService.Setup(m => m.AddProductStocksAsync(product.Id, quantityToAdd)).ReturnsAsync(product);

            //Act
            var result = _productsController.AddProductStocksAsync(product.Id, quantityToAdd).Result as OkObjectResult;
            
            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        [TestMethod]
        public void DeleteProductAsync_Returns_SuccessResult()
        {
            //Arrange
            int productIdToBeDeleted = 100000;

            _productService.Setup(m => m.DeleteProductAsync(productIdToBeDeleted)).ReturnsAsync(ZeissResult<Product>.Success());

            //Act
            var result = _productsController.DeleteProductAsync(productIdToBeDeleted).Result as OkObjectResult;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual((int)HttpStatusCode.OK, result.StatusCode);
        }

        private List<Product> GetProducts() 
        {
            return new List<Product>() 
            {
                new Product()
                {
                   Id = 100000,
                   Name = "Contact Lenses",
                   Description = "A thin plastic lens placed directly on the surface of the eye to correct visual defects.",
                   ExpiryDate = new DateTime(2027,02,10),
                   Inventory = 200,
                   ManufactureDate = new DateTime(2025,02,10),
                   Price = 20000
                },
                new Product()
                {
                   Id = 100001,
                   Name = "Sunglasses",
                   Description = "Glasses tinted to protect the eyes from sunlight or glare.",
                   ExpiryDate = new DateTime(2027,02,28),
                   Inventory = 200,
                   ManufactureDate = new DateTime(2025,01,31),
                   Price = 5000
                },
                new Product()
                {
                   Id = 100002,
                   Name = "Glass Bottles",
                   Description = "A glass bottle can vary in size considerably, but are most commonly found in sizes ranging between about 200 millilitres and 1.5 litres.",
                   ExpiryDate = new DateTime(2027,01,31),
                   Inventory = 200,
                   ManufactureDate = new DateTime(2025,01,10),
                   Price = 1500
                }
            };
        }
    }
}