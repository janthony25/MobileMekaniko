using Microsoft.EntityFrameworkCore;
using MobileMekaniko.Data;
using MobileMekaniko.Models;
using MobileMekaniko.Models.Dto;
using MobileMekaniko.Repository.IRepository;

namespace MobileMekaniko.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly ApplicationDbContext _data;

        public CustomerRepository(ApplicationDbContext data)
        {
            _data = data;
        }

        public async Task AddCustomerAsync(AddCustomerDto model)
        {
            var customer = new Customer
            {
                CustomerName = model.CustomerName,
                CustomerEmail = model.CustomerEmail,
                CustomerNumber = model.CustomerNumber
            };

            _data.Customers.Add(customer);
            await _data.SaveChangesAsync();
        }

        public async Task DeleteCustomerByIdAsync(int id)
        {
            // Find customer by id
            var customer = await _data.Customers.FindAsync(id);

            if(customer == null)
            {
                throw new KeyNotFoundException("Invalid customer id.");
            }

            _data.Customers.Remove(customer);
            await _data.SaveChangesAsync();
        }

        public async Task<UpdateDeleteCustomerDto> GetCustomerForUpdateDeleteAsync(int id)
        {
            return await _data.Customers
                .Where(c => c.CustomerId == id)
                .Select(c => new UpdateDeleteCustomerDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    CustomerEmail = c.CustomerEmail,
                    CustomerNumber = c.CustomerNumber,
                    DateEdited = c.DateEdited
                }).FirstOrDefaultAsync();
        }

        public async Task<List<CustomerListSummaryDto>> GetCustomers()
        {
            return await _data.Customers
                .Select(c => new CustomerListSummaryDto
                {
                    CustomerId = c.CustomerId,
                    CustomerName = c.CustomerName,
                    CustomerEmail = c.CustomerEmail,
                    CustomerNumber = c.CustomerNumber,
                    DateAdded = c.DateAdded,
                    DateEdited = c.DateEdited
                }).ToListAsync();
        }

        public async Task UpdateCustomerByIdAsync(UpdateDeleteCustomerDto model)
        {
            // Find customer by id
            var customer = await _data.Customers
                .Where(c => c.CustomerId == model.CustomerId)
                .FirstOrDefaultAsync();

            if (customer == null)
            {
                throw new KeyNotFoundException("Invalid customer id.");
            }

            customer.CustomerName = model.CustomerName;
            customer.CustomerEmail = model.CustomerEmail;
            customer.CustomerNumber = model.CustomerNumber;

            customer.DateEdited = DateTime.Now;

            await _data.SaveChangesAsync();
        }
    }
}
