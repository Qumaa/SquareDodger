using Project.Architecture;
using UnityEngine;

namespace Project.Game
{
    public class PlayerBlendingShaderMaintainer : PausableAndResettable, IPlayerBlendingShaderMaintainer
    {
        private const string _BUFFER_NAME = "buffer";
        private readonly int _bufferNameId = Shader.PropertyToID(_BUFFER_NAME);
        private const int _STRIDE = 8; // vector2 size in bytes

        private ComputeBuffer _allocatedBuffer;
        private int _allocatedBufferLength;

        private readonly float _defaultBlendingRadius;
        private readonly float _defaultBlendingLength;

        public IPlayerBlendingShader MaintainedShader { get; set; }

        public PlayerBlendingShaderMaintainer(float defaultBlendingRadius, float defaultBlendingLength)
        {
            _defaultBlendingRadius = defaultBlendingRadius;
            _defaultBlendingLength = defaultBlendingLength;
        }

        public void UpdateShader(IObstacle[] data)
        {
            if (_isPaused)
                return;
            
            var bufferDataLength = data.Length + 1;
            
            if (bufferDataLength > _allocatedBufferLength)
                ReallocateBuffer(bufferDataLength);
            
            var bufferData = new Vector2[bufferDataLength];

            // 0th element's X contains the size of the buffer
            bufferData[0].x = data.Length;

            for (var i = 0; i < data.Length; i++)
                bufferData[i + 1] = data[i].Position;
            
            _allocatedBuffer.SetData(bufferData);
        }

        protected override void OnReset()
        {
            base.OnReset();
            ResetShaderValues();
            Dispose();
        }

        private ComputeBuffer AllocateBuffer(int bufferLength)
        {
            _allocatedBufferLength = bufferLength;
            return new ComputeBuffer(bufferLength, _STRIDE);
        }

        private void ReallocateBuffer(int bufferLength)
        {
            _allocatedBuffer?.Release();
            _allocatedBuffer = AllocateBuffer(bufferLength);
            MaintainedShader.Material.SetBuffer(_bufferNameId, _allocatedBuffer);
        }

        private void ResetShaderValues()
        {
            MaintainedShader.HardBlendingRadius = _defaultBlendingRadius;
            MaintainedShader.SoftBlendingLength = _defaultBlendingLength;
        }

        public void Dispose()
        {
            _allocatedBuffer?.Dispose();
        }
    }
}