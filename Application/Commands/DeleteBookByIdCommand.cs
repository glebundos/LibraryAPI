﻿using Core.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Commands
{
    public class DeleteBookByIdCommand : IRequest<Book>
    {
        public Guid Id { get; set; }
    }
}
