using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private VidlyContext _context;

        public MoviesController()
        {
            _context = new VidlyContext();
        }

        protected override void Dispose(bool disposing)
        {
            //base.Dispose(disposing);
            _context.Dispose();
        }
        public IActionResult index()
        {
            //var movies = GetMovies();
            //---Apply egar loading --- add include() -------
            var movies = _context.Movies.Include(m => m.Genre).ToList();
            return View(movies);
        }


        public IActionResult Details(int id)
        {
            //var movie = GetMovies().SingleOrDefault(c => c.Id == id);
            var movie = _context.Movies.Include(m => m.Genre).SingleOrDefault(c => c.Id == id);

            if (movie == null)
            {
                Response.StatusCode = 400;
                return Content("Not Found Movie");
            }

            return View(movie);
             
        }

        private IEnumerable<Movie> GetMovies()
        {
            return new List<Movie>
            {
                new Movie { Id = 1, Name = "Shrek" },
                new Movie { Id = 2, Name = "Wall-e" }
            };
        }

    }
}