using API_Shop_Online.Dto.v1.Customer;
using API_Shop_Online.Models;
using AutoMapper;

namespace API_Shop_Online.Mapper
{
    public class StoreMapper: Profile
    {
        public StoreMapper()
        {
            CreateMap<CustomerSubmissionDto, Customer>();
            CreateMap<Customer, CustomerDto>().ReverseMap();
        }
    }
}
