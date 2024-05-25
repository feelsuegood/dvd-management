using DVDManagement.Models;

namespace DVDManagement.Collections
{
    public class MovieCollection
    {
        private const int MaxMovies = 1000;
        private Movie?[] movies;  // Movie? 로 변경
        private int movieCount;

        public MovieCollection()
        {
            movies = new Movie?[MaxMovies];  // Movie? 로 변경
            movieCount = 0;
        }

        public int MovieCount => movieCount;  // MovieCount 속성 추가

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
            if (movieCount < MaxMovies && !IsMovieExist(movie.Title))
            {
                int index = GetHash(movie.Title);
                while (movies[index] != null)
                {
                    index = (index + 1) % MaxMovies;
                }
                movies[index] = movie;
                movieCount++;
            }
            else
            {
                throw new InvalidOperationException("Cannot add more movies or movie already exists.");
            }
        }

        public void RemoveMovie(string title)
        {
            int index = GetHash(title);
            while (movies[index] != null && movies[index]?.Title != title)  // null 조건부 연산자 사용
            {
                index = (index + 1) % MaxMovies;
            }

            if (movies[index] != null && movies[index]?.Title == title)  // null 조건부 연산자 사용
            {
                movies[index] = null;
                movieCount--;
                for (int i = (index + 1) % MaxMovies; movies[i] != null; i = (i + 1) % MaxMovies)
                {
                    Movie? temp = movies[i];  // Movie? 로 변경
                    movies[i] = null;
                    movieCount--;
                    if (temp != null)
                    {
                        AddMovie(temp);
                    }
                }
            }
        }

        public Movie? FindMovie(string title)
        {
            int index = GetHash(title);
            while (movies[index] != null && movies[index]?.Title != title)  // null 조건부 연산자 사용
            {
                index = (index + 1) % MaxMovies;
            }
            return movies[index];
        }

        private bool IsMovieExist(string title)
        {
            int index = GetHash(title);
            while (movies[index] != null)
            {
                if (movies[index]?.Title == title)  // null 조건부 연산자 사용
                {
                    return true;
                }
                index = (index + 1) % MaxMovies;
            }
            return false;
        }

        public Movie? GetMovie(int index)
        {
            if (index >= 0 && index < movieCount)
            {
                return movies[index];
            }
            return null;
        }
    }
}
