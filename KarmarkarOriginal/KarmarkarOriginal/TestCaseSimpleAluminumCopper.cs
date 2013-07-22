using System;
using MathNet.Numerics.LinearAlgebra.Double;

namespace KarmarkarOriginal
{
    public class TestCaseSimpleAluminumCopper : TestCaseBase
    {
        public TestCaseSimpleAluminumCopper()
        {
            name = "Aluminum Copper";
            A = DenseMatrix.OfArray(new[,] { { 0.0, 1.0 }, { 5.0, 2.0 }, { 0.25, 0.5 }, { -1.0, 0.0 }, { 0.0, -1.0 } });
            b = new DenseVector(5);
            b.SetValues(new[] { 60.0, 500.0, 40.0, 0.0, 0.0 });
            c = new DenseVector(2);
            c.SetValues(new[] { 0.25, 0.4 });
            x = new DenseVector(2);
            x.SetValues(new[] { 1.0, 1.0 });
            gamma = 0.95;
            epsilon = Math.Pow(10, -8);
            eF = Math.Pow(10, -8);
            miu = Math.Pow(10, 5);
        }
    }
}