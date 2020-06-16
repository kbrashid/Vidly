using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : ControllerBase
    {
        private VidlyContext _context;

        public CustomersController()
        {
            _context = new VidlyContext();
        }

        // GET /api/customers
        public IEnumerable<Customer> GetCustomers()
        {
            return _context.Customers.ToList();
        }

        // GET /api/customers/1
        //[ProducesResponseType(StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public ActionResult<Customer> GetCustomer(int id)
        public Customer GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return null;
                //throw new HttpResponseException(HttpStatusCode.NotFound);                
                //throw new HttpResponseException(HttpStatusCode.NotFound);                
                //return new BadRequestObjectResult(this.ModelState);
                //return StatusCode(404);                               
                //return NotFound();
                //return BadRequest();
                return customer;
        }

        //POST /api/customers

        [HttpPost]
        public Customer CreateCustomer(Customer customer)
        {
            if (!ModelState.IsValid)
                return null;
                //return StatusCode(404);
                //return new HttpListenerException(404);

            _context.Customers.Add(customer);
            _context.SaveChanges();

            return customer;
        }

        // PUT /api/customers/1
        [HttpPut]
        public void UpdateCustomer(int id, Customer customer)
        {
            //if (!ModelState.IsValid)
            //throw new HttpResponseException(HttpStatusCode.BadRequest); 
            //return null;
            var customerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            //if (customerInDb == null)
            //throw new HttpResponseException(HttpStatusCode.NotFound); 

            customerInDb.Name = customer.Name;
            customerInDb.Birthdate = customer.Birthdate;
            customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            customerInDb.MembershipTypeId = customer.MembershipTypeId;

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