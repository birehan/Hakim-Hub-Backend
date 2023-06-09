

using AutoMapper;
using Application.Contracts.Persistence;
using Application.Responses;
using MediatR;
namespace Application.Features.Educations.CQRS.Handlers;

public class DeleteEducationCommandHandler: IRequestHandler<DeleteEducationCommand, Result<Guid>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public DeleteEducationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<Guid>> Handle(DeleteEducationCommand request, CancellationToken cancellationToken)
    {
        var education = await _unitOfWork.EducationRepository.Get(request.Id);

        if (education is null) return null;

        await _unitOfWork.EducationRepository.Delete(education);

        if (await _unitOfWork.Save() > 0)
            return Result<Guid>.Success(education.Id);

        return Result<Guid>.Failure("Delete Failed");


    }
}
