using Blockbuster.API.Domain;
using Blockbuster.API.Repositories;
using Blockbuster.API.Requests;
using Blockbuster.API.Responses;
using Blockbuster.API.Services;
using Blockbuster.API.Tests.Utils;

namespace Blockbuster.API.Tests.Services
{
    public class MovieServiceTests
    {
        private readonly Mock<IMovieRepository> _mockMovieRepository = new();
        private readonly MovieService _movieService;

        public MovieServiceTests()
        {
            _movieService = new(_mockMovieRepository.Object);
        }

        [Fact]
        public async Task CreateNewMovie_Should_Return_CreatedMovieResponse()
        {
            var (
                request,
                expected,
                cancellationToken) = ArrangeValidPostNewMovieTest();

            var response = await ActValidPostNewMovieTest(
                request, 
                cancellationToken);

            response.Should().BeOfType<CreatedMovieResponse>();
        }

        [Fact]
        public async Task CreateNewMovie_Should_Return_NewMovie()
        {
            var (
                request,
                expected,
                cancellationToken) = ArrangeValidPostNewMovieTest();

            var response = await ActValidPostNewMovieTest(
                request,
                cancellationToken);

            expected.Should().BeEquivalentTo(response);
        }

        [Fact]
        public async Task CreateNewMovie_Should_Call_AddAsync_Once()
        {
            var (
                request,
                expected,
                cancellationToken) = ArrangeValidPostNewMovieTest();

            var response = await ActValidPostNewMovieTest(
                request,
                cancellationToken);

            _mockMovieRepository.Verify(p => p.AddAsync(
                It.IsAny<Movie>(), 
                It.IsAny<CancellationToken>()), 
                Times.Once);
        }

        [Fact]
        public async Task CreateNewMovie_Should_Call_SaveChanges_Once()
        {
            var (
                request,
                expected,
                cancellationToken) = ArrangeValidPostNewMovieTest();

            var response = await ActValidPostNewMovieTest(
                request,
                cancellationToken);

            _mockMovieRepository.Verify(p => p.SaveChangesAsync(
                It.IsAny<CancellationToken>()), 
                Times.Once);
        }

        private async Task<CreatedMovieResponse> ActValidPostNewMovieTest(
            CreateMovieRequest request, 
            CancellationToken cancellationToken) => 
            await _movieService.CreateAsync(request, cancellationToken);

        private (
            CreateMovieRequest request,
            CreatedMovieResponse expected,
            CancellationToken cancellationToken)
            ArrangeValidPostNewMovieTest()
        {
            var entity = Builder<Movie>.CreateNew()
                .With(p => p.Id = 1).Build();

            CreateMovieRequest request = new()
            {
                Genre = entity.Genre,
                Name = entity.Name,
                ReleaseDate = entity.ReleaseDate,
                Synopsis = entity.Synopsis
            };

            CreatedMovieResponse expected = new()
            {
                Id = entity.Id,
                Genre = entity.Genre,
                Name = entity.Name,
                ReleaseDate = entity.ReleaseDate,
                Synopsis = entity.Synopsis
            };

            var entityEntry = EntityFrameworkUtils.BuildEntityEntry(entity);

            var cancellationToken = new CancellationTokenSource().Token;

            _mockMovieRepository.Setup(p => p.AddAsync(
                It.IsAny<Movie>(), 
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(entityEntry);

            return (
                request,
                expected,
                cancellationToken);
        }
    }
}
