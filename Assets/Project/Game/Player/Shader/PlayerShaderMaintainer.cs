using UnityEngine;

namespace Project.Game
{
    public class PlayerShaderMaintainer : IPlayerShaderMaintainer
    {
        private const string _MATERIAL_BLENDING_RADIUS_NAME = "_BlendingRadius";
        private const string _MATERIAL_BLENDING_WIDTH_NAME = "_BlendingLength";
        private const string _BUFFER_NAME = "buffer";
        private readonly int _bufferNameId = Shader.PropertyToID(_BUFFER_NAME);
        private readonly int _blendingRadiusNameId = Shader.PropertyToID(_MATERIAL_BLENDING_RADIUS_NAME);
        private readonly int _blendingLengthNameId = Shader.PropertyToID(_MATERIAL_BLENDING_WIDTH_NAME);
        private const int _STRIDE = 8; // vector2 size in bytes

        private ComputeBuffer _buffer;
        private int _allocatedBufferLength;
        private Material _material;

        public Material Material
        {
            get => _material;
            set => UpdateMaterial(value);
        }

        public float BlendingRadius { get; private set; }


        public PlayerShaderMaintainer()
        {
            _allocatedBufferLength = 0;
        }

        private void UpdateMaterial(Material material)
        {
            _material = material;
            BlendingRadius = Material.GetFloat(_blendingRadiusNameId) + Material.GetFloat(_blendingLengthNameId);
        }

        public void UpdateBuffer(IObstacle[] data)
        {
            var bufferDataLength = data.Length + 1;
            
            if (bufferDataLength > _allocatedBufferLength)
                ReallocateBuffer(bufferDataLength);
            
            var bufferData = new Vector2[bufferDataLength];

            // 0th element's X contains the size of the buffer
            bufferData[0].x = data.Length;

            for (var i = 0; i < data.Length; i++)
                bufferData[i + 1] = data[i].Position;
            
            _buffer.SetData(bufferData);
        }

        private ComputeBuffer AllocateBuffer(int bufferLength)
        {
            _allocatedBufferLength = bufferLength;
            return new ComputeBuffer(bufferLength, _STRIDE);
        }

        private void ReallocateBuffer(int bufferLength)
        {
            _buffer?.Release();
            _buffer = AllocateBuffer(bufferLength);
            Material.SetBuffer(_bufferNameId, _buffer);
        }

        public void Dispose()
        {
            _buffer?.Dispose();
        }
    }
}