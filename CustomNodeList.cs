namespace IFQ664
{
    // Generic node class for a singly linked list
    public class Node<T>
    {
        public T Data { get; set; }         // The data stored in the node
        public Node<T> Next { get; set; }   // Reference to the next node in the list

        // Constructor to initialize the node with data
        public Node(T data)
        {
            Data = data;
            Next = null; // Initialize the next reference to null
        }
    }

    // Custom generic singly linked list class
    public class CustomNodeList<T>
    {
        private Node<T> head; // Reference to the first node in the list

        // Constructor to initialize an empty list
        public CustomNodeList()
        {
            head = null;
        }

        // Method to add a new node with the specified data at the end of the list
        public void AddLast(T data)
        {
            Node<T> newNode = new Node<T>(data); // Create a new node with the given data
            if (head == null)
            {
                head = newNode; // If the list is empty, set the new node as the head
            }
            else
            {
                Node<T> current = head;
                // Traverse to the end of the list
                while (current.Next != null)
                {
                    current = current.Next;
                }
                current.Next = newNode; // Append the new node at the end
            }
        }

        // Method to remove the first occurrence of a node with the specified data
        public bool Remove(T data)
        {
            if (head == null)
            {
                return false; // List is empty; nothing to remove
            }

            if (head.Data.Equals(data))
            {
                head = head.Next; // Remove the head node if it contains the data
                return true;
            }

            Node<T> current = head;
            // Traverse the list to find the node before the one to remove
            while (current.Next != null && !current.Next.Data.Equals(data))
            {
                current = current.Next;
            }

            if (current.Next == null)
            {
                return false; // Data not found in the list
            }

            // Remove the node by updating the next reference
            current.Next = current.Next.Next;
            return true;
        }

        // Property to get the first node of the list
        public Node<T> First
        {
            get { return head; }
        }

        // Enumerator to allow iteration over the list's data
        public System.Collections.IEnumerator GetEnumerator()
        {
            Node<T> current = head;
            while (current != null)
            {
                yield return current.Data; // Return the data of each node
                current = current.Next;
            }
        }
    }
}
