using System;
using static System.Console;

namespace DVDManagement
{
    class Program
    {
        static MemberCollection memberCollection = new MemberCollection();
        static MovieCollection movieCollection = new MovieCollection();

        static void Main(string[] args)
        {
            while (true)
            {
                Clear();
                WriteLine("================================================================");
                WriteLine("COMMUNITY LIBRARY MOVIE DVD MANAGEMENT SYSTEM");
                WriteLine("================================================================");
                WriteLine();
                WriteLine("Main Menu");
                WriteLine("----------------------------------------------------------------");
                WriteLine("Select from the following:");
                WriteLine();
                WriteLine("1. Staff");
                WriteLine("2. Member");
                WriteLine("0. End the program");
                WriteLine();
                Write("Enter your choice ==> ");

                switch (ReadLine())
                {
                    case "1":
                        StaffLogin();
                        break;
                    case "2":
                        MemberLogin();
                        break;
                    case "0":
                        return;
                    default:
                        WriteLine("Invalid choice, Please enter 1, 2 or 0.");
                        ReadLine();
                        break;
                }
            }
        }

        static void StaffLogin()
        {
            Write("Enter staff username ==> ");
            string username = ReadLine() ?? string.Empty;
            Write("Enter staff password ==> ");
            string password = ReadLine() ?? string.Empty;

            if (username == "staff" && password == "today123")
            {
                StaffMenu();
            }
            else
            {
                WriteLine("Invalid username or password");
                WriteLine("You go back to main menu by pressing any key.");
                ReadLine();
            }
        }

        static void MemberLogin()
        {
            Write("Enter first name ==> ");
            string firstName = ReadLine() ?? string.Empty;
            Write("Enter last name ==> ");
            string lastName = ReadLine() ?? string.Empty;
            Write("Enter password ==> ");
            string password = ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(password))
            {
                WriteLine("Invalid input. You go back to main menu by pressing any key.");
                ReadLine();
                return;
            }

            Member? member = memberCollection.FindMember(firstName, lastName);
            if (member != null && member.Password == password)
            {
                MemberMenu(member);
            }
            else
            {
                WriteLine("Member not found or invalid credentials. You go back to main menu by pressing any key.");
                ReadLine();
            }
        }

        static void StaffMenu()
        {
            while (true)
            {
                Clear();
                WriteLine("Staff Menu");
                WriteLine("----------------------------------------------------------------");
                WriteLine("1. Add DVDs to system");
                WriteLine("2. Remove DVDs from system");
                WriteLine("3. Register a new member to system");
                WriteLine("4. Remove a registered member from system");
                WriteLine("5. Find a member contact phone number, given the member's name");
                WriteLine("6. Find members who are currently renting a particular movie");
                WriteLine("7. Display all movies in dictionary order");
                WriteLine("8. Display the top 3 movies borrowed most frequently");
                WriteLine("0. Return to main menu");

                switch (ReadLine())
                {
                    case "1":
                        AddMovie();
                        break;
                    case "2":
                        RemoveMovie();
                        break;
                    case "3":
                        RegisterMember();
                        break;
                    case "4":
                        RemoveMember();
                        break;
                    case "5":
                        FindMemberPhoneNumber();
                        break;
                    case "6":
                        ListMembersBorrowingMovie();
                        break;
                    case "7":
                        DisplayAllMoviesInDictionaryOrder();
                        break;
                    case "8":
                        DisplayTop3Movies();
                        break;
                    case "0":
                        return;
                    default:
                        WriteLine("Invalid input, please try again.");
                        ReadLine();
                        break;
                }
            }
        }

        static void MemberMenu(Member member)
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
                Write("Enter your choice ==> ");

