using System;

namespace DVDManagement
{
    public class MovieCollection
    {
        private const int MaxMovies = 1000;
        private Movie[] movies;
        private int movieCount;
        private string[] borrowTitles;
        private int[] borrowCounts;

        public MovieCollection()
        {
            movies = new Movie[MaxMovies];
            borrowTitles = new string[MaxMovies];
            borrowCounts = new int[MaxMovies];
            movieCount = 0;
        }

        public int MovieCount => movieCount;

        private int GetHash(string title)
        {
            int hash = 0;
            foreach (char c in title)
            {
                hash += c;
            }
            return hash % MaxMovies;
        }

        public void AddMovie(Movie movie)
        {
            int index = GetHash(movie.Title);
            while (movies[index] != null && movies[index].Title != movie.Title)
            {
                index = (index + 1) % MaxMovies;
            }

            if (movies[index] != null && movies[index].Title == movie.Title)
            {
                movies[index].Copies += movie.Copies;
            }
            else if (movieCount < MaxMovies)
            {
                while (movies[index] != null)
                {
                    index = (index + 1) % MaxMovies;
                }
                movies[index] = movie;
                movieCount++;
            }
            else
            {
                throw new InvalidOperationException("Cannot add more movies.");
            }

            if (Array.IndexOf(borrowTitles, movie.Title) == -1)
            {
                for (int i = 0; i < borrowTitles.Length; i++)
                {
                    if (borrowTitles[i] == null)
                    {
                        borrowTitles[i] = movie.Title;
                        borrowCounts[i] = 0;
                        break;
                    }
                }
            }
        }

        public void RemoveMovie(string title, int copies)
        {
            int index = GetHash(title);
            while (movies[index] != null && movies[index].Title != title)
            {
                index = (index + 1) % MaxMovies;
            }

            if (movies[index] != null && movies[index].Title == title)
            {
                if (movies[index].Copies < copies)
                {
                    throw new InvalidOperationException("The number of copies to remove exceeds the number of copies in the library.");
                }

                movies[index].Copies -= copies;

                if (movies[index].Copies == 0)
                {
                    movies[index] = null;
                    movieCount--;
                    for (int i = (index + 1) % MaxMovies; movies[i] != null; i = (i + 1) % MaxMovies)
                    {
                        Movie temp = movies[i];
                        movies[i] = null;
                        movieCount--;
                        if (temp != null)
                        {
                            AddMovie(temp);
                        }
                    }
                }
            }
            else
            {
                throw new InvalidOperationException("Movie not found.");
            }
        }

        public Movie FindMovie(string title)
        {
            int index = GetHash(title);
            while (movies[index] != null && movies[index].Title != title)
            {
                index = (index + 1) % MaxMovies;
            }
            return movies[index];
        }

        public void BorrowMovie(string title)
        {
            Movie movie = FindMovie(title);
            if (movie == null || movie.Copies <= 0)
            {
                throw new InvalidOperationException("Movie not available.");
            }
            movie.Copies--;

            int index = Array.IndexOf(borrowTitles, title);
            if (index != -1)
            {
                borrowCounts[index]++;
            }
        }

        public void ReturnMovie(string title)
        {
            Movie movie = FindMovie(title);
            if (movie == null)
            {
                throw new InvalidOperationException("Movie not found.");
            }
            movie.Copies++;
        }

        public Movie[] GetMoviesInDictionaryOrder()
        {
            Movie[] sortedMovies = new Movie[movieCount];
            int count = 0;
            for (int i = 0; i < MaxMovies; i++)
            {
                if (movies[i] != null)
                {
                    sortedMovies[count++] = movies[i];
                }
            }
            Array.Sort(sortedMovies, (x, y) => x.Title.CompareTo(y.Title));
            return sortedMovies;
        }

        public (string Title, int Count)[] GetTop3Movies()
        {
            int length = borrowCounts.Length;
            (string Title, int Count)[] borrowInfo = new (string, int)[length];
            for (int i = 0; i < length; i++)
            {
                borrowInfo[i] = (borrowTitles[i], borrowCounts[i]);
            }
            Array.Sort(borrowInfo, (x, y) => y.Count.CompareTo(x.Count));
            return borrowInfo.Take(3).ToArray();
        }

        private bool IsMovieExist(string title)
        {
            int index = GetHash(title);
            while (movies[index] != null)
            {
                if (movies[index]?.Title == title)
                {
                    return true;
                }
                index = (index + 1) % MaxMovies;
            }
            return false;
        }

        public Movie GetMovie(int index)
        {
            if (index >= 0 && index < movieCount)
            {
                return movies[index];
            }
            return null;
        }
    }
}
