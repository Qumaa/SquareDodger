using Project.Game;

namespace Project.Architecture
{
    public struct ScoreCalculatorFactory : IFactory<IPlayerPositionScoreCalculator>
    {
        public IPlayerPositionScoreCalculator CreateNew()
        {
            var calculator = new PlayerPositionScoreCalculator();
            return calculator;
        }
    }
}