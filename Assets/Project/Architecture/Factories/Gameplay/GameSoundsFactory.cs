namespace Project.Game
{
    public struct GameSoundsFactory : IFactory<IGameSounds>
    {
        private GameSoundsRuntimeData _soundsData;

        public GameSoundsFactory(GameSoundsRuntimeData soundsData)
        {
            _soundsData = soundsData;
        }

        public IGameSounds CreateNew()
        {
            return new GameSounds();
        }
    }
}