using UnityEngine;

namespace Project.Game
{
    public class CameraProceduralMotion : IProceduralMotionSystem<Vector2>
    {
        private Vector2 _lastInput;
        private Vector2 _motionState;
        private Vector2 _motionStateDerivative;

        // second order dynamics equation constants
        private readonly float _k1;
        private readonly float _k2;
        private readonly float _k3;

        public CameraProceduralMotion(float speed, float damping, float responsiveness, Vector2 initialValue)
        {
            // calculate constants
            var pi = Mathf.PI;
            var twopispeed = 2 * pi * speed;

            _k1 = damping / (pi * speed);
            _k2 = 1 / (twopispeed * twopispeed);
            _k3 = responsiveness * damping / twopispeed;

            // initialize values
            _lastInput = initialValue;
            _motionState = initialValue;
            _motionStateDerivative = Vector2.zero;
        }
        
        public Vector2 MakeStep(float timeStep, Vector2 destination, Vector2 velocity)
        {
            var k2Clamped = Mathf.Max(_k2, 1.1f * (timeStep * timeStep / 4f + timeStep * _k1 / 2f));
            _motionState += timeStep * _motionStateDerivative;
            _motionStateDerivative +=
                timeStep * (destination + _k3 * velocity - _motionState - _k1 * _motionStateDerivative) / k2Clamped;

            _lastInput = destination;
            return _motionState;
        }

        public Vector2 MakeStep(float timeStep, Vector2 destination) =>
            MakeStep(timeStep, destination, (destination - _lastInput) / timeStep);
    }
}