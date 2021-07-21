using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

using DevelopmentProject.Models;

namespace DevelopmentProject.Data
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly DevProjMainContext _context;

        public CustomerRepository(DevProjMainContext context)
        {
            _context = context;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customer.ToList();
        }

        public Customer GetCustomerByID(int CustomerID)
        {
            return _context.Customer.Find(CustomerID.ToString());
        }

        public void InsertCustomer(Customer Customer)
        {
            _context.Customer.Add(Customer);
            _context.SaveChanges();
        }

        public void DeleteCustomer(int CustomerID)
        {
            Customer Customer = _context.Customer.Find(CustomerID);
            _context.Customer.Remove(Customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer Customer)
        {
            _context.Entry(Customer).State = EntityState.Modified;
            _context.SaveChanges();
        }
    }
}
