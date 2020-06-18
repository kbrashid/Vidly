﻿using System;
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
    public class CustomersController : ControllerBase
    {
        private VidlyContext _context;
        private  IMapper _mapper;

        public CustomersController()
        {
            _context = new VidlyContext();
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Customer, CustomerDto>());
            _mapper = config.CreateMapper();
        }

        // GET /api/customers
        [HttpGet]
        public IEnumerable<CustomerDto> GetCustomers()
        {
            //var config = new MapperConfiguration(cfg => cfg.CreateMap<Customer, CustomerDto>());
            //var mapper = config.CreateMapper();
            //return _context.Customers.ToList().Select(_mapper.Map<Customer, CustomerDto>);

            return _context.Customers.ToList().Select(_mapper.Map<Customer, CustomerDto>);
            //return _context.Customers.ToList();
        }

        // GET /api/customers/1
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<Customer> GetCustomer(int id)
        [HttpGet("{id}")]
        public CustomerDto GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return null;
            //throw new HttpResponseException(HttpStatusCode.NotFound);                              
            //return new BadRequestObjectResult(this.ModelState);
            //return NotFound();
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

           // _mapper.Map<CustomerDto, Customer>(customerDto, customerInDb);
            _mapper.Map(customerDto, customerInDb);

            //customerInDb.Name = customer.Name;
            //customerInDb.Birthdate = customer.Birthdate;
            //customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            //customerInDb.MembershipTypeId = customer.MembershipTypeId;

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