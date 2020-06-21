using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Route("/api/[controller]")]
    [ApiController]
    public class CustomersControllerNotRestful : ControllerBase
    {
        private readonly VidlyContext _context;
        private readonly IMapper _mapper;

        public CustomersControllerNotRestful(VidlyContext context, IMapper mapper)
        {
            //_context = new VidlyContext();
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Customer, CustomerDto>());
            //_mapper = config.CreateMapper();
            _context = context;
            _mapper = mapper;
        }

        // GET /api/customers
        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers.ToList().Select(_mapper.Map<Customer, CustomerDto>);            
            //return _context.Customers.ToList();
        }

        // GET /api/customers/1        
        [HttpGet("{id}")]
        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return null;            
            //return BadRequest();

            //return customer;
            return _mapper.Map<Customer, CustomerDto>(customer);
        }

        //POST /api/customers
        [HttpPost]
        public CustomerDto CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return null;
            //return StatusCode(404);    
            
            var customer = _mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            return customerDto;
        }

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            //if (!ModelState.IsValid)
            //throw new HttpResponseException(HttpStatusCode.BadRequest); 
            //return null;
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            //if (customerInDb == null)
            //throw new HttpResponseException(HttpStatusCode.NotFound); 


            _mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
            //_mapper.Map<Customer, CustomerDto>(customerInDb, customerDto);
            //_mapper.Map(customerDto, customerInDb);
           

            _context.SaveChanges();

        }

        // DELETE /api/customers/1
        [HttpDelete]
        public void DeleteCustomer(int id)
        {
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            //if (customerInDb == null)
            //throw new HttpResponseException(HttpStatusCode.NotFound); 

            _context.Customers.Remove(customerInDb);
            _context.SaveChanges();
        }

    }
}