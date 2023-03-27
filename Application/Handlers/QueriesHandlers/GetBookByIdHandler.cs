using Application.Exceptions;
using Application.Queries;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.QueriesHandlers
{
    public class GetBookByIdHandler : IRequestHandler<GetBookByIdQuery, Book>
    {
        private readonly IBookRepository _bookRepo;

        public GetBookByIdHandler(IBookRepository bookRepo) => _bookRepo = bookRepo;

        public async Task<Book> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book =  await _bookRepo.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new BookException("No book with such id");
            }

            return book;
        }
    }
}
