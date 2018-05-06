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
                var customers =
                        db.Customer
                        .Select(c => new
                        {
                            Klantnummer = c.Id,
                            Naam = $"{c.FirstName} {c.LastName}",
                            Adres = $"{c.City}, ({c.Country})",
                            Bestellingen = c.Order.Select(o => new
                            {
                                Nummer = o.OrderNumber,
                                Datum = ((DateTime)o.OrderDate).ToString("D"),
                                Items = o.OrderItem.Select(oi => new
                                {
                                    Produkt = oi.Product.ProductName,
                                    Prijs = oi.Product.UnitPrice,
                                    Beschikbaar = oi.Product.IsDiscontinued ? "Niet meer leverbaar" : "Beschikbaar"
                                }).ToList()
                            }).ToList()
                        }).ToList();

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

    }
}