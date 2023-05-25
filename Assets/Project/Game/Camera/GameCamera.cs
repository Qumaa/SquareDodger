using UnityEngine;

namespace Project.Game
{
    //TODO: split into monobehaviour-regular class pair; move to bootstrap
    public class GameCamera : MonoBehaviour, IGameCamera
    {
        [SerializeField] private float _depth;

        [SerializeField] private float _motionSpeed;
        [SerializeField] private float _motionDamping;
        [SerializeField] private float _motionResponsiveness;

        [SerializeField] private Transform _player;

        private Camera _cameraToMove;
        private CameraProceduralMotion _motionSystem;
        private Vector2 _position;

        public Vector2 Position
        {
            get => _position;
            set => SetPosition(value);
        }

        public float Depth => _depth;

        private void Awake()
        {
            _cameraToMove = GetComponent<Camera>();
            _motionSystem = new CameraProceduralMotion(
                _motionSpeed,
                _motionDamping,
                _motionResponsiveness,
                Position
                );

            SetPosition(_cameraToMove.transform.position);
        }

        private void FixedUpdate()
        {
            // TODO: make a better follow target
            Position = _motionSystem.MakeStep(Time.deltaTime, _player.position);
        }

        private void SetPosition(Vector2 position)
        {
            _position = position;
            _cameraToMove.transform.position = new Vector3(_position.x, _position.y, -_depth);
        }
    }
}