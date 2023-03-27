using Application.Commands;
using Application.Exceptions;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers
{
    public class DeleteBookByIdHandler : IRequestHandler<DeleteBookByIdCommand, Book>
    {
        private readonly IBookRepository _bookRepo;

        public DeleteBookByIdHandler(IBookRepository bookRepository) => _bookRepo = bookRepository;

        public async Task<Book> Handle(DeleteBookByIdCommand request, CancellationToken cancellationToken)
        {
            var book = await _bookRepo.GetByIdAsync(request.Id);
            if (book == null)
            {
                throw new BookException("No book with such Id");
            }

            await _bookRepo.DeleteAsync(book);
            return book;
        }
    }
}
