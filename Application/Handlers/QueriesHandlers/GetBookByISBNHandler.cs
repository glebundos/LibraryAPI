using Application.Exceptions;
using Application.Queries;
using Core.Entities;
using Core.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Application.Handlers.QueriesHandlers
{
    public class GetBookByISBNHandler : IRequestHandler<GetBookByISBNQuery, Book>
    {
        private readonly IBookRepository _bookRepo;

        public GetBookByISBNHandler(IBookRepository bookRepo) => _bookRepo = bookRepo;

        public async Task<Book> Handle(GetBookByISBNQuery request, CancellationToken cancellationToken)
        {
            var isbn = request.ISBN;
            var isIsbn = Regex.IsMatch(isbn, @"^(?=(?:\D*\d){10}(?:(?:\D*\d){3})?$)[\d-]+$");

            if (!isIsbn) 
            {
                throw new BookException("Invalid ISBN");
            }

            var book = await _bookRepo.GetByISBNAsync(request.ISBN);
            if (book == null)
            {
                throw new BookException("No book with such ISBN");
            }

            return book;
        }
    }
}
