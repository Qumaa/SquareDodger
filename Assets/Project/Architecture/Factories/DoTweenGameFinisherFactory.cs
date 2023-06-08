namespace Project.Architecture
{
    public struct DoTweenGameFinisherFactory : IFactory<IAnimatedGameFinisher>
    {
        public IAnimatedGameFinisher CreateNew()
        {
            var finisher = new DoTweenGameFinisher();
            return finisher;
        }
    }
}