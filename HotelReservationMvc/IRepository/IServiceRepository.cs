using HotelReservationMvc.Models;

namespace HotelReservationMvc.IRepository
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAll();
    }
}
