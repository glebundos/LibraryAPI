using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetBookByIdQuery : IRequest<Book>
    {
        public Guid Id { get; set; }

        public GetBookByIdQuery(Guid id) => Id = id;
    }
}
