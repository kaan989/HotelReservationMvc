using HotelReservationMvc.IRepository;
using HotelReservationMvc.Models;

namespace HotelReservationMvc.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        public Task<IEnumerable<Service>> GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
