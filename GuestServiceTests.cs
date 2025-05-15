using Xunit;
using Moq;
using System.Threading.Tasks;
using System.Collections.Generic;
using HotelRoomManagement.Application.Services;
using HotelRoomManagement.Infrastructure.Interfaces;
using HotelRoomManagement.Infrastructure.Entities;
using HotelRoomManagement.Application.DTOs;
using HotelRoomManagement.Tests.Helpers;
using FluentAssertions;

namespace HotelRoomManagement.Tests.Services
{
    public class GuestServiceTests
    {
        private readonly Mock<IGuestRepository> _repoMock;
        private readonly GuestService _service;

        public GuestServiceTests()
        {
            _repoMock = new Mock<IGuestRepository>();
            _service = new GuestService(_repoMock.Object, AutoMapperHelper.GetMapper());
        }

        [Fact]
        public async Task GetByIdAsync_ReturnsCorrectGuest()
        {
            // Arrange
            _repoMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(new Guest
            {
                Id = 1,
                FirstName = "John",
                LastName = "Doe",
                Address = "NYC",
                DOB = new DateTime(1990, 1, 1),
                Nationality = "USA",
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(2),
                RoomId = 101
            });

            // Act
            var result = await _service.GetByIdAsync(1);

            // Assert
            result.FirstName.Should().Be("John");
            result.LastName.Should().Be("Doe");
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedGuestDto()
        {
            // Arrange
            var guestDto = new GuestDto
            {
                FirstName = "Alice",
                LastName = "Smith",
                Address = "London",
                DOB = new DateTime(1985, 5, 10),
                Nationality = "UK",
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(5),
                RoomId = 101
            };

            _repoMock.Setup(r => r.CreateAsync(It.IsAny<Guest>())).ReturnsAsync(new Guest
            {
                Id = 3,
                FirstName = "Alice",
                LastName = "Smith",
                Address = "London",
                DOB = new DateTime(1985, 5, 10),
                Nationality = "UK",
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(5),
                RoomId = 101
            });

            // Act
            var result = await _service.CreateAsync(guestDto);

            // Assert
            result.Id.Should().Be(3);
            result.FirstName.Should().Be("Alice");
        }

        [Fact]
        public async Task UpdateAsync_ReturnsTrue_WhenUpdated()
        {
            // Arrange
            _repoMock.Setup(r => r.UpdateAsync(It.IsAny<Guest>())).ReturnsAsync(true);

            var guestDto = new GuestDto
            {
                FirstName = "Updated",
                LastName = "User",
                Address = "Updated Address",
                DOB = new DateTime(1990, 1, 1),
                Nationality = "Updated",
                CheckInDate = DateTime.Today,
                CheckOutDate = DateTime.Today.AddDays(1),
                RoomId = 101
            };

            // Act
            var result = await _service.UpdateAsync(1, guestDto);

            // Assert
            result.Should().BeTrue();
        }
    }
}
