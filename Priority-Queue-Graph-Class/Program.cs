using System;
using System.Collections.Generic;

namespace Priority_Queue_Graph_Class
{
    public class BinaryHeap
    {
        Node root;
        List<Node> nodes;

        int nextIndex = -1;
        int parentIndex = 0;

        /// <summary>
        /// Given this list of BFS parkour, we can map
        /// each parent left-right children.
        /// If i is the parent node index, left child index = 2*i +1
        /// and right child index = 2*i +2
        /// </summary>
        List<int> breadthIndex;

        public BinaryHeap()
        {
            this.root = null;
            this.breadthIndex = new List<int>();
            this.nodes = new List<Node>();
        }

        /// <summary>
        /// Insert this value to this binary heap graph: heap = Max
        /// </summary>
        public void Insert(int value)
        {
            //  Just add the  value first and increment the current Index
            this.breadthIndex.Add(value);
            this.nextIndex++;

            //  Check if we are done with a parent
            if (this.breadthIndex.Count - 1 > 2 * this.parentIndex + 2)
            {
                this.parentIndex++;
            }

            //  Check if parent value less than the next index then swap if necessary, parent value
            //  being incremented every 2 insertions

            int pI = this.parentIndex; //We will just make a copy of the parentIndex because we have to keep it
            bool swap = false;
            while (this.breadthIndex[pI] < this.breadthIndex[this.nextIndex])
            {
                swap = true;
                int smaller = this.breadthIndex[pI];
                this.breadthIndex[pI] = this.breadthIndex[this.nextIndex];
                this.breadthIndex[this.nextIndex] = smaller;
                if (this.nextIndex > 2)
                    pI = this.nextIndex - 2; //Bubbling up

            }

            if (this.nextIndex == 0)
            {
                nodes.Add(new Node(this.breadthIndex[nextIndex]));
            }
            else
            {
                if (swap)
                    nodes.Add(new Node(this.breadthIndex[nextIndex], new Node(this.breadthIndex[parentIndex])));
            }

            ////  See the result
            //for (int i = 0; i < nodes.Count; i++)
            //{
            //    if (i == 0)
            //    {
            //        Console.WriteLine("Node: {0} ---> Parent: Null (Root)", nodes[i].value);

            //    }
            //    else
            //        Console.WriteLine("Node: {0} ---> Parent: {1}", nodes[i].value, nodes[i].parent.value);
            //}
            //Console.WriteLine("----------------------------");
        }

        //private void UpdateBinaryHeapGraph()
        //{
        //    Queue<Node> queue = new Queue<Node>();
        //    this.root = new Node(this.breadthIndex[0], null);
        //    if(breadthIndex.Count == 2)
        //    {
        //        this.root.left = new Node(breadthIndex[2 * 0 + 1], this.root);
        //    }

        //    else if (breadthIndex.Count == 3)
        //    {
        //        this.root.left = new Node(breadthIndex[2 * 0 + 1], this.root);
        //        this.root.right = new Node(breadthIndex[2 * 0 + 2], this.root);
        //    }
        //    else
        //    {
        //        queue.Enqueue(this.root.left);
        //        queue.Enqueue(this.root.right);
        //        int index = 0;
        //        while (queue.Count > 0 && index < breadthIndex.Count - 1)
        //        {
        //            Node currentNode = queue.Dequeue();
        //            if (breadthIndex.Count - 1 >= 2 * index + 2)
        //            {
        //                currentNode.left = new Node(breadthIndex[2 * index + 1], currentNode);
        //                currentNode.right = new Node(breadthIndex[2 * index + 2], currentNode);
        //                queue.Enqueue(currentNode.left);
        //                queue.Enqueue(currentNode.right);
        //            }
        //            index++;
        //        }
        //    }

        //    Print();
        //}


