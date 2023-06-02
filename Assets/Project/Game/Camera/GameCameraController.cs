using UnityEngine;

namespace Project.Game
{
    //TODO: split into monobehaviour-regular class pair; move to bootstrap
    public class GameCameraController : MonoBehaviour
    {
        [SerializeField] private float _depth;

        [SerializeField] private float _motionSpeed;
        [SerializeField] private float _motionDamping;
        [SerializeField] private float _motionResponsiveness;

        [SerializeField] private Transform _player;
        [SerializeField] private float _bottomOffset;

        private IGameCamera _gameCamera;

        private void Awake()
        {
             var controlledCamera = GetComponent<Camera>();
            var offsetCalculator = new CameraOffsetCalculatorViewport(controlledCamera);
            var motionSystem = new ProceduralMotionSystemVector2(
                _motionSpeed,
                _motionDamping,
                _motionResponsiveness
            );

            _gameCamera = new GameCameraLogic(controlledCamera, _depth, motionSystem, offsetCalculator, _player, _bottomOffset);
        }

        private void FixedUpdate()
        {
            _gameCamera.Update(Time.deltaTime);
        }
    }
}