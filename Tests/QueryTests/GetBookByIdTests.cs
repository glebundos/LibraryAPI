using Application.Exceptions;
using Application.Handlers.QueriesHandlers;
using Application.Queries;
using Core.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Mocks;
using Xunit;

namespace Tests.QueryTests
{
    public class GetBookByIdTests
    {
        private readonly Mock<IBookRepository> _mockRepo;

        public GetBookByIdTests()
        {
            _mockRepo = MockBookRepository.GetBookRepository();
        }

        [Theory]
        [InlineData("11118b77-f026-4e76-bbcb-9a17d7cf1f09")]
        [InlineData("22226d22-7e0d-407f-8bbb-f8c6b5c24454")]
        [InlineData("f1d08c7e-e503-4845-9384-ad4911e8ed7a")]
        public async Task GetBookById_ReturnsOK(Guid id)
        {
            ///
            var handler = new GetBookByIdHandler(_mockRepo.Object);

            var book = await _mockRepo.Object.GetByIdAsync(id);
            ///
            var response = await handler.Handle(new GetBookByIdQuery(id), CancellationToken.None);

            ///
            Assert.Equal(book, response);
        }

        [Theory]
        [InlineData("00000000-f026-4e76-bbcb-9a17d7cf1f09")]
        [InlineData("99999999-7e0d-407f-8bbb-f8c6b5c24454")]
        [InlineData("FFFFFFFF-e503-4845-9384-ad4911e8ed7a")]
        public async Task GetBookById_ThrowsWrongId(Guid id)
        {
            ///
            var handler = new GetBookByIdHandler(_mockRepo.Object);


            ///
            ///
            await Assert.ThrowsAsync<BookException>(async () => await handler.Handle(new GetBookByIdQuery(id), CancellationToken.None));
        }

    }
}