        /// <summary>
        /// Tried to insert value with swap feature on a binary heap graph but the tree became inbalanced
        /// </summary>
        //private void BinaryHeapInsertDFS(Node currentNode, int value)
        //{
        //    if (this.root == null)
        //    {
        //        this.root = new Node(value, null);
        //        Print();
        //        return;
        //    }
        //
        //    else if (currentNode.left == null) //   Insertion is at the DFS first parent that has no left child
        //    {
        //        if (currentNode.value >= value)
        //        {
        //            currentNode.left = new Node(value, currentNode);
        //            Print();
        //            return;
        //        }
        //        else if (currentNode.value < value) //   Rule violation => swap
        //        {
        //            while (currentNode.value < value)
        //            {
        //                int parentValue = currentNode.value;
        //                currentNode.value = value;
        //                currentNode.left = new Node(parentValue, currentNode);
        //                currentNode = currentNode.parent;
        //                if (currentNode == null)//root
        //                {
        //                    this.root = currentNode;
        //                    break;
        //                }
        //            }
        //            Print();
        //            return;
        //        }
        //    }
        //
        //    else if (currentNode.right == null) // or insertion at right child
        //    {
        //        if (currentNode.value >= value)
        //        {
        //            currentNode.right = new Node(value, currentNode);
        //            Print();
        //            return;
        //        }
        //        else if (currentNode.value < value) //   Rule violation => swap
        //        {
        //            while (currentNode.value < value)
        //            {
        //                int parentValue = currentNode.value;
        //                currentNode.value = value;
        //                currentNode.right = new Node(parentValue, currentNode);
        //                currentNode = currentNode.parent;
        //                if (currentNode == null)//root
        //                {
        //                    this.root = currentNode;
        //                    break;
        //                }
        //            }
        //
        //            Print();
        //            return;
        //        }
        //    }
        //
        //    else // Otherwise find deeply the insertion point (DFS)
        //    {
        //        if (currentNode.left != null)
        //        {
        //            BinaryHeapInsertDFS(currentNode.left, value);
        //        }
        //        else if (currentNode.right != null)
        //        {
        //            BinaryHeapInsertDFS(currentNode.right, value);
        //        }
        //    }
        //
        //
        //    /*
        //    Queue<Node> queue = new Queue<Node>(); //FIFO: we'll put in the left node first then the right node so we'll always go from left to right first
        //    queue.Enqueue(currentNode);
        //
        //    while (queue.Count > 0)
        //    {
        //        currentNode = queue.Dequeue();
        //        Node parent = currentNode.parent;
        //        if (currentNode.value < value)
        //        {
        //            Node temp = currentNode;
        //            Node priorityNode = null;
        //            if (parent == null) // case: root node
        //            {
        //                priorityNode = new Node(value, null);
        //                this.root = priorityNode;
        //                this.root.left = temp;
        //            }
        //            else // case: not root node
        //            {
        //                priorityNode = new Node(value, parent);
        //                priorityNode.left = temp;
        //                parent.left = priorityNode;
        //                priorityNode.left.parent = priorityNode;
        //                
        //            }                
        //            break;
        //        }
        //
        //        if (currentNode.left == null)
        //        {
        //            currentNode.left = new Node(value, currentNode);
        //            currentNode.left.parent = currentNode;
        //            //queue.Enqueue(currentNode.left);
        //        }
        //        else if (currentNode.right == null)
        //        {
        //            currentNode.right = new Node(value, currentNode);
        //            currentNode.right.parent = currentNode;
        //            //queue.Enqueue(currentNode.right);
        //        }
        //        else
        //        {
        //            queue.Enqueue(currentNode.left);
        //        }
        //    }
        //    */
        //}

