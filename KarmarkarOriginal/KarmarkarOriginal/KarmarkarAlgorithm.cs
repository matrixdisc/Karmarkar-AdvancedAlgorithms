using System;
using System.Linq;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace KarmarkarOriginal
{
    public class KarmarkarAlgorithm
    {
        /// <summary>
        /// Full rank constraints coefficient matrix
        /// </summary>
        private readonly Matrix<double> A;
        /// <summary>
        /// Constraints coefficients vector
        /// </summary>
        private readonly Vector<double> b;
        /// <summary>
        /// Variables coeficients vector
        /// </summary>
        private readonly Vector<double> c;
        /// <summary>
        /// Current values of the optimized variables
        /// </summary>
        private Vector<double> x;
        /// <summary>
        /// Last iteration's values of the optimized variables
        /// </summary>
        private Vector<double> xPrevious;
        /// <summary>
        /// Stopping criterion for the algorithm
        /// </summary>
        private readonly IStoppingCriterion stoppingCriterion;
        /// <summary>
        /// Safety retraction constant
        /// </summary>
        private readonly double gamma;
        /// <summary>
        /// Current slack variables vector
        /// </summary>
        private Vector<double> v;
        /// <summary>
        /// Transformation matrix
        /// </summary>
        private DiagonalMatrix Dv;
        /// <summary>
        /// Feasible direction vector in the original problem space (vector defining the next iteration's point)
        /// </summary>
        private Vector<double> hx;
        /// <summary>
        /// Feasible direction vector in the transformed problem space (slacked variables space, allows for checking for unboundendess)
        /// </summary>
        private Vector<double> hv;
        /// <summary>
        /// Safety retraction coefficient depending on the current feasible direction vector hx
        /// </summary>
        private double alpha;

        public KarmarkarAlgorithm(Matrix<double> A, Vector<double> b, Vector<double> c, Vector<double> x, IStoppingCriterion stoppingCriterion, Double gamma)
        {
            this.A = A;
            this.b = b;
            this.c = c;
            this.x = x;
            this.stoppingCriterion = stoppingCriterion;
            this.gamma = gamma;
        }

        /// <summary>
        /// Main method of the algorithm. Starting from the initial, internal point of the polytope, calculates a series of internal points with monotonously increasing value, converging to the optimal solution. Defined in Chapter 2
        /// </summary>
        /// <returns>Optimal solution for original problem P</returns>
        public Vector<double> Run()
        {
            int k = 0;
            while (!stoppingCriterion.IsSatisfied(c, x, xPrevious)) // Algorithm ends when the value of the optimized function has changed by less than epsilon between the last iterations
            {
                v = b - A*x;                                        // Slack variables are calculated
                Dv = DiagonalMatrix.Create(v.Count, v.Count, v.At); // Transformation matrix is calculated
                hx = (A.Transpose()*(Dv.Inverse()* Dv.Inverse())*A).Inverse() * c;  // Feasible direction vector for original problem space calculation 
                hv = -A*hx;                                         // Feasible direction vector for transformed problem space (with slacks) calculation
                if(IsUnbounded(hv))                                 // hv is used to detect unboundedness of the original problem
                {
                    throw new UnboundedException();
                }
                var min = GetMin(v, hv);
                alpha = gamma*min;                                  // Safety retraction coefficient is calculated to prevent "overshooting" the solution space
                xPrevious = x;
                x = x + alpha*hx;                                   // Move to the next point defined by the feasible direction vector for original problem space with safety retraction coefficient
                var profit = x*c;                                   // Current value of the maximized function
                k++;
            }
            return x;
        }

        private bool IsUnbounded(Vector<double> hv)
        {
            return hv.All(d => d >= 0);
        }

        private double GetMin(Vector<double> v, Vector<double> hv)
        {
            var min = double.PositiveInfinity;
            for(int i = 0; i < v.Count; i++)
            {
                if (hv.At(i) >= 0)
                    continue;
                var current = -v.At(i)/hv.At(i);
                if (min > current)
                    min = current;
            }
            return min;
        }
    }
}