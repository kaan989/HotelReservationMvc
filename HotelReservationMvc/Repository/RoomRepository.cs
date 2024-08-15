using HotelReservationMvc.Data;
using HotelReservationMvc.IRepository;
using HotelReservationMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationMvc.Repository
{
    public class RoomRepository : IRoomRepository
    {
        private readonly ApplicationDbContext _context;
        public RoomRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddRoom(Room room)
        {
            _context.Add(room);
            return Save();
        }

        public bool DeleteRoom(Room room)
        {
            _context.Remove(room);
            return Save();
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await _context.Rooms.ToListAsync();
        }

        public async Task<Room> GetRoomById(int id)
        {
            return await _context.Rooms.Include(i => i.Reservations).FirstOrDefaultAsync(i => i.Id == id);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;    
        }

        public bool UpdateRoom(Room room)
        {
            var updated = _context.Update(room);
            return Save();
        }
    }
}