        /// <summary>
        /// Remove a value from the tree
        /// </summary>
        public void Remove(int value)
        {
            if (this.root == null)
            {
                Console.WriteLine("Failed to remove {0}, no root found", value);
            }
            else
            {
                Node currentNode = this.root;
                Node parentNode = null;
                while (currentNode != null)
                {
                    if (value < currentNode.value)
                    {
                        parentNode = currentNode;
                        currentNode = currentNode.left;
                    }
                    else if (value > currentNode.value)
                    {
                        parentNode = currentNode;
                        currentNode = currentNode.right;
                    }
                    else if (currentNode.value == value) //Match
                    {
                        //Case 1: No right child
                        if (currentNode.right == null)
                        {
                            if (parentNode == null) // case: deleting the root
                            {
                                this.root = currentNode.left;
                            }
                            else //Is the value to delete is less/greater than the parent value?
                            {
                                //  If parent value greater than current node value, then
                                //  make the current node left child a child of the parent
                                if (currentNode.value < parentNode.value)
                                {
                                    //parentNode.left = currentNode.left; //act of nullifying the current node
                                    parentNode.left = null;
                                    currentNode = parentNode.left;
                                }
                                //  Else if parent value less than current value, then
                                //  make the current node left child  a child of the right parent
                                else if (currentNode.value > parentNode.value)
                                {
                                    //parentNode.right = currentNode.left;
                                    parentNode.right = null;
                                    currentNode = parentNode.left;
                                }
                            }
                        }
                        //Case 2: Right child doesnt have a left child
                        else if (currentNode.right.left == null)
                        {
                            currentNode.right.left = currentNode.left;
                            if (parentNode == null)
                            {
                                this.root = currentNode.right;
                            }
                            else
                            {
                                //if parent > current, make right child of the left the parent
                                if (currentNode.value < parentNode.value)
                                {
                                    parentNode.left = currentNode.right;
                                    //parentNode.left = null;
                                    //currentNode = parentNode.right;
                                }
                                //if parent < current, make right child a right child of the parent
                                else if (currentNode.value > parentNode.value)
                                {
                                    parentNode.right = currentNode.right;
                                }
                            }
                        }
                        //Option 3: Right child that has a left child
                        else
                        {

                            //find the Right child's left most child
                            Node leftmost = currentNode.right.left;
                            Node leftmostParent = currentNode.right;
                            while (leftmost.left != null)
                            {
                                leftmostParent = leftmost;
                                leftmost = leftmost.left;
                            }

                            //Parent's left subtree is now leftmost's right subtree
                            leftmostParent.left = leftmost.right;
                            leftmost.left = currentNode.left;
                            leftmost.right = currentNode.right;

                            if (parentNode == null)
                            {
                                this.root = leftmost;
                            }
                            else
                            {
                                if (currentNode.value < parentNode.value)
                                {
                                    parentNode.left = leftmost;
                                }
                                else if (currentNode.value > parentNode.value)
                                {
                                    parentNode.right = leftmost;
                                }
                            }
                        }
                    }
                }
            }
            //Console.WriteLine("==============================");
        }


        /// <summary>
        /// Breadth First Search Algorithm.
        /// Searching broader first (left to right) then deeper (down)
        /// NB: the memory consumption for the queue can be very large if the tree is very wide
        /// </summary>
        public List<int> BreadthFirstSearch()
        {
            Node currentNode = this.root;
            List<int> list = new List<int>();
            Queue<Node> queue = new Queue<Node>(); //FIFO: we'll put in the left node first then the right node so we'll always go from left to right first
            queue.Enqueue(currentNode);

            //Console.WriteLine("BFS visit order: ");
            while (queue.Count > 0)
            {
                currentNode = queue.Dequeue(); //Overwrite current node. At the 1st iteration, the current node will be the root. Later it will be the 'First In' on the queue etc
                list.Add(currentNode.value);
                //Console.WriteLine("-> {0}", currentNode.value);
                if (currentNode.left != null)
                {
                    queue.Enqueue(currentNode.left);
                }
                if (currentNode.right != null)
                {
                    queue.Enqueue(currentNode.right);
                }
            }
            Console.Write("BFS visit order Normal method: ");
            Console.WriteLine(string.Join(", ", list));
            return list;
        }

        /// <summary>
        /// Breadth First Search Algorithm (Recursive method)
        /// </summary>
        public List<int> BreadthFirstSearchR()
        {
            Queue<Node> queue = new Queue<Node>();
            List<int> list = new List<int>();
            queue.Enqueue(this.root);
            return BreadthFirstSearchRecursive(queue, list);
        }

