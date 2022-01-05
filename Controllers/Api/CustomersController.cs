using AutoMapper;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    
    public class CustomersController : ApiController
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }
        // GET /api/customers
        public IEnumerable<CustomerDto> GetCustomers()
        {
            return _context.Customers
                .Include(c => c.MembershipType)
                .ToList()
                .Select(Mapper.Map<Customer,  CustomerDto>);

        }
        // GET /api/cusomers/1
        public IHttpActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return NotFound();
            return Ok(Mapper.Map<Customer, CustomerDto>(customer));
        }
        // POST /api/customers
        [HttpPost]
        public IHttpActionResult CreateCustomer(CustomerDto customerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var customer = Mapper.Map<CustomerDto, Customer>(customerDto);
            _context.Customers.Add(customer);
            _context.SaveChanges();

            customerDto.Id = customer.Id;
            return Created(new Uri(Request.RequestUri + "/" + customer.Id), customerDto);

        }
        //PUT /api/customers/1
        public void UpdateCustomer(int id, CustomerDto customerDto)
        {
            if(!ModelState.IsValid)
                throw new HttpRequestException(HttpStatusCode.BadRequest.ToString());
            var customerInDB = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customerInDB == null)
                throw new HttpRequestException(HttpStatusCode.NotFound.ToString());

            Mapper.Map<CustomerDto, Customer>(customerDto, customerInDB);
            
            _context.SaveChanges();
        }
        [HttpDelete]
        //DELETE /api/customers/1
        public IHttpActionResult DeleteCustomer(int id)
        {
            var cusomerInDb = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (cusomerInDb == null)
                return NotFound();
            _context.Customers.Remove(cusomerInDb);
            _context.SaveChanges();
            return Ok(cusomerInDb);
        }
    }
}
