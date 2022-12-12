using AutoMapper;
using FluentValidation;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;
using ValidationException = FluentValidation.ValidationException;

namespace PetShop.Application;

public class ProductService : IProductService
{
    private IShopRepo _productRepository;
    private IMapper _mapper;
    private IValidator<ProdDTO> _validator;
    private readonly IUserRepo _userRepo;
    private IValidator<Product> _productValidator;

    public ProductService(IShopRepo repository, IMapper mapper, IValidator<ProdDTO> validator,
        IValidator<Product> productValidator, IUserRepo userRepo)
    {
        _productRepository = repository;
        _mapper = mapper;
        _validator = validator;
        _userRepo = userRepo;
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
        product = _mapper.Map<Product>(dto);
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
    
    public Rating AddRating(ratingDTO ratingValue, int productId, Guid userId)
    {
        // Validate the rating to ensure it is within the acceptable range.
        if (ratingValue.Rating < 1 || ratingValue.Rating > 6)
        {
            throw new ArgumentException("Rating must be between 1 and 5.");
        }
        
        // Create a new Rating object and set its properties.
        var ratingValuetoAdd = _mapper.Map<Rating>(ratingValue);
        ratingValuetoAdd.ProductId = productId;
        ratingValuetoAdd.UserId = userId;

        // Add the rating to the database.
         return _productRepository.AddRating(ratingValuetoAdd);
    }

    public Rating UpdateRating(Rating rating)
    {
        
        var ratingToUpdate = _mapper.Map<Rating>(rating);
        // Validate the rating to ensure it is within the acceptable range.
        if (ratingToUpdate.RatingValue < 1 || ratingToUpdate.RatingValue > 5)
        {
            throw new ArgumentException("Rating must be between 1 and 5.");
        }

        if (ratingToUpdate.ProductId <= 0 || ratingToUpdate.UserId.ToString() == null)
        {
            throw new ValidationException("ID is invalid");
        }

        // Create a new Rating object and set its properties.

        // Add the rating to the database.
        _productRepository.UpdateRating(ratingToUpdate);
        return ratingToUpdate;
    }
}


