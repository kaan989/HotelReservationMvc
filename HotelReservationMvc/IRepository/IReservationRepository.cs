using HotelReservationMvc.Models;

namespace HotelReservationMvc.IRepository
{
    public interface IReservationRepository
    {
        Task<IEnumerable<Reservation>> GetAll();
        Task<Reservation> GetById(int id);
        bool AddReservation(Reservation reservation);
        bool UpdateReservation(Reservation reservation);
        bool DeleteReservation(Reservation reservation);
        bool Save();
    }
}
