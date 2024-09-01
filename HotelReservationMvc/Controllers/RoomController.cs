using HotelReservationMvc.Data;
using HotelReservationMvc.IRepository;
using HotelReservationMvc.Models;
using HotelReservationMvc.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationMvc.Controllers
{
    public class RoomController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IRoomRepository _roomRepository;
        private readonly IPhotoService _photoService;

        // Constructor to inject dependencies
        public RoomController(ApplicationDbContext context, IRoomRepository roomRepository, IPhotoService photoService)
        {
            _context = context;
            _roomRepository = roomRepository;
            _photoService = photoService;
        }

        // GET: Displays a list of all rooms
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Room> rooms = await _roomRepository.GetAllRooms();
            return View(rooms);
        }

        // GET: Displays details of a specific room by ID
        [Authorize]
        public async Task<IActionResult> Detail(int id)
        {
            Room room = await _roomRepository.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }
            return View(room);
        }

        // GET: Displays the form for creating a new room
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Handles the submission of the new room form
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateRoomViewModel createRoomViewModel)
        {
            if (createRoomViewModel == null)
            {
                return BadRequest(); // Return 400 if no data is provided
            }

            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(createRoomViewModel.Image);

                if (result != null)
                {
                    var room = new Room
                    {
                        Description = createRoomViewModel.Description,
                        PricePerNight = createRoomViewModel.PricePerNight,
                        IsAvailable = true, // By default, new rooms are available
                        Capacity = createRoomViewModel.Capacity,
                        RoomNumber = createRoomViewModel.RoomNumber,
                        Image = result.Url.ToString(),
                    };

                    _roomRepository.AddRoom(room);
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError("", "Photo upload failed.");
                }
            }

            // If the model state is invalid, reload the form with validation errors
            return View(createRoomViewModel);
        }
    }
}
