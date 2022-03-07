using System;

namespace Binary_Tree
{
    /// <summary>
    /// A node prototype for binary trees
    /// </summary>
    class Node
    {
        public int value;
        public Node left;
        public Node right;
        public bool HasNoChild => this.left == null && this.right == null;
        public bool Full => this.left != null && this.right != null;
        public Node(int value)
        {
            this.value = value;
            this.left = null;
            this.right = null;
        }

    }

    /// <summary>
    /// Binary Search Tree class
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class BinarySearchTree
    {
        Node root;

        public BinarySearchTree()
        {
            this.root = null;
        }

        public void Insert(int value)
        {
            Traverse(ref this.root, value);
        }

        private static void Traverse(ref Node currentNode, int value)
        {
            //  Traversing current node
            if (currentNode == null)
            {
                currentNode = new Node(value);
            }
            else
            {
                //  CASE NODE HAS NO CHILDREN
                if (currentNode.HasNoChild)
                {
                    //  CASE SHOULD GO LEFT
                    if (value < currentNode.value)
                    {
                        currentNode.left = new Node(value);
                    }
                    //  CASE SHOULD GO RIGHT
                    else
                    {
                        currentNode.right = new Node(value);
                    }
                }
                else
                {
                    //  NODE HAS 2 CHILDREN
                    if (currentNode.Full)
                    {
                        if (value < currentNode.left.value)
                        {
                            Traverse(ref currentNode.left, value);
                        }
                        else
                        {
                            Traverse(ref currentNode.right, value);
                        }
                    }
                    //  NODE HAS 1 CHILDREN
                    else
                    {
                        if (currentNode.left != null)
                        {
                            if (value < currentNode.left.value)
                                Traverse(ref currentNode.left.left, value);
                            else
                            {
                                Traverse(ref currentNode.left.right, value);
                            }
                        }
                        else if (currentNode.right != null)
                        {
                            if (value < currentNode.right.value)
                                Traverse(ref currentNode.right.right, value);
                            else
                            {
                                Traverse(ref currentNode.right.left, value);
                            }
                        }
                    }
                }
            }
        }

        public Node Lookup(int value)
        {
            return null;
        }

        public void Print()
        {

        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            BinarySearchTree tree = new BinarySearchTree();
            tree.Insert(9);
            tree.Insert(4);
            tree.Insert(6);
            tree.Insert(20);
            tree.Insert(170);
            tree.Insert(15);
            tree.Insert(1);

        }
    }
}
