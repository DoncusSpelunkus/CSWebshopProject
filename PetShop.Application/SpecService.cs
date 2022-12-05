using AutoMapper;
using FluentValidation;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShop.Application;

public class SpecService : ISpecService
    {
        private ISpecRepo _productRepository;
        private IMapper _mapper;
        private IValidator<SpecDTO> _validator;
        private IValidator<Specs> _productValidator;

        public SpecService(ISpecRepo repository, IMapper mapper, IValidator<SpecDTO> validator,IValidator<Specs> specValidator) 
        {
            _productRepository = repository;
            _mapper = mapper;
            _validator = validator;
            _productValidator = specValidator;
        }
    
    
        public List<Specs> GetAllSpecs()
        {
            return _productRepository.GetAllSpecs();
        }
        
        public Specs CreateSpecs(SpecDTO dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
                throw new ValidationException(validation.ToString());
            
            return _productRepository.CreateSpecs(_mapper.Map<Specs>(dto));
        }

        public Specs UpdateSpecs(int specId, Specs specs)
        {
            
            if (specId != specs.ID)
                throw new ValidationException("ID in body and route are different (Update)");
            var validation = _productValidator.Validate(specs);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.ToString());
            }
            return _productRepository.UpdateSpecs(specs);
        }

        public Specs DeleteSpecsById(int specId)
        {
            if (specId == null)
                throw new ValidationException("ID is invalid");
            return _productRepository.DeleteSpecsById(specId);
        }

        public Specs GetSpecByID(int specId)
        {
            if (specId <= 0)
                throw new ValidationException("ID is invalid");
            return _productRepository.GetSpecsByID(specId);
        }
        
}
