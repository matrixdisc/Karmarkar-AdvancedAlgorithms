using MathNet.Numerics.LinearAlgebra.Generic;

namespace KarmarkarOriginal
{
    public class TestCaseBase
    {
        public string name;
        public double gamma;
        public double epsilon;
        public double eF;
        public double miu;
        public Vector<double> b;
        public Vector<double> c;
        public Vector<double> x;
        public Matrix<double> A;
    }
}