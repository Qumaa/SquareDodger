using UnityEngine;

namespace Project.Game
{
    public struct ProceduralMotionSystemOperandVector2 : IProceduralMotionSystemOperand<Vector2>
    {
        private Vector2 _value;

        public ProceduralMotionSystemOperandVector2(Vector2 value)
        {
            _value = value;
        }

        public Vector2 Value => _value;

        public IProceduralMotionSystemOperand<Vector2> Add(IProceduralMotionSystemOperand<Vector2> operand) =>
            new ProceduralMotionSystemOperandVector2(_value + operand.Value);

        public IProceduralMotionSystemOperand<Vector2> Subtract(IProceduralMotionSystemOperand<Vector2> operand) =>
            new ProceduralMotionSystemOperandVector2(_value - operand.Value);
        public IProceduralMotionSystemOperand<Vector2> Scale(float times) =>
            new ProceduralMotionSystemOperandVector2(_value * times);

        public IProceduralMotionSystemOperand<Vector2> Divide(float times) =>
            new ProceduralMotionSystemOperandVector2(_value / times);
    }
}