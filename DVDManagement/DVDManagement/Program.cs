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
            // When the program terminates, call the OnProcessExit method to save data
            AppDomain.CurrentDomain.ProcessExit += OnProcessExit;
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
                        StaffMenu.StaffLogin(memberCollection, movieCollection);
                        break;
                    case "2":
                        MemberMenu.MemberLogin(memberCollection, movieCollection);
                        break;
                    case "0":
                        return;
                    default:
                        WriteLine("\nInvalid choice. Please enter 1, 2 or 0. \nPlease enter any key to retry ==> ");
                        ReadLine();
                        break;
                }
            }
        }

        // * Method called when the program terminates
        static void OnProcessExit(object sender, EventArgs e)
        {
            memberCollection.SaveMembers();
            movieCollection.SaveMovies();
        }
    }
}
