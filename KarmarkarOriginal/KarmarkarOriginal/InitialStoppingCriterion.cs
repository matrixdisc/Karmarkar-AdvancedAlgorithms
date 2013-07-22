using MathNet.Numerics.LinearAlgebra.Generic;

namespace KarmarkarOriginal
{
    public class InitialStoppingCriterion : IStoppingCriterion
    {
        private readonly double _eF;
        private readonly RegularStoppingCriterion _regularStoppingCriterion;

        /// <summary>
        /// Stopping criterion for the calculation of the correct initial starting point for the original problem P, as defined in chapter 4
        /// </summary>
        /// <param name="eF"></param>
        /// <param name="epsilon"></param>
        public InitialStoppingCriterion(double eF, double epsilon)
        {
            _eF = eF;
            _regularStoppingCriterion = new RegularStoppingCriterion(epsilon);
        }

        /// <summary>
        /// Validates whether the stopping criterion is satisfied
        /// </summary>
        /// <param name="c">Variables coeficients vector</param>
        /// <param name="xCurrent">Current values of variables</param>
        /// <param name="xPrevious">Last iteration's values of variables</param>
        /// <returns></returns>
        public bool IsSatisfied(Vector<double> c, Vector<double> xCurrent, Vector<double> xPrevious)
        {
            double x0a = xCurrent.At(xCurrent.Count - 1);
            // Stopping criterion for the calculation of the correct initial starting point for the original problem P is satisfied when the artificial vairable is negative
            if (x0a < 0)
                return true;
            if (_regularStoppingCriterion.IsSatisfied(c, xCurrent, xPrevious))
            {
                if (x0a > _eF)
                    // 
                    throw new InfeasibleException();
                throw new NoInteriorFeasibleSolutionException();
            }
            return false;
        }
    }
}