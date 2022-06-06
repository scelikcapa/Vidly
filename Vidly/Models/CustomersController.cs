using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Caching;
using System.Web.Mvc;
using System.Windows.Forms;
using Vidly.Models.IdentityModels;
using Vidly.ViewModels;

namespace Vidly.Models
{
    public class CustomersController : Controller
    {
        private ApplicationDbContext _context;

        public CustomersController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
            // base.Dispose(disposing);
        }



        public ActionResult New()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                Customer=new Customer(),
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm",viewModel);
        }



        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel()
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };
            // View(string viewName, object model) - change viewName if you want to send another page than "Edit"  
            return View("CustomerForm", viewModel);
        }



        [HttpPost] //if your actions modify data, they should never be accessible by HttpGet
        [ValidateAntiForgeryToken]
        // Attention! Parameter is not equals with the Model. MVC Framework bind this Customer object to Form Data
        public ActionResult Save(Customer customer /*CustomerFormViewModel viewModel*/)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };
                return View("CustomerForm",viewModel);
            }


            if (customer.Id==0) 
                _context.Customers.Add(customer);
            else
            {
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id);

                // Microsoft Way: Security Issues, updates all parameters
                // TryUpdateModel(customerInDb);

                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.MembershipTypeId = customer.MembershipTypeId;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

            }
            
            _context.SaveChanges();

            return RedirectToAction("Index", "Customers");
        }

        // GET: Customers
        public ViewResult Index()
        {
            // SECTION 9: PERFORMANCE OPTIMIZATION - 105. DATA CACHE
            // use this if you do performance optimization before and find that it can be useful
            if (MemoryCache.Default["Genres"]==null)
            {
                MemoryCache.Default["Genres"] = _context.Genres.ToList();
            }

            var genres = MemoryCache.Default["Genres"] as IEnumerable<Genre>;



            return View();

            // NOW WE ARE USING /api/GetCustomers. NO NEED HERE
            //var customers = _context.Customers.Include(c=>c.MembershipType).ToList();

            //return View(customers);
        }










        [Route("Customers/Details/{id:range(1,2)}")]
        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c=>c.MembershipType).SingleOrDefault(c => c.Id == id);
            return View(customer);
        }


        // mosh solution
        //private IEnumerable<Customer> GetCustomers()
        //{
        //    return new List<Customer>
        //    {
        //        new Customer { Id = 1, Name = "John Smith" },
        //        new Customer { Id = 2, Name = "Mary Williams" }
        //    };
        //}

        
    }
}