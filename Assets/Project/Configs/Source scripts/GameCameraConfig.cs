using UnityEngine;

namespace Project.Game
{
    [CreateAssetMenu(menuName = AssetMenuPaths.CAMERA_CONFIG, fileName = "New Camera Config")]
    public class GameCameraConfig : ScriptableObject
    {
        [SerializeField] private float _viewportDepth;
        [SerializeField] private float _viewportWidth;

        [SerializeField] private float _motionSpeed;
        [SerializeField] private float _motionDamping;
        [SerializeField] private float _motionResponsiveness;

        [Tooltip("How many units the player will be offset from the bottom of the screen")]
        [SerializeField] private float _bottomOffset;

        public float ViewportDepth => _viewportDepth;

        public float ViewportWidth => _viewportWidth;

        public float MotionSpeed => _motionSpeed;
        public float MotionDamping => _motionDamping;
        public float MotionResponsiveness => _motionResponsiveness;

        public float BottomOffset => _bottomOffset;
    }
}