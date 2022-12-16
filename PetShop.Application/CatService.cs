using AutoMapper;
using PetShop.Domain;
using FluentValidation;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Application.Validators;

namespace PetShop.Application;

public class CatService: ICatService
{
    private ICatRepo _catRepository;
    private IMapper _mapper;
    private ActualMainCatValidator.MainCatValidator _mainCatValidator;
    private ActualSubCatValidator.SubCatValidator _subCatValidator;
    private IValidator<MainCatDTO> _mainValidator;
    private IValidator<SubCatDTO> _subValidator;

    public CatService(
        ICatRepo repo, 
        IMapper mapper,
        IValidator<MainCategory> mainCatValidator, 
        IValidator<SubCategory> subCatValidator,
        IValidator<MainCatDTO> mainValidator,
        IValidator<SubCatDTO> subValidator
        )
    {
        _mapper = mapper;
        _catRepository = repo;
        _mainCatValidator = new ActualMainCatValidator.MainCatValidator();
        _subCatValidator = new ActualSubCatValidator.SubCatValidator();
        _mainValidator = new MainCatValidator();
        _subValidator = new SubCatValidator();
    }
    public List<MainCategory> GetAllMainCategories( )
    {
        return _catRepository.GetAllMainCategories();
    }
    public MainCategory CreateMainCategory(MainCatDTO mainCategory)
    {
        var validation = _mainValidator.Validate(mainCategory);
        if (!validation.IsValid)
            throw new ValidationException(validation.ToString());
        Console.WriteLine("I Validated the create mc");
        return _catRepository.CreateMainCategory(_mapper.Map<MainCategory>(mainCategory));
    }

    public MainCategory UpdateMainCategory(int mainCatRefID, MainCatDTO mainCategoryDto)
    {
        var validation = _mainValidator.Validate(mainCategoryDto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }

        var mainCat = _mapper.Map<MainCategory>(mainCategoryDto);
        mainCat.MainCategoryID = mainCatRefID;
        return _catRepository.UpdateMainCategory(mainCat);
        
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
        Console.WriteLine("I Validated the create sc");
        return _catRepository.CreateSubCategory(_mapper.Map<SubCategory>(subCategory));
    }

    public SubCategory UpdateSubCategory(int subCatRefID, SubCatDTO subCategoryDto)
    {
        var validation = _subValidator.Validate(subCategoryDto);
        if (!validation.IsValid)
        {
            throw new ValidationException(validation.ToString());
        }

        var subCat = _mapper.Map<SubCategory>(subCategoryDto);
        subCat.SubCategoryID = subCatRefID;
        return _catRepository.UpdateSubCategory(subCat);
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