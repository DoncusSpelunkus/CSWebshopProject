using AutoMapper;
using Factory.Domain;
using FluentValidation;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Application.Validators;

namespace PetShop.Application;

public class CatService: ICatService
{
    private ICatRepo _catRepository;
    private Mapper _mapper;
    private IValidator<MainCategory> _mainCatValidator;
    private IValidator<SubCategory> _subCatValidator;
    private IValidator<MainCatDTO> _mainValidator;
    private IValidator<SubCatDTO> _subValidator;
    public List<MainCategory> GetAllMainCategories()
    {
        return _catRepository.GetAllMainCategories();
    }
    public MainCategory CreateMainCategory(MainCatDTO mainCategory)
    {
        var validation = _mainValidator.Validate(mainCategory);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
            
        return _catRepository.CreateMainCategory(_mapper.Map<MainCategory>(mainCategory));
    }

    public MainCategory UpdateMainCategory(int mainCatRefID, MainCategory mainCategory)
    {
        if (mainCatRefID != mainCategory.MainCategoryID)
            throw new ValidationException("ID in body and route are different (Update)");
        var validation = _mainCatValidator.Validate(mainCategory);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
        return _catRepository.UpdateMainCategory(mainCategory);
    }

    public MainCategory GetMainCategoryById(int mainCatId)
    {
        if (mainCatId <= 0)
            throw new ValidationException("ID is invalid");
        return _catRepository.GetMainCategoryByID(mainCatId);
    }

    public MainCategory DeleteMainCategoryById(int mainCatId)
    {
        if (mainCatId == null)
            throw new ValidationException("ID is invalid");
        return _catRepository.DeleteMainCategoryByID(mainCatId);
    }

    public List<SubCategory> GetAllSubCategories()
    {
        return _catRepository.GetAllSubCategories();
    }
    
    public SubCategory CreateSubCategory(SubCatDTO subCategory)
    {
        var validation = _subValidator.Validate(subCategory);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
            
        return _catRepository.CreateSubCategory(_mapper.Map<SubCategory>(subCategory));
    }

    public SubCategory UpdateSubCategory(int subCatRefID,SubCategory subCategory)
    {
        if (subCatRefID != subCategory.SubCategoryID)
            throw new ValidationException("ID in body and route are different (Update)");
        var validation = _subCatValidator.Validate(subCategory);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }
        return _catRepository.UpdateSubCategory(subCategory);
    }

    public SubCategory GetSubCategoryById(int subCatId)
    {
        if (subCatId <= 0)
            throw new ValidationException("ID is invalid");
        return _catRepository.GetSubCategoryByID(subCatId);
    }
    
    public SubCategory DeleteSubCategoryById(int subCatId)
    {
        return _catRepository.DeleteSubCategoryByID(subCatId);
    }
}