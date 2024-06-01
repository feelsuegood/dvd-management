using System;
using static System.Console;

namespace DVDManagement
{
    public static class StaffMenu
    {
        public static void StaffLogin(MemberCollection memberCollection, MovieCollection movieCollection)
        {
            while (true)
            {
                WriteLine("\n* 0 to go back");
                WriteLine("Please enter staff username ==> ");
                string username = ReadLine() ?? string.Empty;
                if (username == "0") return;

                WriteLine("\n* 0 to go back");
                WriteLine("Please enter staff password ==> ");
                string password = ReadLine() ?? string.Empty;
                if (password == "0") return;

                if (username == "staff" && password == "today123")
                {
                    StaffOnlyMenu(memberCollection, movieCollection);
                    return;
                }
                else
                {
                    WriteLine("Invalid username or password. Please enter a valid username and password.");
                    continue;
                }
            }
        }

        private static void StaffOnlyMenu(MemberCollection memberCollection, MovieCollection movieCollection)
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
                WriteLine("0. Return to main menu");

                WriteLine("\nEnter your choice ==> ");

                switch (ReadLine())
                {
                    case "1":
                        AddMovie(movieCollection);
                        break;
                    case "2":
                        RemoveMovie(movieCollection);
                        break;
                    case "3":
                        RegisterMember(memberCollection);
                        break;
                    case "4":
                        RemoveMember(memberCollection);
                        break;
                    case "5":
                        FindMemberPhoneNumber(memberCollection);
                        break;
                    case "6":
                        ListMembersBorrowingMovie(memberCollection);
                        break;
                    case "0":
                        return;
                    default:
                        WriteLine("\nInvalid choice. Please enter from 0 to 6. \nPlease enter any key to retry ==> ");
                        ReadLine();
                        break;
                }
            }
        }

        static void AddMovie(MovieCollection movieCollection)
        {
            string? title = null;
            string? genre = null;
            string? classification = null;
            int duration = -1;
            int copies = -1;

            while (true)
            {
                WriteLine("\n* 0 to go back");
                WriteLine("Please enter movie title ==> ");
                title = ReadLine();
                if (title == "0") return;
                if (string.IsNullOrWhiteSpace(title))
                {
                    WriteLine("\nInvalid input. Please enter a valid movie title.");
                    continue;
                }

                Movie existingMovie = movieCollection.FindMovie(title);
                if (existingMovie != null)
                {
                    copies = -1; // reset copies
                    while (copies <= 0)
                    {
                        WriteLine("\n* 0 to go back");
                        WriteLine("Movie already exists.");
                        Write("Please enter the number of copies to add (must be greater than 0) ==> ");
                        string? copiesInput = ReadLine();
                        if (copiesInput == "0") return;
                        if (!int.TryParse(copiesInput, out copies) || copies <= 0)
                        {
                            WriteLine("\nInvalid input. Please enter a valid number of copies.");
                            continue;
                        }
                    }
                    existingMovie.Copies += copies;
                    movieCollection.SaveMovies();
                    WriteLine("Copies added successfully.");
                    continue;
                }
                else
                {
                    genre = null;
                    classification = null;
                    duration = -1;
                    copies = -1; // reset all inputs

                    while (!CheckGenre(genre))
                    {
                        WriteLine("\n* 0 to go back");
                        WriteLine("You can choose genre from Drama, Adventure, Family, Action, Sci-fi, Comedy, Animated, Thriller, or Other.");
                        Write("Enter genre ==> ");
                        genre = ReadLine();
                        if (genre == "0") return;
                        if (!CheckGenre(genre))
                        {
                            WriteLine("\nInvalid input. Please input among Drama, Adventure, Family, Action, Sci-fi, Comedy, Animated, Thriller, or Other.");
                        }
                    }

                    while (!CheckClass(classification))
                    {
                        WriteLine("\n* 0 to go back");
                        WriteLine("You can choose classification from General (G), Parental Guidance (PG), Mature (M15+), or Mature Accompanied (MA15+).");
                        WriteLine("Only code input is valid. e.g. Parental Guidance (X) PG (O)");
                        Write("Enter classification ==> ");
                        classification = ReadLine();
                        if (classification == "0") return;
                        if (!CheckClass(classification))
                        {
                            WriteLine("\nInvalid input. Please enter a valid classification code.");
                        }
                    }

                    while (duration < 0 || duration > 300)
                    {
                        WriteLine("\n* 0 to go back");
                        WriteLine("You can enter duration in minutes from 1 to 300 minutes. Please enter duration in minutes ==> ");
                        string? durationInput = ReadLine();
                        if (durationInput == "0") return;
                        if (!int.TryParse(durationInput, out duration) || duration < 0 || duration > 300)
                        {
                            WriteLine("\nInvalid input. Please enter a valid duration in minutes between 1 and 300.");
                        }
                    }

                    while (copies <= 0)
                    {
                        WriteLine("\n* 0 to go back");
                        WriteLine("You can enter the number of copies (must be greater than 0). Please enter the number of copies ==> ");
                        string? copiesInput = ReadLine();
                        if (copiesInput == "0") return;
                        if (!int.TryParse(copiesInput, out copies) || copies <= 0)
                        {
                            WriteLine("\nInvalid input. Please input a valid number of copies.");
                        }
                    }

                    Movie movie = new Movie(title, genre!, classification!, duration, copies);
                    movieCollection.AddMovie(movie);
                    movieCollection.SaveMovies();

                    WriteLine("Movie added successfully.");

                }
            }
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

        static void RemoveMovie(MovieCollection movieCollection)
        {
            while (true)
            {
                WriteLine("\n* 0 to go back");
                WriteLine("Please enter movie title to remove ==> ");
                string? title = ReadLine();
                if (title == "0") return;

                if (string.IsNullOrWhiteSpace(title))
                {
                    WriteLine("\nInvalid input. Please enter a valid movie title.");
                    continue;
                }

                int copies = -1;
                while (copies <= 0)
                {
                    WriteLine("\n* 0 to go back");
                    WriteLine("Please enter the number of copies to remove ==> ");
                    string? copiesInput = ReadLine();
                    if (copiesInput == "0") return;
                    if (!int.TryParse(copiesInput, out copies) || copies <= 0)
                    {
                        WriteLine("\nInvalid input. Please enter a valid number of copies.");
                    }
                }

                try
                {
                    movieCollection.RemoveMovie(title, copies);
                    WriteLine("Movies removed successfully.");
                    continue;
                }
                catch (InvalidOperationException ex)
                {
                    WriteLine(ex.Message);
                    continue;
                }
            }
        }

        static void RegisterMember(MemberCollection memberCollection)
        {
            while (true)
            {
                // Get a valid first name
                string? firstName = null;
                while (true)
                {
                    WriteLine("\n* 0 to go back");
                    Write("Enter first name to register ==> ");
                    firstName = ReadLine();
                    if (firstName == "0") return;

                    if (string.IsNullOrWhiteSpace(firstName) || !IsAlphabetic(firstName))
                    {
                        WriteLine("\nInvalid input. Please enter a valid first name with alphabetic characters only.");
                        continue;
                    }
                    break;
                }

                // Get a valid last name
                string? lastName = null;
                while (true)
                {
                    WriteLine("\n* 0 to go back");
                    Write("Enter last name ==> ");
                    lastName = ReadLine();
                    if (lastName == "0") return;

                    if (string.IsNullOrWhiteSpace(lastName) || !IsAlphabetic(lastName))
                    {
                        WriteLine("\nInvalid input. Please enter a valid last name with alphabetic characters only.");
                        continue;
                    }
                    break;
                }

                // Check if member already exists
                if (memberCollection.IsMemberExist(firstName, lastName))
                {
                    WriteLine("Member already exists. Please enter a different first name and last name.");
                    continue;
                }

                // Get a valid 7-digit phone number
                string? phoneNumber = null;
                while (true)
                {
                    WriteLine("\n* 0 to go back");
                    Write("Enter phone number to register ==> ");
                    phoneNumber = ReadLine();
                    if (phoneNumber == "0") return;

                    if (string.IsNullOrWhiteSpace(phoneNumber) || phoneNumber.Length != 7 || !int.TryParse(phoneNumber, out _))
                    {
                        WriteLine("\nInvalid input. Please enter a 7-digit phone number.");
                        continue;
                    }
                    break;
                }

                // Get a valid 4-digit password
                string? password = null;
                while (true)
                {
                    Write("Enter a 4-digit password for the member ==> ");
                    password = ReadLine();
                    if (string.IsNullOrWhiteSpace(password) || password.Length != 4 || !int.TryParse(password, out _))
                    {
                        WriteLine("Invalid password. Please enter a 4-digit password.");
                        continue;
                    }
                    break;
                }

                // Register the new member
                Member member = new Member(firstName, lastName, phoneNumber, password);
                memberCollection.AddMember(member);
                WriteLine("Member registered successfully.");
                continue;
            }
        }

        // Check if name is alphabetic
        private static bool IsAlphabetic(string input)
        {
            foreach (char c in input)
            {
                if (!char.IsLetter(c))
                {
                    return false;
                }
            }
            return true;
        }

        static void RemoveMember(MemberCollection memberCollection)
        {
            while (true)
            {
                WriteLine("\n* 0 to go back");
                Write("Enter first name to remove ==> ");
                string? firstName = ReadLine();
                if (firstName == "0") return;

                WriteLine("\n* 0 to go back");
                Write("Enter last name to remove ==> ");
                string? lastName = ReadLine();
                if (lastName == "0") return;

                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                {
                    WriteLine("\nInvalid input. Please enter a valid first name and last name.");
                    continue;
                }

                if (!memberCollection.IsMemberExist(firstName, lastName))
                {
                    WriteLine("Member not found. Please enter a valid first name and last name.");
                    continue;
                }

                try
                {
                    memberCollection.RemoveMember(firstName, lastName);
                    WriteLine("Member removed successfully.");
                    continue;
                }
                catch (InvalidOperationException ex)
                {
                    WriteLine(ex.Message);
                }
                ReadLine();
            }
        }

        static void FindMemberPhoneNumber(MemberCollection memberCollection)
        {
            while (true)
            {
                WriteLine("\n* 0 to go back");
                Write("Enter first name ==> ");
                string? firstName = ReadLine();
                if (firstName == "0") return;

                WriteLine("\n* 0 to go back");
                Write("Enter last name ==> ");
                string? lastName = ReadLine();
                if (lastName == "0") return;

                if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
                {
                    WriteLine("\nInvalid input. Please enter a valid first name and last name.");
                    continue;
                }

                if (!memberCollection.IsMemberExist(firstName, lastName))
                {
                    WriteLine("Member not found. Please enter a valid first name and last name.");
                    continue;
                }

                string phoneNumber = memberCollection.FindMemberPhoneNumber(firstName, lastName);
                WriteLine($"Phone Number: {phoneNumber}");
                continue;
            }
        }

        static void ListMembersBorrowingMovie(MemberCollection memberCollection)
        {
            while (true)
            {
                WriteLine("\n* 0 to go back");
                Write("Please enter movie title ==> ");
                string? title = ReadLine();
                if (title == "0") return;

                if (string.IsNullOrWhiteSpace(title))
                {
                    WriteLine("\nInvalid input. Please enter a valid movie title.");
                    continue;
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
                continue;
            }
        }
    }
}
