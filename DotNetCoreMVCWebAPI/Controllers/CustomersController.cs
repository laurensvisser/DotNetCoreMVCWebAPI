using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotNetCoreMVCWebAPI.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNetCoreMVCWebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CustomersController : Controller
    {
        private OrderContext db;

        public CustomersController(OrderContext context)
        {
            db = context;
        }

        #region GET
        //GET: api/Customers/
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                var customers = db.Customer
                                .Select(c => c)
                                .ToList();
                return Ok(customers);
            }
            catch (Exception e)
            {
                return BadRequest($"O oh, something ({e.Message}) went wrong");
            }
        }

        //GET: api/Customers/
        [HttpGet("{id}", Name = "GetCustomer")]
        public IActionResult Get(int id)
        {
            try
            {
                Customer customer = db.Customer
                                      .FirstOrDefault(c => c.Id == id);
                if (customer == null)
                {
                    return NotFound($"Customer with id {id} is not found...");
                }
                return Ok(customer);
            }
            catch (Exception e)
            {
                return BadRequest($"O oh, something ({e.Message}) went wrong");
            }
        }
        #endregion

        #region POST
        //POST: api/Customers/
        [HttpPost]
        public IActionResult Post([FromBody]Customer customer)
        {
            try
            {
                if (customer != null && ModelState.IsValid)
                {
                    Customer newCustomer = new Customer()
                    {
                        FirstName = customer.FirstName,
                        LastName = customer.LastName,
                        City = customer.City,
                        Country = customer.Country,
                        Phone = customer.Phone
                    };
                    db.Customer.Add(newCustomer);
                    db.SaveChanges();
                    return Created(Url.Link("GetCustomer", new { id = newCustomer.Id }), newCustomer);
                }
                else
                {
                    return NotFound($"No correct Customer info retrieved. Model is {ModelState.ValidationState}");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"O oh, something ({e.Message}) went wrong");
            }
        }
        #endregion
    }
}