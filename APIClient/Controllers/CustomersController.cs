using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using APIClient.Models;
using Newtonsoft.Json;


namespace APIClient.Controllers
{
    [Route("")]
    [Route("[controller]")]
    public class CustomersController : Controller
    {
        private HttpClient client;

        public CustomersController()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("http://localhost:2018")
            };
        }

        #region Get list of Customers
        [Route("")]
        [Route("[action]")]
        public IActionResult Index()
        {
            string customerResult = client.GetStringAsync("api/customers").Result;
            List<Customer> customerData = JsonConvert.DeserializeObject<List<Customer>>(customerResult);

            return View(customerData);
        }
        #endregion

        #region Add Customer

        [Route("[action]")]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [Route("[action]")]
        [HttpPost]
        public IActionResult Add(Customer customer)
        {
            //Opgelet: installeer de NuGet package 'Microsoft.AspNet.WebApi.Client' om gebruik
            //te kunnen maken van .PostAsJsonAsync
            HttpResponseMessage customerResult = client
                                .PostAsJsonAsync("/api/customers", customer)
                                //of: PostAsync("/api/customers", new StringContent(JsonConvert.SerializeObject(customer), System.Text.Encoding.UTF8, "application/json"))
                                .Result;
            if (customerResult.StatusCode == HttpStatusCode.Created)
            {
                return RedirectToAction("Index");
            } else
            {
                ModelState.AddModelError("Error","Validation error!");
                return View(customer);
            }
        }
        #endregion
    }
}