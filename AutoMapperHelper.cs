using AutoMapper;
using HotelRoomManagement.Application.Mappings;

namespace HotelRoomManagement.Tests.Helpers
{
    public static class AutoMapperHelper
    {
        public static IMapper GetMapper()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
            });
            return config.CreateMapper();
        }
    }
}
