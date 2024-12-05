namespace IFQ664
{
    public class DynamicList<T>
    {
        private T[] items; // Array to store the elements of the list
        private int count; // Current number of elements in the list

        // Constructor to initialize the list with an optional initial capacity (default is 4)
        public DynamicList(int capacity = 4)
        {
            items = new T[capacity]; // Initialize the array with the specified capacity
            count = 0;               // Set the initial count to zero
        }

        // Property to get the number of elements currently in the list
        public int Count
        {
            get { return count; }
        }

        // Method to add a new item to the end of the list
        public void Add(T item)
        {
            // Check if the array is full and needs to be resized
            if (count == items.Length)
            {
                Resize(); // Double the capacity of the array
            }
            items[count++] = item; // Add the item and increment the count
        }

        // Method to remove the first occurrence of a specific item from the list
        public bool Remove(T item)
        {
            int index = IndexOf(item); // Find the index of the item to remove
            if (index >= 0)
            {
                // Shift all elements after the removed item to the left by one position
                for (int i = index; i < count - 1; i++)
                {
                    items[i] = items[i + 1];
                }
                count--; // Decrement the count after removal
                return true; // Indicate that the item was successfully removed
            }
            return false; // Item not found; nothing was removed
        }

        // Method to find the index of a specific item in the list
        public int IndexOf(T item)
        {
            for (int i = 0; i < count; i++)
            {
                // Use Equals method to compare items
                if (items[i].Equals(item))
                {
                    return i; // Return the index if the item is found
                }
            }
            return -1; // Return -1 if the item is not found
        }

        // Method to check if a specific item exists in the list
        public bool Contains(T item)
        {
            return IndexOf(item) >= 0; // Return true if item is found, false otherwise
        }

        // Indexer to allow access to list elements using array-like syntax
        public T this[int index]
        {
            get
            {
                // Check if the index is within the valid range
                if (index < 0 || index >= count)
                {
                    throw new ArgumentOutOfRangeException(); // Throw exception for invalid index
                }
                return items[index]; // Return the item at the specified index
            }
        }

        // Private method to resize the internal array when capacity is reached
        private void Resize()
        {
            T[] newItems = new T[items.Length * 2]; // Create a new array with double the capacity
            Array.Copy(items, newItems, items.Length); // Copy existing items to the new array
            items = newItems; // Replace the old array with the new array
        }
    }
}
