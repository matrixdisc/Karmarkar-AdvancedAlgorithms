using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KarmarkarSpikes.Samples.MathNet.Numerics.LinearAlgebraExamples;

namespace KarmarkarSpikes
{
    class Program
    {
        static void Main(string[] args)
        {
            MatrixRowColumnOperationsTest();
        }

        public static void MatrixInitializationTest()
        {
            var matrixInitialization = new MatrixInitialization();
            matrixInitialization.Run();
            Console.Read();
        }

        public static void MatrixArithmeticOperationsTest()
        {
            var matrixOperations = new MatrixArithmeticOperations();
            matrixOperations.Run();
            Console.ReadKey();
        }

        public static void MatrixRowColumnOperationsTest()
        {
            var matrixRowColumnOperation = new MatrixRowColumnOperations();
            matrixRowColumnOperation.Run();
            Console.ReadKey();
        }
    }
}
