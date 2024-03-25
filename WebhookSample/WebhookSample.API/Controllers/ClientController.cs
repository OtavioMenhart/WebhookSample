using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using WebhookSample.Domain.Interfaces.Services;
using WebhookSample.Domain.Requests.Clients;
using WebhookSample.Domain.Responses.Clients;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebhookSample.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/<ClientController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ClientController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        /// <summary>
        /// Create a new client
        /// </summary>
        /// <remarks>
        /// Example: 
        /// {
        ///   "name": "Client Name",
        ///   "birthDate": "yyyy-MM-dd",
        ///   "email": "clientEmail@gmail.com"
        /// }
        /// </remarks>
        /// <param name="newclient">New client information</param>
        /// <returns>New client added</returns>
        /// <response code="201">Created</response>
        /// <response code="422">Unprocessable Entity</response>
        /// <response code="500">Internal Server Error</response>
        [HttpPost]
        [ProducesResponseType(typeof(ClientCreatedResponse), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
        public async Task<IActionResult> Post([FromBody] CreateClientRequest newclient)
        {
            var clientAdded = await _clientService.CreateClient(newclient);
            return StatusCode(StatusCodes.Status201Created, clientAdded);
        }

        // PUT api/<ClientController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ClientController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
