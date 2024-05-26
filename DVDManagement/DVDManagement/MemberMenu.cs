using System;
using System.Net;
using static System.Console;

namespace DVDManagement
{
    public static class MemberMenu
    {
        public static void MemberLogin(MemberCollection memberCollection, MovieCollection movieCollection)
        {
            while (true)
            {
                WriteLine("* 0 to go back");
                Write("Enter first name ==> ");
                string firstName = ReadLine() ?? string.Empty;
                if (firstName == "0") return;

                WriteLine("* 0 to go back");
                Write("Enter last name ==> ");
                string lastName = ReadLine() ?? string.Empty;
                if (lastName == "0") return;

                WriteLine("* 0 to go back");
                Write("Enter password ==> ");
                string password = ReadLine() ?? string.Empty;
                if (password == "0") return;

                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(password))
                {
                    WriteLine("* 0 to go back");
                    WriteLine("Invalid input. Please enter any key to retry ==> ");
                    if (ReadLine() == "0") return;
                    continue;
                }

                Member? member = memberCollection.FindMember(firstName, lastName);
                if (member != null && member.Password == password)
                {
                    MemberOnlyMenu(member, movieCollection);
                    return;
                }
                else
                {
                    WriteLine("* 0 to go back");
                    WriteLine("Member not found or invalid credentials. Please enter any key to retry ==> ");
                    if (ReadLine() == "0") return;
                    continue;
                }
            }
        }

        private static void MemberOnlyMenu(Member member, MovieCollection movieCollection)
        {
            while (true)
            {
                Clear();
                WriteLine("Member Menu");
                WriteLine("----------------------------------------------------------------");
                WriteLine("1. Browse all the movies");
                WriteLine("2. Display all the information about a movie, given the title of the movie");
                WriteLine("3. Borrow a movie DVD");
                WriteLine("4. Return a movie DVD");
                WriteLine("5. List current borrowing movies");
                WriteLine("6. Display the top 3 movies rented by the members");
                WriteLine("0. Return to main menu");
                WriteLine();
                WriteLine("Enter your choice ==> ");

                switch (ReadLine())
                {
                    case "1":
                        DisplayAllMoviesInDictionaryOrder(movieCollection);
                        break;
                    case "2":
                        DisplayMovieInfo(movieCollection);
                        break;
                    case "3":
                        BorrowMovie(member, movieCollection);
                        break;
                    case "4":
                        ReturnMovie(member, movieCollection);
                        break;
                    case "5":
                        ListBorrowedMovies(member);
                        break;
                    case "6":
                        DisplayTop3Movies(movieCollection);
                        break;
                    case "0":
                        return;
                    default:
                        WriteLine("Invalid input. Please enter from 0 to 6 ==> ");
                        break;
                }
            }
        }
        static void DisplayAllMoviesInDictionaryOrder(MovieCollection movieCollection)
        {
            while (true)
            {
                WriteLine("All Movies in Dictionary Order:");
                Movie[] sortedMovies = movieCollection.GetMoviesInDictionaryOrder();
                foreach (Movie movie in sortedMovies)
                {
                    WriteLine(movie);
                }
                WriteLine("Please enter any key to go back ==> ");
                continue;
            }

        }

        static void DisplayMovieInfo(MovieCollection movieCollection)
        {
            while (true)
            {
                Write("Enter movie title ==> ");
                string? title = ReadLine();

                if (!string.IsNullOrWhiteSpace(title))
                {
                    Movie? movie = movieCollection.FindMovie(title);
                    if (movie != null)
                    {
                        WriteLine(movie);
                    }
                    else
                    {
                        WriteLine("Movie not found.");
                    }
                }
                else
                {
                    WriteLine("* 0 to go back");
                    WriteLine("Invalid input. Please enter any key to retry ==> ");
                    if (ReadLine() == "0") return;
                    continue;
                }
                WriteLine("* 0 to go back");
                WriteLine("Please enter any key to continue ==> ");
                if (ReadLine() == "0") return;
                continue;
            }

        }


        static void BorrowMovie(Member member, MovieCollection movieCollection)
        {
            while (true)
            {
                WriteLine("* 0 to go back");
                WriteLine("Please enter movie title to borrow ==> ");
                string? title = ReadLine();
                if (!string.IsNullOrWhiteSpace(title))
                {
                    Movie? movie = movieCollection.FindMovie(title);
                    if (movie != null)
                    {
                        try
                        {
                            member.BorrowMovie(movie);
                            movieCollection.BorrowMovie(title);
                            WriteLine("Movie borrowed successfully.");
                        }
                        catch (Exception e)
                        {
                            WriteLine(e.Message);
                        }
                    }
                    else
                    {
                        WriteLine("Movie not found.");
                    }
                }
                else
                {
                    WriteLine("* 0 to go back");
                    WriteLine("Invalid movie title. Please enter any key to retry ==> ");
                    if (ReadLine() == "0") return;
                }
                WriteLine("* 0 to go back");
                WriteLine("Please enter any key to continue ==> ");
                if (ReadLine() == "0") return;
                continue;
            }

        }

        static void ReturnMovie(Member member, MovieCollection movieCollection)
        {
            while (true)
            {
                WriteLine("* 0 to go back");
                WriteLine("Please enter movie title to return ==> ");
                string? title = ReadLine();
                if (ReadLine() == "0") return;

                if (!string.IsNullOrWhiteSpace(title))
                {
                    Movie? movie = movieCollection.FindMovie(title);
                    if (movie != null)
                    {
                        member.ReturnMovie(movie);
                        movieCollection.ReturnMovie(title);
                        WriteLine("Movie returned successfully.");
                    }
                    else
                    {
                        WriteLine("Movie not found.");
                    }
                }
                else
                {
                    WriteLine("* 0 to go back");
                    WriteLine("Invalid input. Please enter any key to retry ==> ");
                    if (ReadLine() == "0") return;
                    continue;
                }
                WriteLine("* 0 to go back");
                WriteLine("Please enter any key to continue ==> ");
                if (ReadLine() == "0") return;
                continue;
            }

        }

        static void ListBorrowedMovies(Member member)
        {
            while (true)
            {
                WriteLine("Borrowed Movies:");
                foreach (Movie? movie in member.BorrowedMovies)
                {
                    if (movie != null)
                    {
                        WriteLine(movie);
                    }
                }
                WriteLine("* 0 to go back");
                WriteLine("Please enter any key to continue ==> ");
                if (ReadLine() == "0") return;
                continue;
            }

        }
        static void DisplayTop3Movies(MovieCollection movieCollection)
        {
            while (true)
            {
                WriteLine("Top 3 Borrowed Movies:");
                var topMovies = movieCollection.GetTop3Movies();
                foreach (var (Title, Count) in topMovies)
                {
                    WriteLine($"{Title} - {Count} times");
                }
                WriteLine("* 0 to go back");
                WriteLine("Please enter any key to continue ==> ");
                if (ReadLine() == "0") return;
                continue;
            }
        }
    }
}

