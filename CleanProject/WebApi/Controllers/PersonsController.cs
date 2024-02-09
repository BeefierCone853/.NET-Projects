using Application.DTOs.Persons;
using Application.Features.Persons.Commands.CreatePerson;
using Application.Features.Persons.Commands.DeletePerson;
using Application.Features.Persons.Commands.UpdatePerson;
using Application.Features.Persons.Queries.GetPersonDetail;
using Application.Features.Persons.Queries.GetPersonList;
using Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonsController(IMediator mediator) : ControllerBase
    {
        // GET: api/<PersonsController>
        [HttpGet]
        public async Task<ActionResult<List<PersonDto>>> Get()
        {
            var persons = await mediator.Send(new GetPersonListRequest());
            return Ok(persons);
        }

        // GET api/<PersonsController>/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PersonDto>> Get(int id)
        {
            var person = await mediator.Send(new GetPersonDetailRequest { Id = id });
            return Ok(person);
        }

        // POST api/<PersonsController>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public async Task<ActionResult<Result>> Post([FromBody] CreatePersonDto person)
        {
            var command = new CreatePersonCommand(person);
            var response = await mediator.Send(command);
            return Ok(response);
        }

        // PUT api/<PersonsController>/5
        [HttpPut("{id:int}")]
        public async Task<ActionResult> Put(int id, [FromBody] UpdatePersonDto person)
        {
            var command = new UpdatePersonCommand(person);
            await mediator.Send(command);
            return NoContent();
        }

        // DELETE api/<PersonsController>/5
        [HttpDelete("{id:int}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<Result>> Delete(int id)
        {
            var command = new DeletePersonCommand(id);
            var response = await mediator.Send(command);
            if (response.Error.Code.Equals("Error.NotFound"))
            {
                return NotFound(response);
            }

            return Ok(response);
        }
    }
}