using UnityEngine;

namespace Project.Game
{
    public class PlayerShaderMaintainer : IPlayerShaderMaintainer
    {
        private const string _BUFFER_NAME = "buffer";
        private readonly int _bufferNameId = Shader.PropertyToID(_BUFFER_NAME);
        private const int _STRIDE = 8; // vector2 size in bytes
        private ComputeBuffer _buffer;
        private int _bufferLength;

        private Material _material;

        public PlayerShaderMaintainer(Material material)
        {
            _material = material;
            _bufferLength = 0;
        }

        public void UpdateBuffer(IObstacle[] data)
        {
            var bufferLength = data.Length + 1;
            
            if (bufferLength > _bufferLength)
                ReallocateBuffer(bufferLength);
            
            var bufferData = new Vector2[bufferLength];

            // 0th element's X contains the size of the buffer
            bufferData[0].x = data.Length;

            for (var i = 0; i < data.Length; i++)
                bufferData[i + 1] = data[i].Position;
            
            _buffer.SetData(bufferData);
        }

        private ComputeBuffer AllocateBuffer(int bufferLength) => 
            new(bufferLength, _STRIDE);

        private void ReallocateBuffer(int bufferLength)
        {
            _buffer?.Release();
            _buffer = AllocateBuffer(bufferLength);
            _material.SetBuffer(_bufferNameId, _buffer);
        }
    }
}