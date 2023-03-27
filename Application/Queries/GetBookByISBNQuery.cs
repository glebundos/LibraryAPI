using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Queries
{
    public class GetBookByISBNQuery : IRequest<Book>
    {
        public string ISBN { get; set; }

        public GetBookByISBNQuery(string iSBN) => ISBN = iSBN;
    }
}