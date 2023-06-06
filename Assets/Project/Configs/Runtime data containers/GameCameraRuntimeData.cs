namespace Project.Game
{
    public class GameCameraRuntimeData : ILoadableFrom<GameCameraConfig>
    {
        public float ViewportDepth { get; private set; }
        public float ViewportWidth { get; private set; }

        public float MotionSpeed { get; private set; }
        public float MotionDamping { get; private set; }
        public float MotionResponsiveness { get; private set; }

        public float BottomOffset { get; private set; }
        
        public void Load(GameCameraConfig data)
        {
            ViewportDepth = data.ViewportWidth;
            ViewportWidth = data.ViewportWidth;
            MotionSpeed = data.MotionSpeed;
            MotionDamping = data.MotionDamping;
            MotionResponsiveness = data.MotionResponsiveness;
            BottomOffset = data.BottomOffset;
        }
    }
}