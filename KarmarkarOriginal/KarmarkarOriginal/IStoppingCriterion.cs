using MathNet.Numerics.LinearAlgebra.Generic;

namespace KarmarkarOriginal
{
    public interface IStoppingCriterion
    {
        bool IsSatisfied(Vector<double> c, Vector<double> xCurrent, Vector<double> xPrevious);
    }
}