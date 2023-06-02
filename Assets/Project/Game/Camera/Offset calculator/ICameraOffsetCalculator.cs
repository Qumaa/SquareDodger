using UnityEngine;

namespace Project.Game
{
    public interface ICameraOffsetCalculator
    {
        Vector2 CalculateOffset(float bottomOffsetUnits);
    }
}