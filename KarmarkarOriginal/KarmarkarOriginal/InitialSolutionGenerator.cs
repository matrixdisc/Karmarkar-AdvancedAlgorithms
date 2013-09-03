using System.Linq;
using MathNet.Numerics.LinearAlgebra.Double;
using MathNet.Numerics.LinearAlgebra.Generic;

namespace KarmarkarOriginal
{
    public class InitialSolutionGenerator
    {
        /// <summary>
        /// Tentative solution for original problem P
        /// </summary>
        private Vector<double> x0;
        /// <summary>
        /// Artificial variable used for calculation of a correct starting point for Karmarkar's Algorithm
        /// </summary>
        private double x0a;
        /// <summary>
        /// Negative unit vector, i.e. {-1,-1,...,-1}
        /// </summary>
        private Vector<double> e;
        /// <summary>
        /// Original constraints coefficient matrix concatenated with negative unit vector
        /// </summary>
        private Matrix<double> modifiedA;
        /// <summary>
        /// Initial n+1 vector (x0, x0a) used for calculation of a correct starting point for Karmarkar's Algorithm
        /// </summary>
        private Vector<double> x;
        /// <summary>
        /// Artificial variable's big artificial coefficient
        /// </summary>
        private double M;
        /// <summary>
        /// n+1 variables coefficient vector
        /// </summary>
        private Vector<double> modifiedC;
        /// <summary>
        /// Karmarkar's Algorithm used to calculate correct starting point for the original problem P, by solving the modified problem P*
        /// </summary>
        private KarmarkarAlgorithm karmarkarAlgorithm;

        /// <summary>
        /// Generates correct initial solution for the original problem P, as described in chapter 4
        /// </summary>
        /// <param name="A">Full rank constraints coefficients matrix</param>
        /// <param name="c">Variables coefficients vector</param>
        /// <param name="b">Constraints coefficients vector</param>
        /// <param name="miu">Big constant used for creation of artificial coefficient for initial artificial variable</param>
        /// <param name="initialStoppingCriterion">Initial stopping criterion as described in Chapter 4</param>
        /// <param name="gamma">Safety factor for step retraction</param>
        /// <returns>Correct initial solution for original problem P</returns>
        public Vector<double> GenerateInitialSolution(Matrix<double> A, Vector<double> c, Vector<double> b, double miu, InitialStoppingCriterion initialStoppingCriterion, double gamma)
        {
            x0 =        CalculateTentativeSolutionForP(b, A, c);
            x0a =       CalculateInitialValueForArtificialVariable(x0, b, A);
            e =         CreateNegativeUnitVector(A);
            modifiedA = GenerateModifiedConstraintsMatrix(e, A);
            x =         GenerateInitialVector(x0, x0a);
            M =         CalculateNegativeArtificialCoefficient(c, miu, x0, x0a);
            modifiedC = GenerateModifiedCoeficientsVector(c, M);
            karmarkarAlgorithm = new KarmarkarAlgorithm(modifiedA, b, modifiedC, x, initialStoppingCriterion, gamma);
            // Modified problem P* with the artificial variable x0a is solved by the Karmakrar's Algorithm
            Vector<double> initialSolutionWithArtificialVariable = karmarkarAlgorithm.Run().solution;
            // Solution of the problem P* is the correct initial point for problem P only after removing the artificial variable
            Vector<double> initialSolution = RemoveArtificialVariableFrom(initialSolutionWithArtificialVariable);
            return initialSolution;
        }

        private Vector<double> RemoveArtificialVariableFrom(Vector<double> initialSolutionWithArtificialVariable)
        {
            Vector<double> initialSolution = new DenseVector(initialSolutionWithArtificialVariable.Count - 1);
            initialSolutionWithArtificialVariable.CopySubVectorTo(initialSolution, 0, 0, initialSolutionWithArtificialVariable.Count - 1);
            return initialSolution;
        }

        private static double CalculateNegativeArtificialCoefficient(Vector<double> c, double miu, Vector<double> x0, double x0a)
        {
            return -miu*((c*x0)/x0a);
        }

        private Vector<double> GenerateModifiedCoeficientsVector(Vector<double> c, double M)
        {
            Vector<double> modifiedC = ConcatenateVectorWithValue(c, M);
            return modifiedC;
        }

        private Vector<double> ConcatenateVectorWithValue(Vector<double> baseVector, double value)
        {
            Vector<double> extendedVector = new DenseVector(baseVector.Count + 1);
            baseVector.CopySubVectorTo(extendedVector, 0, 0, baseVector.Count);
            extendedVector.SetSubVector(extendedVector.Count - 1, 1, DenseVector.Create(1, i => value));
            return extendedVector;
        }

        private Vector<double> GenerateInitialVector(Vector<double> x0, double x0a)
        {
            Vector<double> x = ConcatenateVectorWithValue(x0, x0a);
            return x;
        }

        private Matrix<double> GenerateModifiedConstraintsMatrix(Vector<double> e, Matrix<double> baseMatrix)
        {
            return baseMatrix.InsertColumn(baseMatrix.ColumnCount, e);
        }

        private DenseVector CreateNegativeUnitVector(Matrix<double> baseMatrix)
        {
            return DenseVector.Create(baseMatrix.RowCount, i => -1);
        }

        private double CalculateInitialValueForArtificialVariable(Vector<double> x0, Vector<double> b, Matrix<double> A)
        {
            return -2 * (b - A*x0).Min();
        }

        private double CalculateInitialValueForArtificialVariableIfZeroPossible(Vector<double> x0, Vector<double> b, Matrix<double> A)
        {
            return 2 * (b - A * x0).Norm(2);
        }

        private Vector<double> CalculateTentativeSolutionForP(Vector<double> b, Matrix<double> A, Vector<double> c)
        {
            return (b.Norm(2) / ((A * c).Norm(2))) * c;
        }
    }
}