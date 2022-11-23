using System.ComponentModel.DataAnnotations;
using AutoMapper;
using Factory.Application.Interfaces;
using Factory.Application.PostBoxDTO;
using Factory.Core;
using FluentValidation;
using ValidationException = FluentValidation.ValidationException;

namespace Factory.Application;

    public class BoxService : IBoxService
    {
        private IBoxRepo _boxRepository;
        private IMapper _mapper;
        private IValidator<BoxDTO> _validator;
        private IValidator<Box> _boxValidator;

        public BoxService(IBoxRepo repository, IMapper mapper, IValidator<BoxDTO> validator)
        {
            _boxRepository = repository;
            _mapper = mapper;
            _validator = validator;
        }
    
    
        public List<Box> GetAllBoxes()
        {
            return _boxRepository.GetAllBoxes();
        }
        
        public Box insertBox(BoxDTO dto)
        {
            var validation = _validator.Validate(dto);
            if (!validation.IsValid)
                throw new ValidationException(validation.ToString());
            
            return _boxRepository.insertBox(_mapper.Map<Box>(dto));
        }

        public Box BoxUpdate(int ManFacId, Box box)
        {
            if (ManFacId != box.ManFacId)
                throw new ValidationException("ID in body and route are different (Update)");
            var validation = _boxValidator.Validate(box);
            if (!validation.IsValid)
                throw new ValidationException(validation.ToString());
            return _boxRepository.BoxUpdate(box);
        }

        public Box BoxDelete(int ManFacId)
        {
            if (ManFacId == null)
                throw new ValidationException("ID is invalid");
            return _boxRepository.BoxDelete(ManFacId);
        }

        public Box BoxOfIDFinder(int ManFacId)
        {
            if (ManFacId <= 0)
                throw new ValidationException("ID is invalid");
            return _boxRepository.BoxOfIDFinder(ManFacId);
        }
    }
