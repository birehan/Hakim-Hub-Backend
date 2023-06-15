
using AutoMapper;
using Application.Contracts.Persistence;
using Application.Features.Specialities.DTOs.Validators;
using Application.Responses;
using MediatR;
using Domain;
using Newtonsoft.Json;
using Application.Features.InstitutionProfiles.CQRS.Commands;
using Application.Features.InstitutionProfiles.DTOs.Validators;
using Application.Interfaces;
using Application.Features.Addresses.DTOs;

namespace Application.Features.InstitutionProfiles.CQRS.Handlers
{
    public class CreateInstitutionProfileCommandHandler : IRequestHandler<CreateInstitutionProfileCommand, Result<Guid>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IPhotoAccessor _photoAccessor;


        public CreateInstitutionProfileCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, IPhotoAccessor photoAccessor)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _photoAccessor = photoAccessor;

        }

        public async Task<Result<Guid>> Handle(CreateInstitutionProfileCommand request, CancellationToken cancellationToken)
        {

            var validator = new CreateInstitutionProfileDtoValidator();
            var validationResult = await validator.ValidateAsync(request.CreateInstitutionProfileDto);

            if (!validationResult.IsValid)
                return Result<Guid>.Failure(validationResult.Errors[0].ErrorMessage);

            var uploadedLogo = await _photoAccessor.AddPhoto(request.CreateInstitutionProfileDto.LogoFile);
            var uploadedBanner = await _photoAccessor.AddPhoto(request.CreateInstitutionProfileDto.BannerFile);
            request.CreateInstitutionProfileDto.BannerId = uploadedBanner.PublicId;
            request.CreateInstitutionProfileDto.LogoId = uploadedLogo.PublicId;

            Guid logoId = Guid.NewGuid();
            Photo logo = new Photo { Id = logoId.ToString(), Url = uploadedLogo.Url };
            Guid bannerId = Guid.NewGuid();
            Photo banner = new Photo { Id = bannerId.ToString(), Url = uploadedBanner.Url };

            await _unitOfWork.PhotoRepository.Add(logo);
            await _unitOfWork.PhotoRepository.Add(banner);
            request.CreateInstitutionProfileDto.Logo = logo;
            request.CreateInstitutionProfileDto.Banner = banner;

            var InstitutionProfile = _mapper.Map<InstitutionProfile>(request.CreateInstitutionProfileDto);
            InstitutionProfile institutionProfile = await _unitOfWork.InstitutionProfileRepository.Add(InstitutionProfile);


            if (await _unitOfWork.Save() <= 0)
                return Result<Guid>.Failure("Creation Failed");

            CreateAddressDto addressDto = JsonConvert.DeserializeObject<CreateAddressDto>(request.CreateInstitutionProfileDto.StringAddress);
            var New_Address = _mapper.Map<Address>(addressDto);
            Address Address = await _unitOfWork.AddressRepository.Add(New_Address);
            Address.InstitutionId = institutionProfile.Id;

            if (await _unitOfWork.Save() <= 0)
                return Result<Guid>.Failure("Creation Failed");

            return Result<Guid>.Success(InstitutionProfile.Id);


        }
    }
}
