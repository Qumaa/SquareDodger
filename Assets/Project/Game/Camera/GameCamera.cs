using UnityEngine;

namespace Project.Game
{
    public class GameCamera : PausableAndResettable, IGameCamera
    {
        private ProceduralMotionSystem<Vector2> _motionSystem;
        private ICameraOffsetCalculator _offsetCalculator;
        
        private float _bottomOffset;

        private ICameraController _cameraController;

        public Transform Target { get; set; }
        public Vector2 Position => _cameraController.Position;

        public GameCamera(ICameraController cameraController, ProceduralMotionSystemVector2 motionSystem,
            ICameraOffsetCalculator offsetCalculator, float bottomOffset)
        {
            _cameraController = cameraController;
            _motionSystem = motionSystem;
            _offsetCalculator = offsetCalculator;
            _bottomOffset = bottomOffset;
        }

        public void FixedUpdate(float fixedTimeStep)
        {
            if (_isPaused)
                return;
            
            var position = _motionSystem
                .MakeStep(fixedTimeStep, new ProceduralMotionSystemOperandVector2(GetPositionWithOffset()))
                .Value;
            
            SetPosition(position);
        }

        protected override void OnReset()
        {
            ResetPosition();
        }

        private void ResetPosition()
        {
            SetPosition(GetPositionWithOffset());
            
            _motionSystem.SetInitialValue(
                new ProceduralMotionSystemOperandVector2(_cameraController.Position),
                new ProceduralMotionSystemOperandVector2()
            );
        }

        private void SetPosition(Vector2 position)
        {
            _cameraController.Position = new Vector2(position.x, position.y);
        }

        private Vector2 GetPositionWithOffset() =>
            (Vector2)Target.transform.position + _offsetCalculator.CalculateOffset(_bottomOffset);
    }
}