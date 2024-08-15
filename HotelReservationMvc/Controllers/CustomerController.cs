using HotelReservationMvc.Data;
using HotelReservationMvc.IRepository;
using HotelReservationMvc.Models;
using HotelReservationMvc.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationMvc.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ApplicationDbContext _context;
        public CustomerController(ApplicationDbContext context, ICustomerRepository customerRepository)
        {
            _context = context;
            _customerRepository = customerRepository;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Customer> customers = await _customerRepository.GetAllCustomer();
            return View(customers);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerViewModel customerVM)
        {
            if (ModelState.IsValid)
            {
                var customer = new Customer
                {
                    FirstName = customerVM.FirstName,
                    LastName = customerVM.LastName,
                    Email = customerVM.Email,
                    PhoneNumber = customerVM.PhoneNumber,
                    Address = customerVM.Address,
                };

                _customerRepository.AddCustomer(customer);
                return RedirectToAction("Index");
            }
            return View(customerVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                return View("Error");
            }
            var customerVm = new EditCustomerViewModel
            {
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                Email = customer.Email,
                PhoneNumber = customer.PhoneNumber,
                Address = customer.Address
            };


            return View(customerVm);
        }

        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditCustomer(int id, EditCustomerViewModel customerVm)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit club");
                return View("Edit", customerVm);
            }

            var customer = new Customer
            {
                Id = id,
                FirstName = customerVm.FirstName,
                LastName = customerVm.LastName,
                Email = customerVm.Email,
                PhoneNumber = customerVm.PhoneNumber,
                Address = customerVm.Address
            };
            _customerRepository.UpdateCustomer(customer);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var customerDetail = await _customerRepository.GetCustomerById(id);
            if (customerDetail == null)
            {
                return View("Error");
            }
            return View(customerDetail);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var customerDetail = await _customerRepository.GetCustomerById(id);
            if(customerDetail == null)
                return View("Error");
             _customerRepository.DeleteCustomer(customerDetail);
            return RedirectToAction("Index");
        }
    }
}
