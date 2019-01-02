using Microsoft.AspNetCore.Mvc;
using newvat.Repository;
using newvat.ViewModels;
using System.Linq;

namespace newvat.Controllers
{
    [Route("/api/customers")]
    public class CustomerController : Controller
    {
        private CustomerRepo _repo =new CustomerRepo();
        [HttpGet]
        public IActionResult Index()
        {
            var users = _repo.SelectAll();
            return Ok(users);
        }

        [HttpPost]
        public IActionResult AddCustomer(CustomerVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = _repo.Insert(vm);
            return Ok(result);
        }

        [HttpPut]
        public IActionResult UpdateCustomer(CustomerVM vm)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = _repo.Update(vm);
            return Ok(result);
        }

        [HttpDelete]
        public IActionResult DeleteCustomer(string[] ids)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = _repo.Delete(ids);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public IActionResult GetCustomer(string id)
        {
            var customer = _repo.SelectAll(id).FirstOrDefault();
            if (customer == null)
                return NotFound();
            return Ok(customer);
        }

        [HttpGet("/api/customerGroups")]
        public IActionResult GetCustomerGroup()
        {
            var cGroups = _repo.DropDown();
            return Ok(cGroups);
        }
    }
}
