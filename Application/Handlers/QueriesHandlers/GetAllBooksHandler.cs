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
    public class GetAllBooksHandler : IRequestHandler<GetAllBooksQuery, List<Book>>
    {
        private readonly IBookRepository _bookRepo;

        public GetAllBooksHandler(IBookRepository bookRepo) => _bookRepo = bookRepo;

        public async Task<List<Book>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {
            return (List<Book>)await _bookRepo.GetAllAsync();
        }
    }
}
