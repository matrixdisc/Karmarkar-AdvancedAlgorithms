using System;
using MathNet.Numerics.LinearAlgebra.Double;

namespace KarmarkarOriginal
{
    public class TestCaseBigger : TestCaseBase
    {
        public TestCaseBigger()
        {
            name = "Bigger example";
            A = DenseMatrix.OfArray(new[,] { 
                { -1.0, 1.0, -1.0, 1.0, 4.0, -2.0 }, 
                { -3.0, 3.0, 1.0, -1.0, -2.0, 0.0 }, 
                { 0.0, 0.0, -1.0, 1.0, 0.0, 1.0 }, 
                { 1.0, -1.0, 1.0, -1.0, -1.0, 0.0 },
                { -1.0, 0.0, 0.0, 0.0, 0.0, 0.0 },
                { 0.0, -1.0, 0.0, 0.0, 0.0, 0.0 },
                { 0.0, 0.0, -1.0, 0.0, 0.0, 0.0 },
                { 0.0, 0.0, 0.0, -1.0, 0.0, 0.0},
                { 0.0, 0.0, 0.0, 0.0, -1.0, 0.0},
                { 0.0, 0.0, 0.0, 0.0, 0.0, -1.0}});
            b = new DenseVector(10);
            b.SetValues(new[] { -4.0, 6.0, 1.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0 });
            c = new DenseVector(6);
            c.SetValues(new[] { -3.0, 3.0, 2.0, -2.0, -1.0, 4.0 });
            x = new DenseVector(2);
            x.SetValues(new[] { 1.0, 1.0 });
            gamma = 0.90;
            epsilon = Math.Pow(10, -8);
            eF = Math.Pow(10, -8);
            miu = Math.Pow(10, 5);
        }
    }
}