using Project.Architecture;

namespace Project.Game
{
    public class GameRuntimeData : ILoadableFrom<GameConfig>
    {
        private IThemeResolver _themeResolver;

        public GameRuntimeData(IThemeResolver themeResolver)
        {
            _themeResolver = themeResolver;
        }

        public GameCameraRuntimeData GameCameraData { get; private set; }
        public ObstacleManagerRuntimeData ObstacleManagerData { get; private set; }
        public PlayerRuntimeData PlayerData { get; private set; }
        public GameBackgroundRuntimeData GameBackgroundData { get; private set; }
        public GameColorsRuntimeData GameColorsData { get; private set; }
        public GameUIRuntimeData GameUIData { get; private set; }
        
        public void Load(GameConfig data)
        {
            var theme = _themeResolver.Resolve();
            
            GameCameraData = new GameCameraRuntimeData();
            GameCameraData.Load(data.CameraConfig);

            ObstacleManagerData = new ObstacleManagerRuntimeData();
            ObstacleManagerData.Load(data.ManagerConfig);

            var shaderData = new PlayerShaderRuntimeData();
            shaderData.Load(data.PlayerConfig);
            shaderData.Load(theme);
            
            PlayerData = new PlayerRuntimeData();
            PlayerData.Load(data.PlayerConfig);
            PlayerData.Load(shaderData);

            GameBackgroundData = new GameBackgroundRuntimeData();
            GameBackgroundData.Load(data.GameBackgroundConfig);

            GameColorsData = new GameColorsRuntimeData();
            GameColorsData.Load(theme);

            GameUIData = new GameUIRuntimeData();
            GameUIData.Load(data.GameUIConfig);
        }
    }
}