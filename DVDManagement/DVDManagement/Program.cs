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
            // 프로그램이 종료될 때 OnProcessExit 메서드를 호출하여 데이터 저장
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
                Write("Enter your choice \n==> ");

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
                        WriteLine("Invalid choice \nPlease enter 1, 2 or 0.");
                        WriteLine("==>");
                        ReadLine();
                        break;
                }
            }
        }

        // * 프로그램이 종료될 때 호출되는 메서드
        static void OnProcessExit(object sender, EventArgs e)
        {
            memberCollection.SaveMembers();
            movieCollection.SaveMovies();
        }
    }
}
