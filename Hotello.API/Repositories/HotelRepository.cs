using AutoMapper;
using Hotello.API.Contracts;
using Hotello.API.Data;

namespace Hotello.API.Repositories;

public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
{
    public HotelRepository(HotelloDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
