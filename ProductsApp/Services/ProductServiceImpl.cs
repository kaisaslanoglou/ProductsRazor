using AutoMapper;
using System.Transactions;
using ProductsApp.DAO;
using ProductsApp.DTO;
using ProductsApp.Models;

namespace ProductsApp.Services
{
    public class ProductServiceImpl : IProductService
    {
        private readonly IProductDAO? _productDAO;
        private readonly IMapper? _mapper;
        private readonly ILogger<ProductServiceImpl>? _logger;

        public ProductServiceImpl(IProductDAO? productDAO, IMapper? mapper, ILogger<ProductServiceImpl>? logger)
        {
            _productDAO = productDAO;
            _mapper = mapper;
            _logger = logger;
        }


        public Product? DeleteProduct(int id)
        {
            Product? productToReturn = null;
            try
            {
                using TransactionScope scope = new();

                productToReturn = _productDAO!.GetByID(id);

                if (productToReturn is null) return null;
                _productDAO.Delete(id);
                scope.Complete();

                _logger!.LogInformation("Success in delete");
                return productToReturn;

            }
            catch (Exception e)
            {
                _logger!.LogError("An error occurred" + e.Message);
                throw;

            }
        }

        public IList<Product> GetAllProducts()
            {
                try
                {
                    IList<Product> products = _productDAO!.GetAll();
                    return products;
                } catch (Exception e)
                { _logger!.LogError("An error occurred" + e.Message);
                    throw;
                }
            }

        public Product? GetProduct(int id)
            {
                try
                {
                    return _productDAO!.GetByID(id);

                }
                catch (Exception e)
                {
                    _logger!.LogError("An error occurred" + e.Message);
                    throw;
                }
            }

        public Product? InsertProduct(ProductInsertDTO dto)
            {
                if (dto == null) return null;
            var product = _mapper!.Map<Product>(dto);

            try
                {
                    using TransactionScope scope = new();
                Product? insertedProduct = _productDAO!.Insert(product);
                    scope.Complete();
                    _logger!.LogInformation("Success in insert");
                    return insertedProduct;

                }
                catch (Exception e)
                {
                    _logger!.LogError("An error occurred" + e.Message);
                    throw;
                }
            }

            public Product? UpdateProduct(ProductUpdateDTO dto)
            {
                if (dto == null) return null;
                Product? productToReturn = null;

                try
                {
                    var product = _mapper!.Map<Product>(dto);
                    using TransactionScope scope = new();
                   productToReturn = _productDAO!.Update(product);
                    scope.Complete();
                    _logger!.LogInformation("Success in updating");
                    return productToReturn;
                }
                catch (Exception e)
                {
                    _logger!.LogError("An erro occurred" + e.Message);
                    throw;
                }
            }
    }
    }

