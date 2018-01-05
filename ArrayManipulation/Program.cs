using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;

/*
PROBLEM DESCRIPTION
https://www.hackerrank.com/challenges/crush/problem

You are given a list(1-indexed) of size , initialized with zeroes. You have to perform  operations on the list and output the maximum of final values of all the  elements in the list. For every operation, you are given three integers ,  and  and you have to add value  to all the elements ranging from index  to (both inclusive).

For example, consider a list  of size . The initial list would be  = [, , ] and after performing the update  = , the new list would be  = [, , ]. Here, we've added value 30 to elements between indices 2 and 3. Note the index of the list starts from 1.

Input Format

The first line will contain two integers  and  separated by a single space.
Next  lines will contain three integers ,  and  separated by a single space.
Numbers in list are numbered from  to .

Constraints

Output Format

Print in a single line the maximum value in the updated list.

Sample Input

5 3
1 2 100
2 5 100
3 4 100
Sample Output

200
Explanation

After first update list will be 100 100 0 0 0. 
After second update list will be 100 200 100 100 100.
After third update list will be 100 200 200 200 100.
So the required answer will be 200.

    About Constrains:
    3 <= n <= 10 000 000
    1 <= m <= 200 000
    1 <= a <= b <= n
    0 <= k <= 1 000 000 000

    Then in the most extreme case let's suppose that
    m = 200 000 (number of operation is)
    a = b = 1 for all the m operations
    k = 1 000 000 000

    Then k * m = 200 000 * 1 000 000 000 = 200 000 000 000 000
    The main array need to be of a type whose range support the former number.
    The long is sufficient: ((2**64)/2) - 1 > k*m
 */
namespace ArrayManipulation
{

    // TODO: How to test, the perfomance of a c# code

    // An 1-indexed list is a list which is accesed from 1 instead of 0
    public class Solution
    {
        static StringReader newInputData;
        static TextReader originalReader;

        static void Main(string[] args)
        {
            //ReadingFile();
            Sol();
            /*newInputData = new StringReader(ReadingFile().ToString());

            originalReader = Console.In;
            Console.SetIn(newInputData);

            Console.WriteLine(Console.ReadLine());
            Console.WriteLine(Console.ReadLine());
            Console.WriteLine(Console.ReadLine());
            Console.WriteLine(Console.ReadLine());
            Console.WriteLine(Console.ReadLine()); 

            Console.SetIn(originalReader);*/
            Console.ReadKey();
        }

        public static void Sol()
        {
            // For test vecvtor 1
            string[] n_and_m = Console.ReadLine().Split(' ');
            // Array Lenght
            int n = Convert.ToInt32(n_and_m[0]); // 10 000 000
            // Number of operations
            int m = Convert.ToInt32(n_and_m[1]); // 100 000

            // The main array
            long[] mainArray = new long[n];
            // int size in bits: 32 bits (4 bytes)
            /*
             * If for example: An int array has 4000 elements
             * 4 000int*32bits = 128 000 bits
             * 128 000 bits / 8 bits = 16 000 bytes
             * 16 000 bytes / 1024 bytes = 15.625 kilobyte
             * 15.625 kilobyte / 1024 kilobyte = 0.015258 megabyte
             */

            // Operations info is saved into a jagged array
            //int[][] operationList = new int[m][];
            string[] opInfo;
            long maxValue = 0;

            // Saving the operation info into an array
            for (int i = 0; i < m; i++)
            {
                // Iterates over 100 000 operation

                // For each operation
                // opInfo[0] = a
                // opInfo[1] = b
                // opInfo[2] = k
                opInfo = Console.ReadLine().Split(' ');
                int a = Convert.ToInt32(opInfo[0]);
                int b = Convert.ToInt32(opInfo[1]);
                int k = Convert.ToInt32(opInfo[2]);

                // Save all the operations to perform
                //operationList[i] = new int[3] { a, b, k };

                // Iterates over each element beetween a-1 and b-1 in the main array
                for (int j = 0; j < mainArray.Length; j++)
                {
                    // In each one of the 100 000 operation iterates over the 10 000 000 of items in the mainArray long array
                    // 100 000 * 10 000 000 = 1 000 000 000 000 iterations
                    // If j >= a-1 and j <= b-1
                    if (j >= (a - 1) && j <= (b - 1))
                    {
                        // mainArray[j] += k
                        mainArray[j] += k;
                        // This this values is grater than the latest maximum value found in array
                        maxValue = (mainArray[j] > maxValue) ? mainArray[j] : maxValue;
                    }
                }
            }

            Console.WriteLine(maxValue);
            //Console.ReadKey();
            //return maxValue;
        }

        private static void PrintArrayElements<T>(T[] arr)
        {
            Console.Write("[ ");
            for (int i = 0; i < arr.Length-1; i++)
            {
                Console.Write(arr[i] + ", ");
            }
            Console.WriteLine(arr[arr.Length - 1] + "]");
        }

        private static StringBuilder ReadingFile()
        {
            byte[] data;
            StringBuilder lines = new StringBuilder();
            // The ./../../ is for search the test vectors in root project directory instead of the bin/debug directory which be ignored by .gitignore
            // About using keyword: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-statement
            using (FileStream vectorTest = File.Open("TestVector1.txt", FileMode.Open, FileAccess.Read))
            {
                // The file can be readed
                if (vectorTest.CanRead)
                {
                    using (StreamReader rs = new StreamReader(vectorTest, Encoding.UTF8))
                    {
                        // Read all the lines
                        string line = rs.ReadLine();
                        int index = 0;
                        while (line != null) // Line is null if the end of the input stram is reached
                        {
                            if (index > 0) // The first line of the document have the expected output
                                lines.AppendLine(line);

                            line = rs.ReadLine(); // Read the next line
                            index++;
                        }

                    }
                    /*data = new byte[vectorTest.Length];
                    // The file can be readed now
                    vectorTest.Read(data, 0, (int) vectorTest.Length);                   

                    PrintArrayElements<byte>(data);*/
                }
            }

            return lines;
        }

        private static void ArrayAreReferenceTypes()
        {
            // NOTE: Array are reference types
            int[] names = new int[2] { 1, 2 };
            Console.WriteLine(names[0]);
            int[] foo = names;
            foo[0] = 1111;
            Console.WriteLine(names[0]);
        }
    }
    
}
