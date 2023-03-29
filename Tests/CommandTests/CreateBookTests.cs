using Application.Commands;
using Application.Exceptions;
using Application.Handlers.CommandHandlers;
using Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Mocks;
using Xunit;

namespace Tests.CommandTests
{
    public class CreateBookTests
    {
        private readonly Mock<IBookRepository> _mockRepo;

        public CreateBookTests()
        {
            _mockRepo = MockBookRepository.GetBookRepository();
        }

        [Fact]
        public async Task CreateBook_ReturnsOk()
        {
            ///
            var handler = new CreateBookHandler(_mockRepo.Object);

            var command = new CreateBookCommand
            {
                ISBN = "9993455531",
                Name = "New Book",
                Genre = "Fantasy",
                Description = "Description for the new book",
                Author = "Spider-Man",
                BorrowTime = DateTime.Now.AddHours(4),
                ReturnTime = DateTime.Now.AddDays(2),
            };

            ///
            var response = await handler.Handle(command, CancellationToken.None);

            var books = await _mockRepo.Object.GetAllAsync();

            ///
            Assert.Equal(4, books.Count);

            Assert.Equivalent(response, books[3]);
        }

        [Fact]
        public async Task CreateBook_ThrowsBookWasNull()
        {
            ///
            var handler = new CreateBookHandler(_mockRepo.Object);

            ///
            ///
            await Assert.ThrowsAsync<BookException>(async () => await handler.Handle(null!, CancellationToken.None));
        }

        [Fact]
        public async Task CreateBook_ThrowsExsistingISBN()
        {
            ///
            var handler = new CreateBookHandler(_mockRepo.Object);

            var command = new CreateBookCommand
            {
                ISBN = "1123455531",
                Name = "New Book",
                Genre = "Fantasy",
                Description = "Description for the new book",
                Author = "Spider-Man",
                BorrowTime = DateTime.Now.AddHours(4),
                ReturnTime = DateTime.Now.AddDays(2),
            };

            ///
            ///
            await Assert.ThrowsAsync<BookException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task CreateBook_ThrowsInvalidTime()
        {
            ///
            var handler = new CreateBookHandler(_mockRepo.Object);

            var command = new CreateBookCommand
            {
                ISBN = "9993455531",
                Name = "New Book",
                Genre = "Fantasy",
                Description = "Description for the new book",
                Author = "Spider-Man",
                BorrowTime = DateTime.Now.AddDays(4),
                ReturnTime = DateTime.Now.AddDays(3),
            };

            ///
            ///
            await Assert.ThrowsAsync<BookException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
