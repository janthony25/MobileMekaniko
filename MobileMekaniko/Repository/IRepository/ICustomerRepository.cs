using MobileMekaniko.Models.Dto;

namespace MobileMekaniko.Repository.IRepository
{
    public interface ICustomerRepository
    {
        Task<List<CustomerListSummaryDto>> GetCustomers();
        Task AddCustomerAsync(AddCustomerDto model);
        Task<UpdateDeleteCustomerDto> GetCustomerForUpdateDeleteAsync(int id);
        Task DeleteCustomerByIdAsync(int id);
        Task UpdateCustomerByIdAsync(UpdateDeleteCustomerDto model);
        Task<List<CustomerCarSummaryDto>> GetCustomerCarListAsync(int id);
        
    }
}
