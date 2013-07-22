using System;
using System.Collections.Generic;
using System.Diagnostics;
using MathNet.Numerics.LinearAlgebra.Generic;

/************************************************************************************************
 * Implementation of Karmarkar's Algorithm for Linear Programming based on the research paper   *
 * "An Implementation of Karmakrar's Algorithm for Linear Programming" by                       *
 * Ilan Adler, Narendra karmarkar, Mauricio G.C Resende, Geraldo Veiga                          *
 * Mathematical Programming 44 (1989), 297-335                                                  *
 *                                                                                              *
 * All references to chapter numbers are referring the chapters from this paper                *
 *                                                                                              *
 * Created by Mateusz Łukomski                                                                  *
 ************************************************************************************************/

namespace KarmarkarOriginal
{
    class Program
    {
        static void Main(string[] args)
        {
            var problems = GenerateProblemsList();
            foreach (var tc in problems)
            {
                try
                {
                    Console.WriteLine(tc.name);

                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    var kr = new KarmarkarRunner(tc.A, tc.b, tc.c, tc.gamma, tc.miu, tc.epsilon, tc.eF);
                    Vector<double> result = kr.Run();
                    sw.Stop();
                    
                    Console.WriteLine("Final values for x :");
                    Console.WriteLine(result);
                    Console.WriteLine();
                    Console.WriteLine("Function value :");
                    Console.WriteLine(tc.c*result);
                    Console.WriteLine();
                    Console.WriteLine("Time Elapsed={0}", sw.Elapsed);
                    Console.WriteLine(); 
                    Console.WriteLine();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.WriteLine();
                }
            }
            Console.ReadKey();
        }

        /// <summary>
        /// A batch of pre-defined test cases
        /// </summary>
        /// <returns></returns>
        private static List<TestCaseBase> GenerateProblemsList()
        {
            var problems = new List<TestCaseBase>
                {
                    new TestCaseSimple2x2(),
                    new TestCaseSimple2x4(),
                    new TestCaseSimpleCornSoy(),
                    new TestCaseSimpleAluminumCopper(),
                    new TestCaseSimpleWheatBarley()
                };

            return problems;
        }
    }
}
