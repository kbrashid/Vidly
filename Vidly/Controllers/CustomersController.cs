using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Vidly.Models;
using Vidly.ViewModels;

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

        //adding new Customer -----
        public IActionResult New()
        {
            var membershipTypes = _context.MembershipType.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm",viewModel);
        }

        // ----Saving Form Data --------
        [HttpPost]
       public IActionResult Save(Customer customer)
        {
            if (customer.Id ==0)
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                //Mapper.Map(customer. customerInDb)

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;
            }
            _context.SaveChanges();
            //return View();
            return RedirectToAction("Index", "Customers");

        }
        // ---update data -----
        public IActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if (customer == null)
                return Content("Not Found Customer");
            //return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipType.ToList()
            };

            return View("CustomerForm", viewModel);
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
            //var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            var customer = _context.Customers.Include(c => c.MembershipType).SingleOrDefault(c => c.Id == id);
            if (customer == null)
            {
                Response.StatusCode = 400;
                return Content("Not Found Customer");
            }

            return View(customer);
        }     

       

    }//end class
}//end namespace