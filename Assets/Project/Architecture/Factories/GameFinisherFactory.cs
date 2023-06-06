namespace Project.Architecture
{
    public struct GameFinisherFactory : IFactory<IGameFinisher>
    {
        public IGameFinisher CreateNew()
        {
            return new GameFinisher();
        }
    }
}