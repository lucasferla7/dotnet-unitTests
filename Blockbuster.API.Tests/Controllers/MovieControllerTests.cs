using Blockbuster.API.Controllers;
using Blockbuster.API.Requests;
using Blockbuster.API.Responses;
using Blockbuster.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blockbuster.API.Tests.Controllers
{
    public class MovieControllerTests
    {
        private readonly MoviesController _controller;
        private readonly Mock<IMovieService> _mockedMovieService;

        public MovieControllerTests()
        {
            _mockedMovieService = new();
            _controller = new();
        }

        [Fact]
        public async Task CreateNewMovie_Should_Return_Created()
        {
            var (request,
                expected,
                cancellationToken) = ArrangeValidPostNewMovieTest();

            var result = await ActValidPostNewMovieTest(request, cancellationToken);

            result.Should().BeOfType<CreatedResult>();
        }

        [Fact]
        public async Task CreateNewMovie_Should_Return_NewMovie()
        {
            var (request,
                expected,
                cancellationToken) = ArrangeValidPostNewMovieTest();

            var result = await ActValidPostNewMovieTest(request, cancellationToken);

            expected.Value.Should().BeEquivalentTo(((CreatedResult)result).Value);
        }

        [Fact]
        public async Task CreateNewMovie_Should_Call_Service_Once()
        {
            var (request,
                expected,
                cancellationToken) = ArrangeValidPostNewMovieTest();

            var result = await ActValidPostNewMovieTest(request, cancellationToken);

            _mockedMovieService.Verify(p => p.CreateAsync(request, cancellationToken),
                Times.Once);
        }

        private (
            CreateMovieRequest request, 
            CreatedResult expected,
            CancellationToken cancellationToken) 
            ArrangeValidPostNewMovieTest()
        {
            var request = new CreateMovieRequest
            {
                Genre = Faker.Lorem.GetFirstWord(),
                Name = Faker.Name.FullName(),
                ReleaseDate = Faker.Identification.DateOfBirth(),
                Synopsis = Faker.Lorem.Paragraph(20)
            };

            var response = new CreatedMovieResponse
            {
                Id = Faker.RandomNumber.Next(9999),
                Genre = request.Genre,
                Name = request.Name,
                ReleaseDate = request.ReleaseDate,
                Synopsis = request.Synopsis
            };

            var cancellationToken = new CancellationTokenSource().Token;

            var expected = new CreatedResult($"{response.Id}", response);

            _mockedMovieService.Setup(p => p.CreateAsync(It.IsAny<CreateMovieRequest>(), cancellationToken))
                            .ReturnsAsync(response);

            return (
                request,
                expected, 
                cancellationToken);
        }

        private async Task<IActionResult> ActValidPostNewMovieTest(
            CreateMovieRequest request,
            CancellationToken cancellationToken) =>
            await _controller.Post(request, cancellationToken, _mockedMovieService.Object);
    }
}
