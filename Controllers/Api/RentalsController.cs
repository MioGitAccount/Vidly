using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Models;
using System.Data.Entity;
using Vidly.Dtos;


namespace Vidly.Controllers.Api
{
    public class RentalsController : ApiController
    {
        private ApplicationDbContext _context;

        public RentalsController()
        {
            _context = new ApplicationDbContext();
        }


        [HttpGet]
        public IHttpActionResult GetRentals()
        {
            return Ok(_context.Rentals
                .Include(r => r.Customer)
                .Include(r => r.Movie)
                .ToList());
                

        }
        [HttpPost]
        public IHttpActionResult CreateRentals(RentalDto rentalDto)
        {

            if (rentalDto.MovieIds.Count == 0)
                return BadRequest("No movie ids have been given");
            
            var customer = _context.Customers.SingleOrDefault(
                c => c.Id == rentalDto.CustomerId);
            
            if (customer == null)
                return BadRequest("CustomerId is not valid.");

            int dummy = 0;

            var movies = _context.Movies.Where(
                m => rentalDto.MovieIds.Contains(m.Id)).ToList();
            if (movies.Count != rentalDto.MovieIds.Count)
                return BadRequest("One or more movie ids are invalid");

            foreach(var movie  in movies)
            {
                
                    
                int AlreadyRented = _context
                    .Rentals.Where(r => r.Movie.Id == movie.Id && r.DateReturned == null).Count();

                dummy = AlreadyRented;
                if (movie.NumberInStock <= AlreadyRented)
                    return BadRequest("there is no more available " + movie.Name + " in the stock");

                var rental = new Rental
                {
                    Customer = customer,
                    Movie = movie,
                    DateRented = DateTime.Now
                };

                _context.Rentals.Add(rental);

            }
                _context.SaveChanges();

            return Ok("Easy" + dummy.ToString()); 
        }

    }
}
