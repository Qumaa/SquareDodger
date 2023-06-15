using System.Collections.Generic;
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
        private Vector2[] _allocatedBufferData;
        private int _allocatedBufferLength;

        private readonly float _defaultBlendingRadius;
        private readonly float _defaultBlendingLength;

        public IPlayerBlendingShader MaintainedShader { get; set; }

        public PlayerBlendingShaderMaintainer(float defaultBlendingRadius, float defaultBlendingLength)
        {
            _defaultBlendingRadius = defaultBlendingRadius;
            _defaultBlendingLength = defaultBlendingLength;
        }

        public void UpdateShader(List<IObstacle> data)
        {
            if (_isPaused)
                return;

            var dataLength = data.Count;
            var bufferDataLength = dataLength + 1;
            
            if (bufferDataLength > _allocatedBufferLength)
                ReallocateBuffer(bufferDataLength);
            
            // 0th element's X contains the size of the buffer
            _allocatedBufferData[0].x = dataLength;

            for (var i = 0; i < dataLength; i++)
                _allocatedBufferData[i + 1] = data[i].Position;
            
            UpdateBuffer();
        }

        private void UpdateBuffer() =>
            _allocatedBuffer.SetData(_allocatedBufferData);

        protected override void OnReset()
        {
            base.OnReset();
            ResetShaderValues();
            ResetBuffer();
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

            _allocatedBufferData = new Vector2[bufferLength];
        }

        private void ResetShaderValues()
        {
            MaintainedShader.HardBlendingRadius = _defaultBlendingRadius;
            MaintainedShader.SoftBlendingLength = _defaultBlendingLength;
            MaintainedShader.ColorBalance = 0;
        }

        private void ResetBuffer()
        {
            _allocatedBufferData[0].x = 0;
            UpdateBuffer();
        }

        public void Dispose()
        {
            _allocatedBuffer?.Dispose();
        }
    }
}