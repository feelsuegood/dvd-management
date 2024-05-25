using DVDManagement.Collections;
using DVDManagement.Models;

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
                Console.Clear();
                Console.WriteLine("Welcome to the Community Library DVD Management System");
                Console.WriteLine("1. Staff Login");
                Console.WriteLine("2. Member Login");
                Console.WriteLine("3. Exit");
                Console.Write("Please select an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        StaffLogin();
                        break;
                    case "2":
                        MemberLogin();
                        break;
                    case "3":
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        Console.ReadLine(); // Changed to Console.ReadLine
                        break;
                }
            }
        }

        static void StaffLogin()
        {
            Console.Write("Enter staff username: ");
            string username = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter staff password: ");
            string password = Console.ReadLine() ?? string.Empty;

            if (username == "staff" && password == "today123")
            {
                StaffMenu();
            }
            else
            {
                Console.WriteLine("Invalid credentials. Press any key to return to the main menu.");
                Console.ReadLine(); // Changed to Console.ReadLine
            }
        }

        static void MemberLogin()
        {
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine() ?? string.Empty;
            Console.Write("Enter password: ");
            string password = Console.ReadLine() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(password))
            {
                Console.WriteLine("Invalid input. Press any key to return to the main menu.");
                Console.ReadLine(); // Changed to Console.ReadLine
                return;
            }

            Member? member = memberCollection.FindMember(firstName, lastName);
            if (member != null)
            {
                MemberMenu(member);
            }
            else
            {
                Console.WriteLine("Member not found or invalid credentials. Press any key to return to the main menu.");
                Console.ReadLine(); // Changed to Console.ReadLine
            }
        }

        static void StaffMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Staff Menu");
                Console.WriteLine("1. Add Movie");
                Console.WriteLine("2. Remove Movie");
                Console.WriteLine("3. Register Member");
                Console.WriteLine("4. Remove Member");
                Console.WriteLine("5. Find Member's Contact Number");
                Console.WriteLine("6. List Members Borrowing a Movie");
                Console.WriteLine("7. Logout");
                Console.Write("Please select an option: ");

                switch (Console.ReadLine())
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
                        FindMemberContact();
                        break;
                    case "6":
                        ListMembersBorrowingMovie();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        Console.ReadLine(); // Changed to Console.ReadLine
                        break;
                }
            }
        }

        static void MemberMenu(Member member)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine($"Welcome, {member.FirstName} {member.LastName}");
                Console.WriteLine("1. Display All Movies");
                Console.WriteLine("2. Display Movie Information");
                Console.WriteLine("3. Borrow Movie");
                Console.WriteLine("4. Return Movie");
                Console.WriteLine("5. List Borrowed Movies");
                Console.WriteLine("6. Display Top 3 Borrowed Movies");
                Console.WriteLine("7. Logout");
                Console.Write("Please select an option: ");

                switch (Console.ReadLine())
                {
                    case "1":
                        DisplayAllMovies();
                        break;
                    case "2":
                        DisplayMovieInformation();
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
                    case "6":
                        DisplayTopBorrowedMovies();
                        break;
                    case "7":
                        return;
                    default:
                        Console.WriteLine("Invalid option, please try again.");
                        Console.ReadLine(); // Changed to Console.ReadLine
                        break;
                }
            }
        }

        static void AddMovie()
        {
            Console.Write("Enter movie title: ");
            string? title = Console.ReadLine();
            Console.Write("Enter genre: ");
            string? genre = Console.ReadLine();
            Console.Write("Enter classification: ");
            string? classification = Console.ReadLine();
            Console.Write("Enter duration in minutes: ");
            string? durationInput = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(genre) || string.IsNullOrWhiteSpace(classification) || string.IsNullOrWhiteSpace(durationInput) || !int.TryParse(durationInput, out int duration))
            {
                Console.WriteLine("Invalid input. Press any key to return to the menu.");
                Console.ReadLine(); // Changed to Console.ReadLine
                return;
            }

            Movie movie = new Movie(title, genre, classification, duration);
            movieCollection.AddMovie(movie);

            Console.WriteLine("Movie added successfully. Press any key to return to the menu.");
            Console.ReadLine(); // Changed to Console.ReadLine
        }

        static void RemoveMovie()
        {
            Console.Write("Enter movie title to remove: ");
            string? title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Invalid input. Press any key to return to the menu.");
                Console.ReadLine(); // Changed to Console.ReadLine
                return;
            }

            movieCollection.RemoveMovie(title);

            Console.WriteLine("Movie removed successfully. Press any key to return to the menu.");
            Console.ReadLine(); // Changed to Console.ReadLine
        }

        static void RegisterMember()
        {
            Console.Write("Enter first name: ");
            string? firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string? lastName = Console.ReadLine();
            Console.Write("Enter phone number: ");
            string? phoneNumber = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) || string.IsNullOrWhiteSpace(phoneNumber))
            {
                Console.WriteLine("Invalid input. Press any key to return to the menu.");
                Console.ReadLine(); // Changed to Console.ReadLine
                return;
            }

            Member member = new Member(firstName, lastName, phoneNumber);
            memberCollection.AddMember(member);

            Console.WriteLine("Member registered successfully. Press any key to return to the menu.");
            Console.ReadLine(); // Changed to Console.ReadLine
        }

        static void RemoveMember()
        {
            Console.Write("Enter first name: ");
            string? firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string? lastName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Invalid input. Press any key to return to the menu.");
                Console.ReadLine(); // Changed to Console.ReadLine
                return;
            }

            Member? member = memberCollection.FindMember(firstName, lastName);
            if (member != null && member.BorrowedMovies.Length == 0)
            {
                memberCollection.RemoveMember(member);
                Console.WriteLine("Member removed successfully. Press any key to return to the menu.");
            }
            else
            {
                Console.WriteLine("Member not found or has borrowed movies. Press any key to return to the menu.");
            }
            Console.ReadLine(); // Changed to Console.ReadLine
        }

        static void FindMemberContact()
        {
            Console.Write("Enter first name: ");
            string? firstName = Console.ReadLine();
            Console.Write("Enter last name: ");
            string? lastName = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            {
                Console.WriteLine("Invalid input. Press any key to return to the menu.");
                Console.ReadLine(); // Changed to Console.ReadLine
                return;
            }

            Member? member = memberCollection.FindMember(firstName, lastName);
            if (member != null)
            {
                Console.WriteLine($"Contact Number: {member.PhoneNumber}");
            }
            else
            {
                Console.WriteLine("Member not found.");
            }
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadLine(); // Changed to Console.ReadLine
        }

        static void ListMembersBorrowingMovie()
        {
            Console.Write("Enter movie title: ");
            string? title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Invalid input. Press any key to return to the menu.");
                Console.ReadLine(); // Changed to Console.ReadLine
                return;
            }

            Console.WriteLine("Members currently borrowing this movie:");
            for (int i = 0; i < memberCollection.MembersCount; i++)
            {
                Member? member = memberCollection.GetMember(i);
                if (member != null && member.HasBorrowedMovie(title))
                {
                    Console.WriteLine($"{member.FirstName} {member.LastName}");
                }
            }
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadLine(); // Changed to Console.ReadLine
        }

        static void DisplayAllMovies()
        {
            Console.WriteLine("All Movies:");
            for (int i = 0; i < movieCollection.MovieCount; i++)
            {
                Movie? movie = movieCollection.GetMovie(i);
                if (movie != null)
                {
                    Console.WriteLine(movie);
                }
            }
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadLine(); // Changed to Console.ReadLine
        }

        static void DisplayMovieInformation()
        {
            Console.Write("Enter movie title: ");
            string? title = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(title))
            {
                Movie? movie = movieCollection.FindMovie(title);
                if (movie != null)
                {
                    Console.WriteLine(movie);
                }
                else
                {
                    Console.WriteLine("Movie not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid movie title.");
            }
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadLine(); // Changed to Console.ReadLine
        }

        static void BorrowMovie(Member member)
        {
            Console.Write("Enter movie title to borrow: ");
            string? title = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(title))
            {
                Movie? movie = movieCollection.FindMovie(title);
                if (movie != null)
                {
                    try
                    {
                        member.BorrowMovie(movie);
                        Console.WriteLine("Movie borrowed successfully.");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
                else
                {
                    Console.WriteLine("Movie not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid movie title.");
            }
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadLine(); // Changed to Console.ReadLine
        }

        static void ReturnMovie(Member member)
        {
            Console.Write("Enter movie title to return: ");
            string? title = Console.ReadLine();

            if (!string.IsNullOrWhiteSpace(title))
            {
                Movie? movie = movieCollection.FindMovie(title);
                if (movie != null)
                {
                    member.ReturnMovie(movie);
                    Console.WriteLine("Movie returned successfully.");
                }
                else
                {
                    Console.WriteLine("Movie not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid movie title.");
            }
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadLine(); // Changed to Console.ReadLine
        }

        static void ListBorrowedMovies(Member member)
        {
            Console.WriteLine("Borrowed Movies:");
            foreach (Movie? movie in member.BorrowedMovies)
            {
                if (movie != null)
                {
                    Console.WriteLine(movie);
                }
            }
            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadLine(); // Changed to Console.ReadLine
        }

        static void DisplayTopBorrowedMovies()
        {
            // Implementation for displaying top 3 borrowed movies
            // This part requires additional tracking for borrow count which needs to be implemented in MovieCollection class
            Console.WriteLine("Top 3 Borrowed Movies:");
            // Example:
            // Console.WriteLine("1. Movie Title - 10 times");
            // Console.WriteLine("2. Movie Title - 8 times");
            // Console.WriteLine("3. Movie Title - 6 times");

            Console.WriteLine("Press any key to return to the menu.");
            Console.ReadLine(); // Changed to Console.ReadLine
        }
    }
}
