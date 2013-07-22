using MathNet.Numerics.LinearAlgebra.Generic;

namespace KarmarkarOriginal
{
    public class RegularStoppingCriterion : IStoppingCriterion
    {
        private readonly double epsilon;
        
        /// <summary>
        /// Regular stopping criterion for the Karmarkar's Algorithm, as defined in chapter 6
        /// </summary>
        /// <param name="epsilon"></param>
        public RegularStoppingCriterion(double epsilon)
        {
            this.epsilon = epsilon;
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
            // xPrevious can be null only during the first iteration, obviously the stopping criterion isn't checkable then
            if (xPrevious == null)
                return false;
            double counter = c*xCurrent - c*xPrevious;
            if (counter < 0)
                counter = counter*(-1);
            double denominator = c*xPrevious;
            if (denominator < 1)
                denominator = 1;
            // Stopping criterion is satisfied if the optimized function's value has changed by less than tolerance (epsilon) between the last iterations
            return (counter/denominator < epsilon);
        }
    }
}