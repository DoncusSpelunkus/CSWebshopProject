using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Factory.Application.Interfaces;
using Factory.Application.PostProdDTO;
using Factory.Domain;
using FluentValidation;
using ValidationException = FluentValidation.ValidationException;

namespace Factory.Application;

    public class ShopService : IShopService
    {
        private IShopRepo _boxRepository;
        private IMapper _mapper;
        private IValidator<ProdDTO> _validator;
        private IValidator<Product> _boxValidator;

        public ShopService(IShopRepo repository, IMapper mapper, IValidator<ProdDTO> validator)
        {
            _boxRepository = repository;
            _mapper = mapper;
            _validator = validator;
        }
    
    
        public List<Product> GetAllBoxes()
        {
            return _boxRepository.GetAllBoxes();
        }
        
        public Product insertBox(ProdDTO dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
                throw new ValidationException(validation.ToString());
            
            return _boxRepository.insertBox(_mapper.Map<Product>(dto));
        }

        public Product BoxUpdate(int ManFacId, Product box)
        {
            if (ManFacId != box.ID)
                throw new ValidationException("ID in body and route are different (Update)");
            var validation = _boxValidator.Validate(box);
            if (!validation.IsValid)
                throw new ValidationException(validation.ToString());
            return _boxRepository.BoxUpdate(box);
        }

        public Product BoxDelete(int ManFacId)
        {
            if (ManFacId == null)
                throw new ValidationException("ID is invalid");
            return _boxRepository.BoxDelete(ManFacId);
        }

        public Product BoxOfIDFinder(int ManFacId)
        {
            if (ManFacId <= 0)
                throw new ValidationException("ID is invalid");
            return _boxRepository.BoxOfIDFinder(ManFacId);
        }
    }
