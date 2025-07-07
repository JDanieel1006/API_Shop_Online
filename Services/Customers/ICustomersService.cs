using API_Shop_Online.Dto.v1.Customer;
using API_Shop_Online.Models;

namespace API_Shop_Online.Services.Customers
{
    public interface ICustomersService
    {
        ICollection<Customer> Get();

        Customer? GetById(int id);

        Task<Customer> Create(CustomerSubmissionDto request);

        Task<Customer> Update(int id, CustomerSubmissionDto request);

        void Delete(int id);
    }
}
