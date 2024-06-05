namespace PRORR.Interfaces
{
    public interface IRandomGenerator
    {
        float Next(float minValue, float maxValue);
        int Next(int minValue, int maxValue);
        int NextWeighted(float[] weights);
        float NextGaussian(float mean, float stdDev);
    }
}
