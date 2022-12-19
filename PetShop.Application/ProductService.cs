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

    public Product DeleteProduct(int productId)
    {
        if (productId == null)
            throw new ValidationException("ID is invalid");
        return _productRepository.DeleteProduct(productId);
    }

    public Product GetProductById(int productId)
    {
        if (productId <= 0)
            throw new ValidationException("ID is invalid");
        return _productRepository.GetProductById(productId);
    }


    public void RebuildDB()
    {
        _productRepository.RebuildDB();
    }
    
    public Rating AddRating(ratingDTO ratingValue, int productId, Guid userId)
    {
        // Validate the rating to ensure it is within the acceptable range.
        if (ratingValue.RatingValue is < 1 or > 6)
        {
            throw new ArgumentException("Rating must be between 1 and 5.");
        }
        
        var listOfRatings = _productRepository.GetAllRatings().ToList();
        foreach (var rating in listOfRatings)
        {
            if (rating.ProductId == productId && rating.UserId == userId)
                throw new ArgumentException("This user already rated this product.");
        }
        // Create a new Rating object and set its properties.
        var ratingValueToAdd = _mapper.Map<Rating>(ratingValue);
        ratingValueToAdd.ProductId = productId;
        ratingValueToAdd.UserId = userId;

        // Add the rating to the database.
         return _productRepository.AddRating(ratingValueToAdd);
    }

    public Rating UpdateRating(ratingDTO ratingDto, int productId, Guid userId)
    {
        // Validate the rating to ensure it is within the acceptable range.
        if (ratingDto.RatingValue is < 1 or > 6)
        {
            throw new ArgumentException("Rating must be between 1 and 5.");
        }
        
        // Create a new Rating object and set its properties.
        var ratingValueToAdd = _mapper.Map<Rating>(ratingDto);
        ratingValueToAdd.ProductId = productId;
        ratingValueToAdd.UserId = userId;

        // Add the rating to the database.
        return _productRepository.UpdateRating(ratingValueToAdd);
    }
}


