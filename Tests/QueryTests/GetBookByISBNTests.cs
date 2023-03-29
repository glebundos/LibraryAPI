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
    public class GetBookByISBNTests
    {
        private readonly Mock<IBookRepository> _mockRepo;

        public GetBookByISBNTests()
        {
            _mockRepo = MockBookRepository.GetBookRepository();
        }

        [Theory]
        [InlineData("1123455531")]
        [InlineData("2233999999")]
        [InlineData("3333752418865")]
        public async Task GetBookByISBN_ReturnsOK(string isbn)
        {
            ///
            var handler = new GetBookByISBNHandler(_mockRepo.Object);

            var book = await _mockRepo.Object.GetByISBNAsync(isbn);
            ///
            var response = await handler.Handle(new GetBookByISBNQuery(isbn), CancellationToken.None);

            ///
            Assert.Equal(book, response);
        }

        [Theory]
        [InlineData("0023455531")]
        [InlineData("0033999999")]
        [InlineData("0033752418865")]
        public async Task GetBookById_ThrowsWrongISBN(string isbn)
        {
            ///
            var handler = new GetBookByISBNHandler(_mockRepo.Object);


            ///
            ///
            await Assert.ThrowsAsync<BookException>(async () => await handler.Handle(new GetBookByISBNQuery(isbn), CancellationToken.None));
        }

        [Theory]
        [InlineData("00234555319392")]
        [InlineData("isbn-0033999999")]
        [InlineData("John Doe")]
        public async Task GetBookById_ThrowsInvalidISBN(string isbn)
        {
            ///
            var handler = new GetBookByISBNHandler(_mockRepo.Object);


            ///
            ///
            await Assert.ThrowsAsync<BookException>(async () => await handler.Handle(new GetBookByISBNQuery(isbn), CancellationToken.None));
        }
    }
}
