namespace Project.Architecture
{
    public struct DoTweenGameFinisherFactory : IFactory<IGameFinisher>
    {
        public IGameFinisher CreateNew()
        {
            var finisher = new DoTweenGameFinisher();
            return finisher;
        }
    }
}