using API.Controllers.Base;
using Application.Commands;
using Application.Queries;
using Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Helpers;

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

        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Book>> Get(Guid id)
        {
            return await QueryAsync(new GetBookByIdQuery(id));
        }

        [Authorize]
        [HttpGet("{isbn}")]
        public async Task<ActionResult<Book>> Get(string isbn)
        {
            return await QueryAsync(new GetBookByISBNQuery(isbn));
        }

        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Book>> Create([FromBody] CreateBookCommand command)
        {
            return await CommandAsync(command);
        }

        [Authorize]
        [HttpDelete]
        public async Task<ActionResult<Book>> Delete([FromBody] DeleteBookByIdCommand command)
        {
            return await CommandAsync(command);
        }

        [Authorize]
        [HttpPut]
        public async Task<ActionResult<Book>> Update([FromBody] UpdateBookCommand command)
        {
            return await CommandAsync(command);
        }
    }
}