        private List<int> BreadthFirstSearchRecursive(Queue<Node> queue, List<int> list)
        {
            if (queue.Count == 0)
            {
                Console.Write("BFS visit order Recursive method: ");
                Console.WriteLine(string.Join(", ", list));
                return list;
            }
            Node currentNode = queue.Dequeue();
            list.Add(currentNode.value);
            if (currentNode.left != null)
            {
                queue.Enqueue(currentNode.left);
            }
            if (currentNode.right != null)
            {
                queue.Enqueue(currentNode.right);
            }
            return BreadthFirstSearchRecursive(queue, list);
        }

        /// <summary>
        /// In order Depth first search algorithm (Recursive approach).
        /// Going as deep as possible to the left first then wider to the right
        /// </summary>
        ///<returns>Returns the ordered list of visits in a DFS InOrder method</returns>
        public List<int> DFSInOrder()
        {
            Node currentNode = this.root;
            List<int> list = new List<int>();
            list = DFSInOrderRecursive(currentNode, list);

            Console.Write("DFS InOrder visit: ");
            Console.WriteLine(string.Join(", ", list));
            return list;
        }
        private List<int> DFSInOrderRecursive(Node currentNode, List<int> list)
        {
            if (currentNode.left != null)
            {
                DFSInOrderRecursive(currentNode.left, list); //Going deeper to the left until there is no more children
            }
            // No more node?
            list.Add(currentNode.value);

            if (currentNode.right != null) //Going deeper to the right until there is no more children
            {
                DFSInOrderRecursive(currentNode.right, list);
            }
            return list; //If there is no more depth to go, return the list but also return the last common ancestor 
        }

        /// <summary>
        /// Pre order Depth first search algorithm (Recursive approach).
        /// The order is we start FROM the PARENT FIRST. Useful when you want to recreate a tree because the tree is ordered. 
        /// </summary>
        /// <returns>Returns the ordered list of visits in a DFS PreOrder method ie STARTING FROM A PARENT FIRST.</returns>
        public List<int> DFSPreOrder()
        {
            Node currentNode = this.root;
            List<int> list = new List<int>();
            list = DFSPreOrderRecursive(currentNode, list);

            Console.Write("DFS PreOrder visit: ");
            Console.WriteLine(string.Join(", ", list));
            return list;
        }
        private List<int> DFSPreOrderRecursive(Node currentNode, List<int> list)
        {
            //The only difference from InOrder is you want to push at the very beginning before we get to the left node
            list.Add(currentNode.value);

            if (currentNode.left != null)
            {
                DFSPreOrderRecursive(currentNode.left, list); //Going deeper to the left until there is no more children
            }
            // No more node?
            //list.Add(currentNode.value);

            if (currentNode.right != null) //Going deeper to the right until there is no more children
            {
                DFSPreOrderRecursive(currentNode.right, list);
            }
            return list; //If there is no more depth to go, return the list but also return the last common ancestor 
        }

