using AutoMapper;
using Factory.Domain;
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

        public Product UpdateProduct(int productId, Product product)
        {
            
            if (productId != product.ID)
                throw new ValidationException("ID in body and route are different (Update)");
            var validation = _productValidator.Validate(product);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.ToString());
            }
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

        public List<MainCategory> GetAllMainCategories()
        {
            return _productRepository.GetAllMainCategories();
        }

        public MainCategory CreateMainCategory(MainCategory mainCategory)
        {
            return _productRepository.CreateMainCategory(mainCategory);
        }

        public MainCategory UpdateMainCategory(MainCategory mainCategory)
        {
            return _productRepository.UpdateMainCategory(mainCategory);
        }

        public MainCategory GetMainCategoryByID(int mainCatId)
        {
            if (mainCatId <= 0)
                throw new ValidationException("ID is invalid");
            return _productRepository.GetMainCategoryByID(mainCatId);
        }

        public MainCategory DeleteMainCategoryByID(int mainCatId)
        {
            if (mainCatId == null)
                throw new ValidationException("ID is invalid");
            return _productRepository.DeleteMainCategoryByID(mainCatId);
        }

        public List<SubCategory> GetAllSubCategories()
        {
            return _productRepository.GetAllSubCategories();
        }

        public SubCategory CreateSubCategory(SubCategory subCategory)
        {
            return _productRepository.CreateSubCategory(subCategory);
        }

        public SubCategory UpdateSubCategory(SubCategory subCategory)
        {
            return _productRepository.UpdateSubCategory(subCategory);
        }

        public SubCategory GetSubCategoryByID(int subCatId)
        {
            if (subCatId <= 0)
                throw new ValidationException("ID is invalid");
            return _productRepository.GetSubCategoryByID(subCatId);
        }
        
        public SubCategory DeleteSubCategoryByID(int subCatId)
        {
            if (subCatId == null)
                throw new ValidationException("ID is invalid");
            return _productRepository.DeleteSubCategoryByID(subCatId);
        }
        
        public void RebuildDB()
        {
            _productRepository.RebuildDB();
        }
    }
