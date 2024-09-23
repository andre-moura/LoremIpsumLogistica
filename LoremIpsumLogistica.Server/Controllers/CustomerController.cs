using LoremIpsumLogistica.Application.Services;
using LoremIpsumLogistica.Server.Domains.DTOs.Request;
using LoremIpsumLogistica.Server.Domains.DTOs.Response;
using Microsoft.AspNetCore.Mvc;

namespace LoremIpsumLogistica.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerController : ControllerBase
    {
        private readonly CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomerResponse>>> GetAllCustomers()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return Ok(customers);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CustomerResponse>> GetCustomerById(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
                return NotFound();

            return Ok(customer);
        }

        [HttpPost]
        public async Task<ActionResult> CreateCustomer([FromBody] CustomerRequest customerRequest)
        {
            int customerId = await _customerService.CreateCustomerAsync(customerRequest);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customerId }, customerRequest);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateCustomer(int id, [FromBody] CustomerRequest customerRequest)
        {
            await _customerService.UpdateCustomerAsync(id, customerRequest);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteCustomer(int id)
        {
            await _customerService.DeleteCustomerAsync(id);
            return NoContent();
        }
    }
}