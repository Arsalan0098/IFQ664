namespace IFQ664
{
    public class Movie
    {
        // Properties to store movie details
        public string Title { get; set; }              // The title of the movie
        public string Genre { get; set; }              // The genre of the movie
        public string Classification { get; set; }     // The classification rating of the movie
        public int DurationInMinutes { get; set; }     // The duration of the movie in minutes
        public int Count { get; set; }                 // The number of copies available in the library
        public int BorrowCount { get; set; }           // The number of times the movie has been borrowed

        // Constructor to initialize a new Movie object with provided details
        public Movie(string title, string genre, string classification, int durationInMinutes, int count)
        {
            Title = title;
            Genre = genre;
            Classification = classification;
            DurationInMinutes = durationInMinutes;
            Count = count;
            BorrowCount = 0; // Initialize borrow count to zero
        }

        // Method to display detailed information about the movie
        public void DisplayMovieInfo()
        {
            Console.WriteLine($"Movie Title: {Title}");
            Console.WriteLine($"Movie Genre: {Genre}");
            Console.WriteLine($"Movie Classification: {Classification}");
            Console.WriteLine($"Movie Duration: {DurationInMinutes} minutes");
            Console.WriteLine($"Number of Copies Available: {Count}");
            Console.WriteLine($"Number of Times Borrowed: {BorrowCount}");
        }

        // Static method to add a new movie to the collection
        public static void AddNewMovie(string title, ref MovieCollection movieCollection)
        {
            // Variable to store the classification selected by the user
            string classification = string.Empty;
            int choiceC;

            // Loop to prompt user for movie classification until a valid choice is made
            do
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Please choose one of the options below:");
                Console.WriteLine("1. drama");
                Console.WriteLine("2. adventure");
                Console.WriteLine("3. family");
                Console.WriteLine("4. action");
                Console.WriteLine("5. sci-fi");
                Console.WriteLine("6. comedy");
                Console.WriteLine("7. animated");
                Console.WriteLine("8. thriller");
                Console.WriteLine("9. Other");
                Console.Write("Your selection ==> ");

                // Attempt to parse user input to an integer
                if (int.TryParse(Console.ReadLine(), out choiceC))
                {
                    // Assign classification based on user's choice
                    switch (choiceC)
                    {
                        case 1:
                            classification = "drama";
                            break;
                        case 2:
                            classification = "adventure";
                            break;
                        case 3:
                            classification = "family";
                            break;
                        case 4:
                            classification = "action";
                            break;
                        case 5:
                            classification = "sci-fi";
                            break;
                        case 6:
                            classification = "comedy";
                            break;
                        case 7:
                            classification = "animated";
                            break;
                        case 8:
                            classification = "thriller";
                            break;
                        case 9:
                            classification = "Other";
                            break;
                        default:
                            Console.WriteLine("That option is invalid. Please select a valid choice.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Input is not valid. Please provide a numeric value.");
                }

            } while (choiceC < 1 || choiceC > 9);

            // Variable to store the genre selected by the user
            string genre = string.Empty;
            int choice;

            // Loop to prompt user for movie genre until a valid choice is made
            do
            {
                Console.WriteLine("---------------------");
                Console.WriteLine("Please select one of the following options:");
                Console.WriteLine("1. General (G)");
                Console.WriteLine("2. Parental Guidance (PG)");
                Console.WriteLine("3. Mature (M15+)");
                Console.WriteLine("4. Mature Accompanied (MA15+)");
                Console.Write("Your selection ==> ");

                // Attempt to parse user input to an integer
                if (int.TryParse(Console.ReadLine(), out choice))
                {
                    // Assign genre based on user's choice
                    switch (choice)
                    {
                        case 1:
                            genre = "G";
                            break;
                        case 2:
                            genre = "PG";
                            break;
                        case 3:
                            genre = "M15+";
                            break;
                        case 4:
                            genre = "MA15+";
                            break;
                        default:
                            Console.WriteLine("That choice is invalid. Please try again.");
                            break;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a numerical value.");
                }

            } while (choice < 1 || choice > 4);

            // Prompt user to enter the duration of the movie
            int duration;
            do
            {
                Console.Write("Please enter the duration in minutes: ");
            } while (!int.TryParse(Console.ReadLine(), out duration));

            // Prompt user to enter the number of copies available
            int count;
            do
            {
                Console.Write("Please specify the number of copies: ");
            } while (!int.TryParse(Console.ReadLine(), out count));

            // Create a new Movie object with the provided information
            Movie newMovie = new Movie(title, genre, classification, duration, count);

            // Add the new movie to the movie collection
            movieCollection.AddMovie(title, newMovie);
        }

        // Static method to add movie DVDs to the collection
        public static void AddMovieDVDs(ref MovieCollection movieCollection)
        {
            Console.Write("Please enter the title of the movie: ");
            string title = Console.ReadLine();

            // Check if the movie already exists in the collection
            Movie existingMovie = movieCollection.GetMovie(title);
            if (existingMovie != null)
            {
                Console.Write("Please specify how many new copies to add: ");
                if (int.TryParse(Console.ReadLine(), out int newCopies))
                {
                    // Update the count of available copies
                    existingMovie.Count += newCopies;
                    Console.WriteLine($"Successfully added {newCopies} new copies of '{title}' to the collection.");
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the number of copies.");
                }
            }
            else
            {
                // If the movie doesn't exist, prompt to add it as a new movie
                AddNewMovie(title, ref movieCollection);
            }
        }

        // Static method to remove movie DVDs from the collection
        public static void RemoveMovieDVDs(ref MovieCollection movieCollection)
        {
            Console.Write("Please enter the title of the movie: ");
            string title = Console.ReadLine();

            // Check if the movie exists in the collection
            Movie existingMovie = movieCollection.GetMovie(title);
            if (existingMovie != null)
            {
                Console.Write("Please specify the number of DVDs to remove: ");
                if (int.TryParse(Console.ReadLine(), out int removeCount))
                {
                    if (existingMovie.Count - removeCount > 0)
                    {
                        // Decrease the count of available copies
                        existingMovie.Count -= removeCount;
                        Console.WriteLine($"Removed {removeCount} DVDs of '{title}' from the collection.");
                    }
                    else if (existingMovie.Count == removeCount)
                    {
                        // Remove the movie entirely if all copies are removed
                        movieCollection.RemoveMovie(title);
                        Console.WriteLine($"All copies of '{title}' have been removed from the collection.");
                    }
                    else
                    {
                        Console.WriteLine("Unable to remove DVDs. The number specified exceeds available copies.");
                    }
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer for the number of DVDs to remove.");
                }
            }
            else
            {
                Console.WriteLine($"The movie '{title}' does not exist in the system.");
            }
        }

        // Static method to display all movies in the collection
        public static void DisplayAllMovies(ref MovieCollection movieCollection)
        {
            movieCollection.DisplayMovies();
        }

        // Static method to display detailed information about a specific movie
        public static void DisplayMovieInfo(ref MovieCollection movieCollection)
        {
            Console.Write("Please enter the movie title: ");
            string title = Console.ReadLine();
            movieCollection.DisplayMovieInfo(title);
        }

        // Static method to allow a member to borrow a movie DVD
        public static void BorrowMovieDVD(ref MemberCollection memberCollection, ref MovieCollection movieCollection, int curMemberIndex)
        {
            Console.Write("Please enter the title of the movie DVD you wish to borrow: ");
            string title = Console.ReadLine();
            movieCollection.BorrowMovieDVD(title, ref memberCollection, ref movieCollection, curMemberIndex);
        }

        // Static method to allow a member to return a borrowed movie DVD
        public static void ReturnMovieDVD(ref MemberCollection memberCollection, ref MovieCollection movieCollection, int curMemberIndex)
        {
            Console.Write("Please enter the title of the movie DVD you wish to return: ");
            string title = Console.ReadLine();
            movieCollection.ReturnMovieDVD(title, ref memberCollection, ref movieCollection, curMemberIndex);
        }

        // Static method to list all movies currently borrowed by a member
        public static void ListBorrowedMovies(MovieCollection movieCollection, MemberCollection memberCollection, int curMemberIndex)
        {
            movieCollection.ListBorrowedMovies(curMemberIndex, memberCollection);
        }

        // Static method to display the top three most frequently borrowed movies
        public static void DisplayTopThreeMovies(MovieCollection movieCollection)
        {
            movieCollection.DisplayTopThreeMostFrequentMovies();
        }
    }
}
