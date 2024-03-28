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

        /// <summary>
        /// Get all clients
        /// </summary>
        /// <returns>List of all clients</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<GetClientResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            var clients = await _clientService.GetAllClients();
            if(clients.Any())
                return Ok(clients);

            return NoContent();
        }

        /// <summary>
        /// Get client by id
        /// </summary>
        /// <returns>Client</returns>
        [HttpGet]
        [ProducesResponseType(typeof(GetClientResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [Route("{id}", Name = "GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var client = await _clientService.GetClientById(id);
            return Ok(client);
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
