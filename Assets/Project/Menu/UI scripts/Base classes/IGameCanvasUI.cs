using System;
using UnityEngine;

namespace Project.UI
{
    public interface IGameCanvasUI
    {
        event Action OnShouldPlayTappedSound;
        void Show();
        void Hide();
        void SetCanvas(Canvas canvas);
        void SetAsFocused();
    }
}
