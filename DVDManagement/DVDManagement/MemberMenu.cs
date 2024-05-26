using System;
using static System.Console;

namespace DVDManagement
{
    public static class MemberMenu
    {
        public static void MemberLogin(MemberCollection memberCollection, MovieCollection movieCollection)
        {
            while (true)
            {
                Write("Enter first name \n* 0 to go back \n==> ");
                string firstName = ReadLine() ?? string.Empty;
                if (firstName == "0") return;

                Write("Enter last name \n* 0 to go back \n==> ");
                string lastName = ReadLine() ?? string.Empty;
                if (lastName == "0") return;

                Write("Enter password \n* 0 to go back \n==> ");
                string password = ReadLine() ?? string.Empty;
                if (password == "0") return;

                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(password))
                {
                    WriteLine("Invalid input. Please enter any key to retry or enter 0 to go back to the main menu.");
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
                    WriteLine("Member not found or invalid credentials. Please enter any key to retry or enter 0 to go back to the main menu.");
                    if (ReadLine() == "0") return;
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
                WriteLine("0. Return to main menu");
                WriteLine();
                Write("Enter your choice \n==> ");

                switch (ReadLine())
                {
                    case "1":
                        DisplayAllMovies(movieCollection);
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
                    case "0":
                        return;
                    default:
                        WriteLine("Invalid input. \nPlease enter from 0 to 5.");
                        ReadLine();
                        break;
                }
            }
        }
        static void DisplayAllMovies(MovieCollection movieCollection)
        {
            WriteLine("All Movies:");
            Movie[] allMovies = movieCollection.GetAllMovies();
            foreach (var movie in allMovies)
            {
                WriteLine(movie);
            }
            WriteLine("Please enter any key to go back to the menu.");
            ReadLine();
        }


        static void DisplayMovieInfo(MovieCollection movieCollection)
        {
            Write("Enter movie title \n==> ");
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
                WriteLine("Invalid movie title.");
            }
            WriteLine("Please enter any key to go back to the menu.");
            ReadLine();
        }


        static void BorrowMovie(Member member, MovieCollection movieCollection)
        {
            Write("Enter movie title to borrow \n==> ");
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
                WriteLine("Invalid movie title.");
            }
            WriteLine("Please enter any key to go back to the menu.");
            ReadLine();
        }

        static void ReturnMovie(Member member, MovieCollection movieCollection)
        {
            Write("Enter movie title to return \n==> ");
            string? title = ReadLine();

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
                WriteLine("Invalid movie title.");
            }
            WriteLine("Please enter any key to go back to the menu.");
            ReadLine();
        }

        static void ListBorrowedMovies(Member member)
        {
            WriteLine("Borrowed Movies:");
            foreach (Movie? movie in member.BorrowedMovies)
            {
                if (movie != null)
                {
                    WriteLine(movie);
                }
            }
            WriteLine("Please enter any key to go back to the menu.");
            ReadLine();
        }
    }
}

