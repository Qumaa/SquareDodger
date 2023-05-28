namespace Project
{
    public interface IProceduralMotionSystem<T>
    {
        T MakeStep(float timeStep, T destination, T velocity);
        T MakeStep(float timeStep, T destination);
        void SetInitialValue(T initialValue, T zeroValue);
    }
}