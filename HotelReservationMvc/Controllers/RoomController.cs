using HotelReservationMvc.Data;
using HotelReservationMvc.IRepository;
using HotelReservationMvc.Models;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationMvc.Controllers
{
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRoomRepository _roomRepository;
        public RoomController(ApplicationDbContext context, IRoomRepository roomRepository)
        {
            _context = context;
            _roomRepository = roomRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Room> rooms = await _roomRepository.GetAllRooms();
            return View(rooms);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Room room = await _roomRepository.GetRoomById(id);
            return View(room);
        }


    }
}
