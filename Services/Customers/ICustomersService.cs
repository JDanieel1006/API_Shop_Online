using API_Shop_Online.Common.Enum;
using API_Shop_Online.Dto.v1.Customer;
using API_Shop_Online.Models;

namespace API_Shop_Online.Services.Customers
{
    public interface ICustomersService
    {
        ICollection<Customer> Get();

        Customer? GetById(int id);

        Task<List<CustomerArticleDto>> GetArticlesByCustomer(int customerId, CustomerArticleStatus? status = null);

        Task<Customer> Create(CustomerSubmissionDto request);

        Task<CustomerArticleDto> AddArticleToCustomer(int customerId, CustomerArticleSubmissionDto dto);

        Task<Customer> Update(int id, CustomerSubmissionDto request);

        void Delete(int id);

    }
}
