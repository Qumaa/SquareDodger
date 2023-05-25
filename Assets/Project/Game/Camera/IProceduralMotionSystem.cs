namespace Project.Game
{
    public interface IProceduralMotionSystem<T>
    {
        T MakeStep(float timeStep, T destination, T velocity);
        T MakeStep(float timeStep, T destination);
    }
}