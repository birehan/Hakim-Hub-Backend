using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Contracts.Persistence;
using Application.Features.Addresses.CQRS.Handlers;
using Application.Features.Addresses.CQRS.Queries;
using Application.Features.Addresses.DTOs;
using Application.Responses;
using AutoMapper;
using Domain;
using Moq;
using Xunit;

namespace Application.UnitTest.Addresses.Queries
{
    public class GetAddressDetailQueryHandlerTest
    {
        private readonly GetAddressDetailQueryHandler _handler;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;

        public GetAddressDetailQueryHandlerTest()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _handler = new GetAddressDetailQueryHandler(_mockUnitOfWork.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAddressDetail_ShouldReturnAddressDto()
        {
            // Arrange
            var addressId = Guid.NewGuid();
            var address = new Address
            {
                Id = addressId,
                Country = "Country",
                Region = "Region",
                Zone = "Zone",
                Woreda = "Woreda",
                City = "City",
                SubCity = "SubCity",
                Summary = "Summary",
                Longitude = 0.0,
                Latitude = 0.0,
                InstitutionId = Guid.NewGuid(),
                Institution = new InstitutionProfile()
            };

            var addressDto = new AddressDto
            {
                Id = addressId,
                Country = "Country",
                Region = "Region",
                Zone = "Zone",
                Woreda = "Woreda",
                City = "City",
                SubCity = "SubCity",
                Summary = "Summary",
                Longitude = 0.0,
                Latitude = 0.0
            };

            _mockUnitOfWork.Setup(uow => uow.AddressRepository.Get(addressId)).ReturnsAsync(address);
            _mockMapper.Setup(mapper => mapper.Map<AddressDto>(address)).Returns(addressDto);

            var query = new GetAddressDetailQuery { Id = addressId };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsType<Result<AddressDto>>(result);
            Assert.IsType<AddressDto>(result.Value);
            Assert.Equal(addressId, result.Value.Id);
            // Add more assertions for other properties if needed
        }

        [Fact]
        public async Task GetNonExistingAddress_ShouldReturnEmptyObject()
        {
            // Arrange
            var nonExistingId = Guid.NewGuid();
            _mockUnitOfWork.Setup(uow => uow.AddressRepository.Get(nonExistingId)).ReturnsAsync((Address)null);
        
            var query = new GetAddressDetailQuery { Id = nonExistingId };
        
            // Act
            var result = await _handler.Handle(query, CancellationToken.None);
        
            // Assert
            Assert.IsType<Result<AddressDto>>(result);
            Assert.Equal("Item not found.", result.Error);
            Assert.Null(result.Value);
        }

    }
}
