using System;
using MathNet.Numerics.LinearAlgebra.Double;

namespace KarmarkarOriginal
{
    public class TestCaseSimpleCornSoy : TestCaseBase
    {
        public TestCaseSimpleCornSoy()
        {
            name = "Corn Soy";
            A = DenseMatrix.OfArray(new[,] { { 1.0, 1.0 }, { 50.0, 100.0 }, { 100.0, 40.0 }, { -1.0, 0.0 }, { 0.0, -1.0 } });
            b = new DenseVector(5);
            b.SetValues(new[] { 320.0, 20000.0, 19200.0, 0.0, 0.0 });
            c = new DenseVector(2);
            c.SetValues(new[] { 60.0, 90.0 });
            x = new DenseVector(2);
            x.SetValues(new[] { 1.0, 1.0 });
            gamma = 0.95;
            epsilon = Math.Pow(10, -8);
            eF = Math.Pow(10, -8);
            miu = Math.Pow(10, 5);
        }
    }
}