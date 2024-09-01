using HotelReservationMvc.Data;
using HotelReservationMvc.IRepository;
using HotelReservationMvc.Models;
using HotelReservationMvc.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationMvc.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ApplicationDbContext _context;

        // Constructor to inject dependencies
        public ReservationController(ApplicationDbContext context, IReservationRepository reservationRepository)
        {
            _context = context;
            _reservationRepository = reservationRepository;
        }

        // GET: Displays a list of all reservations
        [Authorize]
        public async Task<IActionResult> Index()
        {
            IEnumerable<Reservation> reservations = await _reservationRepository.GetAll();
            return View(reservations);
        }

        // GET: Displays the form for creating a new reservation
        [Authorize]
        [HttpGet]
        public IActionResult Create()
        {
            // Retrieve customers and available rooms from the database
            var customers = _context.Customers
                .Select(c => new
                {
                    Id = c.Id,
                    FullName = c.FirstName + " " + c.LastName,
                })
                .ToList();
            var rooms = _context.Rooms
                .Where(c => c.IsAvailable == true).ToList();

            // Populate dropdown lists with customers and rooms
            ViewBag.Customers = new SelectList(customers, "Id", "FullName");
            ViewBag.Rooms = new SelectList(rooms, "Id", "RoomNumber");

            return View();
        }

        // POST: Handles the submission of the new reservation form
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationViewModel reservationVm)
        {
            // Validate the selected room and dates
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == reservationVm.RoomId);
            if (room == null)
            {
                ModelState.AddModelError("", "Invalid Room ID");
                return View("Create", reservationVm);
            }

            if (reservationVm.CheckInDate < DateTime.Now.Date)
            {
                ModelState.AddModelError("", "Please choose a valid check-in date.");
                return View("Create", reservationVm);
            }

            if (reservationVm.CheckOutDate <= reservationVm.CheckInDate)
            {
                ModelState.AddModelError("", "Check-out date must be after check-in date.");
                return View("Create", reservationVm);
            }

            var totalDays = (decimal)(reservationVm.CheckOutDate.Date - reservationVm.CheckInDate.Date).TotalDays;

            if (ModelState.IsValid)
            {
                // Create and save the new reservation
                var reservation = new Reservation
                {
                    CustomerId = reservationVm.CustomerId,
                    RoomId = reservationVm.RoomId,
                    CheckInDate = reservationVm.CheckInDate,
                    CheckOutDate = reservationVm.CheckOutDate,
                    TotalPrice = room.PricePerNight * totalDays,
                };
                room.IsAvailable = false; // Mark the room as unavailable
                _context.Rooms.Update(room);
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Reload dropdown lists if the model state is invalid
            var customers = _context.Customers
                .Select(c => new
                {
                    Id = c.Id,
                    FullName = c.FirstName + " " + c.LastName
                })
                .ToList();

            var rooms = _context.Rooms
                .Select(r => new
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber
                })
                .ToList();

            ViewBag.Customers = new SelectList(customers, "Id", "FullName", reservationVm.CustomerId);
            ViewBag.Rooms = new SelectList(rooms, "Id", "RoomNumber", reservationVm.RoomId);

            return View(reservationVm);
        }

        // GET: Fetches customer details by ID
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetCustomerById(int customerId)
        {
            var customer = await _context.Customers
                .Where(c => c.Id == customerId)
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                return NotFound();
            }

            return Json(customer);
        }

        // GET: Displays the form for editing an existing reservation
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _reservationRepository.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            // Load existing reservation data into the ViewModel
            var reservationVm = new EditReservationViewModel
            {
                CustomerId = reservation.CustomerId,
                RoomId = reservation.RoomId,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                TotalPrice = reservation.TotalPrice
            };

            // Populate dropdown lists with customers and rooms
            var customers = _context.Customers
                .Select(c => new
                {
                    Id = c.Id,
                    FullName = c.FirstName + " " + c.LastName
                })
                .ToList();

            var rooms = _context.Rooms
                .Select(r => new
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber
                })
                .ToList();

            ViewBag.Customers = new SelectList(customers, "Id", "FullName", reservationVm.CustomerId);
            ViewBag.Rooms = new SelectList(rooms, "Id", "RoomNumber", reservationVm.RoomId);

            return View(reservationVm);
        }

        // POST: Handles the submission of the edit reservation form
        [Authorize]
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> Edit(int id, EditReservationViewModel reservationVm)
        {
            var customers = _context.Customers
               .Select(c => new
               {
                   Id = c.Id,
                   FullName = c.FirstName + " " + c.LastName
               })
               .ToList();

            var rooms = _context.Rooms
                .Select(r => new
                {
                    Id = r.Id,
                    RoomNumber = r.RoomNumber
                })
                .ToList();

            if (!ModelState.IsValid)
            {
                // Reload dropdown lists if the model state is invalid
                ViewBag.Customers = new SelectList(customers, "Id", "FullName", reservationVm.CustomerId);
                ViewBag.Rooms = new SelectList(rooms, "Id", "RoomNumber", reservationVm.RoomId);

                ModelState.AddModelError("", "Failed to edit reservation");
                return View("Edit", reservationVm);
            }

            var existingReservation = await _reservationRepository.GetById(id);
            if (existingReservation == null)
            {
                return NotFound();
            }

            // Update the reservation with new data
            existingReservation.CustomerId = reservationVm.CustomerId;
            existingReservation.RoomId = reservationVm.RoomId;
            existingReservation.CheckInDate = reservationVm.CheckInDate;
            existingReservation.CheckOutDate = reservationVm.CheckOutDate;
            existingReservation.TotalPrice = reservationVm.TotalPrice;

            // Save the updated reservation
            try
            {
                _reservationRepository.UpdateReservation(existingReservation);
                _reservationRepository.Save();

                return RedirectToAction("Index");
            }
            catch (DbUpdateException)
            {
                // Handle the error and reload the form
                ViewBag.Customers = new SelectList(customers, "Id", "FullName", reservationVm.CustomerId);
                ViewBag.Rooms = new SelectList(rooms, "Id", "RoomNumber", reservationVm.RoomId);

                ModelState.AddModelError("", "There was an error updating the reservation. Please try again.");
            }

            return View("Edit", reservationVm);
        }

        // GET: Displays the reservation details for deletion
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var reserdtail = await _reservationRepository.GetById(id);

            if (reserdtail == null)
                return NotFound();

            return View(reserdtail);
        }

        // POST: Handles the deletion of a reservation
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservationdetail = await _reservationRepository.GetById(id);
            if (reservationdetail == null)
                return NotFound();

            reservationdetail.Room.IsAvailable = true; // Mark the room as available again
            _context.Rooms.Update(reservationdetail.Room);
            _reservationRepository.DeleteReservation(reservationdetail);

            return RedirectToAction("Index");
        }
    }
}
