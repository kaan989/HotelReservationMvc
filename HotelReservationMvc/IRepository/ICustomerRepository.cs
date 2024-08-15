using HotelReservationMvc.Models;
using System.Collections.Generic;

namespace HotelReservationMvc.IRepository
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<Customer>> GetAllCustomer();
        Task<Customer> GetCustomerById(int id);
        Task<Customer> GetCustomerByName(string name);
        bool AddCustomer(Customer customer);
        bool UpdateCustomer(Customer customer); 
        bool DeleteCustomer(Customer customer);
        bool Save();
    }
}
