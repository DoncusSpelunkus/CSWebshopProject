using AutoMapper;
using FluentValidation;
using PetShop.Application.Interfaces;
using PetShop.Application.PostProdDTO;
using PetShop.Domain;

namespace PetShop.Application;

public class SpecService : ISpecService
    {
        private ISpecRepo _specsRepository;
        private IMapper _mapper;
        private IValidator<SpecDTO> _validator;
        private IValidator<Specs> _specsValidator;

        public SpecService(ISpecRepo repository, IMapper mapper, IValidator<SpecDTO> validator,IValidator<Specs> specValidator) 
        {
            _specsRepository = repository;
            _mapper = mapper;
            _validator = validator;
            _specsValidator = specValidator;
        }
    
    
        public List<Specs> GetAllSpecs()
        {
            return _specsRepository.GetAllSpecs();
        }
        
        public Specs CreateSpecs(SpecDTO dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
                throw new ValidationException(validation.ToString());
            
            return _specsRepository.CreateSpecs(_mapper.Map<Specs>(dto));
        }

        public Specs UpdateSpecs(int specId, Specs specs)
        {
            
            if (specId != specs.ID)
                throw new ValidationException("ID in body and route are different (Update)");
            var validation = _specsValidator.Validate(specs);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.ToString());
            }
            return _specsRepository.UpdateSpecs(specs);
        }

        public Specs DeleteSpecsById(int specId)
        {
            if (specId == null)
                throw new ValidationException("ID is invalid");
            return _specsRepository.DeleteSpecsById(specId);
        }

        public Specs GetSpecByID(int specId)
        {
            if (specId <= 0)
                throw new ValidationException("ID is invalid");
            return _specsRepository.GetSpecsByID(specId);
        }
        
        public void RebuildDB()
        {
            _specsRepository.RebuildDB();
        }
}
