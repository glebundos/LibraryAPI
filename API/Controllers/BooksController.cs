using API.Controllers.Base;
using Application.Commands;
using Application.Queries;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BooksController : ApiControllerBase
    {
        public BooksController(IMediator mediator) : base(mediator) { }

        [HttpGet]
        public async Task<ActionResult<List<Book>>> Get()
        {
            return await QueryAsync(new GetAllBooksQuery());
        }

        [HttpPost]
        public async Task<ActionResult<Book>> Create([FromBody] CreateBookCommand command)
        {
            return await CommandAsync(command);
        }
    }
}
