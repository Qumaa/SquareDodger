namespace Project.Game
{
    public interface IProceduralMotionSystemOperand<T>
    {
        T Value { get; }
        IProceduralMotionSystemOperand<T> Add(IProceduralMotionSystemOperand<T> operand);
        IProceduralMotionSystemOperand<T> Subtract(IProceduralMotionSystemOperand<T> operand);
        IProceduralMotionSystemOperand<T> Scale(float times);
        IProceduralMotionSystemOperand<T> Divide(float times);
    }
}