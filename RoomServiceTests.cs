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
    public class RoomServiceTests
    {
        private readonly Mock<IRoomRepository> _repoMock;
        private readonly RoomService _service;

        public RoomServiceTests()
        {
            _repoMock = new Mock<IRoomRepository>();
            _service = new RoomService(_repoMock.Object, AutoMapperHelper.GetMapper());
        }

        [Fact]
        public async Task GetAllAsync_ReturnsListOfRoomDtos()
        {
            // Arrange
            _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<Room>
            {
                new Room { Id = 1, Number = 101, Floor = 1, Type = "Standard" }
            });

            // Act
            var result = await _service.GetAllAsync();

            // Assert
            result.Should().HaveCount(1);
            result.Should().Contain(r => r.Number == 101);
        }

        [Fact]
        public async Task CreateAsync_ReturnsCreatedRoomDto()
        {
            // Arrange
            var roomDto = new RoomDto { Number = 202, Floor = 2, Type = "Deluxe" };

            _repoMock.Setup(r => r.CreateAsync(It.IsAny<Room>())).ReturnsAsync(new Room
            {
                Id = 5,
                Number = 202,
                Floor = 2,
                Type = "Deluxe"
            });

            // Act
            var result = await _service.CreateAsync(roomDto);

            // Assert
            result.Id.Should().Be(5);
            result.Type.Should().Be("Deluxe");
        }

        [Fact]
        public async Task DeleteAsync_ReturnsTrue_WhenRoomDeleted()
        {
            // Arrange
            _repoMock.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);

            // Act
            var result = await _service.DeleteAsync(1);

            // Assert
            result.Should().BeTrue();
        }
    }
}
