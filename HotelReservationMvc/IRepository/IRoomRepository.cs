using HotelReservationMvc.Models;

namespace HotelReservationMvc.IRepository
{
    public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRooms();
        Task<Room> GetRoomById(int id);
        bool AddRoom(Room room);    
        bool UpdateRoom(Room room);
        bool DeleteRoom(Room room);
        bool Save();
    }
}
