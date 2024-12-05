using IFQ664;
using System;

class Program
{
    static void Main(string[] args)
    {
        MovieCollection movieCollection = new MovieCollection();
        MemberCollection memberCollection = new MemberCollection();
        int curMemberIndex = -1;

        Console.WriteLine("=======\nCOMMUNITY LIBRARY MOVIE DVD MANAGEMENT\n=======");

        int choice;
        do
        {
            Console.WriteLine("\nMain Menu");
            Console.WriteLine("---------------------");
            Console.WriteLine("Select from the following:");
            Console.WriteLine("1. Staff");
            Console.WriteLine("2. Member");
            Console.WriteLine("0. End the program");
            Console.Write("Enter your choice ==> ");

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        // Verify staff identity
                        if (VerifyStaff())
                        {
                            StaffMenu(movieCollection, memberCollection);
                        }
                        else
                        {
                            Console.WriteLine("Staff identity verification failed. Access denied.");
                        }
                        break;
                    case 2:
                        // Verify member identity
                        if (VerifyMember(ref memberCollection, ref curMemberIndex))
                        {
                            MemberMenu(memberCollection, movieCollection, curMemberIndex);
                        }
                        else
                        {
                            Console.WriteLine("Member identity verification failed. Access denied.");
                        }
                        break;
                    case 0:
                        Console.WriteLine("Ending the program...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

        } while (choice != 0);
    }

    static bool VerifyStaff()
    {
        Console.Write("Enter username: ");
        string username = Console.ReadLine();
        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        // Check if username and password match
        return (username == "staff" && password == "today123");
    }

    static bool VerifyMember(ref MemberCollection memberCollection, ref int curMemberIndex)
    {
        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine();
        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine();
        Console.Write("Enter password: ");
        string password = Console.ReadLine();

        // member verification based on first name, last name, and password
        if (!string.IsNullOrWhiteSpace(firstName) && !string.IsNullOrWhiteSpace(lastName) && !string.IsNullOrWhiteSpace(password))
        {
            string res = Member.VerifyMember(ref memberCollection, ref curMemberIndex,
                firstName, lastName, Convert.ToInt16(password));

            switch (res)
            {
                case "not found":
                    Console.WriteLine("Member is not found!");
                    return false;
                case "verified":
                    return true;
                case "not verified":
                    Console.WriteLine("Password is wrong!");
                    return false;
                default:
                    return false;
            }
        }
        else
            return false;
    }

    static void StaffMenu(MovieCollection movieCollection, MemberCollection memberCollection)
    {
        int choice;
        do
        {
            Console.WriteLine("\nStaff Menu");
            Console.WriteLine("---------------------");
            Console.WriteLine("1. Add DVDs to system");
            Console.WriteLine("2. Remove DVDs from system");
            Console.WriteLine("3. Register a new member to system");
            Console.WriteLine("4. Remove a registered member from system");
            Console.WriteLine("5. Find a member contact phone number, given the number's name");
            Console.WriteLine("6. Find members who are currently renting a particular movie");
            Console.WriteLine("7. Browse all the movies");
            Console.WriteLine("0. Return to main menu");
            Console.Write("Enter your choice ==> ");

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        // Add movie DVDs
                        Console.WriteLine("----------------  1. Add movie DVDs  -----------------------");
                        Movie.AddMovieDVDs(ref movieCollection);
                        break;
                    case 2:
                        // Remove movie DVDs
                        Console.WriteLine("----------------  2. Remove movie DVDs  -----------------------");
                        Movie.RemoveMovieDVDs(ref movieCollection);
                        break;
                    case 3:
                        // Register a new member
                        Console.WriteLine("----------------  3. Register a new member  -----------------------");
                        Member.RegisterNewMember(ref memberCollection);
                        break;
                    case 4:
                        // Remove a registered member
                        Console.WriteLine("----------------  4. Remove a registered member  -----------------------");
                        Member.RemoveMemberFromSystem(ref memberCollection);
                        break;
                    case 5:
                        // Find member's contact phone number
                        Console.WriteLine("-----------  5. Find member's contact phone number  ---------------");
                        Member.FindMemberPhoneNumber(ref memberCollection);
                        break;
                    case 6:
                        // Find members renting a particular movie
                        Console.WriteLine("-----------  6. Find members renting a particular movie  --------------");
                        Member.FindMembersRentingMovie(ref memberCollection);
                        break;
                    case 7:
                        // Browse all the movies
                        Console.WriteLine("-----------  7. Browse all the movies  ---------------");
                        Movie.DisplayAllMovies(ref movieCollection);
                        break;
                    case 0:
                        Console.WriteLine("Returning to main menu...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

        } while (choice != 0);
    }


    static void MemberMenu(MemberCollection memberCollection, MovieCollection movieCollection, int curMemberIndex)
    {
        int choice;
        do
        {
            Console.WriteLine("\nMember Menu");
            Console.WriteLine("---------------------");
            Console.WriteLine("1. Browse all the movies");
            Console.WriteLine("2. Display all the information about a movie, given the title of the movie");
            Console.WriteLine("3. Borrow a movie DVD");
            Console.WriteLine("4. Return a movie DVD");
            Console.WriteLine("5. List current borrowing movies");
            Console.WriteLine("6. Display the top 3 movies rented by the members");
            Console.WriteLine("0. Return to main menu");
            Console.Write("Enter your choice ==> ");

            if (int.TryParse(Console.ReadLine(), out choice))
            {
                switch (choice)
                {
                    case 1:
                        // Browse all the movies
                        Console.WriteLine("----------------  1. Browse all the movies  -----------------------");
                        Movie.DisplayAllMovies(ref movieCollection);
                        break;
                    case 2:
                        // Display information about a movie
                        Console.WriteLine("----------------  2. Display information about a movie  -----------------------");
                        Movie.DisplayMovieInfo(ref movieCollection);
                        break;
                    case 3:
                        // Borrow a movie DVD
                        Console.WriteLine("----------------  3. Borrow a movie DVD  -----------------------");
                        Movie.BorrowMovieDVD(ref memberCollection, ref movieCollection, curMemberIndex);
                        break;
                    case 4:
                        // Return a movie DVD
                        Console.WriteLine("----------------  4. Return a movie DVD  -----------------------");
                        Movie.ReturnMovieDVD(ref memberCollection, ref movieCollection, curMemberIndex);
                        break;
                    case 5:
                        // List current movie DVDs being borrowed by the member
                        Console.WriteLine("---------  5. List current movie DVDs being borrowed by the member  --------");
                        Movie.ListBorrowedMovies(movieCollection, memberCollection, curMemberIndex);
                        break;
                    case 6:
                        // Display top three most frequently borrowed movies
                        Console.WriteLine("-------  6. Display top three most frequently borrowed movies  -----");
                        Movie.DisplayTopThreeMovies(movieCollection);
                        break;
                    case 0:
                        Console.WriteLine("Returning to main menu...");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }

        } while (choice != 0);
    }

}
