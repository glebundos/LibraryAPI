using Application.Commands;
using Application.Exceptions;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Handlers.CommandHandlers
{
    public class UpdateBookByIdHandler : IRequestHandler<UpdateBookByIdCommand, Book>
    {
        private readonly IBookRepository _bookRepo;

        public UpdateBookByIdHandler(IBookRepository bookRepo) => _bookRepo = bookRepo;

        public async Task<Book> Handle(UpdateBookByIdCommand request, CancellationToken cancellationToken)
        {
            var oldBook = await _bookRepo.GetByIdAsync(request.Id);

            if (oldBook == null) 
            {
                throw new BookException("No book with such id");
            }

            var isbn = request.ISBN;
            var isIsbn = Regex.IsMatch(isbn, @"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$");

            if (!isIsbn)
            {
                throw new BookException("Invalid ISBN");
            }

            oldBook.ISBN = isbn;
            oldBook.Name = request.Name;
            oldBook.Genre = request.Genre;
            oldBook.Description = request.Description;
            oldBook.Author = request.Author;
            oldBook.BorrowTime = request.BorrowTime;
            oldBook.ReturnTime = request.ReturnTime;

            var updatedBook = await _bookRepo.UpdateAsync(oldBook);
            return updatedBook;
        }
    }
}
