namespace PRORR.Interfaces
{
    public interface IMutationController
    {
        float GetMutationRate(int iteration);
        void Mutate(Individual individual);
    }
}
