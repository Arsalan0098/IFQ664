namespace IFQ664
{
    public class MemberCollection
    {
        private const int MaxSize = 1000; // Maximum number of members allowed in the collection

        private DynamicList<Member> members; // Dynamic list to store Member objects
        private int count; // Current number of members in the collection

        // Constructor to initialize the members list and set the count to zero
        public MemberCollection()
        {
            members = new DynamicList<Member>(MaxSize); // Initialize the members list with a maximum size
            count = 0; // Set the initial count of members to zero
        }

        // Method to add a new member to the collection
        public void AddMember(Member member)
        {
            // Check if the collection has reached its maximum capacity
            if (IsMembersFull())
            {
                Console.WriteLine("Cannot add member: the collection has reached its maximum capacity.");
                return;
            }
            members.Add(member); // Add the member to the list
            count++; // Increment the count of members
        }

        // Method to remove a member from the collection by index
        public void RemoveMember(int memberIndex)
        {
            // Check if the memberIndex is within valid bounds
            if (memberIndex < 0 || memberIndex >= count)
            {
                Console.WriteLine("Invalid member index. Cannot remove member.");
                return;
            }
            members.Remove(members[memberIndex]); // Remove the member at the specified index
            count--; // Decrement the count of members
        }

        // Method to check if the collection has reached its maximum capacity
        public bool IsMembersFull()
        {
            return count >= MaxSize;
        }

        // Method to display all members in the collection
        public void DisplayAllMembers()
        {
            Console.WriteLine("=== List of All Members ===");
            for (int i = 0; i < count; i++)
            {
                // Display the first name, last name, and contact phone number of each member
                Console.WriteLine($"{members[i].FirstName} {members[i].LastName} - Contact: {members[i].PhoneNumber}");
            }
        }

        // Method to find the index of a member based on first and last name
        public int FindMemberIndex(string firstName, string lastName)
        {
            for (int i = 0; i < count; i++)
            {
                // Check if the current member's first and last name match the search criteria
                if (members[i].FirstName == firstName && members[i].LastName == lastName)
                {
                    return i; // Return the index if found
                }
            }
            return -1; // Return -1 if the member is not found
        }

        // Method to find and return a member object based on first and last name
        public Member FindMember(string firstName, string lastName)
        {
            for (int i = 0; i < count; i++)
            {
                // Check if the current member's first and last name match the search criteria
                if (members[i].FirstName == firstName && members[i].LastName == lastName)
                {
                    return members[i]; // Return the member object if found
                }
            }
            return null; // Return null if the member is not found
        }

        // Method to count the number of movies borrowed by a member at a given index
        public int CountBorrowBooks(int memberIndex)
        {
            // Check if the memberIndex is within valid bounds
            if (memberIndex < 0 || memberIndex >= count)
            {
                Console.WriteLine("Invalid member index.");
                return 0;
            }
            return members[memberIndex].borrowedMovies.Count; // Return the count of borrowed movies
        }

        // Method to retrieve a member object at a given index
        public Member GetMember(int memberIndex)
        {
            // Check if the memberIndex is within valid bounds
            if (memberIndex < 0 || memberIndex >= count)
            {
                Console.WriteLine("Invalid member index.");
                return null;
            }
            return members[memberIndex]; // Return the member object
        }

        // Method to get a list of members who are currently renting a specific movie
        public DynamicList<Member> RentingMembers(string movieTitle)
        {
            DynamicList<Member> rentingMembers = new DynamicList<Member>(); // List to hold members renting the movie
            for (int i = 0; i < count; i++)
            {
                if (members[i] != null)
                {
                    // Iterate through the borrowed movies of the current member
                    for (int j = 0; j < members[i].borrowedMovies.Count; j++)
                    {
                        // Check if the borrowed movie's title matches the specified movie title
                        if (members[i].borrowedMovies[j].Title == movieTitle)
                        {
                            rentingMembers.Add(members[i]); // Add the member to the list
                            break; // Break the loop since the member is already renting the movie
                        }
                    }
                }
            }
            return rentingMembers; // Return the list of members renting the movie
        }

        // Method to process borrowing a movie for a member at a given index
        public void BorrowMovie(int memberIndex, Movie movie)
        {
            // Check if the memberIndex is within valid bounds
            if (memberIndex < 0 || memberIndex >= count)
            {
                Console.WriteLine("Invalid member index. Cannot borrow movie.");
                return;
            }
            members[memberIndex].BorrowMovie(movie); // Call the BorrowMovie method on the member object
        }

        // Method to process returning a movie for a member at a given index
        public void ReturnMovie(int memberIndex, Movie movie)
        {
            // Check if the memberIndex is within valid bounds
            if (memberIndex < 0 || memberIndex >= count)
            {
                Console.WriteLine("Invalid member index. Cannot return movie.");
                return;
            }
            members[memberIndex].ReturnMovie(movie); // Call the ReturnMovie method on the member object
        }
    }
}
