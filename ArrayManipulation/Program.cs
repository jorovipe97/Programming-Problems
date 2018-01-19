#define UNSAFE_MODE
//#define IGNORE_ALGORITHM

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
            //Sol();
            Sol2();
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

        public static void Sol2()
        {
            string[] tokens_n = Console.ReadLine().Split(' ');
            int n = Convert.ToInt32(tokens_n[0]);
            int m = Convert.ToInt32(tokens_n[1]);
            for (int a0 = 0; a0 < m; a0++) // Iterates over each operation
            {
                string[] tokens_a = Console.ReadLine().Split(' ');
                int a = Convert.ToInt32(tokens_a[0]);
                int b = Convert.ToInt32(tokens_a[1]);
                int k = Convert.ToInt32(tokens_a[2]);
            }
        }

        /* Optimizations strategies
         * 
         * Strategy:
         * Now the inner loop executes from a to b indexes of the main array.
         * Result: Code now executes 2x faster, since TestVecrtor2.txt previously take 1000ms and now 450ms
         * 
         * Strategy:
         * Acces the array in unsafe mode for improve perfomance
         * Result: TestVector2.txt execution time come from 450ms to 360ms
         * 
         * Strategy:
         * Loop two times over the main array first for save the operations in a jagged array and second for perform each operation
         */
        public static void Sol()
        {
#if !IGNORE_ALGORITHM
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
            int[] operationLists = new int[m*3];

            // Saving the operation info into an array
            for (int i = 0; i < m; i++)
            {
                // If each iteration has 30ms, then
                // 100 000 iterationts * 30ms = 3 000 000ms
                // 3 000 000ms/1000ms = 3000s
                // 3000s / 60s = 50minutes
                // Iterates over 100 000 operation

                // For each operation
                // opInfo[0] = a
                // opInfo[1] = b
                // opInfo[2] = k
                opInfo = Console.ReadLine().Split(' ');
                int a = Convert.ToInt32(opInfo[0]);
                int b = Convert.ToInt32(opInfo[1]);
                int k = Convert.ToInt32(opInfo[2]);
                operationLists[i * 3 + 0] = a;
                operationLists[i * 3 + 1] = b;
                operationLists[i * 3 + 2] = k;
                // 4bytes * 3 * 100 000 = 1 200 000 bytes
                // = 1 171.875 kilo bytes
                // = 1.14 mega bytes

                // Let's to test if Array.Copy(...) performs the operations in O(1) time.



                /*
                 * Interesting posts for improve the perfomance of the following
                 * 1. Buffer.Copy https://social.msdn.microsoft.com/Forums/en-US/42189513-2106-4467-af9a-3b1810509cc8/fastest-way-to-clone-an-array-of-ints?forum=csharplanguage
                 * 2. Parallel Loops https://msdn.microsoft.com/en-us/library/ff963542.aspx
                 * 3. amdahl's law
                 * 4. Efficiently looping over an array http://tipsandtricks.runicsoft.com/CSharp/ArrayLooping.html
                 * 
                 * La ley de Amdahl se puede
                 * interpretar de manera más
                 * técnica, pero en términos simples,
                 * significa que es el algoritmo el
                 * que decide la mejora de
                 * velocidad, no el número de
                 * procesadores. Finalmente se
                 * llega a un momento que no se
                 * puede paralelizar más el algoritmo.
                 */

            }


            int operationCount = operationLists.GetLength(0) / 3; // Pseudo m x 3 array
#if (UNSAFE_MODE)
            unsafe
            {
                // The following fixed statement pins the location of the source and
                // target objects in memory so that they will not be moved by garbage
                // collection.
                fixed (long* arr = mainArray)
                {
                    fixed (int* operationListPtr = operationLists)
                    {
                        int i = 0;
                        int* opArr = operationListPtr;
                        int a = 0;
                        int b = 0;
                        int k = 0;
                        while (i < operationCount) // Read each operation
                        {
                            // ********************************
                            // **** EXTRACT EACH OPERATION ****
                            // ********************************
                            a = *(opArr + (i * 3 + 0)); // a = opArr[i][0]
                            b = *(opArr + (i * 3 + 1)); // b = opArr[i][1]
                            k = *(opArr + (i * 3 + 2)); // k = opArr[i][2]
                            i++;

                            // ********************************
                            // **** EXECUTE EACH OPERATION ****
                            // ********************************
                            int elementsToChange = b - a;
                            long* mainarr = arr;
                            int index = a - 1;
                            int upperLimitOp = index + elementsToChange;
                            while (index <= upperLimitOp)
                            {
                                *(mainarr + index) = (*(mainarr + index)) + k;
                                maxValue = (*(mainarr + index) > maxValue) ? *(mainarr + index) : maxValue;
                                //mainarr++;
                                index++;
                            }

                        }
                        
                    }                        
                }
            }
#else
            for (int i = 0; i < operationCount; i++) // Iterate for each operation
            {
                int a = operationLists[i * 3 + 0];
                int b = operationLists[i * 3 + 1];
                int k = operationLists[i * 3 + 2];
                // Iterates over each element beetween a-1 and b-1 in the main array
                // then now iterations = b - a
                for (int j = (a - 1); j < b; j++)
                {
                    // In each one of the 100 000 operation iterates over the 10 000 000 of items in the worst case of the mainArray long array
                    // 100 000 * 10 000 000 = 1 000 000 000 000 iterations

                    // TO-DO: Think in a better algorithm for gets the final max value in the array.
                    // mainArray[j] += k
                    mainArray[j] += k;
                    // This this values is grater than the latest maximum value found in array
                    maxValue = (mainArray[j] > maxValue) ? mainArray[j] : maxValue;
                }
                //int iterationsInnerLoop = b - a;
            }

#endif


            Console.WriteLine(maxValue);
#endif

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
