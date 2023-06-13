using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.Addresses.CQRS.Handlers;
using Application.Features.Addresses.CQRS.Queries;
using Application.Features.Addresses.DTOs;
using Application.Responses;
using Domain;
using AutoMapper;
using Moq;
using Xunit;

namespace Application.UnitTest.Addresses.Queries
{
    public class GetAddressListQueryHandlerTest
    {
        private readonly GetAddressListQueryHandler _handler;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;

        public GetAddressListQueryHandlerTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetAddressListQueryHandler(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAddressList_ShouldReturnListOfAddressDto()
        {
            // Arrange
            var addresses = new List<Address>
            {
                new Address { /* Initialize properties */ }
            };

            var addressDtos = new List<AddressDto>
            {
                new AddressDto { /* Initialize properties */ }
            };

            _mockUnitOfWork.Setup(uow => uow.AddressRepository.GetAll()).ReturnsAsync(addresses);
            _mockMapper.Setup(mapper => mapper.Map<List<AddressDto>>(addresses)).Returns(addressDtos);

            var query = new GetAddressListQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsType<Result<List<AddressDto>>>(result);
            Assert.True(result.IsSuccess);
            Assert.Equal(addressDtos.Count, result.Value.Count);
            // TODO: Add more assertions
        }

        [Fact]
        public async Task GetAddressList_WithEmptyResult_ShouldReturnEmptyList()
        {
            // Arrange
            List<Address> emptyList = new List<Address>();

            _mockUnitOfWork.Setup(uow => uow.AddressRepository.GetAll()).ReturnsAsync(emptyList);

            var query = new GetAddressListQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
            
            // Assert
            Assert.IsType<Result<List<AddressDto>>>(result);
            Assert.True(result.IsSuccess);
            Assert.Null(result.Value);
            Assert.Null(result.Error);
        }
    }
}
