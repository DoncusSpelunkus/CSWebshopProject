using AutoMapper;
using Factory.Domain;
using FluentValidation;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application;

public class BrandService: IBrandService
{
    private IBrandRepo _brandRepository;
    private IMapper _mapper;
    private IValidator<BrandDto> _dtoValidator;

    
    public BrandService(IBrandRepo brandRepository, IMapper mapper,  IValidator<BrandDto> dtoValidator) 
    {
        _brandRepository = brandRepository;
        _mapper = mapper;
        _dtoValidator = dtoValidator;
    }
    public List<Brand> GetAllBrands()
    {
        return _brandRepository.GetAllBrands();
    }

    public Brand CreateBrand(BrandDto dto)
    {
        var validation = _dtoValidator.Validate(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        return _brandRepository.CreateBrand(_mapper.Map<Brand>(dto));
    }
    

    public Brand UpdateBrand(int brandID, BrandDto brandDto)
    {
        var validation = _dtoValidator.Validate(brandDto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }

        var brand = _mapper.Map<Brand>(brandDto);
        brand.Id = brandID;
        return _brandRepository.UpdateBrand(brand);
    }

    public Brand DeleteBrand(int brandID)
    {
        if (brandID == null)
            throw new ValidationException("ID is invalid");
        return _brandRepository.DeleteBrand(brandID);
    }

    public Brand GetBrandByID(int brandID)
    {
        if (brandID <= 0)
            throw new ValidationException("ID is invalid");
        return _brandRepository.GetBrandByID(brandID);
    }
}