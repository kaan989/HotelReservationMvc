using HotelReservationMvc.Data;
using HotelReservationMvc.IRepository;
using HotelReservationMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationMvc.Repository
{
    public class ReservationRepository : IReservationRepository
    {
        private readonly ApplicationDbContext _context;
        public ReservationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddReservation(Reservation reservation)
        {
            _context.Add(reservation);
            return Save();
        }

        public bool DeleteReservation(Reservation reservation)
        {
            _context.Remove(reservation);
            return Save();
        }

        public async Task<IEnumerable<Reservation>> GetAll()
        {
            return await _context.Reservations.Include(a => a.Customer).Include(r => r.Room).ToListAsync();
        }

        public async Task<Reservation> GetById(int id)
        {
            return await _context.Reservations.Include(a => a.Customer).Include(r => r.Room).FirstOrDefaultAsync(a => a.Id == id); 
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateReservation(Reservation reservation)
        {
            var updated = _context.Update(reservation);
            return Save();
        }
    }
}
