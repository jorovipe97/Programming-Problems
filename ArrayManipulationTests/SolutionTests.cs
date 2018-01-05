using ArrayManipulation;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ArrayManipulation.Tests
{

    /// <summary>
    /// Helper class for make easier change the default TextReader and TextWriter
    /// </summary>
    public class ConsoleHelper
    {
        public StringReader newReader;
        static TextReader originalReader;

        static TextWriter originalWriter;
        public StringWriter newWriter;

        public void SetNewReaderAndWriter(StringReader _newReader, StringWriter _newWriter)
        {
            newReader = _newReader;
            newWriter = _newWriter;
        }

        /// <summary>
        /// Change the TextReader which console reads from
        /// </summary>
        public void ChangeDefaultConsoleReader()
        {
            originalReader = Console.In;
            Console.SetIn(newReader);
        }

        /// <summary>
        /// Resets all as in the begining
        /// </summary>
        public void ResetChangesInReader()
        {
            Console.SetIn(originalReader);
        }

        /// <summary>
        /// Changes the default target which console writes to
        /// </summary>
        public void ChangeDefaultConsoleWriter()
        {
            originalWriter = Console.Out;
            Console.SetOut(newWriter);
        }

        /// <summary>
        /// Set the console to the default target to which write.
        /// </summary>
        public void ResetChangesInWriter()
        {
            Console.SetOut(originalWriter);
        }
    }

    [TestClass()]
    public class SolutionTests
    {

        private TestContext testContext;

        /*
         Data Driven Test CSV Template
         Expected Outpu, n, m, op_1a, op_1b, op_1k, op_2a, op_2b, op_2k

         Expected Value
         n m
         op_1a op_1b op_1k
         ... ... ...
         op_2a, op_2b, op_2k        
        */
        /*
        [DataSource("Microsoft.VisualStudio.TestTools.DataSource.CSV", "TestVectors.csv", "TestVectors#csv",  DataAccessMethod.Sequential),         
         TestMethod()]
        public void SolTest2()
        {
            StringBuilder lines = new StringBuilder();
            ConsoleHelper cl = new ConsoleHelper();

            // Console.WriteLine(lines.ToString());
            // Changes the default writer and reader of the console
            StringReader reader = new StringReader(lines.ToString());
            StringWriter writer = new StringWriter();
            cl.SetNewReaderAndWriter(reader, writer);
            cl.ChangeDefaultConsoleReader();
            cl.ChangeDefaultConsoleWriter();

            // Execute console program
            ArrayManipulation.Solution.Sol();

            //Assert.AreEqual<int>(200, Convert.ToInt32(cl.newWriter.ToString()), "The result is not correct");
            int expected = this.testContext.DataRow;
            int actual = 200;
            Assert.AreEqual<int>(expected, actual, "The result is not correct");

            cl.ResetChangesInReader();
            cl.ResetChangesInWriter();
        }*/
        
        [TestMethod()]
        public void SolTest()
        {
            ConsoleHelper cl = new ConsoleHelper();
            byte[] data;
            int finalMaxVal = 0;
            StringBuilder lines = new StringBuilder();
            string expected = "";
            // The ./../../ is for search the test vectors in root project directory instead of the bin/debug directory which be ignored by .gitignore
            // About using keyword: https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/using-statement
            using (FileStream vectorTest = File.Open("TestVector2.txt", FileMode.Open, FileAccess.Read))
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
                            if (index == 0) // The first line of the document have the expected output
                                expected = line.Trim();
                            else
                                lines.AppendLine(line); // Save each line into a list of string

                            // Print the actual line
                            //Console.WriteLine(line);
                            line = rs.ReadLine(); // Read the next line                            
                            index++;
                        }
                        // Console.WriteLine(lines.ToString());
                        // Changes the default writer and reader of the console
                        StringReader reader = new StringReader(lines.ToString());
                        StringWriter writer = new StringWriter();

                        cl.SetNewReaderAndWriter(reader, writer);
                        cl.ChangeDefaultConsoleReader();
                        cl.ChangeDefaultConsoleWriter();
                    }
                    /*data = new byte[vectorTest.Length];
                    // The file can be readed now
                    vectorTest.Read(data, 0, (int) vectorTest.Length);                   

                    PrintArrayElements<byte>(data);*/
                } // End Using
            } // End using

            ArrayManipulation.Solution.Sol();

            //Assert.AreEqual<int>(200, Convert.ToInt32(cl.newWriter.ToString()), "The result is not correct");
            string actual = cl.newWriter.ToString().Trim();
            Assert.AreEqual<string>(expected, actual, "The result is not correct");
            

            cl.ResetChangesInReader();
            cl.ResetChangesInWriter();
        }
    
    }
}