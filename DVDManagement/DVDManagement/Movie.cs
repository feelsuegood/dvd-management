using System;
using System.IO;
using System.Linq;
using static System.Console;

namespace DVDManagement
{
    public class Movie
    {
        public string Title { get; set; }
        public string Genre { get; set; }
        public string Classification { get; set; }
        public int Duration { get; set; }
        public int Copies { get; set; }

        public Movie(string title, string genre, string classification, int duration, int copies)
        {
            Title = title;
            Genre = genre;
            Classification = classification;
            Duration = duration;
            Copies = copies;
        }

        public override string ToString()
        {
            return $"{Title},{Genre},{Classification},{Duration},{Copies}";
        }

        public static Movie FromString(string data)
        {
            var parts = data.Split(',');
            return new Movie(parts[0], parts[1], parts[2], int.Parse(parts[3]), int.Parse(parts[4]));
        }
    }
}
