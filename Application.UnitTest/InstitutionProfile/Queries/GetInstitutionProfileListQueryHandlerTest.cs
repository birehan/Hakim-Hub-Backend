using AutoMapper;
using Application.Contracts.Persistence;
using Application.Features.InstitutionProfiles.CQRS.Handlers;
using Application.Features.InstitutionProfiles.CQRS.Queries;
using Application.Features.InstitutionProfiles.DTOs;
using Application.Profiles;
using Application.Responses;
using Application.UnitTest.Mocks;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UnitTest.InstitutionProfiles.Queries
{
    public class GetInstitutionProfileListQueryHandlerTest
    {

        private readonly IMapper _mapper;
        private readonly Mock<IUnitOfWork> _mockRepo;
        private readonly GetInstitutionProfileListQueryHandler _handler;
        public GetInstitutionProfileListQueryHandlerTest()
        {
            _mockRepo = MockUnitOfWork.GetUnitOfWork();
            var mapperConfig = new MapperConfiguration(c =>
            {
                c.AddProfile<MappingProfile>();
            });
            _mapper = mapperConfig.CreateMapper();

            _handler = new GetInstitutionProfileListQueryHandler(_mockRepo.Object, _mapper);

        }


        [Fact]
        public async Task GetInstitutionProfileList()
        {
            var result = await _handler.Handle(new GetInstitutionProfileListQuery(), CancellationToken.None);
            result.ShouldBeOfType<Result<List<InstitutionProfileDto>>>();
            result.Value.Count.ShouldBe(2);
            result.IsSuccess.ShouldBeTrue();
            result.Error.ShouldBe(null);

        }
    }
}