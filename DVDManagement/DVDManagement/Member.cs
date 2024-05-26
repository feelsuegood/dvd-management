using System;
using System.IO;
using System.Linq;
using static System.Console;

namespace DVDManagement
{
    public class Member
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public Movie[] BorrowedMovies { get; set; }
        private int borrowedMovieCount;

        public Member(string firstName, string lastName, string phoneNumber, string password)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Password = password;
            BorrowedMovies = new Movie[5];
            borrowedMovieCount = 0;
        }

        public void BorrowMovie(Movie movie)
        {
            if (borrowedMovieCount < 5 && !IsMovieBorrowed(movie))
            {
                BorrowedMovies[borrowedMovieCount] = movie;
                borrowedMovieCount++;
            }
            else
            {
                throw new InvalidOperationException("Cannot borrow more than 5 movies or duplicate movie titles.");
            }
        }

        public void ReturnMovie(Movie movie)
        {
            for (int i = 0; i < borrowedMovieCount; i++)
            {
                if (BorrowedMovies[i].Title == movie.Title)
                {
                    for (int j = i; j < borrowedMovieCount - 1; j++)
                    {
                        BorrowedMovies[j] = BorrowedMovies[j + 1];
                    }
                    BorrowedMovies[borrowedMovieCount - 1] = null;
                    borrowedMovieCount--;
                    break;
                }
            }
        }

        private bool IsMovieBorrowed(Movie movie)
        {
            for (int i = 0; i < borrowedMovieCount; i++)
            {
                if (BorrowedMovies[i].Title == movie.Title)
                {
                    return true;
                }
            }
            return false;
        }

        public bool HasBorrowedMovie(string title)
        {
            for (int i = 0; i < borrowedMovieCount; i++)
            {
                if (BorrowedMovies[i].Title == title)
                {
                    return true;
                }
            }
            return false;
        }

        public string Serialize()
        {
            string borrowedMovies = string.Join(",", BorrowedMovies.Select(m => m?.Title ?? "null"));
            return $"{FirstName},{LastName},{PhoneNumber},{Password},{borrowedMovies}";
        }

        public static Member Deserialize(string data)
        {
            var parts = data.Split(',');
            var member = new Member(parts[0], parts[1], parts[2], parts[3]);
            for (int i = 0; i < 5; i++)
            {
                if (parts[4 + i] != "null")
                {
                    member.BorrowedMovies[i] = new Movie(parts[4 + i], "", "", 0, 1); // Placeholder for deserialized movies
                }
            }
            return member;
        }
    }
}
