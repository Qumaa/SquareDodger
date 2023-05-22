using System;
using System.Collections;
using System.Collections.Generic;
using Project.Game;
using UnityEngine;

namespace Project
{
    public class GameInputReader : MonoBehaviour, IPlayerInputService
    {
        public event Action OnTurnInput;

        private void Update()
        {
            if (Input.anyKeyDown)
                OnTurnInput?.Invoke();
        }
    }
}
