using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Features.Chat.CQRS.Queries;
using Application.Features.Chat.DTOs;
using Application.Features.DoctorProfiles.DTOs;
using Application.Features.InstitutionProfiles.DTOs;
using Application.Responses;
using AutoMapper;
using Domain;
using MediatR;

namespace Application.Features.Chat.CQRS.Handlers;

public class ChatRequestQueryHandler: IRequestHandler<ChatRequestQuery, Result<ChatResponseDto>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IChatRequestSender _chatRequestSender;

    public ChatRequestQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, IChatRequestSender chatRequestSender)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _chatRequestSender = chatRequestSender;
    }

    public async Task<Result<ChatResponseDto>> Handle(ChatRequestQuery request, CancellationToken cancellationToken)
    {

        // create the response
        var response = new Result<ChatResponseDto>();

        // send request to henok's api and get api response? infrastrucutre
        var ApiResponse = await _chatRequestSender.SendMessage(request.ChatRequestDto);

        // if error occured
        if(ApiResponse.Error != null)
        {
            response.Error = ApiResponse.Error.message;
            response.IsSuccess = false;
            return response;
        }


        // if data reply message
        response.IsSuccess = true;
        
        var chatResponse = new ChatResponseDto
        {
            reply = ApiResponse.Data.message
        };

        if (ApiResponse.Data.specializations.Any())
        {
            // doctor = repository calling
            var specialization = ApiResponse.Data.specializations[0];
            var Doctors = await _unitOfWork.DoctorProfileRepository.FilterDoctors(Guid.Empty, new List<string>{specialization}, -1, null);
            chatResponse.Doctors = _mapper.Map<List<DoctorProfileDetailDto>>(Doctors);

        var Institutions = Doctors.Select(d => d.MainInstitution).Distinct();
        chatResponse.Institutions = _mapper.Map<List<InstitutionProfileDetailDto>>(Institutions);

     }

        response.Value = chatResponse;
        return response;
  
    }

}
