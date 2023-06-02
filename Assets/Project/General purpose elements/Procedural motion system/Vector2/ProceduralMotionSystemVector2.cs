using UnityEngine;

namespace Project.Game
{
    public class ProceduralMotionSystemVector2 : ProceduralMotionSystem<Vector2>
    {
        public ProceduralMotionSystemVector2(float speed, float damping, float responsiveness, Vector2 initialValue) : 
            base(speed, damping, responsiveness, 
                new ProceduralMotionSystemOperandVector2(initialValue), 
                new ProceduralMotionSystemOperandVector2(Vector2.zero))
        {
        }

        public ProceduralMotionSystemVector2(float speed, float damping, float responsiveness) : 
            base(speed, damping, responsiveness)
        {
        }
    }
}