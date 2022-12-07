using AutoMapper;
using PetShop.Domain;
using FluentValidation;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;

namespace PetShop.Application;

public class BrandService: IBrandService
{
    private IBrandRepo _brandRepository;
    private Mapper _mapper;
    private IValidator<Brand> _brandValidator;
    private IValidator<BrandDto> _validator;

    public List<Brand> GetAllBrands()
    {
        return _brandRepository.GetAllBrands();
    }

    public Brand CreateBrand(BrandDto dto)
    {
        var validation = _validator.Validate(dto);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        return _brandRepository.CreateBrand(_mapper.Map<Brand>(dto));
    }
    

    public Brand UpdateBrand(int brandID, Brand brand)
    {
        if (brandID != brand.Id)
            throw new ValidationException("ID in body and route are different (Update)");
        var validation = _brandValidator.Validate(brand);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
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