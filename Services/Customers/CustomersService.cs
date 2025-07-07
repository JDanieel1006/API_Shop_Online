using API_Shop_Online.Data;
using API_Shop_Online.Dto.v1.Customer;
using API_Shop_Online.Models;
using AutoMapper;

namespace API_Shop_Online.Services.Customers
{
    public class CustomersService : ICustomersService
    {
        private readonly ApplicationDbContext _bd;
        private readonly IMapper _mapper;

        public CustomersService(ApplicationDbContext bd, IMapper mapper)
        {
            _bd = bd;
            _mapper = mapper;
        }

        public ICollection<Customer> Get()
        {
            return _bd.Customers.OrderBy(c => c.Name).ToList();
        }

        public Customer GetById(int id)
        {
            return _bd.Customers.FirstOrDefault(c => c.Id == id);
        }

        public async Task<Customer> Create(CustomerSubmissionDto request)
        {
            var entry = _mapper.Map<Customer>(request);

            entry.CreatedAt = DateTime.UtcNow;

            _bd.Customers.Add(entry);
            await _bd.SaveChangesAsync();

            var result = _mapper.Map<Customer>(entry);
            return result;
        }

        public async Task<Customer> Update(int id, CustomerSubmissionDto request)
        {
            var entry = GetById(id)!;

            entry.Name = request.Name;
            entry.LastName = request.LastName;
            entry.Address = request.Address;

            _bd.Customers.Update(entry);

            await _bd.SaveChangesAsync();

            var result = _mapper.Map<Customer>(entry);

            return result;
        }

        public void Delete(int id)
        {
            var entry = GetById(id);

            _bd.Customers.Remove(entry);

            _bd.SaveChanges();
        }
    }
}
