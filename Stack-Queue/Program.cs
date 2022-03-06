using System;

namespace Stack_Queue
{
    /// <summary>
    /// A node prototype like in Linked lists
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class Node<T>
    {
        public T value;
        public Node<T> next;
        public Node(T value)
        {
            this.value = value;
            this.next = null;
        }
    }
    /// <summary>
    /// A Stack class using the system of Linked lists
    /// </summary>
    /// <typeparam name="T"></typeparam>
    class MyStack<T> where T: default
    {
        Node<T> top;
        Node<T> bottom;
        int length;

        public MyStack(T value)
        {
            Node<T> node = new Node<T>(value);
            this.top = node;
            this.bottom = node;
            this.length++;
        }

        /// <summary>
        /// Add a value on top of the stack
        /// </summary>
        /// <param name="value"></param>
        public void Push(T value)
        {
            Node<T> node = new Node<T>(value);
            if (this.length == 0)
            {
                this.top = node;
                this.bottom = node;
            }
            else
            {
                Node<T> oldTop = this.top; //Necessary: c# has a garbage collector so store this.top before overwrite
                this.top = node; //the top ref is then replaced with the new node
                this.top.next = oldTop; //then the next node will be the old top (because stack)
            }
            this.length++;
        }

        /// <summary>
        /// Remove the top element
        /// </summary>
        /// <param name="value"></param>
        public void Pop()
        {
            if(this.top == null)
            {
                return;
            }
            //Node<T> oldTop = this.top;
            this.top = this.top.next;
            this.length--;

            //oldTop = null;
        }

        /// <summary>
        /// Return the value of the top element of the stack
        /// </summary>
        /// <returns></returns>
        public T Peek()
        {
            try
            {
                return this.top.value;
            }
            catch (Exception e)
            {
                 Console.WriteLine(e.Message);
            }
            return null;
        }

        /// <summary>
        /// Print all the stack's value
        /// </summary>
        public void Print()
        {
            Node<T> nextTop = this.top;

            if(nextTop == null)
            {
                Console.WriteLine("[empty stack]");
            }
            
            else
            {
                Console.Write("[From top: ");
                do
                {
                    Node<T> toDisplay = nextTop;
                    nextTop = toDisplay.next;
                    if (nextTop != null)
                        Console.Write(toDisplay.value + ", ");
                    else
                        Console.WriteLine(toDisplay.value + "]");
                } while (nextTop != null);
            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            //  Create a new stack of int intialized with a top value of 4
            MyStack<int> stack = new MyStack<int>(4);

            //  Push 8
            stack.Push(8); //[8,4]

            //  Push 12
            stack.Push(12); //[12,8,4]

            //  Push 1993
            stack.Push(1993); //[1993, 12,8,4]

            //  Pop 1993
            stack.Pop(); //[12,8,4]

            stack.Print();
        }
    }
}
