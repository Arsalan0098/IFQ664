namespace IFQ664
{
    public class MovieCollection
    {
        private const int MaxSize = 1000; // Maximum number of movies allowed in the collection
        private const int ArraySize = 1000; // Size of the hash table array

        private CustomNodeList<Movie>[] hashTable; // Hash table to store movies using chaining
        private int movieCount; // Current number of movies in the collection

        // Constructor to initialize the hash table and set the movie count to zero
        public MovieCollection()
        {
            hashTable = new CustomNodeList<Movie>[ArraySize];
            for (int i = 0; i < ArraySize; i++)
            {
                hashTable[i] = new CustomNodeList<Movie>(); // Initialize each linked list in the hash table
            }
            movieCount = 0;
        }

        // Hash function to compute an index for a given movie title using the djb2 algorithm
        private int HashFunction(string title)
        {
            ulong hash = 5381;
            foreach (char c in title)
            {
                hash = ((hash << 5) + hash) + c; // Equivalent to hash * 33 + c
            }
            return (int)(hash % (ulong)ArraySize); // Ensure the hash index is within the array bounds
        }

        // Method to add a new movie to the collection
        public void AddMovie(string title, Movie movie)
        {
            // Check if the movie already exists in the collection
            if (GetMovie(title) != null)
            {
                Console.WriteLine($"Movie '{title}' already exists in the collection.");
                return;
            }

            // Check if the collection has reached its maximum capacity
            if (movieCount >= MaxSize)
            {
                Console.WriteLine("Cannot add movie: the collection is at full capacity.");
                return;
            }

            int index = HashFunction(title); // Compute the hash index for the movie title
            hashTable[index].AddLast(movie); // Add the movie to the linked list at the computed index
            movieCount++; // Increment the movie count
            Console.WriteLine($"Successfully added '{title}' to the movie collection.");
        }

        // Method to remove a movie from the collection by title
        public void RemoveMovie(string title)
        {
            int index = HashFunction(title); // Compute the hash index for the movie title
            var movieNode = hashTable[index].First; // Get the first node in the linked list at that index
            while (movieNode != null)
            {
                // Check if the current node's movie title matches the one to remove
                if (movieNode.Data.Title == title)
                {
                    hashTable[index].Remove(movieNode.Data); // Remove the movie from the linked list
                    movieCount--; // Decrement the movie count
                    Console.WriteLine($"'{title}' has been removed from the movie collection.");
                    return;
                }
                movieNode = movieNode.Next; // Move to the next node in the linked list
            }
            Console.WriteLine($"Could not find a movie titled '{title}' in the collection.");
        }

        // Method to retrieve a movie from the collection by title
        public Movie GetMovie(string title)
        {
            int index = HashFunction(title); // Compute the hash index for the movie title
            Node<Movie> currentNode = hashTable[index].First; // Start at the first node in the linked list
            while (currentNode != null)
            {
                // Check if the current node's movie title matches
                if (currentNode.Data.Title == title)
                {
                    return currentNode.Data; // Return the found movie
                }
                currentNode = currentNode.Next; // Move to the next node
            }
            Console.WriteLine($"Movie titled '{title}' does not exist in the collection.");
            return null; // Movie not found
        }

        // Method to display all movies in the collection
        public void DisplayMovies()
        {
            Console.WriteLine("=== Movie Collection ===");
            bool foundAny = false; // Flag to check if any movies are found
            foreach (var linkedList in hashTable)
            {
                Node<Movie> currentNode = linkedList.First; // Get the first node in the linked list
                while (currentNode != null)
                {
                    foundAny = true; // At least one movie is found
                    currentNode.Data.DisplayMovieInfo(); // Display the movie's information
                    Console.WriteLine("---------------------------");
                    currentNode = currentNode.Next; // Move to the next node
                }
            }

            if (!foundAny)
            {
                Console.WriteLine("There are currently no movies in the collection.");
            }
        }

        // Method to display information about a specific movie by title
        public void DisplayMovieInfo(string movieTitle)
        {
            Movie movie = GetMovie(movieTitle); // Retrieve the movie from the collection
            if (movie != null)
            {
                movie.DisplayMovieInfo(); // Display the movie's information
            }
        }

        // Method to handle borrowing a movie DVD for a member
        public void BorrowMovieDVD(string title, ref MemberCollection memberCollection, ref MovieCollection movieCollection, int curMemberIndex)
        {
            Movie movie = movieCollection.GetMovie(title); // Retrieve the movie from the collection

            // Check if the member has already borrowed this movie
            if (memberCollection.GetMember(curMemberIndex).HasBorrowedMovie(title))
            {
                Console.WriteLine($"You have already borrowed '{title}'.");
                return;
            }

            // Check if the member has reached the borrowing limit
            if (memberCollection.GetMember(curMemberIndex).borrowedMovies.Count >= 5)
            {
                Console.WriteLine("Members can borrow up to five movies only.");
                return;
            }

            if (movie != null)
            {
                if (movie.Count > 0)
                {
                    memberCollection.BorrowMovie(curMemberIndex, movie); // Add the movie to the member's borrowed list
                    movie.Count--; // Decrease the available count of the movie
                    movie.BorrowCount++; // Increase the borrow count for popularity tracking
                }
                else
                {
                    Console.WriteLine($"All copies of '{title}' are currently checked out.");
                }
            }
            else
            {
                Console.WriteLine($"'{title}' is not available in the library.");
            }
        }

        // Method to handle returning a borrowed movie DVD by a member
        public void ReturnMovieDVD(string title, ref MemberCollection memberCollection, ref MovieCollection movieCollection, int curMemberIndex)
        {
            Movie movie = movieCollection.GetMovie(title); // Retrieve the movie from the collection
            if (movie != null)
            {
                memberCollection.ReturnMovie(curMemberIndex, movie); // Remove the movie from the member's borrowed list
                movie.Count++; // Increase the available count of the movie
            }
            else
            {
                Console.WriteLine($"'{title}' is not available in the library.");
            }
        }

        // Method to list all movies borrowed by a specific member
        public void ListBorrowedMovies(int curMemberIndex, MemberCollection memberCollection)
        {
            Member member = memberCollection.GetMember(curMemberIndex); // Get the member from the collection
            member.DisplayBorrowedMovies(); // Display the member's borrowed movies
        }

        // Method to display the top three most frequently borrowed movies
        public void DisplayTopThreeMostFrequentMovies()
        {
            var allMovies = new List<Movie>(); // List to hold all movies

            // Collect all movies from the hash table
            foreach (var linkedList in hashTable)
            {
                Node<Movie> currentNode = linkedList.First;
                while (currentNode != null)
                {
                    allMovies.Add(currentNode.Data); // Add movie to the list
                    currentNode = currentNode.Next;
                }
            }

            // Order movies by borrow count in descending order and take the top three
            var topThreeMovies = allMovies.OrderByDescending(m => m.BorrowCount).Take(3);

            Console.WriteLine("Top 3 Most Frequently Borrowed Movies:");
            foreach (var movie in topThreeMovies)
            {
                Console.WriteLine($"Title: {movie.Title}, Borrowed: {movie.BorrowCount} times");
            }
        }
    }
}
