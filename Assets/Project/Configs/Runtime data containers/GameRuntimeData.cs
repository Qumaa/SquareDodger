namespace Project.Game
{
    public class GameRuntimeData : ILoadableFrom<GameConfig>
    {
        public GameCameraRuntimeData GameCameraData { get; private set; }
        public ObstacleManagerRuntimeData ObstacleManagerData { get; private set; }
        public PlayerRuntimeData PlayerData { get; private set; }
        public GameBackgroundRuntimeData GameBackgroundData { get; private set; }
        public GameUIRuntimeData GameUIData { get; private set; }
        public GameSoundsRuntimeData GameSoundsData { get; private set; }
        
        public void Load(GameConfig data)
        {
            GameCameraData = new GameCameraRuntimeData();
            GameCameraData.Load(data.CameraConfig);

            ObstacleManagerData = new ObstacleManagerRuntimeData();
            ObstacleManagerData.Load(data.ManagerConfig);

            var shaderData = new PlayerShaderRuntimeData();
            shaderData.Load(data.PlayerConfig);

            PlayerData = new PlayerRuntimeData();
            PlayerData.Load(data.PlayerConfig);
            PlayerData.Load(shaderData);

            GameBackgroundData = new GameBackgroundRuntimeData();
            GameBackgroundData.Load(data.GameBackgroundConfig);

            GameUIData = new GameUIRuntimeData();
            GameUIData.Load(data.GameUIConfig);

            GameSoundsData = new GameSoundsRuntimeData();
            GameSoundsData.Load(data.GameSoundsConfig);
        }
    }
}