﻿using API_Shop_Online.Common.Enum;
using API_Shop_Online.Data;
using API_Shop_Online.Dto.v1.Customer;
using API_Shop_Online.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API_Shop_Online.Services.Customers
{
    public class CustomersService : ICustomersService
    {
        private readonly ApplicationDbContext _bd;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public CustomersService(ApplicationDbContext bd, IMapper mapper, IConfiguration configuration)
        {
            _bd = bd;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<string> AuthCustomer(string email, string password)
        {
            var customer = await _bd.Customers
                .FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            if (customer == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.NameIdentifier, customer.Id.ToString()),
                new Claim(ClaimTypes.Name, customer.Email)
            }),
                Expires = DateTime.UtcNow.AddHours(2),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public ICollection<Customer> Get()
        {
            return _bd.Customers.OrderBy(c => c.Name).ToList();
        }

        public async Task<List<CustomerArticleDto>> GetArticlesByCustomer(int customerId, CustomerArticleStatus? status = null)
        {
            var query = from ca in _bd.CustomerArticles
                        join a in _bd.Articles on ca.ArticleId equals a.Id
                        where ca.CustomerId == customerId
                        select new CustomerArticleDto
                        {
                            CustomerId = ca.CustomerId,
                            ArticleId = ca.ArticleId,
                            CreatedAt = ca.CreatedAt,
                            Status = ca.Status,
                            ArticlePrice = a.Price
                        };

            // Si se manda un estado, filtra por ese estado
            if (status != null)
                query = query.Where(dto => dto.Status == status.Value);

            return await query.ToListAsync();
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

        public async Task<CustomerArticleDto> AddArticleToCustomer(int customerId, CustomerArticleSubmissionDto dto)
        {
            // Elimina relaciones existentes entre el cliente y ese artículo
            var existingRelations = await _bd.CustomerArticles
                .Where(ca => ca.CustomerId == customerId && ca.ArticleId == dto.ArticleId)
                .ToListAsync();

            _bd.CustomerArticles.RemoveRange(existingRelations);

            var customerArticle = new CustomerArticle
            {
                CustomerId = customerId,
                ArticleId = dto.ArticleId,
                CreatedAt = DateTime.UtcNow,
                Status = dto.Status
            };

            _bd.CustomerArticles.Add(customerArticle);
            await _bd.SaveChangesAsync();

            return _mapper.Map<CustomerArticleDto>(customerArticle);
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
