using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Vidly.Models;

namespace Vidly.Controllers
{
    public class CustomersController : Controller
    {
        private VidlyContext _context;

        public CustomersController()
        {
            _context = new VidlyContext();
        }

        protected override void Dispose(bool disposing)
        {
            //base.Dispose(disposing);
            _context.Dispose();
        }
        // GET: Customers
        public ViewResult Index()
        {
            //var customers = _context.Customers.ToList();
            //---Apply egar loading --- add include() -------
            var customers = _context.Customers.Include(c => c.MembershipType).ToList();

            return View(customers);
        }

        public IActionResult Details(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
            {
                Response.StatusCode = 400;
                return Content("Not Found Customer");
            }

            return View(customer);
        }     


    }//end class
}//end namespace