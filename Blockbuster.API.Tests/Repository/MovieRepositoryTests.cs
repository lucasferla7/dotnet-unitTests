using Blockbuster.API.Contexts;
using Blockbuster.API.Domain;
using Blockbuster.API.Repositories;
using Blockbuster.API.Tests.Utils;
using Microsoft.EntityFrameworkCore;

namespace Blockbuster.API.Tests.Repository
{
    public class MovieRepositoryTests
    {
        private readonly Mock<DbSet<Movie>> _mockDbSet;
        private readonly Mock<BlockbusterContext> _mockContext;
        private readonly MovieRepository _repository;

        public MovieRepositoryTests()
        {
            _mockDbSet = new();
            _mockContext = new(new DbContextOptions<BlockbusterContext>());

            _mockContext.Setup(p => p.Set<Movie>())
                .Returns(_mockDbSet.Object);

            _repository = new MovieRepository(_mockContext.Object);
        }

        [Fact]
        public async Task CreateNewMovie_Should_Return_NewMovie()
        {
            var (entity,
                cancellationToken) = Arrange_CreateNewMovie();

            var entityEntry = await _repository.AddAsync(entity, cancellationToken);

            entityEntry.Entity.Should().BeEquivalentTo(entity);
        }

        [Fact]
        public async Task CreateNewMovie_Should_Call_AddAsync_Once()
        {
            var (entity,
                cancellationToken) = Arrange_CreateNewMovie();

            await _repository.AddAsync(entity, cancellationToken);

            _mockDbSet.Verify(p => p.AddAsync(
                It.IsAny<Movie>(), 
                It.IsAny<CancellationToken>()));
        }

        private (
            Movie entity,
            CancellationToken cancellationToken)
            Arrange_CreateNewMovie()
        {
            var entity = Builder<Movie>.CreateNew()
                            .With(p => p.Id = 1).Build();

            var entityEntry = EntityFrameworkUtils.BuildEntityEntry(entity);

            var cancellationToken = new CancellationTokenSource().Token;

            _mockDbSet.Setup(p => p.AddAsync(
                It.IsAny<Movie>(), 
                It.IsAny<CancellationToken>()))
                .ReturnsAsync(entityEntry);

            return (entity, cancellationToken);
        }
    }
}
