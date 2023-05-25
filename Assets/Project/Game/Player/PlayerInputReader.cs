using System;
using Project.Game;
using UnityEngine;

namespace Project.Game
{
    public class PlayerInputReader : MonoBehaviour, IPlayerInputService
    {
        public event Action OnTurnInput;

        private void Update()
        {
            if (Input.anyKeyDown)
                OnTurnInput?.Invoke();
        }
    }
}
