
namespace DVDManagement.Models
{
    public class Movie
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Classification { get; set; }
        public int Duration { get; set; }

        public Movie(string title, string genre, string classification, int duration)
        {
            Title = title;
            Genre = genre;
            Classification = classification;
            Duration = duration;
        }

        public override string ToString()
        {
            return $"{Title} - {Genre} - {Classification} - {Duration} minutes";
        }
    }
}