                switch (ReadLine())
                {
                    case "1":
                        DisplayAllMovies();
                        break;
                    case "2":
                        DisplayMovieInfo();
                        break;
                    case "3":
                        BorrowMovie(member);
                        break;
                    case "4":
                        ReturnMovie(member);
                        break;
                    case "5":
                        ListBorrowedMovies(member);
                        break;
                    case "0":
                        return;
                    default:
                        WriteLine("Invalid input, please enter from 0 to 5.");
                        ReadLine();
                        break;
                }
            }
        }

        static void AddMovie()
        {
            string? title = null;
            string? genre = null;
            string? classification = null;
            int duration = -1;
            int copies = -1;

            while (string.IsNullOrWhiteSpace(title))
            {
                Write("Please enter movie title ==> ");
                title = ReadLine();
                if (string.IsNullOrWhiteSpace(title))
                {
                    WriteLine("Invalid title. Please try again.");
                }
            }

            while (!CheckGenre(genre))
            {
                WriteLine("You can choose genre from Drama, Adventure, Family, Action, Sci-fi, Comedy, Animated, Thriller, or Other");
                Write("Enter genre ==> ");
                genre = ReadLine();
                if (!CheckGenre(genre))
                {
                    WriteLine("Invalid genre. Please try again.");
                }
            }

            while (!CheckClass(classification))
            {
                WriteLine("You can choose classification from General (G), Parental Guidance (PG), Mature (M15+), or Mature Accompanied (MA15+)");
                WriteLine("Only code input is valid. e.g. Parental Guidance (X) PG (O)");
                Write("Enter classification ==> ");
                classification = ReadLine();
                if (!CheckClass(classification))
                {
                    WriteLine("Invalid classification. Please try again.");
                }
            }

            while (duration < 0 || duration > 300)
            {
                WriteLine("You can enter duration in minutes from 0 to 300 minutes.");
                Write("Enter duration in minutes ==> ");
                string? durationInput = ReadLine();
                if (!int.TryParse(durationInput, out duration) || duration < 0 || duration > 300)
                {
                    WriteLine("Invalid duration. Please try again.");
                }
            }

            while (copies <= 0)
            {
                WriteLine("You can enter the number of copies (must be greater than 0).");
                Write("Enter the number of copies ==> ");
                string? copiesInput = ReadLine();
                if (!int.TryParse(copiesInput, out copies) || copies <= 0)
                {
                    WriteLine("Invalid number of copies. Please try again.");
                }
            }

            Movie movie = new Movie(title, genre!, classification!, duration, copies);
            movieCollection.AddMovie(movie);

            WriteLine("Movie added successfully. Please enter any key to go back to the menu.");
            ReadLine();
        }

        static bool CheckGenre(string? genre)
        {
            string[] validGenres = { "Drama", "Adventure", "Family", "Action", "Sci-fi", "Comedy", "Animated", "Thriller", "Other" };
            return genre != null && validGenres.Contains(genre);
        }

        static bool CheckClass(string? classification)
        {
            string[] validClassifications = { "G", "PG", "M15+", "MA15+" };
            return classification != null && validClassifications.Contains(classification);
        }

        static void RemoveMovie()
        {
            Write("Enter movie title to remove ==> ");
            string? title = ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                WriteLine("Invalid input. Please enter any key to go back to the menu.");
                ReadLine();
                return;
            }

            Write("Enter number of copies to remove ==> ");
            string? copiesInput = ReadLine();
            if (!int.TryParse(copiesInput, out int copies) || copies <= 0)
            {
                WriteLine("Invalid number of copies. Please enter any key to go back to the menu.");
                ReadLine();
                return;
            }

            try
            {
                movieCollection.RemoveMovie(title, copies);
                WriteLine("Movies removed successfully. Please enter any key to go back to the menu.");
            }
            catch (InvalidOperationException ex)
            {
                WriteLine(ex.Message);
            }

            ReadLine();
        }

        static void RegisterMember()
        {
            Write("Enter first name ==> ");
            string? firstName = ReadLine();
            Write("Enter last name ==> ");
            string? lastName = ReadLine();
            Write("Enter phone number ==> ");
            string? phoneNumber = ReadLine();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                WriteLine("Invalid input. Please enter any key to go back to the menu.");
                ReadLine();
                return;
            }

            if (memberCollection.IsMemberExist(firstName, lastName))
            {
                WriteLine("Member already exists. Please enter any key to go back to the menu.");
                ReadLine();
                return;
            }

            string? password = null;
            while (string.IsNullOrWhiteSpace(password) || password.Length != 4 || !int.TryParse(password, out _))
            {
                Write("Enter a 4-digit password for the member ==> ");
                password = ReadLine();
                if (string.IsNullOrWhiteSpace(password) || password.Length != 4 || !int.TryParse(password, out _))
                {
                    WriteLine("Invalid password. Please enter a valid 4-digit password.");
                }
            }

            Member member = new Member(firstName, lastName, phoneNumber, password);
            memberCollection.AddMember(member);

            WriteLine("Member registered successfully. Please enter any key to go back to the menu.");
            ReadLine();
        }

        static void RemoveMember()
        {
            Write("Enter first name ==> ");
            string? firstName = ReadLine();
            Write("Enter last name ==> ");
            string? lastName = ReadLine();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                WriteLine("Invalid input. Please enter any key to go back to the menu.");
                ReadLine();
                return;
            }

            try
            {
                memberCollection.RemoveMember(firstName, lastName);
                WriteLine("Member removed successfully. Please enter any key to go back to the menu.");
            }
            catch (InvalidOperationException ex)
            {
                WriteLine(ex.Message);
            }

            ReadLine();
        }

        static void FindMemberPhoneNumber()
        {
            Write("Enter first name ==> ");
            string? firstName = ReadLine();
            Write("Enter last name ==> ");
            string? lastName = ReadLine();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                WriteLine("Invalid input. Please enter any key to go back to the menu.");
                ReadLine();
                return;
            }

            string phoneNumber = memberCollection.FindMemberPhoneNumber(firstName, lastName);
            WriteLine($"Phone Number: {phoneNumber}");
            WriteLine("Please enter any key to go back to the menu.");
            ReadLine();
        }

        static void ListMembersBorrowingMovie()
        {
            Write("Enter movie title ==> ");
            string? title = ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                WriteLine("Invalid input. Please enter any key to go back to the menu.");
                ReadLine();
                return;
            }

            Member[] rentingMembers = memberCollection.FindMembersByMovie(title);
            if (rentingMembers.Length > 0)
            {
                WriteLine("Members currently renting this movie:");
                foreach (var member in rentingMembers)
                {
                    WriteLine($"{member.FirstName} {member.LastName} - {member.PhoneNumber}");
                }
            }
            else
            {
                WriteLine("No members are currently renting this movie.");
            }

            WriteLine("Please enter any key to go back to the menu.");
            ReadLine();
        }

        static void DisplayAllMovies()
        {
            WriteLine("All Movies:");
            for (int i = 0; i < movieCollection.MovieCount; i++)
            {
                Movie? movie = movieCollection.GetMovie(i);
                if (movie != null)
                {
                    WriteLine(movie);
                }
            }
            WriteLine("Please enter any key to go back to the menu.");
            ReadLine();
        }

        static void DisplayAllMoviesInDictionaryOrder()
        {
            WriteLine("All Movies in Dictionary Order:");
            Movie[] sortedMovies = movieCollection.GetMoviesInDictionaryOrder();
            foreach (Movie movie in sortedMovies)
            {
                WriteLine(movie);
            }
            WriteLine("Please enter any key to go back to the menu.");
            ReadLine();
        }

        static void DisplayMovieInfo()
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
                WriteLine("Invalid movie title.");
            }
            WriteLine("Please enter any key to go back to the menu.");
            ReadLine();
        }

        static void BorrowMovie(Member member)
        {
            Write("Enter movie title to borrow ==> ");
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

        static void ReturnMovie(Member member)
        {
            Write("Enter movie title to return ==> ");
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

        static void DisplayTop3Movies()
        {
            WriteLine("Top 3 Borrowed Movies:");
            var topMovies = movieCollection.GetTop3Movies();
            foreach (var (Title, Count) in topMovies)
            {
                WriteLine($"{Title} - {Count} times");
            }
            WriteLine("Please enter any key to go back to the menu.");
            ReadLine();
        }
    }
}
