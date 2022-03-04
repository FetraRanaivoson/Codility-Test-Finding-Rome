using System;

namespace Reverse_String
{
    class Program
    {
        static void Main(string[] args)
        {
            string text = "Hello, my name is Fetra. I am a game developer";
            Console.WriteLine(Reverse(text));
        }

        static string Reverse(string text)
        {
            //  I do not need the extra loop to create a separate array to hold the characters.
            //  string is already an array but I just need to convert it into string when assigning to the new array
            //  It is just a minor optimization because O(n+n) <====> 0(n)

            //  Transform the text to an array
            //string[] oldArray = new string[text.Length];
            //for (int i = 0; i < oldArray.Length; i++) // O(n)
            //{
            //    oldArray[i] = text[i].ToString();
            //    //Console.WriteLine(oldArray[i]);
            //}

            //  text = "abc"
            //  1-  You create a new array with the same length as the array we are working on
            string[] newArray = new string[text.Length]; 

            //  2-  Assign new values for the new array starting from old array index going backward from the end
            //  int currentOldArrayIndex = oldArray.Length -1
            //      For each item in the new array (increment loop)
            //          item[indexForward] = oldArray[currentOldArrayIndex]
            //          if(currentOldArrayIndex) >= 0)
            //              currentOldArrayIndex -- 
            //          
            //  OldArr = "a", "b", "c"
            //  NArr = "c", "b", "a"

            //int currentOldArrayIndex = oldArray.Length - 1;
            int currentOldArrayIndex = text.Length - 1;

            for (int i = 0; i < text.Length; i++) // O(n)
            {
                //newArray[i] = oldArray[currentOldArrayIndex];
                newArray[i] = text[currentOldArrayIndex].ToString();
                if (currentOldArrayIndex >= 0)
                {
                    currentOldArrayIndex--;
                }
            }

            //for (int i = 0; i < newArray.Length; i++)
            //{
            //    Console.WriteLine(newArray[i]);
            //}

            //  3-  Transform the array to a text
            return string.Join("",newArray);
        }
    }
}
