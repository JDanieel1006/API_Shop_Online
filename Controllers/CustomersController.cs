﻿using API_Shop_Online.Common.Enum;
using API_Shop_Online.Dto.v1.Customer;
using API_Shop_Online.Models;
using API_Shop_Online.Services.Customers;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace API_Shop_Online.Controllers
{
    [Authorize]
    [ApiController]
    [Route("v1/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersService _customersService;
        private readonly IMapper _mapper;

        public CustomersController(ICustomersService ctRepo, IMapper mapper)
        {
            _customersService = ctRepo;
            _mapper = mapper;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] CustomerLoginSubmissionDto request)
        {
            var token = await _customersService.AuthCustomer(request.Email, request.Password);

            if (token == null)
                return Unauthorized(new { message = "Credenciales inválidas" });

            return Ok(new { token });
        }

        [HttpGet]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Get()
        {
            var response = _customersService.Get();

            var lisDto = new List<CustomerDto>();

            foreach (var lista in response)
            {
                lisDto.Add(_mapper.Map<CustomerDto>(lista));
            }
            return Ok(lisDto);
        }

        [HttpGet("{id}", Name = "GetCustomerById")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerArticleDto))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetById(int id)
        {
            return Ok(_customersService.GetById(id));
        }

        [HttpGet("{customerId}/articles")]
        [AllowAnonymous]
        public async Task<IActionResult> GetArticlesByCustomer(int customerId, [FromQuery] CustomerArticleStatus? status)
        {
            var result = await _customersService.GetArticlesByCustomer(customerId, status);
            return Ok(result);
        }

        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Create([FromBody] CustomerSubmissionDto request)
        {
            var response = await _customersService.Create(request);

            return CreatedAtAction(nameof(GetById), new { id = response.Id }, response);
        }

        [HttpPost("{customerId}/articles")]
        [AllowAnonymous]
        public async Task<IActionResult> AddArticleToCustomer(int customerId, [FromBody] CustomerArticleSubmissionDto dto)
        {
            var result = await _customersService.AddArticleToCustomer(customerId, dto);
            return CreatedAtAction(nameof(GetArticlesByCustomer), new { customerId = customerId }, result);
        }

        [HttpPut("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, [FromBody] CustomerSubmissionDto request)
        {
            var result = await _customersService.Update(id, request);

            return Ok(result);
        }

        [HttpDelete("{id}")]
        [AllowAnonymous]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            _customersService.Delete(id);

            return NoContent();
        }

    }
}
