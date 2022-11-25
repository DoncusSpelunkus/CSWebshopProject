using AutoMapper;
using Factory.Application.Interfaces;
using Factory.Application.PostProdDTO;
using Factory.Domain;
using FluentValidation;
using ValidationException = FluentValidation.ValidationException;

namespace Factory.Application;

    public class ShopService : IShopService
    {
        private IShopRepo _productRepository;
        private IMapper _mapper;
        private IValidator<ProdDTO> _validator;
        private IValidator<Product> _productValidator;

        public ShopService(IShopRepo repository, IMapper mapper, IValidator<ProdDTO> validator)
        {
            _productRepository = repository;
            _mapper = mapper;
            _validator = validator;
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

        public Product UpdateProduct(int productId, Product product)
        {
            if (productId != product.ID)
                throw new ValidationException("ID in body and route are different (Update)");
            var validation = _productValidator.Validate(product);
            if (!validation.IsValid)
                throw new ValidationException(validation.ToString());
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
