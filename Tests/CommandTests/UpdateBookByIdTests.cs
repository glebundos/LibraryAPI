using Application.Commands;
using Application.Exceptions;
using Application.Handlers.CommandHandlers;
using Core.Entities;
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
    public class UpdateBookByIdTests
    {
        private readonly Mock<IBookRepository> _mockRepo;

        public UpdateBookByIdTests()
        {
            _mockRepo = MockBookRepository.GetBookRepository();
        }

        [Fact]
        public async Task UpdateBookById_ReturnsOk()
        {
            ///
            var handler = new UpdateBookByIdHandler(_mockRepo.Object);

            var command = new UpdateBookByIdCommand
            {
                Id = Guid.Parse("11118b77-f026-4e76-bbcb-9a17d7cf1f09"),
                ISBN = "1010101010",
                Name = "Updated Book",
                Genre = "Romantic Novel",
                Description = "The Way Greater Description",
                Author = "Leo Tolstoy",
                BorrowTime = DateTime.Now.AddDays(1),
                ReturnTime = DateTime.Now.AddDays(2),
            };

            var bookToBeUpdated = await _mockRepo.Object.GetByIdAsync(Guid.Parse("11118b77-f026-4e76-bbcb-9a17d7cf1f09"));
            ///

            var response = await handler.Handle(command, CancellationToken.None);

            ///
            Assert.Equal(bookToBeUpdated, response);
        }

        [Theory]
        [InlineData("99999999-9999-9999-9999-999999999999")]
        [InlineData("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF")]
        [InlineData("22228b77-f026-4e76-bbcb-9a17d7cf1f09")]
        public async Task UpdateBookById_ThrowsWrongId(Guid id)
        {
            ///
            var handler = new UpdateBookByIdHandler(_mockRepo.Object);

            var command = new UpdateBookByIdCommand
            {
                Id = id,
                ISBN = "1010101010",
                Name = "Updated Book",
                Genre = "Romantic Novel",
                Description = "The Way Greater Description",
                Author = "Leo Tolstoy",
                BorrowTime = DateTime.Now.AddDays(1),
                ReturnTime = DateTime.Now.AddDays(2),
            };

            ///
            ///
            await Assert.ThrowsAsync<BookException>(async () => await handler.Handle(command, CancellationToken.None));
        }

        [Theory]
        [InlineData("1934770109384719")]
        [InlineData("isbn-194748842")]
        [InlineData("cool-book")]
        public async Task UpdateBookById_ThrowsInvalidISBN(string isbn)
        {
            ///
            var handler = new UpdateBookByIdHandler(_mockRepo.Object);

            var command = new UpdateBookByIdCommand
            {
                Id = Guid.Parse("11118b77-f026-4e76-bbcb-9a17d7cf1f09"),
                ISBN = isbn,
                Name = "Updated Book",
                Genre = "Romantic Novel",
                Description = "The Way Greater Description",
                Author = "Leo Tolstoy",
                BorrowTime = DateTime.Now.AddDays(1),
                ReturnTime = DateTime.Now.AddDays(2),
            };

            ///
            ///
            await Assert.ThrowsAsync<BookException>(async () => await handler.Handle(command, CancellationToken.None));
        }
    }
}
