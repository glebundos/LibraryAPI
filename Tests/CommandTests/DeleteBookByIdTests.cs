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
    public class DeleteBookByIdTests
    {
        private readonly Mock<IBookRepository> _mockRepo;

        public DeleteBookByIdTests()
        {
            _mockRepo = MockBookRepository.GetBookRepository();
        }

        [Fact]
        public async Task DeleteBookById_ReturnsOK()
        {
            ///
            var handler = new DeleteBookByIdHandler(_mockRepo.Object);

            var bookToBeDeleted = await _mockRepo.Object.GetByIdAsync(Guid.Parse("11118b77-f026-4e76-bbcb-9a17d7cf1f09"));
            ///
            var response = await handler.Handle(new DeleteBookByIdCommand { Id = Guid.Parse("11118b77-f026-4e76-bbcb-9a17d7cf1f09") }, CancellationToken.None);

            var books = await _mockRepo.Object.GetAllAsync();
            ///
            Assert.Equal(2, books.Count);

            Assert.Equivalent(bookToBeDeleted, response);
        }

        [Theory]
        [InlineData("99999999-9999-9999-9999-999999999999")]
        [InlineData("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF")]
        [InlineData("22228b77-f026-4e76-bbcb-9a17d7cf1f09")]
        public async Task DeleteBookById_ThrowsWrongId(Guid id)
        {
            ///
            var handler = new DeleteBookByIdHandler(_mockRepo.Object);

            ///
            ///
            await Assert.ThrowsAsync<BookException>(async () => await handler.Handle(new DeleteBookByIdCommand { Id = id }, CancellationToken.None));
        }
    }
}
