namespace Project.Game
{
    public interface IObstacleDespawner
    {
        void DespawnNecessaryObstacles(IObstacle[] obstacles);
    }
}