using API_Shop_Online.Data;

namespace API_Shop_Online.Services.Customers
{
    public class CustomersService : ICustomersService
    {
        private readonly ApplicationDbContext _bd;

        public CustomersService(ApplicationDbContext bd)
        {
            _bd = bd;
        }


    }
}
