using AutoMapper;
using Hotello.Core.Contracts;
using Hotello.Data;

namespace Hotello.Core.Repositories;

public class HotelRepository : GenericRepository<Hotel>, IHotelRepository
{
    public HotelRepository(HotelloDbContext context, IMapper mapper) : base(context, mapper)
    {
    }
}
