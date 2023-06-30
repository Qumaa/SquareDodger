using System;

namespace Project.Game
{
    public interface IGameShaderModeObserver
    {
        void SetPlayerShaderMode(ShaderBlendingMode mode);
        event Action<ShaderBlendingMode> OnPlayerShaderModeChanged;
    }
}