        /// <summary>
        /// Post order Depth first search algorithm (Recursive approach).
        /// The order is we start from the LEAF NODES (deep) from left to right then GO UP to a parent
        /// </summary>
        /// <returns>Returns the ordered list of visits in a DFS PostOrder method ie LEAFS FIRST THEN PARENT</returns>
        public List<int> DFSPostOrder()
        {
            Node currentNode = this.root;
            List<int> list = new List<int>();
            list = DFSPostOrderRecursive(currentNode, list);

            Console.Write("DFS PostOrder visit: ");
            Console.WriteLine(string.Join(", ", list));
            return list;
        }
        private List<int> DFSPostOrderRecursive(Node currentNode, List<int> list)
        {
            if (currentNode.left != null)
            {
                DFSPostOrderRecursive(currentNode.left, list); //Going deeper to the left until there is no more children
            }

            if (currentNode.right != null) //Going deeper to the right until there is no more children
            {
                DFSPostOrderRecursive(currentNode.right, list);
            }

            //Push when there is no left and right child anymore ie push the parent lastly
            list.Add(currentNode.value);

            return list; //If there is no more depth to go, return the list but also return the last common ancestor 
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"> value to search</param>
        /// <returns>returns the value if found, else returns -1</returns>
        public int Lookup(int value)
        {
            if (this.root == null)
            {
                Console.WriteLine("Cannot find {0}, tree has not root.", value);
                return -1;
            }
            Node currentNode = this.root;
            while (true)
            {
                if (currentNode == null)//At the end of a node (left/right are null)
                {
                    Console.WriteLine("Entry {0} not found!", value);
                    return -1;
                }
                if (value == currentNode.value)
                {
                    Console.WriteLine("Entry {0} found!", value);
                    return currentNode.value;
                }
                else
                {
                    if (value < currentNode.value)
                    {
                        currentNode = currentNode.left;
                    }
                    else if (value > currentNode.value)
                    {
                        currentNode = currentNode.right;
                    }
                    //else
                    //{
                    //    Console.WriteLine("Entry {0} not found", value);
                    //    return -1;
                    //}
                }
            }
        }

        /// <summary>
        /// Print every node and their connections (if any)
        /// </summary>
        public void Print()
        {
            Console.WriteLine("==============PRIORITY QUEUE===============");
            PrintRecursive(this.root);
            Console.WriteLine("========================================");
        }
        private void PrintRecursive(Node currentNode)
        {
            //  Base case
            //  if(currentNode == null){ do nothing because nothing to print)

            //  Recursive case
            if (currentNode != null)
            {
                if (currentNode.left != null && currentNode.right != null)
                {
                    Console.WriteLine("{0}--> L:{1}, R:{2}", currentNode.value, currentNode.left.value, currentNode.right.value);
                    PrintRecursive(currentNode.left);
                    PrintRecursive(currentNode.right);
                }
                else if (currentNode.left != null && currentNode.right == null)
                {
                    Console.WriteLine("{0}--> L:{1}, R:{2}", currentNode.value, currentNode.left.value, "Empty");
                    PrintRecursive(currentNode.left);
                }
                else if (currentNode.left == null && currentNode.right != null)
                {
                    Console.WriteLine("{0}--> L:{1}, R:{2}", currentNode.value, "Empty", currentNode.right.value);
                    PrintRecursive(currentNode.right);
                }
                else
                {
                    //Console.WriteLine("{0}--> L:{1}, R:{2}", currentNode.value, "Empty", "Empty");
                    Console.WriteLine("{0}--> Leaf", currentNode.value);
                }
            }

        }
    }

    /// <summary>
    /// A node prototype for binary trees
    /// </summary>
    class Node
    {
        public int value;
        public Node left;
        public Node right;
        public Node parent;
        public List<Node> children;
        public bool HasNoChild => this.left == null && this.right == null;
        public bool Full => this.left != null && this.right != null;
        public bool IsLeftNull => this.left == null;
        public bool isRightNull => this.right == null;

        public Node(int value)
        {
            this.value = value;
            this.left = null;
            this.right = null;
            //this.children = new List<Node>();
        }

        public Node(int value, Node parent)
        {
            this.value = value;
            this.left = null;
            this.right = null;
            this.parent = parent;
            this.children = new List<Node>();
            //parent.children.Add(this);

        }


    }
    /// <summary>
    /// A vertex (Node) prototype for graphs that implement BFS and DFS
    /// </summary>


    public class Vertex
    {
        /// <summary>
        /// The value of this vertex
        /// </summary>
        public int value;

        /// <summary>
        /// The keyvalue pair int,Vertex connections of this vertex
        /// </summary>
        public Dictionary<int, Vertex> connections;

        /// <summary>
        /// Is this vertex already visited?
        /// </summary>
        public bool IsVisited { get; set; }


        public Vertex(int value)
        {
            this.value = value;
            this.IsVisited = false;
            this.connections = new Dictionary<int, Vertex>();
        }

        /// <summary>
        /// Add a connection using key value pair <int,Vertex> to the dictionary
        /// </summary>
        public void AddConn(int key, Vertex vertex)
        {
            connections.Add(key, vertex);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            BinaryHeap bh = new BinaryHeap();

            bh.Insert(5);
            Console.ReadKey();
            bh.Insert(1);
            Console.ReadKey();
            bh.Insert(4);
            Console.ReadKey();
            bh.Insert(2);
            Console.ReadKey();
            bh.Insert(3);
            Console.ReadKey();
            bh.Insert(6);
            Console.ReadKey();

        }
    }
}
