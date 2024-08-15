using HotelReservationMvc.Data;
using HotelReservationMvc.IRepository;
using HotelReservationMvc.Models;
using HotelReservationMvc.Repository;
using HotelReservationMvc.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationMvc.Controllers
{
    public class ReservationController : Controller
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ApplicationDbContext _context;
        public ReservationController(ApplicationDbContext context, IReservationRepository reservationRepository)
        {
            _context = context;
            _reservationRepository = reservationRepository;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Reservation> reservations = await _reservationRepository.GetAll();
            return View(reservations);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var customers = _context.Customers
                .Select(c => new
                {
                    Id = c.Id,
                    FullName = c.FirstName + " " + c.LastName
                })
                .ToList();

            ViewBag.Customers = new SelectList(customers, "Id", "FullName");
            ViewBag.Rooms = new SelectList(_context.Rooms, "Id", "RoomNumber");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateReservationViewModel reservationVm)
        {
            reservationVm.TotalPrice = 0;
            var room = await _context.Rooms.FirstOrDefaultAsync(r => r.Id == reservationVm.RoomId);
            if (room == null)
            {
                ModelState.AddModelError("", "Invalid Room ID");
                return View("Create", reservationVm);
            }

            var days = (decimal)(reservationVm.CheckOutDate - reservationVm.CheckInDate).TotalDays;
            var totalPrice = days * room.PricePerNight;


            if (ModelState.IsValid)
            {
               

                // Yeni rezervasyonu oluştur
                var reservation = new Reservation
                {
                    CustomerId = reservationVm.CustomerId,
                    RoomId = reservationVm.RoomId,
                    CheckInDate = reservationVm.CheckInDate,
                    CheckOutDate = reservationVm.CheckOutDate,
                    TotalPrice = reservationVm.TotalPrice,
                    

                };

                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }

            // Model geçersizse formu yeniden yükle ve dropdown listeleri tekrar oluştur
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

            // Dropdown listeleri ViewBag'e atama
            ViewBag.Customers = new SelectList(customers, "Id", "FullName", reservationVm.CustomerId);
            ViewBag.Rooms = new SelectList(rooms, "Id", "RoomNumber", reservationVm.RoomId);

            return View(reservationVm);
        }


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

        public async Task<IActionResult> Edit(int id)
        {
            var reservation = await _reservationRepository.GetById(id);
            if (reservation == null)
            {
                return NotFound();
            }

            // ViewModel'e mevcut verileri yükleyin
            var reservationVm = new EditReservationViewModel
            {
                CustomerId = reservation.CustomerId,
                RoomId = reservation.RoomId,
                CheckInDate = reservation.CheckInDate,
                CheckOutDate = reservation.CheckOutDate,
                TotalPrice = reservation.TotalPrice
            };


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

            // Dropdown listeleri ViewBag'e atama
            ViewBag.Customers = new SelectList(customers, "Id", "FullName", reservationVm.CustomerId);
            ViewBag.Rooms = new SelectList(rooms, "Id", "RoomNumber", reservationVm.RoomId);

            return View(reservationVm);
        }

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
               

                // Dropdown listeleri ViewBag'e atama
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

            existingReservation.CustomerId = reservationVm.CustomerId;
            existingReservation.RoomId = reservationVm.RoomId;
            existingReservation.CheckInDate = reservationVm.CheckInDate;
            existingReservation.CheckOutDate = reservationVm.CheckOutDate;
            existingReservation.TotalPrice = reservationVm.TotalPrice;

            // Güncelleme işlemi
            try
            {
                

                _reservationRepository.UpdateReservation(existingReservation);
                 _reservationRepository.Save();

                return RedirectToAction("Index");
            }
            catch (DbUpdateException ex)
            {
                

                // Dropdown listeleri ViewBag'e atama
                ViewBag.Customers = new SelectList(customers, "Id", "FullName", reservationVm.CustomerId);
                ViewBag.Rooms = new SelectList(rooms, "Id", "RoomNumber", reservationVm.RoomId);

                ModelState.AddModelError("", "There was an error updating the reservation. Please try again.");
                // Hatanın detaylarını loglayabilirsiniz
            }

            return View("Edit", reservationVm);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var reserdtail = await _reservationRepository.GetById(id);
            if(reserdtail == null)
                return NotFound();
            return View(reserdtail);
        }

        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteReservation(int id)
        {
            var reservationdetail = await _reservationRepository.GetById(id);
            if (reservationdetail == null)
                return NotFound();
            _reservationRepository.DeleteReservation(reservationdetail);
            return RedirectToAction("Index");
        }

      
    }
}
