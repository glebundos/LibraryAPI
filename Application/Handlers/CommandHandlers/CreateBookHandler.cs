using Application.Commands;
using Application.Exceptions;
using Application.Mappers;
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
    public class CreateBookHandler : IRequestHandler<CreateBookCommand, Book>
    {
        private readonly IBookRepository _bookRepo;

        public CreateBookHandler(IBookRepository bookRepo) => _bookRepo = bookRepo;

        public async Task<Book> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var bookEntity = BookMapper.Mapper.Map<Book>(request);
            if (bookEntity == null)
            {
                throw new BookException(message: "Book was null");
            }

            if (bookEntity.BorrowTime >= bookEntity.ReturnTime)
            {
                throw new BookException(message: "Borrow time cannot be greater or equal return time");
            }

            return await _bookRepo.AddAsync(bookEntity);
        }
    }
}
