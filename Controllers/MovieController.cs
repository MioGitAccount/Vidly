using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using System.Data.Entity;
using Vidly.ViewModels;
using System.Data.Entity.Validation;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        // GET: Movie
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New()
        {
            var genres = _context.Genres.ToList();
            var viewModel = new NewMovieViewModel
            {
                
                Genres = genres

            };
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(Movie movie)
        {
          

            if (!ModelState.IsValid)
            {
                var genres = _context.Genres.ToList();
                var viewModel = new NewMovieViewModel
                {
                    Genres = genres,
                    Movie =  movie
                };
                return View("New", viewModel);
            }

            movie.Date = DateTime.Now;
            movie.Genre = _context.Genres.Single(c => c.Id == movie.GenreId);

            if (movie.Id == 0)
                _context.Movies.Add(movie);
            else
            {
                var movieInDb = _context.Movies.Single(c => c.Id == movie.Id);
                movieInDb.Name = movie.Name;
                movieInDb.GenreId = movie.GenreId;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.Date = DateTime.Now;

               
            }
            
                _context.SaveChanges();
            
            return RedirectToAction("Index", "Movies");
        }
        public ActionResult Index()
        {
            if (User.IsInRole("CanManageMovies")) 
            return View("List");

            return View("ReadOnlyList");


        }
        
        
        public ActionResult Details(int id)
        {
            var movies = _context.Movies.Include(c => c.Genre).SingleOrDefault(c => c.Id == id);

            
            if (movies == null)
                return HttpNotFound();
            return View("Details",movies); 
        }
        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult Edit(int id)
        {
            var genres = _context.Genres.ToList();
            var viewModel = new NewMovieViewModel
            {
                Genres = genres,
                Movie = _context.Movies.SingleOrDefault(m => m.Id == id)
            };
            return View("New",viewModel);
                    }
    }
}