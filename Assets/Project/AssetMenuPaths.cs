namespace Project
{
    public static class AssetMenuPaths
    {
        private const string _PROJECT = "Project/";

        private const string _OBSTACLES = _PROJECT + "Obstacles/";
        public const string OBSTACLE_SPAWNER = _OBSTACLES + "Spawner Config";
        public const string OBSTACLE_MANAGER_CONFIG = _OBSTACLES + "Manager Config";
        
        private const string _VISUALS = _PROJECT + "Visuals/";
        public const string BACKGROUND_CONFIG = _VISUALS + "Visuals Config";
        public const string GAME_COLORS_CONFIG = _VISUALS + "Game Colors Config";

        public const string CAMERA_CONFIG = _PROJECT + "Camera Config";
        public const string PLAYER_CONFIG = _PROJECT + "Player Config";
        public const string GAME_CONFIG = _PROJECT + "Game Config";
        public const string UI_CONFIG = _PROJECT + "UI Config";
        public const string SOUND_CONFIG = _PROJECT + "Sound Config";
    }
}
