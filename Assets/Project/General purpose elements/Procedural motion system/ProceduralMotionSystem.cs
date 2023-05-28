using UnityEngine;

namespace Project
{
    public abstract class ProceduralMotionSystem<T> :
        IProceduralMotionSystem<IProceduralMotionSystemOperand<T>>
    {
        private IProceduralMotionSystemOperand<T> _lastInput;
        private IProceduralMotionSystemOperand<T> _motionState;
        private IProceduralMotionSystemOperand<T> _motionStateDerivative;

        // second order dynamics equation constants
        private float _k1;
        private float _k2;
        private float _k3;

        protected ProceduralMotionSystem(float speed, float damping, float responsiveness,
            IProceduralMotionSystemOperand<T> initialValue, IProceduralMotionSystemOperand<T> zeroValue)
        {
            CalculateConstants(speed, damping, responsiveness);

            SetInitialValue(initialValue, zeroValue);
        }

        protected ProceduralMotionSystem(float speed, float damping, float responsiveness)
        {
            CalculateConstants(speed, damping, responsiveness);
        }

        public void SetInitialValue(IProceduralMotionSystemOperand<T> initialValue, IProceduralMotionSystemOperand<T> zeroValue)
        {
            _lastInput = initialValue;
            _motionState = initialValue;
            _motionStateDerivative = zeroValue;
        }

        protected void CalculateConstants(float speed, float damping, float responsiveness)
        {
            var pi = Mathf.PI;
            var twopispeed = 2 * pi * speed;

            _k1 = damping / (pi * speed);
            _k2 = 1 / (twopispeed * twopispeed);
            _k3 = responsiveness * damping / twopispeed;
        }

        public IProceduralMotionSystemOperand<T> MakeStep(float timeStep, IProceduralMotionSystemOperand<T> destination,
            IProceduralMotionSystemOperand<T> velocity)
        {
            var k2Clamped = Mathf.Max(_k2, 1.1f * (timeStep * timeStep / 4f + timeStep * _k1 / 2f));
            
            _motionState = _motionState.Add(_motionStateDerivative.Scale(timeStep));
            _motionStateDerivative = _motionStateDerivative.Add(
                destination.Add(velocity.Scale(_k3))
                    .Subtract(_motionState)
                    .Subtract(_motionStateDerivative.Scale(_k1))
                    .Scale(timeStep)
                    .Divide(k2Clamped)
            );

            _lastInput = destination;
            return _motionState;
        }

        public IProceduralMotionSystemOperand<T> MakeStep(float timeStep, IProceduralMotionSystemOperand<T> destination) =>
            MakeStep(timeStep, destination, destination.Subtract(_lastInput).Scale(timeStep));
    }
}