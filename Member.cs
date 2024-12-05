namespace IFQ664
{
    public class Member
    {
        // Public properties to store member's personal information
        public string FirstName { get; }       // Member's first name
        public string LastName { get; }        // Member's last name
        public string PhoneNumber { get; }     // Member's contact phone number
        public int Password { get; }           // Member's password (as an integer)

        private const int MaxBorrowedMovies = 5;  // Maximum number of movies a member can borrow

        public DynamicList<Movie> borrowedMovies; // List of movies currently borrowed by the member

        // Default constructor initializing properties to default values
        public Member()
        {
            FirstName = string.Empty;
            LastName = string.Empty;
            PhoneNumber = string.Empty;
            Password = 0;
            borrowedMovies = new DynamicList<Movie>();
        }

        // Constructor to initialize a new member with provided information
        public Member(string firstName, string lastName, string phoneNumber, int password)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Password = password;
            borrowedMovies = new DynamicList<Movie>();
        }

        // Static method to verify a member's credentials
        public static string VerifyMember(ref MemberCollection memberCollection, ref int curMemberIndex,
            string firstName, string lastName, int password)
        {
            curMemberIndex = memberCollection.FindMemberIndex(firstName, lastName);
            if (curMemberIndex == -1)
            {
                return "not found"; // Member not found in the collection
            }
            else
            {
                if (memberCollection.GetMember(curMemberIndex).Password == password)
                {
                    return "verified"; // Password matches, member verified
                }
                return "not verified"; // Password does not match
            }
        }

        // Method for a member to borrow a movie
        public bool BorrowMovie(Movie movie)
        {
            // Check if the member has reached the maximum borrowing limit
            if (borrowedMovies.Count >= MaxBorrowedMovies)
            {
                Console.WriteLine($"Apologies, {FirstName}, you've reached your borrowing limit.");
                return false;
            }

            // Check if the member has already borrowed this movie
            if (borrowedMovies.Contains(movie))
            {
                Console.WriteLine($"Sorry, {FirstName}, you've already borrowed '{movie.Title}'.");
                return false;
            }

            borrowedMovies.Add(movie); // Add the movie to the member's borrowed list
            Console.WriteLine($"'{movie.Title}' has been successfully borrowed by {FirstName}.");
            return true;
        }

        // Method for a member to return a borrowed movie
        public bool ReturnMovie(Movie movie)
        {
            // Check if the member has borrowed this movie
            if (borrowedMovies.Contains(movie))
            {
                borrowedMovies.Remove(movie); // Remove the movie from the borrowed list
                Console.WriteLine($"'{movie.Title}' has been successfully returned by {FirstName}.");
                return true;
            }
            else
            {
                Console.WriteLine($"Unfortunately, {FirstName}, you haven't borrowed '{movie.Title}'.");
                return false;
            }
        }

        // Method to display all movies currently borrowed by the member
        public void DisplayBorrowedMovies()
        {
            Console.WriteLine($"Movies borrowed by {FirstName} {LastName}:");
            if (borrowedMovies.Count == 0)
            {
                Console.WriteLine("You have not borrowed any movies.");
            }
            else
            {
                for (int i = 0; i < borrowedMovies.Count; i++)
                {
                    Console.WriteLine($"- {borrowedMovies[i].Title}");
                }
            }
        }

        // Static method to register a new member into the member collection
        public static void RegisterNewMember(ref MemberCollection memberCollection)
        {
            // Check if the member collection is full
            if (memberCollection.IsMembersFull())
            {
                Console.WriteLine("Cannot add new member: the collection is full.");
                return;
            }

            // Prompt user for member details
            Console.Write("Please enter your first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Please enter your last name: ");
            string lastName = Console.ReadLine();

            // Check if the member already exists
            if (memberCollection.FindMemberIndex(firstName, lastName) != -1)
            {
                Console.WriteLine($"The member {firstName} {lastName} is already registered.");
                return;
            }

            Console.Write("Please enter your phone number: ");
            string phoneNumber = Console.ReadLine();

            // Prompt for a four-digit password with validation
            string password;
            do
            {
                Console.Write("Please enter a four-digit password: ");
                password = Console.ReadLine();
                if (password.Length != 4 || !password.All(char.IsDigit))
                {
                    Console.WriteLine("The password must be exactly four digits.");
                }
            } while (password.Length != 4 || !password.All(char.IsDigit));

            // Create a new member object with the provided details
            Member newMember = new Member(firstName, lastName, phoneNumber, Convert.ToInt16(password));

            // Add the new member to the member collection
            memberCollection.AddMember(newMember);

            Console.WriteLine($"Member {firstName} {lastName} has been added to the collection.");
        }

        // Static method to remove a member from the member collection
        public static void RemoveMemberFromSystem(ref MemberCollection memberCollection)
        {
            // Prompt for the member's name to remove
            Console.Write("Enter the first name of the member to remove: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter the last name of the member to remove: ");
            string lastName = Console.ReadLine();

            int index = memberCollection.FindMemberIndex(firstName, lastName);
            if (index != -1)
            {
                // Check if the member has any DVDs checked out
                if (memberCollection.CountBorrowBooks(index) > 0)
                {
                    Console.WriteLine($"Cannot remove {firstName} {lastName} because they have DVDs checked out. Please ensure all DVDs are returned first.");
                }
                else
                {
                    // Remove the member from the collection
                    memberCollection.RemoveMember(index);
                    Console.WriteLine($"Member {firstName} {lastName} has been removed from the collection.");
                }
            }
            else
            {
                Console.WriteLine($"Member {firstName} {lastName} does not exist in the collection.");
            }
        }

        // Static method to find and display a member's phone number
        public static void FindMemberPhoneNumber(ref MemberCollection memberCollection)
        {
            // Prompt for the member's name
            Console.Write("Enter the member's first name: ");
            string firstName = Console.ReadLine();
            Console.Write("Enter the member's last name: ");
            string lastName = Console.ReadLine();

            int index = memberCollection.FindMemberIndex(firstName, lastName);
            if (index != -1)
            {
                // Display the member's phone number
                Console.WriteLine($"Phone number for {firstName} {lastName}: {memberCollection.GetMember(index).PhoneNumber}");
            }
            else
            {
                Console.WriteLine($"Member {firstName} {lastName} was not found in the collection.");
            }
        }

        // Static method to find and display members currently renting a specific movie
        public static void FindMembersRentingMovie(ref MemberCollection memberCollection)
        {
            // Prompt for the movie title
            Console.Write("Enter the movie title: ");
            string movieTitle = Console.ReadLine();

            // Get a list of members renting the movie
            DynamicList<Member> rentingMembers = memberCollection.RentingMembers(movieTitle);

            if (rentingMembers.Count > 0)
            {
                Console.WriteLine($"Members currently borrowing '{movieTitle}':");
                for (int i = 0; i < rentingMembers.Count; i++)
                {
                    Console.WriteLine($"{rentingMembers[i].FirstName} {rentingMembers[i].LastName}");
                }
            }
            else
            {
                Console.WriteLine($"No members are currently borrowing '{movieTitle}'.");
            }
        }

        // Method to check if the member has borrowed a specific movie by title
        public bool HasBorrowedMovie(string title)
        {
            for (int i = 0; i < borrowedMovies.Count; i++)
            {
                if (borrowedMovies[i].Title == title)
                {
                    return true; // Movie is in the borrowed list
                }
            }
            return false; // Movie not found in the borrowed list
        }
    }
}
