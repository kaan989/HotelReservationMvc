using HotelReservationMvc.Data;
using HotelReservationMvc.IRepository;
using HotelReservationMvc.Models;
using HotelReservationMvc.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HotelReservationMvc.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ApplicationDbContext _context;

        // Constructor for initializing the customer repository and application context
        public CustomerController(ApplicationDbContext context, ICustomerRepository customerRepository)
        {
            _context = context;
            _customerRepository = customerRepository;
        }

        // GET: Display list of customers (accessible only by authorized users)
        [Authorize]
        public async Task<IActionResult> Index()
        {
            // Retrieve all customers
            IEnumerable<Customer> customers = await _customerRepository.GetAllCustomer();
            return View(customers);
        }

        // GET: Show the form to create a new customer (accessible only by authorized users)
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        // POST: Create a new customer (accessible only by authorized users)
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Create(CreateCustomerViewModel customerVM)
        {
            // Check if the model state is valid
            if (ModelState.IsValid)
            {
                // Map the ViewModel data to the Customer model
                var customer = new Customer
                {
                    FirstName = customerVM.FirstName,
                    LastName = customerVM.LastName,
                    Email = customerVM.Email,
                    PhoneNumber = customerVM.PhoneNumber,
                    Address = customerVM.Address,
                };

                // Add the new customer to the repository
                _customerRepository.AddCustomer(customer);
                return RedirectToAction("Index");
            }
            // If the model state is invalid, return the ViewModel back to the view
            return View(customerVM);
        }

        // GET: Display the form to edit an existing customer (accessible only by authorized users)
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            // Retrieve the customer by ID
            var customer = await _customerRepository.GetCustomerById(id);
            if (customer == null)
            {
                return View("Error");
            }

            // Map the Customer data to the EditCustomerViewModel
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

        // POST: Update the customer information (accessible only by authorized users)
        [Authorize]
        [HttpPost, ActionName("Edit")]
        public async Task<IActionResult> EditCustomer(int id, EditCustomerViewModel customerVm)
        {
            // Check if the model state is valid
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit customer");
                return View("Edit", customerVm);
            }

            // Map the ViewModel data to the Customer model
            var customer = new Customer
            {
                Id = id,
                FirstName = customerVm.FirstName,
                LastName = customerVm.LastName,
                Email = customerVm.Email,
                PhoneNumber = customerVm.PhoneNumber,
                Address = customerVm.Address
            };

            // Update the customer information in the repository
            _customerRepository.UpdateCustomer(customer);
            return RedirectToAction("Index");
        }

        // GET: Display the confirmation page to delete a customer (accessible only by authorized users)
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            // Retrieve the customer details by ID
            var customerDetail = await _customerRepository.GetCustomerById(id);
            if (customerDetail == null)
            {
                return View("Error");
            }
            return View(customerDetail);
        }

        // POST: Delete the customer (accessible only by authorized users)
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            // Retrieve the customer details by ID
            var customerDetail = await _customerRepository.GetCustomerById(id);
            if (customerDetail == null)
            {
                return View("Error");
            }

            // Delete the customer from the repository
            _customerRepository.DeleteCustomer(customerDetail);
            return RedirectToAction("Index");
        }
    }
}
