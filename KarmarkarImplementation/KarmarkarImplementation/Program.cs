using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace KarmarkarImplementation
{
    class Program
    {
        private static Matrix A = new  DenseMatrix();
        private static Vector pi = new DenseVector();
        private static Vector s = new DenseVector();
        private static Vector x = new DenseVector();
        private static Vector b = new DenseVector();
        private static Vector c = new DenseVector();
        private static Matrix X = new DenseMatrix();
        private static Matrix S = new DenseMatrix();
        private static Matrix<double> D_square = new DenseMatrix();
        private static Vector<double> xi_s = new DenseVector();
        private static Vector<double> xi_x = new DenseVector();
        private static Vector<double> p1_pi;
        private static Vector<double> p1_s;
        private static Vector<double> p1_x;

        static void Main(string[] args)
        {
            Step0ComputeErrors();
            Step1ComputeFirstDerivative();
            Step2ComputeCenteringParameter();
        }


        private static void Step0ComputeErrors()
        {
            xi_s = ComputeXiS(A, pi, s, c);
            xi_x = ComputeXiX(A, x, b);
            D_square = ComputeD_square(S, X);
        }

        private static void Step1ComputeFirstDerivative()
        {
            p1_pi = ComputeP1_pi(A, D_square, b, xi_s);
            p1_s = ComputeP1_s(xi_s, A, p1_pi);
            p1_x = ComputeP1_x(x, D_square, p1_s);
        }

        private static void Step2ComputeCenteringParameter()
        {
            int lx = ComputeL(x, p1_x);
            double epsilon1_x = ComputeEpsilon1(x, p1_x, lx);
            int ls = ComputeL(s, p1_s);
            double epsilon1_s = ComputeEpsilon1(s, p1_s, ls);
            double mdg = ComputeMdg(x, epsilon1_x, p1_x, s, epsilon1_s, p1_s);
        }

        private static double ComputeMdg(Vector<double> x, double epsilon1_x, Vector<double> p1_x, Vector<double> s, double epsilon1_s, Vector<double> p1_s)
        {
            return (x - epsilon1_x*p1_x)*
        }

        private static double ComputeEpsilon1(Vector<double> top, Vector<double> bottom, int index)
        {
            return Math.Min(top.At(index)/bottom.At(index), 1);
        }

        private static int ComputeL(Vector<double> top, Vector<double> bottom)
        {
            var currentLx = 0;
            var currentMin = double.PositiveInfinity;
            for (var i = 0; i < top.Count; i++)
            {
                if(bottom.At(i) <= 0) continue;
                var potentialMin = top.At(i)/bottom.At(i);
                if (potentialMin > currentMin) continue;
                currentMin = potentialMin;
                currentLx = i;
            }
            return currentLx;
        }

        private static Vector<double> ComputeP1_x(Vector<double> x, Matrix<double> D_square, Vector<double> p1_s)
        {
            return x - D_square*p1_s;
        }

        private static Vector<double> ComputeP1_s(Vector<double> xi_s, Matrix A, Vector<double> p1_pi)
        {
            return xi_s - A.Transpose()*p1_pi;
        }

        private static Vector<double> ComputeP1_pi(Matrix<double> A, Matrix<double> D_square, Vector<double> b, Vector<double> xi_s)
        {
            Matrix<double> part1 = -((A*D_square*A.Transpose()).Inverse());
            Vector<double> part2 = b - A*D_square*xi_s;
            return part1*part2;
        }

        private static Matrix<double> ComputeD_square(Matrix<double> S, Matrix<double> X)
        {
            return S.Inverse()*X;
        }

        private static Vector<double> ComputeXiX(Matrix<double> A, Vector<double> x, Vector<double> b)
        {
            return A*x - b;
        }

        private static Vector<double> ComputeXiS(Matrix<double> A, Vector<double> pi, Vector<double> s, Vector<double> c)
        {
            return A.Transpose()*pi + s - c;
        }
    }
}
