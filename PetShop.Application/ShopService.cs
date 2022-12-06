using AutoMapper;
using FluentValidation;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;
using ValidationException = FluentValidation.ValidationException;

namespace PetShop.Application;

    public class ShopService : IShopService
    {
        private IShopRepo _productRepository;
        private IMapper _mapper;
        private IValidator<ProdDTO> _validator;
        private IValidator<Product> _productValidator;

        public ShopService(IShopRepo repository, IMapper mapper, IValidator<ProdDTO> validator,IValidator<Product> productValidator) 
        {
            _productRepository = repository;
            _mapper = mapper;
            _validator = validator;
            _productValidator = productValidator;
        }
    
    
        public List<Product> GetAllProducts()
        {
            return _productRepository.GetAllProducts();
        }
        
        public Product CreateProduct(ProdDTO dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
                throw new ValidationException(validation.ToString());
            
            return _productRepository.CreateProduct(_mapper.Map<Product>(dto));
        }

        public Product UpdateProduct(int productId, ProdDTO dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.ToString());
            }
            Product product = new Product();
            product = (_mapper.Map<Product>(dto));
            product.ID = productId;
            return _productRepository.UpdateProduct(product);
        }

        public Product DeleteProduct(int productID)
        {
            if (productID == null)
                throw new ValidationException("ID is invalid");
            return _productRepository.DeleteProduct(productID);
        }

        public Product GetProductByID(int productId)
        {
            if (productId <= 0)
                throw new ValidationException("ID is invalid");
            return _productRepository.GetProductByID(productId);
        }
        
        public void RebuildDB()
        {
            _productRepository.RebuildDB();
        }
    }
