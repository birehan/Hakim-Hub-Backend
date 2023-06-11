using MediatR;
using Application.Responses;
using Application.Features.InstitutionProfiles.DTOs;

namespace Application.Features.InstitutionProfiles.CQRS.Commands
{
    public class CreateInstitutionProfileCommand : IRequest<Result<Guid>>
    {
<<<<<<< HEAD
<<<<<<< HEAD
        public CreateInstitutionProfileDto CreateInstitutionProfileDto { get; set; }
=======
        public CreateInstitutionProfileDto InstitutionProfileDto { get; set; }
>>>>>>> 4db4375 (fix(institution): changes some attributes from institution)
=======
        public CreateInstitutionProfileDto CreateInstitutionProfileDto { get; set; }
>>>>>>> d90788f (feat(crud-biruk): done with the cruds about to pull)
    }
}
