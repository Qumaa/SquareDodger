namespace Project
{
    public class ObstacleFactory : IFactory<Obstacle>
    {
        public Obstacle CreateNew() => 
            new();
    }
}