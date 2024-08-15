using HotelReservationMvc.Data;
using HotelReservationMvc.IRepository;
using HotelReservationMvc.Models;
using Microsoft.EntityFrameworkCore;

namespace HotelReservationMvc.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public bool AddCustomer(Customer customer)
        {
            _context.Add(customer);
            return Save();
        }

        public bool DeleteCustomer(Customer customer)
        {
            _context.Remove(customer);
            return Save();
        }

        public async Task<IEnumerable<Customer>> GetAllCustomer()
        {
            return await _context.Customers.Include(i => i.Reservations).ToListAsync();
        }

        public async Task<Customer> GetCustomerById(int id)
        {
            return await _context.Customers.Include(i => i.Reservations).FirstOrDefaultAsync(i => i.Id == id);
        }

        public async Task<Customer> GetCustomerByName(string name)
        {
            return await _context.Customers.Include(i => i.Reservations).FirstOrDefaultAsync(i => i.FirstName == name);
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCustomer(Customer customer)
        {
            var updated = _context.Update(customer);
            return Save();
        }
    }
}
