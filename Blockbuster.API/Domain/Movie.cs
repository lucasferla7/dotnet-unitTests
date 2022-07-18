namespace Blockbuster.API.Domain
{
    public class Movie : Entity
    {
        public string Name { get; set; } = default!;
        public string Synopsis { get; set; } = default!;
        public string Genre { get; set; } = default!;
        public DateTime ReleaseDate { get; set; }
    }
}
