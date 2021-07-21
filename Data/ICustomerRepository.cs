using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using DevelopmentProject.Models;

namespace DevelopmentProject.Data
{
	public interface ICustomerRepository
	{
		IEnumerable<Customer> GetCustomers();
		Customer GetCustomerByID(int customerID);
		void InsertCustomer(Customer customer);
		void DeleteCustomer(int customerID);
		void UpdateCustomer(Customer customer);
	}
}
