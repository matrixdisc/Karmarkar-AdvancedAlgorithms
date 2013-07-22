using MathNet.Numerics.LinearAlgebra.Generic;

namespace KarmarkarOriginal
{
    public class KarmarkarRunner
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
        /// Safety factor for step retraction
        /// </summary>
        private readonly double gamma;
        /// <summary>
        /// Big constant used for creation of artificial coefficient for initial artificial variable
        /// </summary>
        private readonly double miu;
        /// <summary>
        /// Precission of the solution
        /// </summary>
        private readonly double _epsilon;
        /// <summary>
        /// Infeasibility tolerance for initial solution
        /// </summary>
        private readonly double _eF;
        /// <summary>
        /// Initial stopping criterion as described in Chapter 4
        /// </summary>
        private InitialStoppingCriterion initialStoppingCriterion;
        /// <summary>
        /// Regular stopping criterion as described in chapter 6
        /// </summary>
        private RegularStoppingCriterion regularStoppingCriterion;
        /// <summary>
        /// Instance of Karmarkar's Algorithm for Linear Programming
        /// </summary>
        private KarmarkarAlgorithm karmarkarAlgorithm;
        /// <summary>
        /// Initial solution generator
        /// </summary>
        private InitialSolutionGenerator initialSolutionGenerator;

        public KarmarkarRunner(Matrix<double> A, Vector<double> b, Vector<double> c, double gamma, double miu, double epsilon, double eF)
        {
            this.A = A;
            this.b = b;
            this.c = c;
            this.gamma = gamma;
            this.miu = miu;
            _epsilon = epsilon;
            _eF = eF;
            initialStoppingCriterion = new InitialStoppingCriterion(_eF, _epsilon);
            regularStoppingCriterion = new RegularStoppingCriterion(_epsilon);
            initialSolutionGenerator = new InitialSolutionGenerator();
        }

        /// <summary>
        /// Runs modular parts of the algorithm in appropriate order.
        /// </summary>
        /// <returns></returns>
        public Vector<double> Run()
        {
            Vector<double> initialSolution = initialSolutionGenerator.GenerateInitialSolution(A, c, b, miu, initialStoppingCriterion, gamma);
            karmarkarAlgorithm = new KarmarkarAlgorithm(A, b, c, initialSolution, regularStoppingCriterion, gamma);
            return karmarkarAlgorithm.Run();
        }
    }
}