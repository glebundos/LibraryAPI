using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Book
    {
        public Guid Id { get; set; }

        [RegularExpression("/^(?=(?:\\D*\\d){10}(?:(?:\\D*\\d){3})?$)[\\d-]+$/gm")]
        public string ISBN { get; set; }

        public string Name { get; set; }

        public string Genre { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public DateTime BorrowTime { get; set; }

        public DateTime ReturnTime { get; set; }
    }
}
