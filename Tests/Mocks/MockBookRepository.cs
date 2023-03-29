using Core.Entities;
using Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Mocks
{
    public class MockBookRepository
    {
        public static Mock<IBookRepository> GetBookRepository()
        {
            var _books = new List<Book>
            {
                new Book
                {
                    Id = Guid.Parse("11118b77-f026-4e76-bbcb-9a17d7cf1f09"),
                    ISBN = "1123455531",
                    Name = "Book 1",
                    Genre = "Novel",
                    Description = "Description 1 for the book 1",
                    Author = "Leo Tolstoy",
                    BorrowTime = DateTime.Now.AddDays(1),
                    ReturnTime = DateTime.Now.AddDays(2),
                },

                new Book
                {
                    Id = Guid.Parse("22226d22-7e0d-407f-8bbb-f8c6b5c24454"),
                    ISBN = "2233999999",
                    Name = "Book 2",
                    Genre = "Detective",
                    Description = "Description 2 for the book 2",
                    Author = "Erich Maria Remarque",
                    BorrowTime = DateTime.Now,
                    ReturnTime = DateTime.Now.AddSeconds(100),
                },

                new Book
                {
                    Id = Guid.Parse("f1d08c7e-e503-4845-9384-ad4911e8ed7a"),
                    ISBN = "3333752418865",
                    Name = "Book 3",
                    Genre = "Horror",
                    Description = "Description 3 for the book 3",
                    Author = "Nikolai Gogol",
                    BorrowTime = DateTime.Now.AddDays(3),
                    ReturnTime = DateTime.Now.AddDays(7),
                }
            };

            var mockRepo = new Mock<IBookRepository>();

            mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(_books);

            mockRepo.Setup(r => r.GetByIdAsync(It.IsAny<Guid>())).ReturnsAsync((Guid id) => _books.FirstOrDefault(x => x.Id == id));

            mockRepo.Setup(r => r.GetByISBNAsync(It.IsAny<string>())).ReturnsAsync((string isbn) => _books.FirstOrDefault(x => x.ISBN == isbn));

            mockRepo.Setup(r => r.AddAsync(It.IsAny<Book>())).ReturnsAsync(
                (Book book) =>
                {
                    book.Id = Guid.NewGuid();
                    _books.Add(book);
                    return book;
                });

            mockRepo.Setup(r => r.UpdateAsync(It.IsAny<Book>())).ReturnsAsync(
                (Book supplier) =>
                {
                    var index = _books.FindIndex(f => f.Id == supplier.Id);

                    if (index == -1)
                    {
                        return null;
                    }

                    _books[index] = supplier;
                    return supplier;
                });

            mockRepo.Setup(r => r.DeleteAsync(It.IsAny<Book>())).Returns(
                (Book book) =>
                {
                    _books.Remove(book);
                    return Task.FromResult(1);
                });

            return mockRepo;
        }
    }
}
