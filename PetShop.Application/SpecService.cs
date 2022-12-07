using AutoMapper;
using FluentValidation;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShop.Application;

public class SpecService : ISpecService
    {
        private ISpecRepo _specRepository;
        private IMapper _mapper;
        private IValidator<SpecDTO> _validator;
        private IValidator<Specs> _productValidator;

        public SpecService(ISpecRepo repository, IMapper mapper, IValidator<SpecDTO> validator,IValidator<Specs> specValidator) 
        {
            _specRepository = repository;
            _mapper = mapper;
            _validator = validator;
            _productValidator = specValidator;
        }
    
    
        public List<Specs> GetAllSpecs()
        {
            return _specRepository.GetAllSpecs();
        }
        
        public Specs CreateSpecs(SpecDTO dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
                throw new ValidationException(validation.ToString());
            
            return _specRepository.CreateSpecs(_mapper.Map<Specs>(dto));
        }

        public Specs UpdateSpecs(int specId, SpecDTO dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.ToString());
            }
            Specs specs = new Specs();
            specs = _mapper.Map<Specs>(dto);
            specs.ID = specId;
            return _specRepository.UpdateSpecs(specs);

        }

        public Specs DeleteSpecsById(int specId)
        {
            if (specId == null)
                throw new ValidationException("ID is invalid");
            return _specRepository.DeleteSpecsById(specId);
        }

        public Specs GetSpecByID(int specId)
        {
            if (specId <= 0)
                throw new ValidationException("ID is invalid");
            return _specRepository.GetSpecsByID(specId);
        }
        
}
