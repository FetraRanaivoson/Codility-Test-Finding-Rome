using System;
using System.Collections.Generic;

namespace Priority_Queue_Graph_Class
{
    public enum Heap
    {
        Min, Max
    }
    public class BinaryHeap
    {
        /// <summary>
        /// The root of this binary heap
        /// </summary>
        Node root;

        /// <summary>
        /// Starting from -1, this is the index of the last child node we inserted on the 
        /// node list. Next node will always be inserted at childIndex by default then
        /// can bubble up if violates the rule
        /// </summary>
        int childIndex = -1;

        /// <summary>
        /// Parent index is incremented incremented every 2 node insertions,
        /// to let us know the parent of the child index and therefore 
        /// we can compare the value of those 2
        /// </summary>
        int parentIndex = 0;

        /// <summary>
        /// A BFS list representation of the nodes in the binary heap.
        /// BFS because nodes are filled from left to right
        /// If 'i' is the parent node index, left child index = 2*i +1
        /// and right child index = 2*i +2
        /// </summary>
        List<Node> nodeList;

        public BinaryHeap()
        {
            this.root = null;
            this.nodeList = new List<Node>();
        }

        /// <summary>
        /// Insert this value to this binary heap graph: heap = Max
        /// </summary>
        public void Add(/*int value*/Node node)
        {
            //  Just add the  value first and increment the child Index
            this.nodeList.Add(node);
            this.childIndex++;

            //  Check if we are done with a parent
            if (this.nodeList.Count - 1 > 2 * this.parentIndex + 2)
            {
                this.parentIndex++;
            }

            //  Check if parent value less than the next index then swap if necessary, parent value
            //  being incremented every 2 insertions

            //  The bubbling needs to keep track of the parentIndex of a particular childIndex in the array
            //  If the childIndex is bubbling up, we need also to make sure that every parent of that child
            //  is less in value
            int pI = this.parentIndex;
            int cI = this.childIndex;

            Heap heap = Heap.Max;
            BubbleCheck(pI, cI, heap);

            //Console.WriteLine("{0} added.", node.value);
            //Console.Write("Binary heap array: ");
            //foreach (Node item in this.breadthNodes)
            //{
            //    Console.Write("{0},", item.value);
            //}
            //Console.WriteLine();
            UpdateBinaryHeapGraph();
        }

        /// <summary>
        /// The bubble method
        /// </summary>
        /// <param name="pI">The current parent Index for insertion</param>
        /// <param name="cI">The current child Index of to be inserted </param>
        /// <param name="heap">The heap type: bubbling up Max or bubbling up Min?</param>
        private void BubbleCheck(int pI, int cI, Heap heap)
        {
            if(heap == Heap.Max)
            {
                while (this.nodeList[pI].value < this.nodeList[cI].value)
                {
                    Node smaller = this.nodeList[pI];
                    smaller.left = null; smaller.right = null;
                    this.nodeList[pI] = this.nodeList[cI];
                    this.nodeList[cI] = smaller;

                    //  As soon as the swap occurs
                    cI = pI;

                    // What is the parent index of this childIndex (cI) that has been bubbled up?
                    if (pI >= 1) // There need to be at least 1 parent index to bubble up
                    {
                        if (cI % 2 != 0) // If this child index is odd, this child is on the left of its parent
                        {
                            pI = (cI - 1) / 2; // A formula to get the parentIndex of the its left child
                        }
                        else // Else if this child index is even, this child is on the right of its parent
                        {
                            pI = (cI - 2) / 2;  // A formula to get the parentIndex of the its right child
                        }
                    }
                }
            }
            else
            {
                while (this.nodeList[pI].value > this.nodeList[cI].value)
                {
                    Node smaller = this.nodeList[pI];
                    smaller.left = null; smaller.right = null;
                    this.nodeList[pI] = this.nodeList[cI];
                    this.nodeList[cI] = smaller;

                    //  As soon as the swap occurs
                    cI = pI;

                    // What is the parent index of this childIndex (cI) that has been bubbled up?
                    if (pI >= 1) // There need to be at least 1 parent index to bubble up
                    {
                        if (cI % 2 != 0) // If this child index is odd, this child is on the left of its parent
                        {
                            pI = (cI - 1) / 2; // A formula to get the parentIndex of the its left child
                        }
                        else // Else if this child index is even, this child is on the right of its parent
                        {
                            pI = (cI - 2) / 2;  // A formula to get the parentIndex of the its right child
                        }
                    }
                }
            }
        
        }

        /// <summary>
        /// Update the binary heap graph to handle any changes
        /// </summary>
        public void UpdateBinaryHeapGraph()
        {
            Queue<Node> bfsQueue = new Queue<Node>();
            for (int i = 0; i < nodeList.Count; i++)
            {
                bfsQueue.Enqueue(nodeList[i]);
            }
            Node currentNode = bfsQueue.Dequeue();
            this.root = currentNode;
            Queue<Node> parentNode = new Queue<Node>();

            while (bfsQueue.Count > 0)
            {
                Node left = bfsQueue.Dequeue();
                if (parentNode.Count > 0)
                    currentNode = parentNode.Dequeue();
                if (left != null)
                {
                    currentNode.left = left;
                    if (bfsQueue.Count == 0)
                        return;
                    Node right = bfsQueue.Dequeue();
                    if (right != null)
                    {
                        currentNode.right = right;
                        parentNode.Enqueue(left);
                        parentNode.Enqueue(right);
                    }
                }
            }
        }


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
            Console.WriteLine("==============BINARY HEAP===============");

            Console.Write("Binary heap array: ");
            foreach (Node item in this.nodeList)
            {
                Console.Write("{0},", item.value);
            }
            Console.WriteLine();

            PrintRecursive(this.root);
            Console.WriteLine("===========================================");
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
    public class Node
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

    class Program
    {
        static void Main(string[] args)
        {
            BinaryHeap bh = new BinaryHeap();


            //            10
            //          /    \
            //         6       5
            //        / \     / \
            //       4   2   4   3
            //      / \  /\
            //     1   3 n
            //
            // n = next insertion.
            // n will bubble up as its parent has less value than him

            bh.Add(new Node(5));
            bh.Add(new Node(1));
            bh.Add(new Node(4));
            bh.Add(new Node(2));
            bh.Add(new Node(3));
            bh.Add(new Node(6));

            bh.Add(new Node(3));
            bh.Add(new Node(10));

            bh.Add(new Node(4));

            bh.Print(); //BFS [10,6,5,4,2,4,3,1,3]

            bh.Add(new Node(21)); // n= 21 => BFS [21,10,5,4,6,4,3,1,3,2]
            bh.Print();

            bh.BreadthFirstSearch(); // CQFD BFS [21,10,5,4,6,4,3,1,3,2]


        }
    }
}
