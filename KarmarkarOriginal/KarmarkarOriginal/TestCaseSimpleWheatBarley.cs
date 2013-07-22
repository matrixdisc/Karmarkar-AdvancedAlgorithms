using System;
using MathNet.Numerics.LinearAlgebra.Double;

namespace KarmarkarOriginal
{
    public class TestCaseSimpleWheatBarley : TestCaseBase
    {
        public TestCaseSimpleWheatBarley()
        {
            name = "Wheat Barley";
            A = DenseMatrix.OfArray(new[,] { { 1.0, 1.0 }, { 2.0, 1.0 }, { -1.0, 0.0 }, { 0.0, -1.0 } });
            b = new DenseVector(4);
            b.SetValues(new[] { 8.0, 10.0, 0.0, 0.0 });
            c = new DenseVector(2);
            c.SetValues(new[] { 5.0, 3.0 });
            x = new DenseVector(2);
            x.SetValues(new[] { 1.0, 1.0 });
            gamma = 0.95;
            epsilon = Math.Pow(10, -8);
            eF = Math.Pow(10, -8);
            miu = Math.Pow(10, 5);
        }
    }
}